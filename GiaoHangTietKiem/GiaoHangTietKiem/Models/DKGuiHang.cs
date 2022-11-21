using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GiaoHangTietKiem.Models
{
    public class DKGuiHang
    {
        private string tenKN;
        private string sDTKN;
        private string ls_province1;
        private string ls_district1;
        private string ls_ward1;
        private string diaChiKN;
        private string gioiTinhKN;
        private int kg;
        private string maLVC;
        public DKGuiHang() { }
        public DKGuiHang(string tenKN, string sDTKN, string ls_province1, string ls_district1, string ls_ward1, string diaChiKN, string gioiTinhKN, int kg, string maLVC)
        {
            this.TenKN = tenKN;
            this.SDTKN = sDTKN;
            this.Ls_province1 = ls_province1;
            this.Ls_district1 = ls_district1;
            this.Ls_ward1 = ls_ward1;
            this.DiaChiKN = diaChiKN;
            this.GioiTinhKN = gioiTinhKN;
            this.Kg = kg;
            this.MaLVC = maLVC;
        }

        public string TenKN { get => tenKN; set => tenKN = value; }
        public string SDTKN { get => sDTKN; set => sDTKN = value; }
        public string Ls_province1 { get => ls_province1; set => ls_province1 = value; }
        public string Ls_district1 { get => ls_district1; set => ls_district1 = value; }
        public string Ls_ward1 { get => ls_ward1; set => ls_ward1 = value; }
        public string DiaChiKN { get => diaChiKN; set => diaChiKN = value; }
        public string GioiTinhKN { get => gioiTinhKN; set => gioiTinhKN = value; }
        public int Kg { get => kg; set => kg = value; }
        public string MaLVC { get => maLVC; set => maLVC = value; }

        public string getFullAddressKN(DKGuiHang dangKy)
        {
            return dangKy.DiaChiKN + ", " + dangKy.Ls_ward1 + ", " + dangKy.Ls_district1 + ", " + dangKy.Ls_province1;
        }
    }
   }