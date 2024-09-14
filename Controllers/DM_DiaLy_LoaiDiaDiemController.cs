using ERP.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
    public class DM_DiaLy_LoaiDiaDiemController : ControllerBase
    {
        private readonly IUnitofWork uow;
        private readonly UserManager<ApplicationUser> userManager;
        public static IWebHostEnvironment environment;
        private readonly DbAdapter dbAdapter;
        private readonly IConfiguration configuration;
        public DM_DiaLy_LoaiDiaDiemController(IConfiguration _configuration, IUnitofWork _uow, UserManager<ApplicationUser> _userManager, IWebHostEnvironment _environment)
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
            dbAdapter.connect();
            dbAdapter.createStoredProceder("Get_DMDiaLy_LoaiDiaDiem");
            var result = dbAdapter.runStored2ObjectList();
            dbAdapter.deConnect();
            return Ok(result);
        }

        [HttpPost]
        public ActionResult Post(DiaLy_LoaiDiaDiem data)
        {
            lock (Commons.LockObjectState)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (uow.DiaLy_LoaiDiaDiems.Exists(x => x.MaLoaiDiaDiem == data.MaLoaiDiaDiem && !x.IsDeleted))
                    return StatusCode(StatusCodes.Status409Conflict, "Mã " + data.MaLoaiDiaDiem + " đã tồn tại trong hệ thống");
                else if (uow.DiaLy_LoaiDiaDiems.Exists(x => x.MaLoaiDiaDiem == data.MaLoaiDiaDiem && x.IsDeleted))
                {
                    var t = uow.DiaLy_LoaiDiaDiems.GetAll(x => x.MaLoaiDiaDiem == data.MaLoaiDiaDiem).FirstOrDefault();
                    t.IsDeleted = false;
                    t.DeletedBy = null;
                    t.DeletedDate = null;
                    t.UpdatedBy = Guid.Parse(User.Identity.Name);
                    t.UpdatedDate = DateTime.Now;
                    t.MaLoaiDiaDiem = data.MaLoaiDiaDiem;
                    t.TenLoaiDiaDiem = data.TenLoaiDiaDiem;
                    t.TenLoaiDiaDiem_EN = data.TenLoaiDiaDiem_EN;
                    uow.DiaLy_LoaiDiaDiems.Update(t);

                }
                else
                {
                    DiaLy_LoaiDiaDiem dv = new DiaLy_LoaiDiaDiem();
                    Guid id = Guid.NewGuid();
                    dv.Id = id;
                    dv.CreatedDate = DateTime.Now;
                    dv.CreatedBy = Guid.Parse(User.Identity.Name);
                    dv.MaLoaiDiaDiem = data.MaLoaiDiaDiem;
                    dv.TenLoaiDiaDiem = data.TenLoaiDiaDiem;
                    dv.TenLoaiDiaDiem_EN = data.TenLoaiDiaDiem_EN;

                    uow.DiaLy_LoaiDiaDiems.Add(dv);
                }

                uow.Complete();
                return Ok();
            }
        }

        [HttpGet("GetById")]
        public ActionResult GetById(Guid id)
        {
            DiaLy_LoaiDiaDiem duLieu = uow.DiaLy_LoaiDiaDiems.GetById(id);
            if (duLieu == null)
            {
                return NotFound();
            }
            return Ok(duLieu);
        }

        [HttpPut]
        public ActionResult Put(Guid id, DiaLy_LoaiDiaDiem duLieu)
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
                uow.DiaLy_LoaiDiaDiems.Update(duLieu);
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
                DiaLy_LoaiDiaDiem duLieu = uow.DiaLy_LoaiDiaDiems.GetById(id);
                if (duLieu == null)
                {
                    return NotFound();
                }
                if ( uow.DiaLy_LoaiDiaDiems.Exists(x => x.Id == id && !x.IsDeleted))
                {
                    duLieu.DeletedDate = DateTime.Now;
                    duLieu.DeletedBy = Guid.Parse(User.Identity.Name);
                    duLieu.IsDeleted = true;
                    uow.DiaLy_LoaiDiaDiems.Update(duLieu);
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
                uow.DiaLy_LoaiDiaDiems.Delete(id);
                uow.Complete();
                return Ok();
            }
        }
    }
}
