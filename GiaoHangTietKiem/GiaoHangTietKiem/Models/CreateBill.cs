using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GiaoHangTietKiem.Models
{
    public class CreateBill
    {
        private string tenKH;
        private string sDTKH;
        private string ls_province;
        private string ls_district;
        private string ls_ward;
        private string diaChiKH;
        private string gioiTinhKH;
        private string tenKN;
        private string sDTKN;
        private string ls_province1;
        private string ls_district1;
        private string ls_ward1;
        private string diaChiKN;
        private string gioiTinhKN;
        private int kg;
        private bool cod;
        private string maLVC;
        private string maLHH;
        public CreateBill() { }
        public CreateBill(string tenKH, string sDTKH, string ls_province, string ls_district, string ls_ward, string diaChiKH, string gioiTinhKH, string tenKN, string sDTKN, string ls_province1, string ls_district1, string ls_ward1, string diaChiKN, string gioiTinhKN, int kg, bool cod, string maLVC, string maLHH)
        {
            this.tenKH = tenKH;
            this.sDTKH = sDTKH;
            this.ls_province = ls_province;
            this.ls_district = ls_district;
            this.ls_ward = ls_ward;
            this.diaChiKH = diaChiKH;
            this.gioiTinhKH = gioiTinhKH;
            this.tenKN = tenKN;
            this.sDTKN = sDTKN;
            this.ls_province1 = ls_province1;
            this.ls_district1 = ls_district1;
            this.ls_ward1 = ls_ward1;
            this.diaChiKN = diaChiKN;
            this.gioiTinhKN = gioiTinhKN;
            this.kg = kg;
            this.cod = cod;
            this.maLVC = maLVC;
            this.maLHH = maLHH;
        }

        public string TenKH { get => tenKH; set => tenKH = value; }
        public string SDTKH { get => sDTKH; set => sDTKH = value; }
        public string Ls_province { get => ls_province; set => ls_province = value; }
        public string Ls_district { get => ls_district; set => ls_district = value; }
        public string Ls_ward { get => ls_ward; set => ls_ward = value; }
        public string DiaChiKH { get => diaChiKH; set => diaChiKH = value; }
        public string GioiTinhKH { get => gioiTinhKH; set => gioiTinhKH = value; }
        public string TenKN { get => tenKN; set => tenKN = value; }
        public string SDTKN { get => sDTKN; set => sDTKN = value; }
        public string Ls_province1 { get => ls_province1; set => ls_province1 = value; }
        public string Ls_district1 { get => ls_district1; set => ls_district1 = value; }
        public string Ls_ward1 { get => ls_ward1; set => ls_ward1 = value; }
        public string DiaChiKN { get => diaChiKN; set => diaChiKN = value; }
        public string GioiTinhKN { get => gioiTinhKN; set => gioiTinhKN = value; }
        public int Kg { get => kg; set => kg = value; }
        public bool Cod { get => cod; set => cod = value; }
        public string MaLVC { get => maLVC; set => maLVC = value; }
        public string MaLHH { get => maLHH; set => maLHH = value; }
    }
}