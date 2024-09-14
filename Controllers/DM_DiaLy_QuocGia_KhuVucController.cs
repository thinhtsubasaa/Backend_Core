using ERP.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using static ERP.Data.MyDbContext;
using ThacoLibs;
using System.Linq;
using System.Linq.Expressions;
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
    public class DM_DiaLy_QuocGia_KhuVucController : ControllerBase
    {
        private readonly IUnitofWork uow;
        private readonly UserManager<ApplicationUser> userManager;
        public static IWebHostEnvironment environment;
        private readonly DbAdapter dbAdapter;
        private readonly IConfiguration configuration;
        public DM_DiaLy_QuocGia_KhuVucController(IConfiguration _configuration, IUnitofWork _uow, UserManager<ApplicationUser> _userManager, IWebHostEnvironment _environment)
        {
            uow = _uow;
            userManager = _userManager;
            environment = _environment;
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            dbAdapter = new DbAdapter(connectionString);
        }

        [HttpGet]
        public ActionResult Get(Guid? vungMien_Id, string keyword)
        {
            Expression<Func<DiaLy_QuocGia_KhuVuc, bool>> whereFunc = item => !item.IsDeleted ;
            Func<IQueryable<DiaLy_QuocGia_KhuVuc>, IOrderedQueryable<DiaLy_QuocGia_KhuVuc>> orderByFunc = item => item.OrderBy(x => x.DonViHanhChinh_Id).ThenBy(x => x.ThuTu);
            var query = uow.DiaLy_QuocGia_KhuVucs.GetAll(whereFunc, orderByFunc);
            var list = query.Select(t => new
            {
                t.Id,
                t.MaQuocGia_KhuVuc,
                t.TenQuocGia_KhuVuc,
                t.TenQuocGia_KhuVuc_EN,
                Parent_Id =t.DonViHanhChinh_Id,
                t.VungMien_Id,
                IsUsed = query.Where(x => x.DonViHanhChinh_Id == t.Id).Count() > 0

            });
            return Ok(list);
        }

        // [HttpPost("Post_DiaLy_QuocGia_KhuVuc")]
        // public ActionResult Post_DiaLy_QuocGia_KhuVuc(DiaLy_QuocGia_KhuVuc Data)
        // {
        //     dbAdapter.connect();
        //     dbAdapter.createStoredProceder("Post_DMDiaLy_QuocGia_KhuVuc");
        //      DiaLy_QuocGia_KhuVuc dv = new DiaLy_QuocGia_KhuVuc();
        //     dbAdapter.sqlCommand.Parameters.Add("@MaQuocGia_KhuVuc", SqlDbType.UniqueIdentifier).Value = Data.MaQuocGia_KhuVuc;
        //     dbAdapter.sqlCommand.Parameters.Add("@TenQuocGia_KhuVuc", SqlDbType.UniqueIdentifier).Value = Data.TenQuocGia_KhuVuc;
        //     dbAdapter.sqlCommand.Parameters.Add("@MaSoThue", SqlDbType.UniqueIdentifier).Value = Data.MaSoThue;
        //     dbAdapter.sqlCommand.Parameters.Add("@DiaChi", SqlDbType.UniqueIdentifier).Value = Data.DiaChi;
        //     dbAdapter.sqlCommand.Parameters.Add("@SoDienThoai", SqlDbType.UniqueIdentifier).Value = Data.SoDienThoai;
        //     dbAdapter.sqlCommand.Parameters.Add("@Fax", SqlDbType.UniqueIdentifier).Value = Data.Fax;
        //     dbAdapter.sqlCommand.Parameters.Add("@Email", SqlDbType.UniqueIdentifier).Value = Data.Email;
        //     dbAdapter.sqlCommand.Parameters.Add("@NguoiLienHe", SqlDbType.UniqueIdentifier).Value = Data.NguoiLienHe;
        //     dbAdapter.sqlCommand.Parameters.Add("@LoaiDiaLy_QuocGia_KhuVuc_Id", SqlDbType.UniqueIdentifier).Value = Data.LoaiDiaLy_QuocGia_KhuVuc_Id;
        //     dbAdapter.sqlCommand.Parameters.Add("@NhomDiaLy_QuocGia_KhuVuc_Id", SqlDbType.UniqueIdentifier).Value = Data.NhomDiaLy_QuocGia_KhuVuc_Id;
        //     var result = dbAdapter.runStored2ObjectList();
        //     dbAdapter.deConnect();
        //     return Ok(result);
        // }

        [HttpPost]
        public ActionResult Post(DiaLy_QuocGia_KhuVuc data)
        {
            lock (Commons.LockObjectState)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (uow.DiaLy_QuocGia_KhuVucs.Exists(x => x.MaQuocGia_KhuVuc == data.MaQuocGia_KhuVuc && !x.IsDeleted))
                    return StatusCode(StatusCodes.Status409Conflict, "Mã " + data.MaQuocGia_KhuVuc + " đã tồn tại trong hệ thống");
                else if (uow.DiaLy_QuocGia_KhuVucs.Exists(x => x.MaQuocGia_KhuVuc == data.MaQuocGia_KhuVuc && x.IsDeleted))
                {
                    var t = uow.DiaLy_QuocGia_KhuVucs.GetAll(x => x.MaQuocGia_KhuVuc == data.MaQuocGia_KhuVuc).FirstOrDefault();
                    t.IsDeleted = false;
                    t.DeletedBy = null;
                    t.DeletedDate = null;
                    t.UpdatedBy = Guid.Parse(User.Identity.Name);
                    t.UpdatedDate = DateTime.Now;
                    t.MaQuocGia_KhuVuc = data.MaQuocGia_KhuVuc;
                    t.TenQuocGia_KhuVuc = data.TenQuocGia_KhuVuc;
                    t.TenQuocGia_KhuVuc_EN = data.TenQuocGia_KhuVuc_EN;
                    t.ThuTu = data.ThuTu;
                    t.DonViHanhChinh_Id = data.DonViHanhChinh_Id;  
                    t.VungMien_Id = data.VungMien_Id;

                    uow.DiaLy_QuocGia_KhuVucs.Update(t);
                }
                else
                {
                    DiaLy_QuocGia_KhuVuc dv = new DiaLy_QuocGia_KhuVuc();
                    Guid id = Guid.NewGuid();
                    dv.Id = id;
                    dv.CreatedDate = DateTime.Now;
                    dv.CreatedBy = Guid.Parse(User.Identity.Name);
                    dv.MaQuocGia_KhuVuc = data.MaQuocGia_KhuVuc;
                    dv.TenQuocGia_KhuVuc = data.TenQuocGia_KhuVuc;
                    dv.TenQuocGia_KhuVuc_EN = data.TenQuocGia_KhuVuc_EN;
                    dv.ThuTu = data.ThuTu;
                    dv.VungMien_Id = data.VungMien_Id;

                    uow.DiaLy_QuocGia_KhuVucs.Add(dv);
                }

                uow.Complete();
                return Ok();
            }
        }

        [HttpGet("GetById")]
        public ActionResult GetById(Guid id)
        {
            DiaLy_QuocGia_KhuVuc duLieu = uow.DiaLy_QuocGia_KhuVucs.GetById(id);
            if (duLieu == null)
            {
                return NotFound();
            }
            return Ok(duLieu);
        }

        [HttpPut]
        public ActionResult Put(Guid id, DiaLy_QuocGia_KhuVuc duLieu)
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
                uow.DiaLy_QuocGia_KhuVucs.Update(duLieu);
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
                DiaLy_QuocGia_KhuVuc duLieu = uow.DiaLy_QuocGia_KhuVucs.GetById(id);
                if (duLieu == null)
                {
                    return NotFound();
                }
                if (uow.DiaLy_QuocGia_KhuVucs.Exists(x => x.Id == id && !x.IsDeleted))
                {
                    duLieu.DeletedDate = DateTime.Now;
                    duLieu.DeletedBy = Guid.Parse(User.Identity.Name);
                    duLieu.IsDeleted = true;
                    uow.DiaLy_QuocGia_KhuVucs.Update(duLieu);
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
                uow.DiaLy_QuocGia_KhuVucs.Delete(id);
                uow.Complete();
                return Ok();
            }
        }
    }
}
