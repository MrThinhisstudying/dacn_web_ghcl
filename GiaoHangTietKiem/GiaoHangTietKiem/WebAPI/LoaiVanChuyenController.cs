using GiaoHangTietKiem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GiaoHangTietKiem.WebAPI
{
    public class LoaiVanChuyenController : ApiController
    {
        [HttpGet]
        public async Task<IHttpActionResult> GetLoaiVC()
        {
            using (var context = new GiaoHangChatLuongContext())
            {
                List<LoaiVanChuyen> lst = await (from p in context.LoaiVanChuyens select p).ToListAsync();
                if (lst != null)
                {
                    List<LoaiVanChuyenModel> lstLVC = new List<LoaiVanChuyenModel>();
                    foreach (LoaiVanChuyen e in lst)
                    {
                        lstLVC.Add(new LoaiVanChuyenModel(e));
                    }
                    return Json(lstLVC);
                }
                return Json(new ResultAPI(false, "Không tồn tại!"));

            }
        }

        public class LoaiVanChuyenModel
        {
            public string MaLVC { get; set; }
            public string TenLVC { get; set; }
            public double Gia { get; set; }
            public LoaiVanChuyenModel() { }

            public LoaiVanChuyenModel(LoaiVanChuyen lvc)
            {
                this.MaLVC = lvc.MaLVC;
                this.TenLVC = lvc.TenLVC;
                this.Gia = lvc.Gia;
            }
        }
    }
}
