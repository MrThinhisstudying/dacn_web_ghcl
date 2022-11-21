using GiaoHangTietKiem.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GiaoHangTietKiem.Controllers.Model
{
    public class TheoDoi
    {
        [BindProperty]
        public string TenKH { set; get; }
        [BindProperty]
        public string SDTKH { set; get; }
        [BindProperty]
        public string DiaChiKH { set; get; }
        [BindProperty]
        public string TenKN { set; get; }
        [BindProperty]
        public string SDTKN { set; get; }
        [BindProperty]
        public string DiaChiKN { set; get; }
        [BindProperty]
        public string MaKH { set; get; }
        [BindProperty]
        public long TongTien { set; get; }
        [BindProperty]
        public bool COD { set; get; }
        [BindProperty]
        public bool TrangThai { set; get; }
        GiaoHangChatLuongContext data = new GiaoHangChatLuongContext();
        public TheoDoi() { }
        public TheoDoi(HoaDonVanChuyen HD)
        {
            KhachHang KH = data.KhachHangs.SingleOrDefault(n => n.MaKH.Equals(HD.MaKH));
            KhachNhan KN = data.KhachNhans.SingleOrDefault(n => n.MaKN.Equals(HD.MaKN));
            TenKH = KH.TenKH;
            SDTKH = KH.SDT;
            DiaChiKH = KH.DiaChi;
            TenKN = KN.TenKN;
            SDTKN = KN.SDT;
            DiaChiKN = KN.DiaChi;
            COD = (bool)HD.COD;
            TongTien = (long)HD.TongTien;
            MaKH = KH.MaKH;
        }
    }
}