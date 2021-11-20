using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MyStore
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // router menu
            routes.MapRoute(
               name: "Menu",
               url: "menu",
               defaults: new { controller = "Menu", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "MyStore.Controllers" }
           );

            routes.MapRoute(
               name: "Lịch biểu",
               url: "lich-bieu",
               defaults: new { controller = "LichBieu", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "MyStore.Controllers" }
           );

            routes.MapRoute(
               name: "Footer",
               url: "footer",
               defaults: new { controller = "Footer", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "MyStore.Controllers" }
           );

            routes.MapRoute(
                name: "Chi tiết đơn hàng",
                url: "chi-tiet-don-hang/{id}",
                defaults: new { controller = "Cart", action = "ShowDetailTran", id = UrlParameter.Optional },
                namespaces: new[] { "MyStore.Controllers" }
            );

            routes.MapRoute(
                name: "Lịch sử giao dịch",
                url: "lich-su-giao-dich",
                defaults: new { controller = "Cart", action = "ShowTransactionHistory" },
                namespaces: new[] { "MyStore.Controllers" }
            );

            routes.MapRoute(
                name: "Giới Thiệu",
                url: "gioi-thieu",
                defaults: new { controller = "About", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "MyStore.Controllers" }
            );
            routes.MapRoute(
                name: "Chính Sách",
                url: "chinh-sach",
                defaults: new { controller = "Privacy", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "MyStore.Controllers" }
            );
            routes.MapRoute(
                name: "Liên Hệ",
                url: "lien-he",
                defaults: new { controller = "Contact", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "MyStore.Controllers" }
            );

            routes.MapRoute(
               name: "Tin tức",
               url: "tin-tuc",
               defaults: new { controller = "Contact", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "MyStore.Controllers" }
           );

            routes.MapRoute(
               name: "Sản Phẩm",
               url: "san-pham",
               defaults: new { controller = "Product", action = "GetProducts", id = UrlParameter.Optional },
               namespaces: new[] { "MyStore.Controllers" }
           );
            routes.MapRoute(
            name: "Chi Tiết Sản Phẩm",
            url: "chi-tiet/{metatitle}-{proID}",
            defaults: new { controller = "Product", action = "ViewDetail", id = UrlParameter.Optional },
            namespaces: new[] { "MyStore.Controllers" }
           );

            routes.MapRoute(
            name: "Sản phẩm theo thể loại",
            url: "san-pham/{MetaTitle}-{cateID}",
            defaults: new { controller = "Product", action = "Index", id = UrlParameter.Optional },
            namespaces: new[] { "MyStore.Controllers" }
            );
            routes.MapRoute(
            name: "Trang chủ",
            url: "trang-chu",
            defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
            namespaces: new[] { "MyStore.Controllers" }
            );
            routes.MapRoute(
            name: "Đăng nhập",
            url: "dang-nhap",
            defaults: new { controller = "Login", action = "Index", id = UrlParameter.Optional },
            namespaces: new[] { "MyStore.Controllers" }
            );
            routes.MapRoute(
            name: "Đăng ký",
            url: "dang-ky",
            defaults: new { controller = "Login", action = "Register", id = UrlParameter.Optional },
            namespaces: new[] { "MyStore.Controllers" }
            );
            routes.MapRoute(
            name: "Thông tin cá nhân",
            url: "thong-tin-ca-nhan",
            defaults: new { controller = "User", action = "viewProfile", id = UrlParameter.Optional },
            namespaces: new[] { "MyStore.Controllers" }
            );
            routes.MapRoute(
            name: "Giỏ hàng",
            url: "gio-hang",
            defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional },
            namespaces: new[] { "MyStore.Controllers" }
            );
            routes.MapRoute(
            name: "Thông báo",
            url: "thong-bao",
            defaults: new { controller = "Cart", action = "Message", id = UrlParameter.Optional },
            namespaces: new[] { "MyStore.Controllers" }
            );
            routes.MapRoute(
            name: "Thanh toán",
            url: "thanh-toan",
            defaults: new { controller = "Cart", action = "Payment", id = UrlParameter.Optional },
            namespaces: new[] { "MyStore.Controllers" }
            );
            routes.MapRoute(
            name: "Thành viên thanh toán",
            url: "thanh-vien-thanh-toan",
            defaults: new { controller = "Cart", action = "PaymentWithLogin", id = UrlParameter.Optional },
            namespaces: new[] { "MyStore.Controllers" }
            );

            routes.MapRoute(
            name: "Cập nhật giỏ hàng",
            url: "cap-nhat-gio-hang",
            defaults: new { controller = "Cart", action = "Update", id = UrlParameter.Optional },
            namespaces: new[] { "MyStore.Controllers" }
            );

            routes.MapRoute(
            name: "Xóa giỏ hàng",
            url: "xoa-gio-hang",
            defaults: new { controller = "Cart", action = "DeleteAll", id = UrlParameter.Optional },
            namespaces: new[] { "MyStore.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "MyStore.Controllers" }
            );
        }
    }
}
