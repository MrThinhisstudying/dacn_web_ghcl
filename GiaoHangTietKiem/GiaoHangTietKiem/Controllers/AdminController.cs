using Common1;
using GiaoHangTietKiem.Controllers.Model;
using GiaoHangTietKiem.Models;
using GiaoHangTietKiem.Views.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GiaoHangTietKiem.Controllers
{
    public class AdminController : Controller
    {
        GiaoHangChatLuongContext data = new GiaoHangChatLuongContext();
        public ActionResult IndexAdmin()
        {
            if (Session["TaiKhoan"] != null)
            {
                Cap_quyen((string)Session["TaiKhoan"]);
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        public ActionResult Login()
        {
            if (Session["TaiKhoan"] != null)
            {
                string tk = (string)Session["TaiKhoan"];
                TaiKhoan tk1 = data.TaiKhoans.FirstOrDefault(n => n.TenTK.Equals(tk));
                tk1.TrangThai = false;
                data.SaveChanges();
            }
            return View();
        }
        [HttpPost]
        public ActionResult Login(UserAdmin model)
        {
            if (ModelState.IsValid)
            {
                string mk = MaHoaMD5(model.Password);
                TaiKhoan tk = data.TaiKhoans.FirstOrDefault(p => p.TenTK.Equals(model.UserName) && p.MatKhau.Equals(mk));
                if (tk != null)
                {
                    Session["TaiKhoan"] = tk.TenTK;
                    Session["ImgAcc"] = "/img/" + tk.TenTK + ".jpg";
                    Cap_quyen(tk.TenTK);
                    tk.TrangThai = true;
                    data.SaveChanges();
                    return RedirectToAction("IndexAdmin");
                }
                else
                {
                    ModelState.AddModelError("", "mk sai");
                }
            }
            else return HttpNotFound();
            return View(model);
        }
        public void Cap_quyen(String TenTK)
        {
            var lst_role = data.CT_Role.Where(n => n.TenTK.Equals(TenTK));
            foreach (var item in data.Roles.ToList())
            {
                Session[item.RoleName] = null;
            }
            foreach (var item in lst_role)
            {
                string name = data.Roles.FirstOrDefault(n => n.IDRole == item.IDRole).RoleName;
                Session[name] = true;
            }
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterAdminModel model)
        {
            if (ModelState.IsValid)
            {
                var checktk = data.TaiKhoans.SingleOrDefault(t => t.TenTK.Equals(model.UserName));
                if (checktk != null)
                {
                    SetAlert("Tên đăng nhập đã có!", "warning");
                }
                else
                {
                    var manv = data.NhanViens.SingleOrDefault(n => n.SDT.Equals(model.SDT));
                    if (manv != null)
                    {
                        TaiKhoan tk = new TaiKhoan();
                        tk.TenTK = model.UserName;
                        tk.MatKhau = MaHoaMD5(model.Password);
                        tk.Email = model.Email;
                        tk.LoaiTK = false;
                        tk.MaNV = manv.MaNV;
                        data.TaiKhoans.Add(tk);
                        data.SaveChanges();
                        SetAlert("Tạo tài khoản thành công!", "success");
                    }
                    else
                    {
                        SetAlert("Số điện thoại không có trong danh sách nhân viên!", "warning");
                    }

                }
                return View(model);
            }
            else
            {
                return HttpNotFound();
            }
        }
        public ActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgetPassword(string Email)
        {
            var tk = data.TaiKhoans.FirstOrDefault(n => n.Email.Equals(Email));
            if (tk != null)
            {
                string content = System.IO.File.ReadAllText(Server.MapPath("~/template/ComfimForgetPasswordAdmin.html"));
                content = content.Replace("{{Email}}", Email);
                String SMS;
                Random ramdom = new Random();
                SMS = ramdom.Next(100000, 999999).ToString();
                content = content.Replace("{{SMS}}", SMS);
                Session["SMSForgetPass"] = SMS;
                Session["UserForgetPass"] = tk;
                try
                {
                    new MailHelper().SendMail(tk.Email, "Đổi mật khẩu", content);
                    return RedirectToAction("ComfimForgetPassword");
                }
                catch (Exception e)
                {
                    SetAlert("Email không tồn tại!", "error");
                    return View(Email);
                }
            }
            else
            {
                SetAlert("Email không tồn tại!", "error");
                return View(Email);
            }
            return View();
        }
        public ActionResult ComfimForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ComfimForgetPassword(string sms)
        {
            if (Session["SMSForgetPass"].Equals(sms))
            {
                return RedirectToAction("NewPassword");
            }
            SetAlert("Mã xác nhận không đúng", "error");
            return View();
        }
        public ActionResult NewPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewPassword(ForgetPass model)
        {
            if (model.Pass.Equals(model.AgainPass))
            {
                TaiKhoan tk = (TaiKhoan)Session["UserForgetPass"];
                data.TaiKhoans.FirstOrDefault(n => n.TenTK.Equals(tk.TenTK)).MatKhau = MaHoaMD5(model.Pass);
                data.SaveChanges();
                Session["successChangePass"] = true;
                SetAlert("Đổi mật khẩu thành công", "success");
            }
            else
            {
                SetAlert("Mật khẩu không khớp", "warning");
            }
            return View();
        }
        public ActionResult UserManage()
        {
            if (Session["Phân Quyền"] != null)
            {
                var lst = data.TaiKhoans.ToList();
                return View(lst);
            }
            return RedirectToAction("Error404");
        }

        public ActionResult Role(String TenTK)
        {
            var lstRole = data.CT_Role.Where(n => n.TenTK.Equals(TenTK));
            var lst = data.Roles.ToList();
            List<Role_Temp> model = new List<Role_Temp>();
            List_Role lst_role = new List_Role();
            foreach (var item in lst)
            {
                Role_Temp temp = new Role_Temp();
                temp.IDRole = item.IDRole;
                temp.RoleName = item.RoleName;
                temp.TenTk = TenTK;
                if (lstRole == null) temp.Status = false;
                else
                {
                    if (lstRole.FirstOrDefault(n => n.IDRole.Equals(item.IDRole)) != null)
                    {
                        temp.Status = true;
                    }
                    else temp.Status = false;
                }
                model.Add(temp);
            }
            lst_role.RoleList = model;
            lst_role.Name = TenTK;
            Session["lst_role"] = lst_role.RoleList;
            return View(lst_role);
        }
        public ActionResult SaveRole(int IDRole, string TenTk)
        {
            var check = data.CT_Role.FirstOrDefault(p => p.TenTK.Equals(TenTk) && p.IDRole == IDRole);
            if (check != null)
            {
                data.CT_Role.Remove(check);
            }
            else
            {
                string s = string.Format("INSERT dbo.CT_Role(TenTK,IDRole)VALUES(   '{0}',  {1} )", TenTk, IDRole);
                data.Database.ExecuteSqlCommand(s);
            }
            data.SaveChanges();
            return RedirectToAction("Role", "Admin", new { TenTk = TenTk });
        }
        public ActionResult Profile()
        {
            string tentk = (string)Session["TaiKhoan"];
            if (tentk != null)
            {
                var TK = data.TaiKhoans.FirstOrDefault(n => n.TenTK.Equals(tentk));
                NhanVien nk = data.NhanViens.FirstOrDefault(n => n.MaNV.Equals(TK.MaNV));
                Profile_Model model = new Profile_Model(nk, TK);
                return View(model);
            }
            return RedirectToAction("Error404");
        }
        public ActionResult Social()
        {
            return View();
        }
        protected void SetAlert(string mess, string type)
        {
            TempData["AlretMessage"] = mess;
            if (type == "success")
            {
                TempData["AlretType"] = "alret-success";
            }
            else if (type == "warning")
            {
                TempData["AlretType"] = "alret-warning";
            }
            else if (type == "error")
            {
                TempData["AlretType"] = "alret-danger";
            }
        }
        private string MaHoaMD5(string s)
        {
            //Tạo MD5 
            MD5 mh = MD5.Create();
            //Chuyển kiểu chuổi thành kiểu byte
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(s);
            //mã hóa chuỗi đã chuyển
            byte[] hash = mh.ComputeHash(inputBytes);
            //tạo đối tượng StringBuilder (làm việc với kiểu dữ liệu lớn)
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }
        ////----------Blog----------

        //public ActionResult IndexBlog()
        //{

        //    var listBlogCategory = data.LoaiBlogs.ToList();
        //    return View(listBlogCategory);
        //}
        //public ActionResult DetailsBlog(int id)
        //{
        //    var listBlog = data.Blogs.Where(n => n.MaLoaiBlog == id);
        //    return View(listBlog);
        //}
        //public ActionResult CreateBlog(int id)
        //{
        //    var loaiblog = data.LoaiBlogs.FirstOrDefault(n => n.MaLoaiBlog == id);
        //    Session["TenLoai"] = loaiblog.TenLoaiBlog;
        //    return View();
        //}
        //----------KHÁCH HÀNG----------
        [HttpGet]
        public ActionResult IndexCustomer()
        {
            if (Session["Khách hàng"] == null) return RedirectToAction("Error404");
            var listCustomer = data.KhachHangs.ToList();
            return View(listCustomer);
        }

        [HttpGet]
        public ActionResult CreateCustomer()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateCustomer(string tenKH, string SDT, string DiaChi, bool GioiTinh)
        {
            string s = string.Format("INSERT dbo.KhachHang(TenKH, SDT, DiaChi, GioiTinh)VALUES( N'{0}', N'{1}', N'{2}', '{3}')", tenKH, SDT, DiaChi, GioiTinh);
            data.Database.ExecuteSqlCommand(s);
            data.SaveChanges();

            return RedirectToAction("IndexCustomer");
        }

        [HttpGet]
        public ActionResult DetailsCustomer(string id)
        {
            KhachHang kh = data.KhachHangs.FirstOrDefault(r => r.MaKH == id);
            if (kh == null)
                return HttpNotFound();
            return View(kh);
        }

        [HttpGet]
        public ActionResult EditCustomer(string id)
        {
            KhachHang kh = data.KhachHangs.FirstOrDefault(r => r.MaKH == id);
            if (kh == null)
                return HttpNotFound();
            return View(kh);
        }
        [HttpPost]
        public ActionResult EditCustomer(KhachHang kh)
        {
            KhachHang dbUpdate = data.KhachHangs.FirstOrDefault(r => r.MaKH == kh.MaKH);
            if (dbUpdate != null)
            {
                dbUpdate.TenKH = kh.TenKH;
                dbUpdate.SDT = kh.SDT;
                dbUpdate.DiaChi = kh.DiaChi;
                dbUpdate.GioiTinh = kh.GioiTinh;
                data.SaveChanges();
            }

            return RedirectToAction("IndexCustomer", "Admin");
        }

        [HttpGet]
        public ActionResult DeleteCustomer(string id)
        {
            KhachHang kh = data.KhachHangs.FirstOrDefault(r => r.MaKH == id);
            if (kh != null)
            {
                data.KhachHangs.Remove(kh);
                data.SaveChanges();
            }

            return RedirectToAction("IndexCustomer", "Admin");
        }

        //----------KHÁCH NHẬN----------
        [HttpGet]
        public ActionResult IndexRecipients()
        {
            if (Session["Khách nhận"] == null) return RedirectToAction("Error404");
            var listrecipients = data.KhachNhans.ToList();
            return View(listrecipients);
        }

        [HttpGet]
        public ActionResult CreateRecipients()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateRecipients(string tenKN, string SDT, string diaChi, bool gioiTinh)
        {
            string s = string.Format("INSERT dbo.KhachNhan(TenKN,    SDT,    DiaChi,    GioiTinh)VALUES(N'{0}',     '{1}',      N'{2}',     {3}     )", tenKN, SDT, diaChi, gioiTinh);
            data.Database.ExecuteSqlCommand(s);
            data.SaveChanges();

            return RedirectToAction("IndexRecipients");
        }

        [HttpGet]
        public ActionResult DetailsRecipients(string id)
        {
            KhachNhan kn = data.KhachNhans.FirstOrDefault(r => r.MaKN == id);
            if (kn == null)
                return HttpNotFound();
            return View(kn);
        }

        [HttpGet]
        public ActionResult EditRecipients(string id)
        {
            KhachNhan kn = data.KhachNhans.FirstOrDefault(r => r.MaKN == id);
            if (kn == null)
                return HttpNotFound();
            return View(kn);
        }
        [HttpPost]
        public ActionResult EditRecipients(KhachNhan kn)
        {
            KhachNhan dbUpdate = data.KhachNhans.FirstOrDefault(r => r.MaKN == kn.MaKN);
            if (dbUpdate != null)
            {
                dbUpdate.TenKN = kn.MaKN;
                dbUpdate.SDT = kn.SDT;
                dbUpdate.DiaChi = kn.DiaChi;
                dbUpdate.GioiTinh = kn.GioiTinh;
                data.SaveChanges();
            }

            return RedirectToAction("IndexRecipients", "Admin");
        }

        [HttpGet]
        public ActionResult DeleteRecipients(string id)
        {
            KhachNhan kn = data.KhachNhans.FirstOrDefault(r => r.MaKN == id);
            if (kn != null)
            {
                data.KhachNhans.Remove(kn);
                data.SaveChanges();
            }

            return RedirectToAction("IndexRecipients", "Admin");
        }

        //----------NHÂN VIÊN----------
        [HttpGet]
        public ActionResult IndexStaff()
        {
            if (Session["Nhân viên"] == null) return RedirectToAction("Error404");
            var listStaff = data.NhanViens.ToList();
            return View(listStaff);
        }

        [HttpGet]
        public ActionResult CreateStaff()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateStaff(string tenNV, DateTime ngaySinh, string diaChi, string chucVu, float bacLuong, string maPB, string SDT, bool gioiTinh, string maNK)
        {
            string s = string.Format("INSERT dbo.NhanVien(TenNV,    NgaySinh,    DiaChi,    ChucVu,    BacLuong,    MaPB,    SDT,    GioiTinh,    MaNK)VALUES(  N'{1}',          {2},     N'{3}',         N'{4}',           {5},         '{6}',          N'{7}', '{8}',  '{9}')", tenNV, ngaySinh, diaChi, chucVu, bacLuong, maPB, SDT, gioiTinh, maNK);
            data.Database.ExecuteSqlCommand(s);
            data.SaveChanges();

            return RedirectToAction("IndexStaff");
        }

        [HttpGet]
        public ActionResult DetailsStaff(string id)
        {
            NhanVien nv = data.NhanViens.FirstOrDefault(r => r.MaNV == id);
            if (nv == null)
                return HttpNotFound();
            return View(nv);
        }

        [HttpGet]
        public ActionResult EditStaff(string id)
        {
            NhanVien nv = data.NhanViens.FirstOrDefault(r => r.MaNV == id);
            if (nv == null)
                return HttpNotFound();
            return View(nv);
        }
        [HttpPost]
        public ActionResult EditStaff(NhanVien nv)
        {
            NhanVien dbUpdate = data.NhanViens.FirstOrDefault(r => r.MaNV == nv.MaNV);
            if (dbUpdate != null)
            {
                dbUpdate.TenNV = nv.TenNV;
                dbUpdate.NgaySinh = nv.NgaySinh;
                dbUpdate.SDT = nv.SDT;
                dbUpdate.DiaChi = nv.DiaChi;
                dbUpdate.GioiTinh = nv.GioiTinh;
                dbUpdate.ChucVu = nv.ChucVu;
                dbUpdate.BacLuong = nv.BacLuong;
                dbUpdate.MaPB = nv.MaPB;
                dbUpdate.MaNK = nv.MaNK;
                data.SaveChanges();
            }

            return RedirectToAction("IndexStaff", "Admin");
        }

        [HttpGet]
        public ActionResult DeleteStaff(string id)
        {
            NhanVien nv = data.NhanViens.FirstOrDefault(r => r.MaNV == id);
            if (nv != null)
            {
                data.NhanViens.Remove(nv);
                data.SaveChanges();
            }

            return RedirectToAction("IndexStaff", "Admin");
        }

        //----------NHÀ KHO----------
        [HttpGet]
        public ActionResult IndexStoreHouse()
        {
            if (Session["Nhà kho"] == null) return RedirectToAction("Error404");
            var listStoreHouse = data.NhaKhoes.ToList();
            return View(listStoreHouse);
        }

        [HttpGet]
        public ActionResult CreateStoreHouse()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateStoreHouse(string tenNK, float dienTich, int suaChua, string diaChi, string maKV)
        {
            string s = string.Format("INSERT dbo.NhaKho(TenNK,    DienTich,    SucChua,    DiaChi,    MaKV)VALUES( N'{1}',     {2},     {3},       N'{4}',     '{5}'       )", tenNK, dienTich, suaChua, diaChi, maKV);
            data.Database.ExecuteSqlCommand(s);
            data.SaveChanges();

            return RedirectToAction("IndexStoreHouse");
        }

        [HttpGet]
        public ActionResult DetailsStoreHouse(string id)
        {
            NhaKho nk = data.NhaKhoes.FirstOrDefault(r => r.MaNK == id);
            if (nk == null)
                return HttpNotFound();
            return View(nk);
        }

        [HttpGet]
        public ActionResult EditStoreHouse(string id)
        {
            NhaKho nk = data.NhaKhoes.FirstOrDefault(r => r.MaNK == id);
            if (nk == null)
                return HttpNotFound();
            return View(nk);
        }
        [HttpPost]
        public ActionResult EditStoreHouse(NhaKho nk)
        {
            NhaKho dbUpdate = data.NhaKhoes.FirstOrDefault(r => r.MaNK == nk.MaNK);
            if (dbUpdate != null)
            {
                dbUpdate.TenNK = nk.TenNK;
                dbUpdate.DienTich = nk.DienTich;
                dbUpdate.SucChua = nk.SucChua;
                dbUpdate.DiaChi = nk.DiaChi;
                dbUpdate.MaKV = nk.MaKV;
                data.SaveChanges();
            }

            return RedirectToAction("IndexStoreHouse", "Admin");
        }

        [HttpGet]
        public ActionResult DeleteStoreHouse(string id)
        {
            NhaKho nk = data.NhaKhoes.FirstOrDefault(r => r.MaNK == id);
            if (nk != null)
            {
                data.NhaKhoes.Remove(nk);
                data.SaveChanges();
            }

            return RedirectToAction("IndexStoreHouse", "Admin");
        }

        //----------CÁC MẶT HÀNG----------
        //[HttpGet]
        //public ActionResult IndexGoods()
        //{
        //    if (Session["Hàng hóa"] == null) return RedirectToAction("Error404");
        //    var listGoods = data.HangHoas.ToList();
        //    return View(listGoods);
        //}

        //[HttpGet]
        //public ActionResult CreateGoods()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult CreateGoods(string tenHH, string moTa, string donVT, string maLHH, int giaTien)
        //{
        //    string s = string.Format("INSERT dbo.HangHoa(TenHH,    MoTa,    DonVT,    MaLHH,    GiaTien)VALUES(   '{0}',      N'{1}',     N'{2}',     N'{3}',     '{4}'        )", tenHH, moTa, donVT, maLHH, giaTien);
        //    data.Database.ExecuteSqlCommand(s);
        //    data.SaveChanges();

        //    return RedirectToAction("IndexGoods");
        //}

        //[HttpGet]
        //public ActionResult DetailsGoods(string id)
        //{
        //    HangHoa hh = data.HangHoas.FirstOrDefault(r => r.MaHH == id);
        //    if (hh == null)
        //        return HttpNotFound();
        //    return View(hh);
        //}

        //[HttpGet]
        //public ActionResult EditGoods(string id)
        //{
        //    HangHoa hh = data.HangHoas.FirstOrDefault(r => r.MaHH == id);
        //    if (hh == null)
        //        return HttpNotFound();
        //    return View(hh);
        //}
        //[HttpPost]
        //public ActionResult EditGoods(HangHoa hh)
        //{
        //    HangHoa dbUpdate = data.HangHoas.FirstOrDefault(r => r.MaHH == hh.MaHH);
        //    if (dbUpdate != null)
        //    {
        //        dbUpdate.TenHH = hh.TenHH;
        //        dbUpdate.MoTa = hh.MoTa;
        //        dbUpdate.DonVT = hh.DonVT;
        //        dbUpdate.MaLHH = hh.MaLHH;
        //        dbUpdate.GiaTien = hh.GiaTien;
        //        data.SaveChanges();
        //    }

        //    return RedirectToAction("IndexGoods", "Admin");
        //}

        //[HttpGet]
        //public ActionResult DeleteGoods(string id)
        //{
        //    HangHoa hh = data.HangHoas.FirstOrDefault(r => r.MaHH == id);
        //    if (hh != null)
        //    {
        //        data.HangHoas.Remove(hh);
        //        data.SaveChanges();
        //    }

        //    return RedirectToAction("IndexGoods", "Admin");
        //}

        //----------TUYÊN DƯƠNG----------
        [HttpGet]
        public ActionResult IndexCommend()
        {
            if (Session["Tuyến đường"] == null) return RedirectToAction("Error404");
            var listCommend = data.TuyenDuongs.ToList();
            return View(listCommend);
        }

        [HttpGet]
        public ActionResult CreateCommend()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateCommend(string tenTD, int thoiGian, string maKhoBD, string maKhoKT, string maLVC)
        {
            string s = string.Format("INSERT dbo.TuyenDuong(TenTD,    ThoiGian,    MaKhoBD,    MaKhoKT,    MaLVC)VALUES(N'{0}',     {1},       '{2}',      '{3}',      '{4}'   )", tenTD, thoiGian, maKhoBD, maKhoKT, maLVC);
            data.Database.ExecuteSqlCommand(s);
            data.SaveChanges();

            return RedirectToAction("IndexCommend");
        }

        [HttpGet]
        public ActionResult DetailsCommend(string id)
        {
            TuyenDuong td = data.TuyenDuongs.FirstOrDefault(r => r.MaTD == id);
            if (td == null)
                return HttpNotFound();
            return View(td);
        }

        [HttpGet]
        public ActionResult EditCommend(string id)
        {
            TuyenDuong td = data.TuyenDuongs.FirstOrDefault(r => r.MaTD == id);
            if (td == null)
                return HttpNotFound();
            return View(td);
        }
        [HttpPost]
        public ActionResult EditCommend(TuyenDuong td)
        {
            TuyenDuong dbUpdate = data.TuyenDuongs.FirstOrDefault(r => r.MaTD == td.MaTD);
            if (dbUpdate != null)
            {
                dbUpdate.TenTD = td.TenTD;
                dbUpdate.ThoiGian = td.ThoiGian;
                dbUpdate.MaKhoBD = td.MaKhoBD;
                dbUpdate.MaKhoKT = td.MaKhoKT;
                data.SaveChanges();
            }

            return RedirectToAction("IndexCommend", "Admin");
        }

        [HttpGet]
        public ActionResult DeleteCommend(string id)
        {
            TuyenDuong td = data.TuyenDuongs.FirstOrDefault(r => r.MaTD == id);
            if (td != null)
            {
                data.TuyenDuongs.Remove(td);
                data.SaveChanges();
            }

            return RedirectToAction("IndexCommend", "Admin");
        }

        //----------PHÒNG BAN----------
        [HttpGet]
        public ActionResult IndexDepartment()
        {
            if (Session["Phòng ban"] == null) return RedirectToAction("Error404");
            var listDepartment = data.PhongBans.ToList();
            return View(listDepartment);
        }

        [HttpGet]
        public ActionResult CreateDepartment()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateDepartment(string tenPB, string nhiemVu)
        {
            string s = string.Format("INSERT dbo.PhongBan(TenPB,    NhiemVu)VALUES( N'{0}',     N'{1}'      )", tenPB, nhiemVu);
            data.Database.ExecuteSqlCommand(s);
            data.SaveChanges();

            return RedirectToAction("IndexCommend");
        }

        [HttpGet]
        public ActionResult DetailsDepartment(string id)
        {
            PhongBan pb = data.PhongBans.FirstOrDefault(r => r.MaPB == id);
            if (pb == null)
                return HttpNotFound();
            return View(pb);
        }

        [HttpGet]
        public ActionResult EditDepartment(string id)
        {
            PhongBan pb = data.PhongBans.FirstOrDefault(r => r.MaPB == id);
            if (pb == null)
                return HttpNotFound();
            return View(pb);
        }
        [HttpPost]
        public ActionResult EditDepartment(PhongBan pb)
        {
            PhongBan dbUpdate = data.PhongBans.FirstOrDefault(r => r.MaPB == pb.MaPB);
            if (dbUpdate != null)
            {
                dbUpdate.TenPB = pb.TenPB;
                dbUpdate.NhiemVu = pb.NhiemVu;
                data.SaveChanges();
            }

            return RedirectToAction("IndexDepartment", "Admin");
        }

        [HttpGet]
        public ActionResult DeleteDepartment(string id)
        {
            PhongBan pb = data.PhongBans.FirstOrDefault(r => r.MaPB == id);
            if (pb != null)
            {
                data.PhongBans.Remove(pb);
                data.SaveChanges();
            }

            return RedirectToAction("IndexDepartment", "Admin");
        }
        //----------PHIẾU YÊU CẦU----------
        [HttpGet]
        public ActionResult IndexRequestFrom()
        {
            var listRequest = data.PhieuYeuCaus.ToList();
            return View(listRequest);
        }

        [HttpGet]
        public ActionResult CreateRequestFrom()
        {
            Session["PhieuYeuCau"] = "co";
            PhieuYeuCau pyc = data.PhieuYeuCaus.FirstOrDefault(x => x.SoPYC.Equals("PYC000001"));
            KhachHang khachHang = data.KhachHangs.FirstOrDefault(x => x.MaKH.Equals(pyc.MaKH));
            KhachNhan khachNhan = data.KhachNhans.FirstOrDefault(x => x.MaKN.Equals(pyc.MaKN));
            CreateBill createBill = new CreateBill(khachHang.TenKH, khachHang.SDT, "", "", "", khachHang.DiaChi, khachHang.GioiTinh ? "Nam" : "Nữ", khachNhan.TenKN, khachNhan.SDT, "", "", "", khachNhan.DiaChi, khachNhan.GioiTinh ? "Nam" : "Nữ", 0, true, "", "");
            Session["CreateBill"] = createBill;
            List<LoaiVanChuyen> lstLVC = data.LoaiVanChuyens.ToList();
            ViewBag.TenLVC = new SelectList(lstLVC, "MaLVC", "TenLVC");
            return View();
        }
        [HttpPost]
        public ActionResult CreateRequestFrom(DateTime ngayLapHD, long tongTien, string soPGH, string maNV, bool trangThai, string maTD)
        {
            string s = string.Format("INSERT dbo.HoaDonVanChuyen(NgayLapHD, TongTien, SoPGH, MaNV, TrangThai, MaTD)VALUES('{0}', {2}, '{3}', '{4}', '{5}')", ngayLapHD, tongTien, soPGH, maNV, trangThai, maTD);
            data.Database.ExecuteSqlCommand(s);
            data.SaveChanges();

            return RedirectToAction("IndexBill", "Admin");
        }

        [HttpGet]
        public ActionResult DetailsRequestFrom(string id)
        {
            HoaDonVanChuyen hdvc = data.HoaDonVanChuyens.FirstOrDefault(r => r.SoHD == id);
            if (hdvc == null)
                return HttpNotFound();
            return View(hdvc);
        }

        [HttpGet]
        public ActionResult EditRequestFrom(string id)
        {
            HoaDonVanChuyen hdvc = data.HoaDonVanChuyens.FirstOrDefault(r => r.SoHD == id);
            if (hdvc == null)
                return HttpNotFound();
            return View(hdvc);
        }
        [HttpPost]
        public ActionResult EditRequestFrom(HoaDonVanChuyen hdvc)
        {
            HoaDonVanChuyen dbUpdate = data.HoaDonVanChuyens.FirstOrDefault(r => r.SoHD == hdvc.SoHD);
            if (dbUpdate != null)
            {
                dbUpdate.NgayLapHD = hdvc.NgayLapHD;
                dbUpdate.TongTien = hdvc.TongTien;
                dbUpdate.MaNV = hdvc.MaNV;
                dbUpdate.TrangThai = hdvc.TrangThai;
                dbUpdate.MaTD = hdvc.MaTD;
                data.SaveChanges();
            }

            return RedirectToAction("IndexBill", "Admin");
        }

        [HttpGet]
        public ActionResult DeleteRequestFrom(string id)
        {
            HoaDonVanChuyen hdvc = data.HoaDonVanChuyens.FirstOrDefault(r => r.SoHD == id);
            if (hdvc != null)
            {
                data.HoaDonVanChuyens.Remove(hdvc);
                data.SaveChanges();
            }

            return RedirectToAction("IndexBill", "Admin");
        }
        //----------HOÁ ĐƠN VẬN CHUYỂN----------
        [HttpGet]
        public ActionResult IndexBill()
        {
            var listBill = data.HoaDonVanChuyens.ToList();
            return View(listBill);
        }

        [HttpGet]
        public ActionResult CreateBill(string id)
        {
            if (id != null)
            {
                Session["PhieuYeuCau"] = id;
                PhieuYeuCau pyc = data.PhieuYeuCaus.FirstOrDefault(x => x.SoPYC.Equals(id));
                KhachHang khachHang = data.KhachHangs.FirstOrDefault(x => x.MaKH.Equals(pyc.MaKH));
                KhachNhan khachNhan = data.KhachNhans.FirstOrDefault(x => x.MaKN.Equals(pyc.MaKN));
                CreateBill createBill = new CreateBill(khachHang.TenKH, khachHang.SDT, "", "", "", khachHang.DiaChi, khachHang.GioiTinh ? "Nam" : "Nữ", khachNhan.TenKN, khachNhan.SDT, "", "", "", khachNhan.DiaChi, khachNhan.GioiTinh ? "Nam" : "Nữ", 0, true, "", "");
                Session["CreateBill"] = createBill;
            }
            List<LoaiVanChuyen> lstLVC = data.LoaiVanChuyens.ToList();
            ViewBag.TenLVC = new SelectList(lstLVC, "MaLVC", "TenLVC");
            List<LoaiHH> lstLHH = data.LoaiHHs.ToList();
            ViewBag.LHH = new SelectList(lstLHH, "MaLHH", "TenLHH");
            return View();
        }
        [HttpPost]
        public ActionResult CreateBill(CreateBill model)
        {
            string tk = (string)Session["TaiKhoan"];
            TaiKhoan tk1 = data.TaiKhoans.FirstOrDefault(n => n.TenTK.Equals(tk));
            string s = "";
            if (Session["PhieuYeuCau"] != null)
            {
                string maPYC = (string)Session["PhieuYeuCau"];
                PhieuYeuCau pyc = data.PhieuYeuCaus.FirstOrDefault(x => x.SoPYC.Equals(maPYC));
                KhachHang khachHang = data.KhachHangs.FirstOrDefault(x => x.MaKH.Equals(pyc.MaKH));
                KhachNhan khachNhan = data.KhachNhans.FirstOrDefault(x => x.MaKN.Equals(pyc.MaKN));
                TuyenDuong tuyenduong = new TuyenDuong();
                int i = khachHang.DiaChi.IndexOf("Thành Phố");
                string address = "";
                if (i != -1)
                {
                    address = khachHang.DiaChi.Substring(i + 10, khachHang.DiaChi.Length - i - 10);
                }
                int t = khachHang.DiaChi.IndexOf("Tỉnh");
                if (t != -1)
                {
                    address = khachHang.DiaChi.Substring(t + 5, khachHang.DiaChi.Length - t - 5);
                }
                foreach (var item in data.CT_TuyenDuong.ToList())
                {
                    if (item.NhaKho.TenNK.IndexOf(address) != -1)
                        tuyenduong = item.TuyenDuong;
                }
                s = string.Format("INSERT dbo.HoaDonVanChuyen(SoHD,NgayLapHD,TongTien,TienVC,MaNV,COD,MaTD,MaLVC,MaKH,MaKN,MaTT,MaLHH)VALUES(DEFAULT,DEFAULT,{0},{1},'{2}',{3},'{4}','{5}','{6}','{7}','{8}','{9'})", 0, 0, tk1.MaNV, model.Cod ? 1 : 0, tuyenduong.MaTD, model.MaLVC, pyc.MaKH, pyc.MaKN, "TT01", model.MaLHH);
            }
            else
            {
                s = string.Format("INSERT dbo.HoaDonVanChuyen(SoHD,NgayLapHD,TongTien,TienVC,MaNV,COD,MaTD,MaLVC,MaKH,MaKN,MaTT,MaLHH)VALUES(DEFAULT,DEFAULT,{0},{1},'{2}',{3},'{4}','{5}','{6}','{7}','{8}','{9'})", 0, 0, tk1.MaNV, model.Cod ? 1 : 0, "", model.MaLVC, "", "", "TT01", model.MaLHH);
            }
            data.Database.ExecuteSqlCommand(s);
            data.SaveChanges();
            return RedirectToAction("IndexBill", "Admin");
        }

        [HttpGet]
        public ActionResult DetailsBill(string id)
        {
            HoaDonVanChuyen hdvc = data.HoaDonVanChuyens.FirstOrDefault(r => r.SoHD == id);
            if (hdvc == null)
                return HttpNotFound();
            return View(hdvc);
        }

        [HttpGet]
        public ActionResult EditBill(string id)
        {
            HoaDonVanChuyen hdvc = data.HoaDonVanChuyens.FirstOrDefault(r => r.SoHD == id);
            if (hdvc == null)
                return HttpNotFound();
            return View(hdvc);
        }
        [HttpPost]
        public ActionResult EditBill(HoaDonVanChuyen hdvc)
        {
            HoaDonVanChuyen dbUpdate = data.HoaDonVanChuyens.FirstOrDefault(r => r.SoHD == hdvc.SoHD);
            if (dbUpdate != null)
            {
                dbUpdate.NgayLapHD = hdvc.NgayLapHD;
                dbUpdate.TongTien = hdvc.TongTien;
                dbUpdate.MaNV = hdvc.MaNV;
                dbUpdate.TrangThai = hdvc.TrangThai;
                dbUpdate.MaTD = hdvc.MaTD;
                data.SaveChanges();
            }

            return RedirectToAction("IndexBill", "Admin");
        }

        [HttpGet]
        public ActionResult DeleteBill(string id)
        {
            HoaDonVanChuyen hdvc = data.HoaDonVanChuyens.FirstOrDefault(r => r.SoHD == id);
            if (hdvc != null)
            {
                data.HoaDonVanChuyens.Remove(hdvc);
                data.SaveChanges();
            }

            return RedirectToAction("IndexBill", "Admin");
        }

        public ActionResult Error404()
        {
            return View();
        }

    }
}