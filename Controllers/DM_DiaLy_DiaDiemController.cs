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

using System.Linq.Expressions;


namespace ERP.Controllers
{
    [EnableCors("CorsApi")]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DM_DiaLy_DiaDiemController : ControllerBase
    {
        private readonly IUnitofWork uow;
        private readonly UserManager<ApplicationUser> userManager;
        public static IWebHostEnvironment environment;
        private readonly DbAdapter dbAdapter;
        private readonly IConfiguration configuration;
        public DM_DiaLy_DiaDiemController(IConfiguration _configuration, IUnitofWork _uow, UserManager<ApplicationUser> _userManager, IWebHostEnvironment _environment)
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
            dbAdapter.connect();
            dbAdapter.createStoredProceder("Get_DMDiaLy_DiaDiem");
            // dbAdapter.sqlCommand.Parameters.Add("@vungMien_Id", SqlDbType.UniqueIdentifier).Value = vungMien_Id;
            // dbAdapter.sqlCommand.Parameters.Add("@keyword", SqlDbType.UniqueIdentifier).Value = keyword;
            var result = dbAdapter.runStored2ObjectList();
            dbAdapter.deConnect();
            return Ok(result);
        }
        [HttpGet("DiaDiemKho")]
        public ActionResult DiaDiemKho()
        {
            Expression<Func<WMS_Kho_KhoXe, bool>> whereFunc = item => !item.IsDeleted && item.DiaDiem_Id != null;
            string[] includes = { "DiaLy_DiaDiem" };
            var result = uow.WMS_Kho_KhoXes.GetAll(whereFunc, null, includes).Select(t => new
            {
                Id = t.DiaLy_DiaDiem.Id,
                t.DiaLy_DiaDiem.TenDiaDiem,
                t.DiaLy_DiaDiem.MaDiaDiem,
            });
            return Ok(result);
        }
        [HttpGet("DiaDiemKhoXe")]
        public ActionResult DiaDiemKhoXe()
        {
            Expression<Func<WMS_Kho_BaiXe, bool>> whereFunc = item => !item.IsDeleted && item.DiaDiem_Id != null;
            string[] includes = { "DiaLy_DiaDiem" };
            var result = uow.WMS_Kho_BaiXes.GetAll(whereFunc, null, includes)
            .GroupBy(t => new { t.DiaLy_DiaDiem.Id, t.DiaLy_DiaDiem.TenDiaDiem, t.DiaLy_DiaDiem.MaDiaDiem })
            .Select(g => new
            {
                Id = g.Key.Id,
                TenDiaDiem = g.Key.TenDiaDiem,
                MaDiaDiem = g.Key.MaDiaDiem,
            });
            return Ok(result);
        }

        [HttpGet("DiaDiemKhoXe_VungMien")]
        public ActionResult DiaDiemKhoXe_VungMien(Guid? VungMien_Id, string keyword)
        {
            dbAdapter.connect();
            dbAdapter.createStoredProceder("Get_WMS_DiaDiemKho");
            dbAdapter.sqlCommand.Parameters.Add("@VungMien_Id", SqlDbType.UniqueIdentifier).Value = VungMien_Id;
            dbAdapter.sqlCommand.Parameters.Add("@keyword", SqlDbType.UniqueIdentifier).Value = keyword;
            var result = dbAdapter.runStored2ObjectList();
            dbAdapter.deConnect();
            return Ok(result);
        }

        [HttpGet("DiaDiem_VungMien")]
        public ActionResult DiaDiem_VungMien(Guid? VungMien_Id, string keyword)
        {
            dbAdapter.connect();
            dbAdapter.createStoredProceder("Get_WMS_DiaDiem_VungMien");
            dbAdapter.sqlCommand.Parameters.Add("@VungMien_Id", SqlDbType.UniqueIdentifier).Value = VungMien_Id;
            dbAdapter.sqlCommand.Parameters.Add("@keyword", SqlDbType.UniqueIdentifier).Value = keyword;
            var result = dbAdapter.runStored2ObjectList();
            dbAdapter.deConnect();
            return Ok(result);
        }

        [HttpGet("DiaDiemmobi")]
        public ActionResult DiaDiemmobi(Guid? vungMien_Id, string keyword)
        {
            dbAdapter.connect();
            dbAdapter.createStoredProceder("Get_DMDiaLy_DiaDiem_MOBI");
            // dbAdapter.sqlCommand.Parameters.Add("@vungMien_Id", SqlDbType.UniqueIdentifier).Value = vungMien_Id;
            // dbAdapter.sqlCommand.Parameters.Add("@keyword", SqlDbType.UniqueIdentifier).Value = keyword;
            var result = dbAdapter.runStored2ObjectList();
            dbAdapter.deConnect();
            return Ok(result);
        }
        [HttpPost]
        public ActionResult Post(DiaLy_DiaDiem data)
        {
            lock (Commons.LockObjectState)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (uow.DiaLy_DiaDiems.Exists(x => x.MaDiaDiem == data.MaDiaDiem && !x.IsDeleted))
                    return StatusCode(StatusCodes.Status409Conflict, "Mã " + data.MaDiaDiem + " đã tồn tại trong hệ thống");
                else if (uow.DiaLy_DiaDiems.Exists(x => x.MaDiaDiem == data.MaDiaDiem && x.IsDeleted))
                {
                    var t = uow.DiaLy_DiaDiems.GetAll(x => x.MaDiaDiem == data.MaDiaDiem).FirstOrDefault();
                    t.IsDeleted = false;
                    t.DeletedBy = null;
                    t.DeletedDate = null;
                    t.UpdatedBy = Guid.Parse(User.Identity.Name);
                    t.UpdatedDate = DateTime.Now;
                    t.MaDiaDiem = data.MaDiaDiem;
                    t.TenDiaDiem = data.TenDiaDiem;
                    t.TenDiaDiem_EN = data.TenDiaDiem_EN;
                    t.ThoiGianVanChuyen = data.ThoiGianVanChuyen;
                    t.VungMien_Id = data.VungMien_Id;
                    t.TenDiaDiem_VietTat = data.TenDiaDiem_VietTat;
                    t.DiaLy_LoaiDiaDiem_Id = data.DiaLy_LoaiDiaDiem_Id;
                    t.SoNha_ThonXom = data.SoNha_ThonXom;
                    t.DiaChi = data.DiaChi;
                    uow.DiaLy_DiaDiems.Update(t);

                }
                else
                {
                    DiaLy_DiaDiem dv = new DiaLy_DiaDiem();
                    Guid id = Guid.NewGuid();
                    dv.Id = id;
                    dv.CreatedDate = DateTime.Now;
                    dv.CreatedBy = Guid.Parse(User.Identity.Name);
                    dv.MaDiaDiem = data.MaDiaDiem;
                    dv.TenDiaDiem = data.TenDiaDiem;
                    dv.TenDiaDiem_EN = data.TenDiaDiem_EN;
                    dv.ThoiGianVanChuyen = data.ThoiGianVanChuyen;
                    dv.TenDiaDiem_VietTat = data.TenDiaDiem_VietTat;
                    dv.DiaLy_LoaiDiaDiem_Id = data.DiaLy_LoaiDiaDiem_Id;
                    dv.SoNha_ThonXom = data.SoNha_ThonXom;
                    dv.DiaChi = data.DiaChi;
                    dv.VungMien_Id = data.VungMien_Id;
                    uow.DiaLy_DiaDiems.Add(dv);
                }
                uow.Complete();
                return Ok();
            }
        }

        [HttpGet("GetById")]
        public ActionResult GetById(Guid id)
        {
            DiaLy_DiaDiem duLieu = uow.DiaLy_DiaDiems.GetById(id);
            if (duLieu == null)
            {
                return NotFound();
            }
            return Ok(duLieu);
        }

        [HttpPut]
        public ActionResult Put(Guid id, DiaLy_DiaDiem duLieu)
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
                uow.DiaLy_DiaDiems.Update(duLieu);
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
                DiaLy_DiaDiem duLieu = uow.DiaLy_DiaDiems.GetById(id);
                if (duLieu == null)
                {
                    return NotFound();
                }
                if (uow.DiaLy_DiaDiems.Exists(x => x.Id == id && !x.IsDeleted))
                {
                    duLieu.DeletedDate = DateTime.Now;
                    duLieu.DeletedBy = Guid.Parse(User.Identity.Name);
                    duLieu.IsDeleted = true;
                    uow.DiaLy_DiaDiems.Update(duLieu);
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
                uow.DiaLy_DiaDiems.Delete(id);
                uow.Complete();
                return Ok();
            }
        }
    }
}
