using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Models
{
    public class DiaLy_LoaiDiaDiem: Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [StringLength(50)]
        [Required(ErrorMessage = "Mã bắt buộc")]
        public string MaLoaiDiaDiem { get; set; }
        [StringLength(250)]
        [Required(ErrorMessage = "Tên bắt buộc")]
        public string TenLoaiDiaDiem { get; set; }
        public string TenLoaiDiaDiem_EN { get; set; }

        }
}
