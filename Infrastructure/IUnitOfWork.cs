using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.Repositories;

namespace ERP.Infrastructure
{
  public interface IUnitofWork : IDisposable
  {
    IMenuRepository Menus { get; }
    IDonViTinhRepository DonViTinhs { get; }
    IPhongbanRepository phongbans { get; }
    IDonViRepository DonVis { get; }
    IMenu_RoleRepository Menu_Roles { get; }
    ILogRepository Logs { get; }
    IBoPhanRepository BoPhans { get; }
    ITapDoanRepository tapDoans { get; }
    IChucVuRepository chucVus { get; }
    IDieuChuyenNhanVienRepository dieuChuyenNhanViens { get; }
    ICBNV_DieuChuyenRepository cBNV_DieuChuyens { get; }
    IConfigRepository Configs { get; }
    IRoleByDonViRepository roleByDonVis { get; }
    IRole_DV_PBRepository role_DV_PBs { get; }
    ITokenGenRepository tokenGens { get; }
    IPhuongThucDangNhapRepository PhuongThucDangNhaps { get; }
    IChiTietBOMRepository chiTietBOMs { get; }
    IChiTietNangLucSXRepository chiTietNangLucSXs { get; }
    IKhachHangRepository khachHangs { get; }
    ICauTrucKhoRepository cauTrucKhos { get; }
    ILoaiDinhMucTonKhoRepository loaiDinhMucTonKhos { get; }
    ILoaiKhachHangRepository loaiKhachHangs { get; }
    ILoaiSanPhamRepository loaiSanPhams { get; }
    INangLucSanXuatRepository nangLucSanXuats { get; }
    ILotRepository lots { get; }
    IMauSacRepository mauSacs { get; }
    INhomVatTuRepository nhomVatTus { get; }
    ISanPhamRepository sanPhams { get; }
    IThietBiRepository thietBis { get; }
    IVatTuRepository vatTus { get; }
    IXuongSanXuatRepository xuongSanXuats { get; }
    IFlexible_Fields_CollectionRepository flexible_Fields_Collections { get; }
    IFlexibleFieldData_SanPhamRepository flexibleFieldData_SanPhams { get; }
    IPhanMemRepository phanMems { get; }
    IChiTiet_DV_PB_BPRepository chiTiet_DV_PB_BPs { get; }
    Ilkn_VersionKeHoachRepository lkn_VersionKeHoachs { get; }
    Ilkn_KeHoachRepository lkn_KeHoachs { get; }
    Ilkn_LoaiKeHoachRepository lkn_LoaiKeHoachs { get; }
    Ilkn_ChiTietKeHoachRepository lkn_ChiTietKeHoachs { get; }
    INhaCungCapRepository nhaCungCaps { get; }
    Ilkn_LoaiNhaCungCapRepository lkn_LoaiNhaCungCaps { get; }
    Ilkn_PhieuMuaHangRepository lkn_PhieuMuaHangs { get; }
    Ilkn_ChiTietPhieuMuaHangRepository lkn_ChiTietPhieuMuaHangs { get; }
    Ilkn_DinhMucVatTuRepository lkn_DinhMucVatTus { get; }
    Ilkn_PhieuTraHangNCCRepository lkn_PhieuTraHangNCCs { get; }
    Ilkn_ChiTietMauSacSanPhamRepository lkn_ChiTietMauSacSanPhams { get; }
    Ilkn_ChiTietPhieuTraHangNCCRepository lkn_ChiTietPhieuTraHangNCCs { get; }
    Ilkn_PhieuNhanHangRepository lkn_PhieuNhanHangs { get; }
    Ilkn_ChiTietPhieuNhanHangRepository lkn_ChiTietPhieuNhanHangs { get; }
    Ilkn_ChiTietPhieuNhapKhoVatTuRepository lkn_ChiTietPhieuNhapKhoVatTus { get; }
    Ilkn_PhieuNhapKhoVatTuRepository lkn_PhieuNhapKhoVatTus { get; }
    Ilkn_ChiTietKhoVatTuRepository lkn_ChiTietKhoVatTus { get; }
    Ilkn_ChiTietPhieuNhapKhoThanhPhamRepository lkn_ChiTietPhieuNhapKhoThanhPhams { get; }
    Ilkn_PhieuNhapKhoThanhPhamRepository lkn_PhieuNhapKhoThanhPhams { get; }
    Ilkn_ChiTietKhoThanhPhamRepository lkn_ChiTietKhoThanhPhams { get; }
    Ilkn_PhieuDeNghiCapVatTuRepository lkn_PhieuDeNghiCapVatTus { get; }
    Ilkn_ChiTietPhieuDeNghiCapVatTuRepository lkn_ChiTietPhieuDeNghiCapVatTus { get; }
    Ilkn_PhieuXuatKhoVatTuRepository lkn_PhieuXuatKhoVatTus { get; }
    Ilkn_ChiTietPhieuXuatKhoVatTuRepository lkn_ChiTietPhieuXuatKhoVatTus { get; }
    Ilkn_PhieuDieuChuyen_ThanhLyRepository lkn_PhieuDieuChuyen_ThanhLys { get; }
    Ilkn_ChiTietPhieuDieuChuyen_ThanhLyRepository lkn_ChiTietPhieuDieuChuyen_ThanhLys { get; }
    Ilkn_DinhMucTonKhoRepository lkn_DinhMucTonKhos { get; }
    Ilkn_PhieuKiemKeRepository lkn_PhieuKiemKes { get; }
    Ilkn_ChiTietPhieuKiemKeRepository lkn_ChiTietPhieuKiemKes { get; }
    Ilkn_PhieuXuatKhoThanhPhamRepository lkn_PhieuXuatKhoThanhPhams { get; }
    Ilkn_ChiTietPhieuXuatKhoThanhPhamRepository lkn_ChiTietPhieuXuatKhoThanhPhams { get; }
    ILoaiDoiTacRepository LoaiDoiTacs { get; }
    INhomDoiTacRepository NhomDoiTacs { get; }
    IDoiTacRepository DoiTacs { get; }
    IDonViTinh_QuyDoiRepository DonViTinh_QuyDois { get; }
    IDiaLy_QuocGia_KhuVucRepository DiaLy_QuocGia_KhuVucs { get; }
    IDiaLy_VungMienRepository DiaLy_VungMiens { get; }
    IDiaLy_DiaDiemRepository DiaLy_DiaDiems { get; }
    IDiaLy_LoaiDiaDiemRepository DiaLy_LoaiDiaDiems { get; }
    IDongContRepository DongConts { get; }
    IDanhSachXe_DongContRepository DanhSachXe_DongConts { get; }
    IWMS_Kho_KhoXeRepository WMS_Kho_KhoXes { get; }
    IWMS_Kho_BaiXeRepository WMS_Kho_BaiXes { get; }
    IWMS_Kho_ViTriTrongBaiRepository WMS_Kho_ViTriTrongBais { get; }
    IWMS_Kho_PhuTrachBaiXeRepository WMS_Kho_PhuTrachBaiXes { get; }
    ITMS_LoaiPhuongTienRepository TMS_LoaiPhuongTiens { get; }
    ITMS_DanhSachPhuongTienRepository TMS_DanhSachPhuongTiens { get; }
    ITMS_PhuongThucVanChuyenRepository TMS_PhuongThucVanChuyens { get; }
    IXe_DongXeRepository Xe_DongXes { get; }

    IXe_XeThanhPhamRepository Xe_XeThanhPhams { get; }
    IXe_LichSu_GiaoXeRepository Xe_LichSu_GiaoXes { get; }
    IXe_LichSu_XuatXeRepository Xe_LichSu_XuatXes { get; }
    IXe_LichSu_BaiXeRepository Xe_LichSu_BaiXes { get; }
    IXe_LichSuXeQuaThilogiRepository Xe_LichSuXeQuaThilogis { get; }
    IWMS_TMS_DonHangRepository WMS_TMS_DonHangs { get; }
    IWMS_TMS_DonHang_ChiTietRepository WMS_TMS_DonHang_ChiTiets { get; }
    IWMS_TMS_DonHang_ChiTiet_LichSuRepository WMS_TMS_DonHang_ChiTiet_LichSus { get; }
    IWMS_TMS_DonHang_LichSuRepository WMS_TMS_DonHang_LichSus { get; }
    ITaiXeRepository TaiXes { get; }
    ILoaiBinhRepository LoaiBinhs { get; }
    ILoaiLopRepository LoaiLops { get; }
    IMamXeRepository MamXes { get; }
    ILopMacDinhRepository LopMacDinhs { get; }
    IPhuKienRepository PhuKiens { get; }
    IPhuKienTheoXe_ChiTietRepository PhuKienTheoXe_ChiTiets { get; }
    IPhuKienTheoXeRepository PhuKienTheoXes { get; }
    IPhuKienTheoXe_LopRepository PhuKienTheoXe_Lops { get; }
    IXe_SanPhamRepository Xe_SanPhams { get; }
    IXe_NhanHieuRepository Xe_NhanHieus { get; }
    IXe_LoaiPhuongTienRepository Xe_LoaiPhuongTiens { get; }
    IXe_DongSanPhamRepository Xe_DongSanPhams { get; }
    IXe_LoaiXeRepository Xe_LoaiXes { get; }
    IXe_PhanLoaiXeRepository Xe_PhanLoaiXes { get; }
    IKhoThanhPham_DieuChuyenRepository KhoThanhPham_DieuChuyens { get; }
    ITongHopTonKhoThanhPhamRepository TongHopTonKhoThanhPhams { get; }
    ITienDoSanXuat_PhuKienRepository TienDoSanXuat_PhuKiens { get; }
    ITienDoSanXuat_PhuKien_LopRepository TienDoSanXuat_PhuKien_Lops { get; }
    ITienDoSanXuat_PhuKien_ChiTietRepository TienDoSanXuat_PhuKien_ChiTiets { get; }

    IKeHoachGiaoXeRepository KeHoachGiaoXes { get; }
    IQuanLyBienSoTamRepository QuanLyBienSoTams { get; }
    IPhienBanRepository PhienBans { get; }

    IBienSoTamRepository BienSoTams { get; }
    INhaMayRepository NhaMays { get; }
    ITMS_VC_PhuTrachVenderRepository TMS_VC_PhuTrachVenders { get; }

    IWMS_TMS_DonHang_ChiTietNhanXeRepository WMS_TMS_DonHang_ChiTietNhanXes { get; }

    IXeRaCongRepository XeRaCongs { get; }


    int Complete();
  }
}
