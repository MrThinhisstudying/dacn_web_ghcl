namespace GiaoHangTietKiem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KhachHang")]
    public partial class KhachHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KhachHang()
        {
            HoaDonVanChuyens = new HashSet<HoaDonVanChuyen>();
            PhieuYeuCaus = new HashSet<PhieuYeuCau>();
            PhieuYeuCaus1 = new HashSet<PhieuYeuCau>();
            UserKHs = new HashSet<UserKH>();
            UserKHs1 = new HashSet<UserKH>();
        }
        public KhachHang(string tenKH, string sDT, string diaChi, bool gioiTinh)
        {
            TenKH = tenKH;
            SDT = sDT;
            DiaChi = diaChi;
            GioiTinh = gioiTinh;
        }
        [Key]
        [StringLength(10)]
        public string MaKH { get; set; }

        [Required]
        [StringLength(50)]
        public string TenKH { get; set; }

        [Required]
        [StringLength(10)]
        public string SDT { get; set; }

        [StringLength(100)]
        public string DiaChi { get; set; }

        public bool GioiTinh { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoaDonVanChuyen> HoaDonVanChuyens { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuYeuCau> PhieuYeuCaus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuYeuCau> PhieuYeuCaus1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserKH> UserKHs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserKH> UserKHs1 { get; set; }
    }
}
