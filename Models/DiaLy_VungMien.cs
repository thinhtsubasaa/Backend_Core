using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Models
{
    public class DiaLy_VungMien: Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [StringLength(50)]
        [Required(ErrorMessage = "Mã bắt buộc")]
        public string MaVungMien { get; set; }
        [StringLength(250)]
        [Required(ErrorMessage = "Tên bắt buộc")]
        public string TenVungMien { get; set; }
        public string TenVungMien_EN { get; set; }
        public int ThuTu { get; set; }
        public Guid? DiaLy_VungMien_Id { get; set; }
        }
}
