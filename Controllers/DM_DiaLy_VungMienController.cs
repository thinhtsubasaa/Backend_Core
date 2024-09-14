using ERP.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Extensions.Configuration;
using static ERP.Data.MyDbContext;
using ThacoLibs;
using System.Data;
using System;
using ERP.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace ERP.Controllers
{
    [EnableCors("CorsApi")]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DM_DiaLy_VungMienController : ControllerBase
    {
        private readonly IUnitofWork uow;
        private readonly UserManager<ApplicationUser> userManager;
        public static IWebHostEnvironment environment;
        private readonly DbAdapter dbAdapter;
        private readonly IConfiguration configuration;
        public DM_DiaLy_VungMienController(IConfiguration _configuration, IUnitofWork _uow, UserManager<ApplicationUser> _userManager, IWebHostEnvironment _environment)
        {
            uow = _uow;
            userManager = _userManager;
            environment = _environment;
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            dbAdapter = new DbAdapter(connectionString);
        }

        [HttpGet]
        public ActionResult Get()
        {
            Expression<Func<DiaLy_VungMien, bool>> whereFunc = item => !item.IsDeleted ;
            Func<IQueryable<DiaLy_VungMien>, IOrderedQueryable<DiaLy_VungMien>> orderByFunc = item => item.OrderBy(x => x.DiaLy_VungMien_Id).ThenBy(x => x.ThuTu);
            var query = uow.DiaLy_VungMiens.GetAll(whereFunc, orderByFunc);
            var list = query.Select(t => new
            {
                t.Id,
                t.MaVungMien,
                t.TenVungMien,
                t.TenVungMien_EN,
                Parent_Id =t.DiaLy_VungMien_Id,
                Title = t.DiaLy_VungMien_Id == null ? t.TenVungMien : t.MaVungMien + " || " + t.MaVungMien,
                IsUsed = query.Where(x => x.DiaLy_VungMien_Id == t.Id).Count() > 0
                  || uow.DiaLy_QuocGia_KhuVucs.Exists(x => x.VungMien_Id == t.Id && !x.IsDeleted)
            });
            return Ok(list);
        }

        [HttpPost]
        public ActionResult Post(DiaLy_VungMien data)
        {
            lock (Commons.LockObjectState)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (uow.DiaLy_VungMiens.Exists(x => x.MaVungMien == data.MaVungMien && !x.IsDeleted))
                    return StatusCode(StatusCodes.Status409Conflict, "Mã " + data.MaVungMien + " đã tồn tại trong hệ thống");
                else if (uow.DiaLy_VungMiens.Exists(x => x.MaVungMien == data.MaVungMien && x.IsDeleted))
                {
                    var t = uow.DiaLy_VungMiens.GetAll(x => x.MaVungMien == data.MaVungMien).FirstOrDefault();
                    t.IsDeleted = false;
                    t.DeletedBy = null;
                    t.DeletedDate = null;
                    t.UpdatedBy = Guid.Parse(User.Identity.Name);
                    t.UpdatedDate = DateTime.Now;
                    t.MaVungMien = data.MaVungMien;
                    t.TenVungMien = data.TenVungMien;
                    t.TenVungMien_EN = data.TenVungMien_EN;
                    t.DiaLy_VungMien_Id = data.DiaLy_VungMien_Id;
                    uow.DiaLy_VungMiens.Update(t);

                }
                else
                {
                    DiaLy_VungMien dv = new DiaLy_VungMien();
                    Guid id = Guid.NewGuid();
                    dv.Id = id;
                    dv.CreatedDate = DateTime.Now;
                    dv.CreatedBy = Guid.Parse(User.Identity.Name);
                    dv.MaVungMien = data.MaVungMien;
                    dv.TenVungMien = data.TenVungMien;
                    dv.TenVungMien_EN = data.TenVungMien_EN;
                    dv.DiaLy_VungMien_Id = data.DiaLy_VungMien_Id;

                    uow.DiaLy_VungMiens.Add(dv);
                }

                uow.Complete();
                return Ok();
            }
        }

        [HttpGet("GetById")]
        public ActionResult GetById(Guid id)
        {
            DiaLy_VungMien duLieu = uow.DiaLy_VungMiens.GetById(id);
            if (duLieu == null)
            {
                return NotFound();
            }
            return Ok(duLieu);
        }

        [HttpPut]
        public ActionResult Put(Guid id, DiaLy_VungMien duLieu)
        {
            lock (Commons.LockObjectState)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (id != duLieu.Id)
                {
                    return BadRequest();
                }
                duLieu.UpdatedBy = Guid.Parse(User.Identity.Name);
                duLieu.UpdatedDate = DateTime.Now;
                uow.DiaLy_VungMiens.Update(duLieu);
                uow.Complete();
                //Ghi log truy cập
                return StatusCode(StatusCodes.Status204NoContent);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            lock (Commons.LockObjectState)
            {
                DiaLy_VungMien duLieu = uow.DiaLy_VungMiens.GetById(id);
                if (duLieu == null)
                {
                    return NotFound();
                }
                if ( uow.DiaLy_VungMiens.Exists(x => x.Id == id && !x.IsDeleted))
                {
                    duLieu.DeletedDate = DateTime.Now;
                    duLieu.DeletedBy = Guid.Parse(User.Identity.Name);
                    duLieu.IsDeleted = true;
                    uow.DiaLy_VungMiens.Update(duLieu);
                    uow.Complete();
                    return Ok(duLieu);
                }
                else
                    return StatusCode(StatusCodes.Status409Conflict, "Bạn chỉ có thể chỉnh sửa thông tin này");
            }

        }
        [HttpDelete("Remove/{id}")]
        public ActionResult Delete_Remove(Guid id)
        {
            lock (Commons.LockObjectState)
            {
                uow.DiaLy_VungMiens.Delete(id);
                uow.Complete();
                return Ok();
            }
        }
    }
}
