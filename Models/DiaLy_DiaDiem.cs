using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Models
{
    public class DiaLy_DiaDiem : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [StringLength(50)]
        [Required(ErrorMessage = "Mã bắt buộc")]
        public string MaDiaDiem { get; set; }
        [StringLength(250)]
        [Required(ErrorMessage = "Tên bắt buộc")]
        public string TenDiaDiem { get; set; }
        [StringLength(250)]
        public string TenDiaDiem_VietTat { get; set; }
        public string TenDiaDiem_EN { get; set; }
        [StringLength(250)]
        public string DiaChi { get; set; }
        public string ThoiGianVanChuyen { get; set; }
        public string SoNha_ThonXom { get; set; }
        [ForeignKey("DiaLy_QuocGia_KhuVuc")]
        public Guid? QuocGia_KhuVuc_Id { get; set; }
        public DiaLy_QuocGia_KhuVuc DiaLy_QuocGia_KhuVuc { get; set; }
        [ForeignKey("DiaLy_LoaiDiaDiem")]
        public Guid? DiaLy_LoaiDiaDiem_Id { get; set; }
        public DiaLy_LoaiDiaDiem DiaLy_LoaiDiaDiem { get; set; }
        [ForeignKey("DiaLy_VungMien")]
        public Guid? VungMien_Id { get; set; }
        public DiaLy_VungMien DiaLy_VungMien { get; set; }
    }
}
