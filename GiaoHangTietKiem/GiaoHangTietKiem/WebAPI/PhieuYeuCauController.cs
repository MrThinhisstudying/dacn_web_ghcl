using GiaoHangTietKiem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace GiaoHangTietKiem.WebAPI
{
    public class PhieuYeuCauController : ApiController
    {
        private GiaoHangChatLuongContext db = new GiaoHangChatLuongContext();

        [ResponseType(typeof(PhieuYeuCauModel))]
        [HttpGet]
        public async Task<IHttpActionResult> GetDSPhieuYeuCau(string MaKH)
        {
            using (var context = new GiaoHangChatLuongContext())
            {
                List<PhieuYeuCau> lst = await (from p in db.PhieuYeuCaus where p.MaKH.Equals(MaKH) select p).ToListAsync();
                if (lst.Count > 0)
                {
                    List<PhieuYeuCauModel> lsPYC = new List<PhieuYeuCauModel>();
                    foreach (PhieuYeuCau e in lst)
                    {
                        lsPYC.Add(new PhieuYeuCauModel(e));
                    }
                    return Json(new ResultAPI(true, "Lấy các phiếu yêu cầu thành công", lsPYC));
                }
                else
                {
                    return Json(new ResultAPI(false, "Không có phiếu yêu cầu"));
                }
            }
        }

        [ResponseType(typeof(PhieuYeuCauModel))]
        [HttpPost]
        public async Task<IHttpActionResult> PostPhieuYeuCau([FromBody] PhieuYeuCauModel pyc)
        {
            if (!ModelState.IsValid)
            {
                return Json(new ResultAPI(false, "Bad request"));
            }
            try
            {
                string s = string.Format("INSERT dbo.PhieuYeuCau( SoPYC , NgayLap, MaKH, KhoiLuong, MaKN, MaLVC ,ThanhToan, MaTT)VALUES( DEFAULT, '{0}', '{1}', {2}, '{3}' , '{4}', {5},0)", pyc.NgayLap.ToString("MM-dd-yyyy"), pyc.MaKH, pyc.KhoiLuong.ToString().Replace(',', '.'), pyc.MaKN, pyc.MaLVC, pyc.ThanhToan);
                db.Database.ExecuteSqlCommand(s);
                await db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return Json(new ResultAPI(false, e.Message.ToString()));

            }
            return Json(new ResultAPI(true, "Thêm mới phiếu yêu cầu thành công!"));
        }





        public class PhieuYeuCauModel
        {
            public string SoPYC { get; set; }
            public string MaKH { get; set; }
            public DateTime NgayLap { get; set; }
            public string MaKN { get; set; }
            public string MaLVC { get; set; }
            public long? ThanhToan { get; set; }
            public double KhoiLuong { get; set; }
            public bool TrangThai { get; set; }

            public PhieuYeuCauModel(string SoPYC, string MaKH, DateTime Ngaylap, string MaKN, string MalVC, long ThanhToan, double KhoiLuong, bool TrangThai)
            {
                this.SoPYC = SoPYC;
                this.MaKH = MaKH;
                this.NgayLap = NgayLap;
                this.MaKN = MaKN;
                this.MaLVC = MaLVC;
                this.ThanhToan = ThanhToan;
                this.KhoiLuong = KhoiLuong;
                this.TrangThai = TrangThai;
            }

            public PhieuYeuCauModel(PhieuYeuCau pyc)
            {
                this.SoPYC = pyc.SoPYC;
                this.MaLVC = pyc.MaLVC;
                this.MaKN = pyc.MaKN;
                this.KhoiLuong = (double)pyc.KhoiLuong;
                this.MaKH = pyc.MaKH;
                this.ThanhToan = pyc.ThanhToan;
                this.NgayLap = (DateTime)pyc.NgayLap;
                this.TrangThai = (bool)pyc.TrangThai;
            }

            public PhieuYeuCauModel()
            {

            }
        }
    }
}
