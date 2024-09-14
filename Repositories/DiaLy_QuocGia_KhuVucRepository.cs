using ERP.Data;
using ERP.Infrastructure;
using ERP.Models;

namespace ERP.Repositories
{
    public interface IDiaLy_QuocGia_KhuVucRepository : IRepository<DiaLy_QuocGia_KhuVuc>
    {

    }
    public class DiaLy_QuocGia_KhuVucRepository : Repository<DiaLy_QuocGia_KhuVuc>, IDiaLy_QuocGia_KhuVucRepository
    {
        public DiaLy_QuocGia_KhuVucRepository(MyDbContext _db) : base(_db)
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