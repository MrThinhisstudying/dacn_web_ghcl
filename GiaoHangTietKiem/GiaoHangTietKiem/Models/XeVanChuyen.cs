namespace GiaoHangTietKiem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("XeVanChuyen")]
    public partial class XeVanChuyen
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public XeVanChuyen()
        {
            CT_ChuyenXe = new HashSet<CT_ChuyenXe>();
            HoaDonVanChuyens = new HashSet<HoaDonVanChuyen>();
        }

        [Key]
        public int SoXe { get; set; }

        public bool? TrangThai { get; set; }

        public long? KG { get; set; }

        public int? SL { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CT_ChuyenXe> CT_ChuyenXe { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoaDonVanChuyen> HoaDonVanChuyens { get; set; }
    }
}
