using Demo.BLL.Interfaces;
using Demo.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DEMO_PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;

        public EmployeeController(IEmployeeRepository EmployeeRepository,IDepartmentRepository departmentRepository) 
        {
            _employeeRepository = EmployeeRepository;
            _departmentRepository = departmentRepository;
        }


        public IActionResult Index()
        {
            // Binding is One way binding in MVC
            // means send information from controller to view
            // 1. ViewData object dictionary [key,value] 
            ViewData["message"] = "Hello View Data";
            //2.viewbag --> dynamic object 
            ViewBag.Message = "Hello View Bag";


            /*This happens when ViewData and ViewBag refer to the same underlying data store.
             * In ASP.NET MVC, both ViewData and ViewBag are backed by the same dictionary (ViewDataDictionary). 
             * If you set one, both will reflect the change.*/
            var employees = _employeeRepository.GetAll();
            return View(employees);
        }
        public IActionResult Create()
        {   
            //ViewBag.Departments = _departmentRepository.GetAll();
            
            return View(); 
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid) 
            {
                _employeeRepository.Add(employee);
                return RedirectToAction(nameof(Index));
            }
            return View(employee); 
        }

        
        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (id is null)
                return BadRequest(); 

            var employee = _employeeRepository.Get(id.Value);

            if (employee is null)

                return NotFound(); 

            return View(viewName, employee);


        }
        public IActionResult Edit(int? id)
        {
            
            return Details(id, "Edit");
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int id, Employee employee)
        {
            if (id != employee.Id)
                return BadRequest();
            if (ModelState.IsValid)  
            {
                try
                {
                    _employeeRepository.Update(employee);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(employee);
        }

        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id, Employee employee)
        {
            if (id != employee.Id)
                return BadRequest();

            try
            {
                _employeeRepository.Delete(employee);
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {

              
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(employee);

        }
    }
}
