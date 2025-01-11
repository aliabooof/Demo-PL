using AutoMapper;
using Demo.BLL;
using Demo.BLL.Interfaces;
using Demo.DAL.Models;
using DEMO_PL.Helpers;
using DEMO_PL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DEMO_PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnitPOfWork _unitPOfWork;

        /*private readonly IEmployeeRepository _employeeRepository;
         private readonly IDepartmentRepository _departmentRepository;*/
        private readonly IMapper _mapper;

        public EmployeeController(IUnitPOfWork unitPOfWork/*IEmployeeRepository EmployeeRepository,IDepartmentRepository departmentRepository*/,
            IMapper mapper) 
        {
            _unitPOfWork = unitPOfWork;
            /*_employeeRepository = EmployeeRepository;
_departmentRepository = departmentRepository;*/

            _mapper = mapper;
    
        }


        public async Task<IActionResult> Index(string SearchValue)
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

            IEnumerable<Employee> employees;
            if (string.IsNullOrEmpty(SearchValue)) {
               employees = await _unitPOfWork.EmployeeRepository.GetAll();
               
            }
            else
            {
                employees = _unitPOfWork.EmployeeRepository.SearchEmployeesByName(SearchValue);         
            }
            var mappedEmps = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
            return View(mappedEmps);

        }
        public async Task<IActionResult> Create()
        {   
           /* ViewBag.Departments = _unitPOfWork.DepartmentRepository.GetAll();*/

            return View(); 
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel employeeVM)
        {
            if (ModelState.IsValid) 
            {

                // manual mapping
                /*var employee = new Employee()
                               {
                                   Name = employeeVM.Name,
                                   Address = employeeVM.Address,
                                   Age = employeeVM.Age,
                                   Email = employeeVM.Email,
                                   Salary = employeeVM.Salary,
                                   Phone = employeeVM.Phone,
                                   DepartmentId = employeeVM.DepartmentId,
                                   IsActive = employeeVM.IsActive,
                                   HireDate = employeeVM.HireDate,
                               };*/

                // impliment exciplist casting in class and write lines above 
                //Employee employee = (Employee)employeeVM;
                employeeVM.ImageName = await DocumentSettings.UploadFileAsync(employeeVM.Image, "images"); 
                var mappedEmp = _mapper.Map<EmployeeViewModel,Employee>(employeeVM);
               await _unitPOfWork.EmployeeRepository.Add(mappedEmp);
                 await _unitPOfWork.Complete();
                return RedirectToAction(nameof(Index));

            }
            return View(employeeVM); 
        } 

        
        public async Task<IActionResult> Details(int? id, string viewName = "Details")
        {
            if (id is null)
                return BadRequest(); 

            var employee = await _unitPOfWork.EmployeeRepository.Get(id.Value);

            if (employee is null)

                return NotFound(); 
            var mappedEmp = _mapper.Map<Employee, EmployeeViewModel>(employee);
            return View(viewName, mappedEmp);


        }
        public async Task<IActionResult> Edit(int? id)
        {
            
            return await Details(id, "Edit");
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] int id, EmployeeViewModel employeeVM)
        {
            if (id != employeeVM.Id)
                return BadRequest();
            if (ModelState.IsValid)  
            {
                try
                {
                    var mappedEmp= _mapper.Map<EmployeeViewModel,Employee>(employeeVM);
                    _unitPOfWork.EmployeeRepository.Update(mappedEmp);
                    await _unitPOfWork.Complete();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            return View(employeeVM);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int id, EmployeeViewModel employeeVM)
        {
            if (id != employeeVM.Id)
                return BadRequest();

            try
            {
                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                _unitPOfWork.EmployeeRepository.Delete(mappedEmp);
                int count = await _unitPOfWork.Complete();
                await DocumentSettings.DeleteFileAsync(mappedEmp.ImageName, "images");
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {

              
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(employeeVM);

        }
    }
}
