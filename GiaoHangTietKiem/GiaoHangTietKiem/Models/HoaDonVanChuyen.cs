namespace GiaoHangTietKiem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HoaDonVanChuyen")]
    public partial class HoaDonVanChuyen
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HoaDonVanChuyen()
        {
            CTVanChuyens = new HashSet<CTVanChuyen>();
            CTVanChuyens1 = new HashSet<CTVanChuyen>();
        }

        [Key]
        [StringLength(10)]
        public string SoHD { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayLapHD { get; set; }

        public long? TongTien { get; set; }

        public long? TienVC { get; set; }

        [StringLength(10)]
        public string MaNV { get; set; }

        public bool COD { get; set; }

        [StringLength(10)]
        public string MaTD { get; set; }

        [StringLength(10)]
        public string MaLVC { get; set; }

        [StringLength(10)]
        public string MaKH { get; set; }

        [StringLength(10)]
        public string MaKN { get; set; }

        [StringLength(10)]
        public string MaTT { get; set; }

        [StringLength(10)]
        public string MaLHH { get; set; }

        public int? KG { get; set; }

        public int? SoXe { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTVanChuyen> CTVanChuyens { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTVanChuyen> CTVanChuyens1 { get; set; }

        public virtual LoaiVanChuyen LoaiVanChuyen { get; set; }

        public virtual LoaiVanChuyen LoaiVanChuyen1 { get; set; }

        public virtual NhanVien NhanVien { get; set; }

        public virtual NhanVien NhanVien1 { get; set; }

        public virtual LoaiHH LoaiHH { get; set; }

        public virtual TrangThai TrangThai { get; set; }

        public virtual XeVanChuyen XeVanChuyen { get; set; }

        public virtual KhachHang KhachHang { get; set; }

        public virtual KhachNhan KhachNhan { get; set; }

        public virtual TuyenDuong TuyenDuong { get; set; }
    }
}
