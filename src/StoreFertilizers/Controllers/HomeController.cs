using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

namespace StoreFertilizers.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Sale()
        {
            ViewData["Message"] = "หน้าขายสินค้า";

            return View();
        }

        public IActionResult Stock()
        {
            ViewData["Message"] = "หน้าคลังสินค้า";

            return View();
        }

        public IActionResult SummaryOfSale()
        {
            ViewData["Message"] = "สรุปยอดการขาย";

            return View();
        }

        public IActionResult SettingOfApplication()
        {
            ViewData["Message"] = "ตั้งค่าโปรแกรม";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
