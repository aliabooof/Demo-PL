using Demo.BLL.Repositories;
using Demo.DAL.Models;
using Demo.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics.Eventing.Reader;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading;
using System.Threading.Tasks;

namespace DEMO_PL.Controllers
{
    // Inheritance : DepartmentController  is a Controller
    // Aggregation : DepartmentController  has a  DepartmentRepository 
    public class DepartmentController : Controller
    {
        //private DepartmentRepository _departmentRepository;
        // i used IDepartmentRepository in development cuz i will use DepartmentRepository
        // and in testing i will use MockDepartmentRepository
        // the two depend on IDepartmentRepository so it will be the same code for develep and testing

        private readonly IDepartmentRepository _departmentRepository;
        private readonly IUnitPOfWork _unitOfWork;

        //public IDepartmentRepository DepartmentRepository { get; }

        public DepartmentController( IUnitPOfWork unitOfWork) // ask clr to create onstance of class implements IDepartmentRepository 
        {
           
            _unitOfWork = unitOfWork;
        }

        // public non static func 
        // to execute it i have to  create instance of the class 
        // it will execute ctor 
        // initialize _departmentRepository
        public async Task<IActionResult> Index() // action index always direct you to the master page for the controller
        {
            var departments = await _unitOfWork.DepartmentRepository.GetAll();

            //return View(); // returns view whose name is the same name as the action method here it will return Index from file views
            //return View("ali"); // returns view whose name alifrom file views
            return View(departments); // send model to the view and the view name will be index c
                                      // return View("ali",departments); // send model wit hte view and the view name will be index c
        }
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Department department)
        {
            if (ModelState.IsValid)  // server side validation
            {
           await  _unitOfWork.DepartmentRepository.Add(department);
             int count = await _unitOfWork.Complete();

                // 3. Temp data
                if (count > 0)
                    TempData["Message"] = "department is created";
                return RedirectToAction(nameof(Index));
            }
            return View(department); // return with same data 
        }

        // department/details/id
        // department/details make id nullable
        public async Task<IActionResult> Details(int? id,string viewName = "Details")
        {
            if (id is null)
                return BadRequest(); // response 400

            var department = await _unitOfWork.DepartmentRepository.Get(id.Value);

            if (department is null)

                return NotFound(); // return 404

            return View(viewName,department);


        }
        public async Task<IActionResult> Edit(int? id)
        {
            /* if (id is null)
                 return BadRequest(); // response 400
             var department = _departmentRepository.Get(id.Value);

             if (department is null)

                 return NotFound(); // return 404

             return View(department);*/
            return await Details(id,"Edit");
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute]int id,Department department )
        {
            if (id != department.Id)
                return BadRequest();
            if (ModelState.IsValid)  // server side validation
            {
                try
                {
                    _unitOfWork.DepartmentRepository.Update(department);
                   await _unitOfWork.Complete();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // 1. log exception
                    // 2. friendly message
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(department);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int id,Department department)
        {
            if (id != department.Id)
                return BadRequest();
         
            try
            {
                _unitOfWork.DepartmentRepository.Delete(department);
                await _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex )
            {

                // 1. log exception
                // 2. friendly message
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(department);

        }
    }
}
