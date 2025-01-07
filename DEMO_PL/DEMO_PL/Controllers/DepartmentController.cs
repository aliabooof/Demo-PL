using Demo.BLL.Repositories;
using Demo.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace DEMO_PL.Controllers
{
    public class DepartmentController : Controller
    {
        private DepartmentRepository _departmentRepository;
        public IActionResult Index() // action index always direct you to the master page for the controller
        {
            return View();
        }
    }
}
