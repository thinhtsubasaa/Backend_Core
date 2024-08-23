using System.Collections.Generic;
using ERP.Data;
using ERP.Infrastructure;
using ERP.Models;
using ERP.Repositories;


namespace ERP.UOW
{
    public class UnitofWork : IUnitofWork
    {

        public IConfigRepository Configs { get; private set; }

        public ITokenGenRepository tokenGens { get; private set; }
        public IPhuongThucDangNhapRepository PhuongThucDangNhaps { get; private set; }







        private MyDbContext db;

        public UnitofWork(MyDbContext _db)
        {
            db = _db;
            Menus = new MenuRepository(db);


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