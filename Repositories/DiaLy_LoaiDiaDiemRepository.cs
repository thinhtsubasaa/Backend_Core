using ERP.Data;
using ERP.Infrastructure;
using ERP.Models;

namespace ERP.Repositories
{
    public interface IDiaLy_LoaiDiaDiemRepository : IRepository<DiaLy_LoaiDiaDiem>
    {

    }
    public class DiaLy_LoaiDiaDiemRepository : Repository<DiaLy_LoaiDiaDiem>, IDiaLy_LoaiDiaDiemRepository
    {
        public DiaLy_LoaiDiaDiemRepository(MyDbContext _db) : base(_db)
        {
        }
        public MyDbContext MyDbContext
        {
            get
            {
                return _db as MyDbContext;
            }
        }


    }
}