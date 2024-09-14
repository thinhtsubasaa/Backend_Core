using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Models
{
    public class DiaLy_QuocGia_KhuVuc : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [StringLength(50)]
        [Required(ErrorMessage = "Mã bắt buộc")]
        public string MaQuocGia_KhuVuc { get; set; }
        [StringLength(250)]
        [Required(ErrorMessage = "Tên bắt buộc")]
        public string TenQuocGia_KhuVuc { get; set; }
        public string TenQuocGia_KhuVuc_EN { get; set; }
        public int ThuTu { get; set; }
        public Guid? DonViHanhChinh_Id { get; set; }
        [ForeignKey("DiaLy_VungMien")]
        public Guid? VungMien_Id { get; set; }
    }
}
