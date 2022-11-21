using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GiaoHangTietKiem.Models
{
    public class Profile_Model
    {
        GiaoHangChatLuongContext data = new GiaoHangChatLuongContext();
        public Profile_Model(string tenTK, string hoTen, string email, string ngaySinh, string sDT, string diachi, string chucVu, string phongBan, string gioiTinh, string nhaKho)
        {
            TenTK = tenTK;
            HoTen = hoTen;
            Email = email;
            NgaySinh = ngaySinh;
            SDT = sDT;
            Diachi = diachi;
            ChucVu = chucVu;
            PhongBan = phongBan;
            GioiTinh = gioiTinh;
            NhaKho = nhaKho;
        }
        public Profile_Model() { }
        public Profile_Model(NhanVien nv, TaiKhoan TK)
        {
            TenTK = TK.TenTK;
            HoTen = nv.TenNV;
            Email = TK.Email;
            NgaySinh = nv.NgaySinh.ToString();
            SDT = nv.SDT;
            Diachi = nv.DiaChi;
            ChucVu = nv.ChucVu;
            var pb = data.PhongBans.FirstOrDefault(n => n.MaPB.Equals(nv.MaPB));
            PhongBan = pb.TenPB;
            GioiTinh = (bool)nv.GioiTinh ? "Nữ" : "Nam";
            NhaKho = data.NhaKhoes.FirstOrDefault(n => n.MaNK.Equals(nv.MaNK)).TenNK;
        }
        public string TenTK { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }
        public string NgaySinh { get; set; }
        public string SDT { get; set; }
        public string Diachi { get; set; }
        public string ChucVu { get; set; }
        public string PhongBan { get; set; }
        public string GioiTinh { get; set; }
        public string NhaKho { get; set; }
    }
}