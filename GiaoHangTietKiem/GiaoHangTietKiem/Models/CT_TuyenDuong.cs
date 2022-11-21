namespace GiaoHangTietKiem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CT_TuyenDuong
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CT_TuyenDuong()
        {
            CT_ChuyenXe = new HashSet<CT_ChuyenXe>();
        }

        [Key]
        [StringLength(10)]
        public string Ma_CTTD { get; set; }

        [Required]
        [StringLength(10)]
        public string MaNK { get; set; }

        [Required]
        [StringLength(10)]
        public string MaTD { get; set; }

        [Required]
        [StringLength(10)]
        public string MaKhoDen { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CT_ChuyenXe> CT_ChuyenXe { get; set; }

        public virtual NhaKho NhaKho { get; set; }

        public virtual NhaKho NhaKho1 { get; set; }

        public virtual TuyenDuong TuyenDuong { get; set; }

        public virtual TuyenDuong TuyenDuong1 { get; set; }
    }
}
