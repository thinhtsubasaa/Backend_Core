using System.Collections.Generic;
using ERP.Data;
using ERP.Infrastructure;
using ERP.Models;
using ERP.Repositories;


namespace ERP.UOW
{
    public class UnitofWork : IUnitofWork
    {


        public IDiaLy_QuocGia_KhuVucRepository DiaLy_QuocGia_KhuVucs { get; private set; }
        public IDiaLy_VungMienRepository DiaLy_VungMiens { get; private set; }
        public IDiaLy_DiaDiemRepository DiaLy_DiaDiems { get; private set; }
        public IDiaLy_LoaiDiaDiemRepository DiaLy_LoaiDiaDiems { get; private set; }




        private MyDbContext db;

        public UnitofWork(MyDbContext _db)
        {
            db = _db;
            Menus = new MenuRepository(db);
            DiaLy_QuocGia_KhuVucs = new DiaLy_QuocGia_KhuVucRepository(db);
            DiaLy_VungMiens = new DiaLy_VungMienRepository(db);
            DiaLy_DiaDiems = new DiaLy_DiaDiemRepository(db);
            DiaLy_LoaiDiaDiems = new DiaLy_LoaiDiaDiemRepository(db);


        }
        public void Dispose()
        {
            db.Dispose();
        }
        public int Complete()
        {
            return db.SaveChanges();
        }
    }
}