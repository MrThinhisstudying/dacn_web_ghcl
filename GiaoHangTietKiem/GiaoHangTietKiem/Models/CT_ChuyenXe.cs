namespace GiaoHangTietKiem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CT_ChuyenXe
    {
        [Key]
        public int Ma_CTTX { get; set; }

        [StringLength(10)]
        public string Ma_CTTD { get; set; }

        public int? SoXe { get; set; }

        public bool? TrangThai { get; set; }

        public DateTime? ThoiGian { get; set; }

        public virtual CT_TuyenDuong CT_TuyenDuong { get; set; }

        public virtual XeVanChuyen XeVanChuyen { get; set; }
    }
}
