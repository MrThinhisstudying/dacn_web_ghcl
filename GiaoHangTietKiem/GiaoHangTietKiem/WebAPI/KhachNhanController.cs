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
    public class KhachNhanController : ApiController
    {
        private GiaoHangChatLuongContext db = new GiaoHangChatLuongContext();
        [HttpGet]
        public async Task<IHttpActionResult> GetTTKhachNhan(string SDT)
        {
            KhachNhan kn = await db.KhachNhans.FirstOrDefaultAsync(p => p.SDT.Equals(SDT));
            if (kn != null)
            {
                KhachNhanModel knModel = new KhachNhanModel(kn);
                return Json(new ResultAPI(true, "Tìm thông tin khách nhận thành công", knModel));
            }
            return Json(new ResultAPI(false, "Không tồn tại!"));
        }

        [ResponseType(typeof(void))]
        [HttpPut]
        public async Task<IHttpActionResult> PutKhachNhan([FromBody] KhachNhanModel khacNhan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (KhachNhanExists(khacNhan.SDT))
            {
                try
                {
                    KhachNhan kn = await db.KhachNhans.FirstOrDefaultAsync(p => p.SDT.Equals(khacNhan.SDT));
                    if (kn != null)
                    {
                        kn.TenKN = khacNhan.TenKN;
                        kn.DiaChi = khacNhan.DiaChi;
                        kn.GioiTinh = khacNhan.GioiTinh;
                        await db.SaveChangesAsync();
                    }
                }
                catch (Exception e)
                {
                    return Json(new ResultAPI(false, e.Message.ToString()));

                }
            }
            else
            {
                return Json(new ResultAPI(false, "Số điện thoại không tồn tại!"));
            }
            return Json(new ResultAPI(true, "Cập nhật thông tin khách nhận thành công!"));

        }


        [ResponseType(typeof(void))]
        [HttpPost]
        public async Task<IHttpActionResult> PostKhachNhan([FromBody] KhachNhanModel khachNhan)
        {
            if (!ModelState.IsValid)
            {
                return Json(new ResultAPI(false, "Bad request"));
            }

            if (!KhachNhanExists(khachNhan.SDT))
            {
                try
                {
                    string s = string.Format("INSERT dbo.KhachNhan( MaKH,TenKH, SDT, DiaChi, GioiTinh)VALUES( DEFAULT, N'{0}', '{1}', N'{2}', {3})", khachNhan.TenKN, khachNhan.SDT, khachNhan.DiaChi, khachNhan.GioiTinh == true ? 1 : 0);
                    db.Database.ExecuteSqlCommand(s);
                    await db.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    return Json(new ResultAPI(false, e.Message.ToString()));

                }
            }
            else
            {
                return Json(new ResultAPI(false, "Số điện thoại đã tồn tại, vui lòng nhập số khác!"));
            }
            return Json(new ResultAPI(true, "Thêm mới khách nhận thành công!"));
        }

        private bool KhachNhanExists(string SDT)
        {
            return db.KhachNhans.Count(e => e.SDT == SDT) > 0;
        }


        public class KhachNhanModel
        {
            public string TenKN { get; set; }
            public string MaKN { get; set; }
            public string SDT { get; set; }
            public string DiaChi { get; set; }
            public bool GioiTinh { get; set; }

            public KhachNhanModel(string TenKN, string MaKN, string SDT, string DiaChi, bool GioiTinh)
            {
                this.TenKN = TenKN;
                this.MaKN = MaKN;
                this.SDT = SDT;
                this.DiaChi = DiaChi;
                this.GioiTinh = GioiTinh;
            }

            public KhachNhanModel(KhachNhan kn)
            {
                this.TenKN = kn.TenKN;
                this.MaKN = kn.MaKN;
                this.SDT = kn.SDT;
                this.DiaChi = kn.DiaChi;
                this.GioiTinh = kn.GioiTinh;
            }
            public KhachNhanModel() { }
        }
    }
}
