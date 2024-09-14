using ERP.Data;
using ERP.Infrastructure;
using ERP.Models;

namespace ERP.Repositories
{
    public interface IDiaLy_VungMienRepository : IRepository<DiaLy_VungMien>
    {

    }
    public class DiaLy_VungMienRepository : Repository<DiaLy_VungMien>, IDiaLy_VungMienRepository
    {
        public DiaLy_VungMienRepository(MyDbContext _db) : base(_db)
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