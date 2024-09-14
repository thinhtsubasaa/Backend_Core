using ERP.Data;
using ERP.Infrastructure;
using ERP.Models;

namespace ERP.Repositories
{
    public interface IDiaLy_DiaDiemRepository : IRepository<DiaLy_DiaDiem>
    {

    }
    public class DiaLy_DiaDiemRepository : Repository<DiaLy_DiaDiem>, IDiaLy_DiaDiemRepository
    {
        public DiaLy_DiaDiemRepository(MyDbContext _db) : base(_db)
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