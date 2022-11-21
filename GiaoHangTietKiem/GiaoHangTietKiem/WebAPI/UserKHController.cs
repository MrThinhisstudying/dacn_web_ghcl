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
using System.Web.Routing;

namespace GiaoHangTietKiem.WebAPI
{
    public class UserKHController : ApiController
    {
        // GET: api/UserKH
        private GiaoHangChatLuongContext db = new GiaoHangChatLuongContext();

        [HttpGet]
        public async Task<IHttpActionResult> GetTaiKhoanKH(string TenTK, string MatKhau)
        {
            UserKH tk = await db.UserKHs.FirstOrDefaultAsync(p => p.UserName.Equals(TenTK.Trim()) &&
                                          p.MatKhau.Equals(MatKhau.Trim()));
            if (tk != null)
            {
                UserKHModel uskh = new UserKHModel(tk);
                return Json(uskh);
            }
            return Json(new ResultAPI(false, "Không tồn tại!"));
        }

        [ResponseType(typeof(void))]
        [HttpGet]
        public async Task<IHttpActionResult> CheckUserKH(string TenTK)
        {
            UserKH userKH = await db.UserKHs.FirstOrDefaultAsync(p => p.UserName.Equals(TenTK.Trim()));
            if (userKH !=null)
            {
                return Json(new ResultAPI(true, "Tên tài khoản đã tồn tại, vui lòng nhập lại!"));
            }
            else
            {
                return Json(new ResultAPI(false, "Không tồn tại!"));
            }
        }

        [ResponseType(typeof(void))]
        [HttpPost]
        public async Task<IHttpActionResult> PostUserKH([FromBody] UserKHModel taiKhoanKh)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!UserKHExists(taiKhoanKh.TenTK))
            {
                try
                {
                    string s = string.Format("INSERT dbo.UserKH( MatKhau,Email,MaKH,Username)VALUES(  N'{0}', '{1}', N'{2}', {3})", taiKhoanKh.MatKhau, taiKhoanKh.Email, taiKhoanKh.MaKH, taiKhoanKh.TenTK);
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
                return Json(new ResultAPI(false, "Tên tài khoản đã tồn tại, vui lòng nhập lại khác!"));
            }
            return Json(new ResultAPI(true, "Thêm mới tài khoản thành công!"));
        }

        private bool UserKHExists(string username)
        {
            return db.UserKHs.Count(e => e.UserName.Equals(username.Trim() )) > 0;
        }
        public class UserKHModel
        {
            public string TenTK { get; set; }
            public string MatKhau { get; set; }
            public string MaKH { get; set; }
            public string Email { get; set; }


            public UserKHModel(UserKH tk)
            {
                this.TenTK = tk.UserName;
                this.MatKhau = tk.MatKhau;
                this.MaKH = tk.MaKH;
                this.Email = tk.Email;
            }
            public UserKHModel() { }
        }
    }
}
