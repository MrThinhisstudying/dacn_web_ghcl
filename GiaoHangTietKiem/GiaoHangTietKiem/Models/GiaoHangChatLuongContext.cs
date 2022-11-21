using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace GiaoHangTietKiem.Models
{
    public partial class GiaoHangChatLuongContext : DbContext
    {
        public GiaoHangChatLuongContext()
            : base("name=GiaoHangChatLuongContext")
        {
        }

        public virtual DbSet<CT_ChuyenXe> CT_ChuyenXe { get; set; }
        public virtual DbSet<CT_Role> CT_Role { get; set; }
        public virtual DbSet<CT_TuyenDuong> CT_TuyenDuong { get; set; }
        public virtual DbSet<CTVanChuyen> CTVanChuyens { get; set; }
        public virtual DbSet<HoaDonVanChuyen> HoaDonVanChuyens { get; set; }
        public virtual DbSet<KhachHang> KhachHangs { get; set; }
        public virtual DbSet<KhachNhan> KhachNhans { get; set; }
        public virtual DbSet<KhuVuc> KhuVucs { get; set; }
        public virtual DbSet<KhuyenMai> KhuyenMais { get; set; }
        public virtual DbSet<LoaiHH> LoaiHHs { get; set; }
        public virtual DbSet<LoaiVanChuyen> LoaiVanChuyens { get; set; }
        public virtual DbSet<NhaKho> NhaKhoes { get; set; }
        public virtual DbSet<NhanVien> NhanViens { get; set; }
        public virtual DbSet<PhieuYeuCau> PhieuYeuCaus { get; set; }
        public virtual DbSet<PhongBan> PhongBans { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }
        public virtual DbSet<TrangThai> TrangThais { get; set; }
        public virtual DbSet<TuyenDuong> TuyenDuongs { get; set; }
        public virtual DbSet<UserKH> UserKHs { get; set; }
        public virtual DbSet<XeVanChuyen> XeVanChuyens { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CT_ChuyenXe>()
                .Property(e => e.Ma_CTTD)
                .IsUnicode(false);

            modelBuilder.Entity<CT_Role>()
                .Property(e => e.TenTK)
                .IsUnicode(false);

            modelBuilder.Entity<CT_TuyenDuong>()
                .Property(e => e.Ma_CTTD)
                .IsUnicode(false);

            modelBuilder.Entity<CT_TuyenDuong>()
                .Property(e => e.MaNK)
                .IsUnicode(false);

            modelBuilder.Entity<CT_TuyenDuong>()
                .Property(e => e.MaTD)
                .IsUnicode(false);

            modelBuilder.Entity<CT_TuyenDuong>()
                .Property(e => e.MaKhoDen)
                .IsUnicode(false);

            modelBuilder.Entity<CTVanChuyen>()
                .Property(e => e.MaCTVC)
                .IsUnicode(false);

            modelBuilder.Entity<CTVanChuyen>()
                .Property(e => e.SoHD)
                .IsUnicode(false);

            modelBuilder.Entity<CTVanChuyen>()
                .Property(e => e.MaNK)
                .IsUnicode(false);

            modelBuilder.Entity<HoaDonVanChuyen>()
                .Property(e => e.SoHD)
                .IsUnicode(false);

            modelBuilder.Entity<HoaDonVanChuyen>()
                .Property(e => e.MaNV)
                .IsUnicode(false);

            modelBuilder.Entity<HoaDonVanChuyen>()
                .Property(e => e.MaTD)
                .IsUnicode(false);

            modelBuilder.Entity<HoaDonVanChuyen>()
                .Property(e => e.MaLVC)
                .IsUnicode(false);

            modelBuilder.Entity<HoaDonVanChuyen>()
                .Property(e => e.MaKH)
                .IsUnicode(false);

            modelBuilder.Entity<HoaDonVanChuyen>()
                .Property(e => e.MaKN)
                .IsUnicode(false);

            modelBuilder.Entity<HoaDonVanChuyen>()
                .Property(e => e.MaTT)
                .IsUnicode(false);

            modelBuilder.Entity<HoaDonVanChuyen>()
                .Property(e => e.MaLHH)
                .IsUnicode(false);

            modelBuilder.Entity<HoaDonVanChuyen>()
                .HasMany(e => e.CTVanChuyens)
                .WithOptional(e => e.HoaDonVanChuyen)
                .HasForeignKey(e => e.SoHD);

            modelBuilder.Entity<HoaDonVanChuyen>()
                .HasMany(e => e.CTVanChuyens1)
                .WithOptional(e => e.HoaDonVanChuyen1)
                .HasForeignKey(e => e.SoHD);

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.MaKH)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.SDT)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang>()
                .HasMany(e => e.PhieuYeuCaus)
                .WithOptional(e => e.KhachHang)
                .HasForeignKey(e => e.MaKH);

            modelBuilder.Entity<KhachHang>()
                .HasMany(e => e.PhieuYeuCaus1)
                .WithOptional(e => e.KhachHang1)
                .HasForeignKey(e => e.MaKH);

            modelBuilder.Entity<KhachHang>()
                .HasMany(e => e.UserKHs)
                .WithOptional(e => e.KhachHang)
                .HasForeignKey(e => e.MaKH);

            modelBuilder.Entity<KhachHang>()
                .HasMany(e => e.UserKHs1)
                .WithOptional(e => e.KhachHang1)
                .HasForeignKey(e => e.MaKH);

            modelBuilder.Entity<KhachNhan>()
                .Property(e => e.MaKN)
                .IsUnicode(false);

            modelBuilder.Entity<KhachNhan>()
                .Property(e => e.SDT)
                .IsUnicode(false);

            modelBuilder.Entity<KhachNhan>()
                .HasMany(e => e.PhieuYeuCaus)
                .WithOptional(e => e.KhachNhan)
                .HasForeignKey(e => e.MaKN);

            modelBuilder.Entity<KhachNhan>()
                .HasMany(e => e.PhieuYeuCaus1)
                .WithOptional(e => e.KhachNhan1)
                .HasForeignKey(e => e.MaKN);

            modelBuilder.Entity<KhuVuc>()
                .Property(e => e.MaKV)
                .IsUnicode(false);

            modelBuilder.Entity<KhuVuc>()
                .HasMany(e => e.NhaKhoes)
                .WithOptional(e => e.KhuVuc)
                .HasForeignKey(e => e.MaKV);

            modelBuilder.Entity<KhuVuc>()
                .HasMany(e => e.NhaKhoes1)
                .WithOptional(e => e.KhuVuc1)
                .HasForeignKey(e => e.MaKV);

            modelBuilder.Entity<KhuyenMai>()
                .Property(e => e.MaKM)
                .IsUnicode(false);

            modelBuilder.Entity<LoaiHH>()
                .Property(e => e.MaLHH)
                .IsUnicode(false);

            modelBuilder.Entity<LoaiHH>()
                .HasMany(e => e.HoaDonVanChuyens)
                .WithOptional(e => e.LoaiHH)
                .WillCascadeOnDelete();

            modelBuilder.Entity<LoaiVanChuyen>()
                .Property(e => e.MaLVC)
                .IsUnicode(false);

            modelBuilder.Entity<LoaiVanChuyen>()
                .HasMany(e => e.HoaDonVanChuyens)
                .WithOptional(e => e.LoaiVanChuyen)
                .HasForeignKey(e => e.MaLVC);

            modelBuilder.Entity<LoaiVanChuyen>()
                .HasMany(e => e.HoaDonVanChuyens1)
                .WithOptional(e => e.LoaiVanChuyen1)
                .HasForeignKey(e => e.MaLVC);

            modelBuilder.Entity<LoaiVanChuyen>()
                .HasMany(e => e.PhieuYeuCaus)
                .WithOptional(e => e.LoaiVanChuyen)
                .HasForeignKey(e => e.MaLVC);

            modelBuilder.Entity<LoaiVanChuyen>()
                .HasMany(e => e.PhieuYeuCaus1)
                .WithOptional(e => e.LoaiVanChuyen1)
                .HasForeignKey(e => e.MaLVC);

            modelBuilder.Entity<NhaKho>()
                .Property(e => e.MaNK)
                .IsUnicode(false);

            modelBuilder.Entity<NhaKho>()
                .Property(e => e.MaKV)
                .IsUnicode(false);

            modelBuilder.Entity<NhaKho>()
                .HasMany(e => e.CT_TuyenDuong)
                .WithRequired(e => e.NhaKho)
                .HasForeignKey(e => e.MaNK)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NhaKho>()
                .HasMany(e => e.CT_TuyenDuong1)
                .WithRequired(e => e.NhaKho1)
                .HasForeignKey(e => e.MaNK)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NhaKho>()
                .HasMany(e => e.CTVanChuyens)
                .WithOptional(e => e.NhaKho)
                .HasForeignKey(e => e.MaNK);

            modelBuilder.Entity<NhaKho>()
                .HasMany(e => e.CTVanChuyens1)
                .WithOptional(e => e.NhaKho1)
                .HasForeignKey(e => e.MaNK);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.MaNV)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.MaPB)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.SDT)
                .IsFixedLength();

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.MaNK)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .HasMany(e => e.HoaDonVanChuyens)
                .WithOptional(e => e.NhanVien)
                .HasForeignKey(e => e.MaNV);

            modelBuilder.Entity<NhanVien>()
                .HasMany(e => e.HoaDonVanChuyens1)
                .WithOptional(e => e.NhanVien1)
                .HasForeignKey(e => e.MaNV);

            modelBuilder.Entity<NhanVien>()
                .HasMany(e => e.TaiKhoans)
                .WithRequired(e => e.NhanVien)
                .HasForeignKey(e => e.MaNV)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NhanVien>()
                .HasMany(e => e.TaiKhoans1)
                .WithRequired(e => e.NhanVien1)
                .HasForeignKey(e => e.MaNV)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PhieuYeuCau>()
                .Property(e => e.SoPYC)
                .IsUnicode(false);

            modelBuilder.Entity<PhieuYeuCau>()
                .Property(e => e.MaKH)
                .IsUnicode(false);

            modelBuilder.Entity<PhieuYeuCau>()
                .Property(e => e.KhoiLuong)
                .HasPrecision(3, 1);

            modelBuilder.Entity<PhieuYeuCau>()
                .Property(e => e.MaKN)
                .IsUnicode(false);

            modelBuilder.Entity<PhieuYeuCau>()
                .Property(e => e.MaLVC)
                .IsUnicode(false);

            modelBuilder.Entity<PhongBan>()
                .Property(e => e.MaPB)
                .IsUnicode(false);

            modelBuilder.Entity<PhongBan>()
                .HasMany(e => e.NhanViens)
                .WithRequired(e => e.PhongBan)
                .HasForeignKey(e => e.MaPB)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PhongBan>()
                .HasMany(e => e.NhanViens1)
                .WithRequired(e => e.PhongBan1)
                .HasForeignKey(e => e.MaPB)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.CT_Role)
                .WithRequired(e => e.Role)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.TenTK)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.MatKhau)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.MaNV)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .HasMany(e => e.CT_Role)
                .WithRequired(e => e.TaiKhoan)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TrangThai>()
                .Property(e => e.MaTT)
                .IsUnicode(false);

            modelBuilder.Entity<TrangThai>()
                .HasMany(e => e.HoaDonVanChuyens)
                .WithOptional(e => e.TrangThai)
                .WillCascadeOnDelete();

            modelBuilder.Entity<TuyenDuong>()
                .Property(e => e.MaTD)
                .IsUnicode(false);

            modelBuilder.Entity<TuyenDuong>()
                .Property(e => e.MaKhoBD)
                .IsUnicode(false);

            modelBuilder.Entity<TuyenDuong>()
                .Property(e => e.MaKhoKT)
                .IsUnicode(false);

            modelBuilder.Entity<TuyenDuong>()
                .HasMany(e => e.CT_TuyenDuong)
                .WithRequired(e => e.TuyenDuong)
                .HasForeignKey(e => e.MaTD)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TuyenDuong>()
                .HasMany(e => e.CT_TuyenDuong1)
                .WithRequired(e => e.TuyenDuong1)
                .HasForeignKey(e => e.MaTD)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserKH>()
                .Property(e => e.SDT)
                .IsUnicode(false);

            modelBuilder.Entity<UserKH>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<UserKH>()
                .Property(e => e.MaKH)
                .IsUnicode(false);

            modelBuilder.Entity<UserKH>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<UserKH>()
                .Property(e => e.authkey)
                .IsUnicode(false);
        }
    }
}
