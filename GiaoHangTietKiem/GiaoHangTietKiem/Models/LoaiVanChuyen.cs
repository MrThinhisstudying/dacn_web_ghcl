namespace GiaoHangTietKiem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LoaiVanChuyen")]
    public partial class LoaiVanChuyen
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LoaiVanChuyen()
        {
            HoaDonVanChuyens = new HashSet<HoaDonVanChuyen>();
            HoaDonVanChuyens1 = new HashSet<HoaDonVanChuyen>();
            PhieuYeuCaus = new HashSet<PhieuYeuCau>();
            PhieuYeuCaus1 = new HashSet<PhieuYeuCau>();
        }

        [Key]
        [StringLength(10)]
        public string MaLVC { get; set; }

        [Required]
        [StringLength(100)]
        public string TenLVC { get; set; }

        public double Gia { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoaDonVanChuyen> HoaDonVanChuyens { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoaDonVanChuyen> HoaDonVanChuyens1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuYeuCau> PhieuYeuCaus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuYeuCau> PhieuYeuCaus1 { get; set; }
    }
}
