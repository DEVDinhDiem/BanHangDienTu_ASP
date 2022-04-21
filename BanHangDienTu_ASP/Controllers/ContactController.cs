using Common;
using ModelDb.DAO;
using ModelDb.EntityFramework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanHangDienTu_ASP.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index()
        {
            var model = new ContactDAO().GetActiveContact();
            return View(model);
        }
        [HttpPost]
        public JsonResult Send(string name, string mobile, string address, string email, string content)
        {
            var feedback = new Feedback();
            feedback.Name = name;
            feedback.Email = email;
            feedback.CreatedDate = DateTime.Now;
            feedback.Phone = mobile;
            feedback.Content = content;
            feedback.Address = address;
            

            var id = new ContactDAO().InsertFeedBack(feedback);
            if (id > 0)
            {
                string Feedback = System.IO.File.ReadAllText(Server.MapPath("~/assets/client/template/feedback.html"));

                Feedback = Feedback.Replace("{{CustomerName}}", name);
                Feedback = Feedback.Replace("{{Phone}}", mobile);
                Feedback = Feedback.Replace("{{Email}}", email);
                Feedback = Feedback.Replace("{{Address}}", address);
                Feedback = Feedback.Replace("{{FeedBack}}", content);
                var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

                new MailHelper().SendMail(email, "Feedback mới từ 2k1Shop", Feedback);
                new MailHelper().SendMail(toEmail, "2k1Shop cảm ơn bạn đã Feedback!", Feedback);
                return Json(new
                {
                    status = true
                });
                //send mail
               
            }

            else
                return Json(new
                {
                    status = false
                });

        }
    }
}