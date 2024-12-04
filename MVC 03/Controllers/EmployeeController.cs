using AutoMapper;
using Company.G05.BLL.Interface;
using Company.G05.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC_03.Helper;
using MVC_03.ViewModels;
using System.Collections.ObjectModel;

namespace MVC_03.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        //private readonly IEmployeeRepository _employeeRepository;
        //private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeController(
            //IEmployeeRepository employeeRepository,
            //IDepartmentRepository departmentRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            //_employeeRepository = employeeRepository;
            //_departmentRepository = departmentRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public  async Task<IActionResult> Index(string SearchInput)
        {
            var employees = Enumerable.Empty<Employee>();
            if (string.IsNullOrEmpty(SearchInput))
            {
                 employees = await _unitOfWork.EmployeeRepository.GetAllAsync();
            }
            else
            {
                employees = await _unitOfWork.EmployeeRepository.GetByNameAsync(SearchInput);
            }
            //Auto Mapping
            var Result = _mapper.Map<IEnumerable<EmployeeViewModel>>(employees);
            //string Message = "Hello World";
            // View Dictionary : Transfer Data From Action To View [One Way]

            // 1. ViewData : Property Inherit From Controller - Dictionary

            //ViewData["Message"] = Message + "From View Data";

            //// 2. ViewBag : Property Inherit From Controller - Dynamic
             
            //ViewBag.Bag = Message + "From View Bag";

            //// 3. TempData: Property Inherit From Controller - Dictionary

            //TempData["Message01"] = Message + "From TempData";
            return View(Result);
        }
        [HttpGet]

        public async Task<IActionResult> Create() 
        { 
            var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
            // Views Dictionary
            // 1. ViewData
            ViewData["Departments"] = departments;
            // 2. ViewBag
            // 3. TempData
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(EmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                if(model.Image is not null)
                {
                    model.ImageName = DocumentSettings.Upload(model.Image, "images");
                }
                //Castion : EmployeeViewModel to Employee
                // Mapping 
                //1. Manual Mapping
                //Employee employee = new Employee()
                //{
                //   Name = model.Name,
                //   Age = model.Age,
                //   Address = model.Address,
                //   Salary = model.Salary,
                //   PhoneNumber = model.PhoneNumber,
                //   Email = model.Email,
                //   IsActive = model.IsActive,
                //   HiringDate = model.HiringDate,
                //   WorkForId = model.WorkForId,
                //   WorkFor = model.WorkFor,
                //   DateOfCreation = model.DateOfCreation,

                //};
                // AutoMaping
                var employee = _mapper.Map<Employee>(model);
                await _unitOfWork.EmployeeRepository.AddAsync(employee);
                var count =  _unitOfWork.Complete();
                if(count > 0) 
                {
                    TempData["Message"] = "Employee Is Created Successfully";
                }
                else
                {
                    TempData["Message"] = "Employee Is Created Successfully";
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        [HttpGet]
        public  async Task<IActionResult> Details(int? id , string viewName = "Details") 
        {
            if (id is null) return BadRequest();
            var employee = await _unitOfWork.EmployeeRepository.GetAsync(id.Value);
            if (employee is null) return NotFound();
            var emp = _mapper.Map<EmployeeViewModel>(employee);
            return View(viewName, emp);
        }

        [HttpGet]

        public async Task<IActionResult> Edit(int? id) 
        {
            var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
            // Views Dictionary
            // 1. ViewData
            ViewData["Departments"] = departments;
            // 2. ViewBag
            // 3. TempData
            return await Details(id, "Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit([FromRoute] int? id,EmployeeViewModel model)
        {
            try
            {
                if(model.ImageName is not null)
                {
                    DocumentSettings.Delete(model.ImageName,"images");
                }

                if(model.Image is not null)
                {
                  model.ImageName = DocumentSettings.Upload(model.Image, "images");
                }
                //Manual Mapping
                //Employee employee = new Employee()
                //{
                //    Name = model.Name,
                //    Age = model.Age,
                //    Address = model.Address,
                //    Salary = model.Salary,
                //    PhoneNumber = model.PhoneNumber,
                //    Email = model.Email,
                //    IsActive = model.IsActive,
                //    HiringDate = model.HiringDate,
                //    WorkForId = model.WorkForId,
                //    WorkFor = model.WorkFor,
                //    DateOfCreation = model.DateOfCreation,

                //};
                // Auto Mapper
                var employee = _mapper.Map<Employee>(model);
                if (id != model.Id) return BadRequest();
                if (ModelState.IsValid)
                {
                     _unitOfWork.EmployeeRepository.Update(employee);
                    var count =  _unitOfWork.Complete();
                    if (count > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            catch(Exception ex) 
            {
                ModelState.AddModelError(string.Empty,ex.Message);
            }
            return View(model);
        }
        [HttpGet]

        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }
        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] int? id, EmployeeViewModel model)
        {
            try
            {
                //Manual Mapping
                //Employee employee = new Employee()
                //{
                //    Name = model.Name,
                //    Age = model.Age,
                //    Address = model.Address,
                //    Salary = model.Salary,
                //    PhoneNumber = model.PhoneNumber,
                //    Email = model.Email,
                //    IsActive = model.IsActive,
                //    HiringDate = model.HiringDate,
                //    WorkForId = model.WorkForId,
                //    WorkFor = model.WorkFor,
                //    DateOfCreation = model.DateOfCreation,

                //};
                // Auto Mapper
                var employee = _mapper.Map<Employee>(model);
                if (id != model.Id) return BadRequest();
                if (ModelState.IsValid)
                {
                     _unitOfWork.EmployeeRepository.Delete(employee);
                    var count = _unitOfWork.Complete();


                    if (count > 0)
                    {
                        if (model.ImageName is not null)
                        {
                            DocumentSettings.Delete(model.ImageName, "images");
                        }
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(model);
        }
    }
}
