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
    public class KhachHangController : ApiController
    {
        private GiaoHangChatLuongContext db = new GiaoHangChatLuongContext();
        // GET api/khachhang

        [ResponseType(typeof(KhachHangModel))]
        [HttpGet]
        public async Task<IHttpActionResult> GetKhachHang(string MaKH)
        {
            try
            {
                /*var khachhang = await db.Database.SqlQuery<KhachHangModel>("select  k.MaKH, k.TenKH, k.DiaChi, k.GioiTinh, k.SDT, u.UserName,u.MatKhau " +
               "from KhachHang k , UserKH u " +
               "where k.MaKH = '{0}' and u.MaKH  = '{0}' ",MaKH).FirstOrDefaultAsync();*/
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


        }

        /* [ResponseType(typeof(KhachHangModel))]
         [HttpGet]
         public async Task<IHttpActionResult> GetKhachHangSDT(string SDT)
         {
             using (var context = new GiaoHangChatLuongContext())
             {
                 KhachHang khachhang = await context.KhachHangs.FirstOrDefaultAsync(p => p.SDT.Equals(SDT));
                 if (khachhang == null)
                 {
                     return NotFound();
                 }
                 else
                 {
                     KhachHangModel kh = new KhachHangModel(khachhang);
                     return Json(kh);
                 }
             }
         }*/

        [ResponseType(typeof(void))]
        [HttpGet]
        public async Task<IHttpActionResult> CheckKhachHangSDT(string SDT)
        {
            KhachHang khachhang = await db.KhachHangs.FirstOrDefaultAsync(p => p.SDT.Equals(SDT));
            if (khachhang == null)
            {
                return Json(new ResultAPI(false, "Không tồn tại!"));
            }
            else
            {
                return Json(new ResultAPI(true, "Số điện thoại đã tồn tại!"));

            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetDSKhachHang()
        {
            using (var context = new GiaoHangChatLuongContext())
            {
                List<KhachHang> lst = await (from p in context.KhachHangs select p).ToListAsync();
                if (lst != null)
                {
                    List<KhachHangModel> lstKH = new List<KhachHangModel>();
                    foreach (KhachHang e in lst)
                    {
                        lstKH.Add(new KhachHangModel(e));
                    }
                    return Json(lstKH);
                }
                return Json(new ResultAPI(false, "Không tồn tại!"));
            }
        }

        [ResponseType(typeof(void))]
        [HttpPut]
        public async Task<IHttpActionResult> PutKhachHang([FromBody] KhachHangModel khacHang)
        {
            if (!ModelState.IsValid)
            {
                return Json(new ResultAPI(false, "Bad request"));
            }

            if (KhachHangExists(khacHang.SDT))
            {
                try
                {
                    KhachHang kh = await db.KhachHangs.FirstOrDefaultAsync(p => p.SDT.Equals(khacHang.SDT));
                    if (kh != null)
                    {
                        kh.TenKH = khacHang.TenKH;
                        kh.DiaChi = khacHang.DiaChi;
                        kh.GioiTinh = khacHang.GioiTinh;
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
            return Json(new ResultAPI(true, "Cập nhật thông tin khách hàng thành công!"));

        }


        [ResponseType(typeof(void))]
        [HttpPost]
        public async Task<IHttpActionResult> PostKhachHang([FromBody] KhachHangModel khacHang)
        {
            if (!ModelState.IsValid)
            {
                return Json(new ResultAPI(false, "Bad request"));
            }

            if (!KhachHangExists(khacHang.SDT))
            {
                if (!UserKHExists(khacHang.UserName))
                {
                    try
                    {
                        string s = string.Format("INSERT dbo.KhachHang( MaKH,TenKH, SDT, DiaChi, GioiTinh)VALUES( DEFAULT, N'{0}', '{1}', N'{2}', {3})\n" +
                                        "DECLARE @ID VARCHAR(10) \n" +
                                        "select @ID = MaKH from KhachHang where SDT = '{1}'\n" +
                                        " insert dbo.UserKH(UserName, MaKH, MatKhau, SDT)\n" +
                                        " values('{4}', @ID, '{5}', '{1}')",
                            khacHang.TenKH, khacHang.SDT, khacHang.DiaChi, khacHang.GioiTinh == true ? 1 : 0, khacHang.UserName, khacHang.MatKhau);
                        db.Database.ExecuteSqlCommand(s);

                        await db.SaveChangesAsync();
                    }
                    catch (Exception e)
                    {
                        return Json(new ResultAPI(false, e.Message.ToString(), khacHang));

                    }
                }
                else
                {
                    return Json(new ResultAPI(false, "Tên tài khoản đã tồn tại, vui lòng nhập lại!"));
                }
            }
            else
            {
                return Json(new ResultAPI(false, "Số điện thoại đã tồn tại, vui lòng nhập số khác!"));
            }
            return Json(new ResultAPI(true, "Thêm mới khách hàng thành công!")); //,db.KhachHangs.SingleOrDefaultAsync(p=>p.SDT.Equals(khacHang.SDT)).Result.MaKH)

        }
        private bool UserKHExists(string username)
        {
            return db.UserKHs.Count(e => e.UserName.Equals(username)) > 0;
        }

        private bool KhachHangExists(string SDT)
        {
            return db.KhachHangs.Count(e => e.SDT == SDT) > 0;
        }



        public class KhachHangModel
        {
            public string MaKH { get; set; }
            public string TenKH { get; set; }
            public bool GioiTinh { get; set; }
            public string DiaChi { get; set; }
            public string SDT { get; set; }
            public string UserName { get; set; }
            public string MatKhau { get; set; }

            public KhachHangModel(KhachHang kh)
            {
                this.MaKH = kh.MaKH;
                this.TenKH = kh.TenKH;
                this.GioiTinh = kh.GioiTinh;
                this.DiaChi = kh.DiaChi;
                this.SDT = kh.SDT;
            }

            public KhachHangModel()
            {

            }

            public KhachHangModel(string TenKH, string DiaChi, string SDT, bool GioiTinh, string UserName, string MatKhau)
            {
                this.SDT = SDT;
                this.TenKH = TenKH;
                this.DiaChi = DiaChi;
                this.GioiTinh = GioiTinh;
                this.UserName = UserName;
                this.MatKhau = MatKhau;
            }

        }
    }
}
