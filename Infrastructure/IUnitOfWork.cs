using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.Repositories;

namespace ERP.Infrastructure
{
  public interface IUnitofWork : IDisposable
  {
    IDiaLy_QuocGia_KhuVucRepository DiaLy_QuocGia_KhuVucs { get; }
    IDiaLy_VungMienRepository DiaLy_VungMiens { get; }
    IDiaLy_DiaDiemRepository DiaLy_DiaDiems { get; }
    IDiaLy_LoaiDiaDiemRepository DiaLy_LoaiDiaDiems { get; }

    int Complete();
  }
}
