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
    public class HoaDonVanChuyenController : ApiController
    {
        private GiaoHangChatLuongContext db = new GiaoHangChatLuongContext();

        [ResponseType(typeof(HoaDonVanChuyenModel))]
        [HttpGet]
        public async Task<IHttpActionResult> GetDSHoaDonVanChuyen(string MaKH)
        {
            using (var context = new GiaoHangChatLuongContext())
            {
                List<HoaDonVanChuyen> lst = await (from p in db.HoaDonVanChuyens where p.MaKH.Equals(MaKH) select p).ToListAsync();
                if (lst.Count > 0)
                {
                    List<HoaDonVanChuyenModel> lsHD = new List<HoaDonVanChuyenModel>();
                    foreach (HoaDonVanChuyen e in lst)
                    {
                        lsHD.Add(new HoaDonVanChuyenModel(e));
                    }
                    return Json(new ResultAPI(true, "Lấy các hóa đơn vận chuyển thành công", lsHD));
                }
                else
                {
                    return Json(new ResultAPI(false, "Không có hóa đơn vận chuyển"));
                }
            }
        }

        /*[ResponseType(typeof(HoaDonVanChuyenModel))]
        [HttpPost]
        public async Task<IHttpActionResult> PostHoaDonVanChuyen([FromBody] HoaDonVanChuyenModel hd)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                string s = string.Format("INSERT [dbo].[HoaDonVanChuyen] ([SoHD],[NgayLapHD],[TongTien],[TienVC],[MaNV],[MaTT],[COD],[MaTD],[MaLVC],[MaKH],[MaKN])VALUES( DEFAULT, '{0}', {1}, {2},'{3}','{4}',{5},'{6}','{7}','{8}','{9}' )",
                    hd.NgayLap.ToString("MM-dd-yyyy"), hd.TongTien,hd.TienVC,hd.);
                db.Database.ExecuteSqlCommand(s);
                await db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return Json(new ResultAPI(false, e.Message.ToString()));

            }
            return Json(new ResultAPI(true, "Thêm mới phiếu yêu cầu thành công!"));
        }
    }*/

        public class HoaDonVanChuyenModel
        {
            public HoaDonVanChuyenModel(DateTime ngayLap, long tongTien, long tienVC, string maTT, string maLVC, string maKH, string maKN, bool cOD)
            {
                //DateTime.Now.ToString("MM/dd/yyyy");
                
                  
                NgayLap = ngayLap.ToShortDateString(); 
                TongTien = tongTien;
                TienVC = tienVC;
                MaTT = maTT;
                MaLVC = maLVC;
                MaKH = maKH;
                MaKN = maKN;
                COD = cOD;
            }

            public HoaDonVanChuyenModel(HoaDonVanChuyen obj)
            {
                this.SoHD = obj.SoHD;
                this.NgayLap = ((DateTime)obj.NgayLapHD).ToShortDateString();
                this.TongTien = (long)obj.TongTien;
                this.TienVC = (long)obj.TienVC;
                this.MaLVC = obj.MaLVC;
                this.MaTT = obj.MaTT;
                this.MaKH = obj.MaKH;
                this.MaKN = obj.MaKN;

            }

            public string SoHD { get; set; }
            public string NgayLap { get; set; }
            public long TongTien { get; set; }
            public long TienVC { get; set; }
            public string MaTT { get; set; }
            public string MaLVC { get; set; }
            public string MaKH { get; set; }
            public string MaKN { get; set; }
            public bool COD { get; set; }



        }
    }
}
