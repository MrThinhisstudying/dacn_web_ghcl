namespace GiaoHangTietKiem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KhuyenMai")]
    public partial class KhuyenMai
    {
        [Key]
        [StringLength(10)]
        public string MaKM { get; set; }

        [Required]
        [StringLength(50)]
        public string TenKM { get; set; }

        public int PhanTram { get; set; }

        public int ToiDa { get; set; }

        public DateTime NgayBD { get; set; }

        public DateTime NgayKT { get; set; }
    }
}
