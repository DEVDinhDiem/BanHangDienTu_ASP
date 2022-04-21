using BanHangDienTu_ASP.Common;
using BanHangDienTu_ASP.Models;
using ModelDb.DAO;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using System.Web.UI;

namespace BanHangDienTu_ASP.Controllers
{
    public class HomeController : Controller
    {
      
        // get: home
        public ActionResult Index()
        {
            ViewBag.Slides = new SlideDAO().ListAll();
            var productDAO = new ProductDAO();
            ViewBag.NewProducts = productDAO.ListNewProduct(4);
            ViewBag.ListFeatureProducts = productDAO.ListFeatureProduct(4);
            ViewBag.ListNewProductIphone = productDAO.ListNewProductIphone(4);

            // set seo title
            ViewBag.Title = ConfigurationManager.AppSettings["HomeTitle"];
            ViewBag.Keywords = ConfigurationManager.AppSettings["HomeKeyword"];
            ViewBag.Descriptions = ConfigurationManager.AppSettings["HomeDescriptions"];
            return View();
        }

        [ChildActionOnly]
        [OutputCache(Duration =3600*24)]
        public ActionResult MainMenu()
        {
            var model = new MenuDAO().ListByGroupId(1);
                return PartialView(model);
        }
        [ChildActionOnly]
        [OutputCache(Duration = 3600 * 24)]
        public ActionResult TopMenu()
        {
            var model = new MenuDAO().ListByGroupId(2);
            return PartialView(model);
        }
        [ChildActionOnly]
        [OutputCache(Duration = 3600 * 24)]
        public ActionResult Footer()
        {
            var model = new FooterDAO().GetFooter();
            return PartialView(model);
        }
        [ChildActionOnly]
        public PartialViewResult HeaderCart()
        {
            var cart = Session[CommonConstants.CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return PartialView(list);
        }
    }
}