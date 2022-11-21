using GiaoHangTietKiem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GiaoHangTietKiem.WebAPI
{
    public class BaoGiaController : ApiController
    {
        private GiaoHangChatLuongContext db = new GiaoHangChatLuongContext();


        /* [HttpGet]
         public async Task<IHttpActionResult> GetGia(string provinceFrom, string provinceTO)
         {
             try
             {

                 var kh = await (from p in db.KhachHangs
                                 join u in db.UserKHs
                                 on p.MaKH equals u.MaKH into g
                                 from d in g
                                 where d.MaKH.Equals(MaKH)
                                 select new KhachHangModel()
                                 {
                                     MaKH = d.MaKH,
                                     TenKH = d.KhachHang.TenKH,
                                     DiaChi = d.KhachHang.DiaChi,
                                     SDT = d.KhachHang.SDT,
                                     UserName = d.UserName,
                                     GioiTinh = d.KhachHang.GioiTinh,
                                     MatKhau = d.MatKhau
                                 }
                            ).FirstOrDefaultAsync();
                 if (kh == null)
                 {
                     return Json(new ResultAPI(false, "Mã khách hàng không tồn tại.", kh));
                 }
                 else
                 {
                     if (kh is KhachHangModel)
                     {
                         //KhachHangModel kh = new KhachHangModel(khachhang);
                         return Json(new ResultAPI(true, "Đã lấy được khách hàng.", kh));
                     }
                     else return Json(new ResultAPI(false, "Mã khách hàng không tồn tại.", kh));

                 }
             }
             catch (Exception e)
             {
                 return Json(new ResultAPI(false, e.Message));
             }
         }*/
    }
}
