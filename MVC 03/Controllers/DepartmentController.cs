using AutoMapper;
using Company.G05.BLL.Interface;
using Company.G05.BLL.Repositories;
using Company.G05.DAL.Data.Context;
using Company.G05.DAL.Models;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_03.ViewModels;

namespace Company.G05.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        //private readonly IDepartmentRepository _departmentReposirtory;

        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public DepartmentController(//IDepartmentRepository departmentReposirtory
            IUnitOfWork unitOfWork
            ,IMapper mapper, AppDbContext context) // ASk CLR To CREATE FROM departmentReposirtory
        {
            //_departmentReposirtory = departmentReposirtory;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
            return View(departments);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DepartmentViewModel model)
        {
            if(ModelState.IsValid)
            {
                var deparment = _mapper.Map<Department>(model);
                 await _unitOfWork.DepartmentRepository.AddAsync(deparment);
                var count =  _unitOfWork.Complete();
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            
                return View(model);
           
        }

        public async Task<IActionResult> Details(int? id,string viewName = "Details")
        {
            if(id is null)
            {
                return BadRequest();//400
            }
            var department = await _unitOfWork.DepartmentRepository.GetAsync(id.Value);

            if (department is null) return NotFound();
            var dept = _mapper.Map<DepartmentViewModel>(department);
            return View(viewName,dept);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
            {
                return BadRequest();
            }
            var department = await _unitOfWork.DepartmentRepository.GetAsync(id.Value);
            if (department is null) return NotFound();
            //return View(department);
            return await Details(id,"Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute]int? id ,DepartmentViewModel model)
        {
            try
            {
                var department = _mapper.Map<Department>(model);
                if (id != model.Id) return BadRequest();
                if (ModelState.IsValid)
                {
                     _unitOfWork.DepartmentRepository.Update(department);
                    var count =  _unitOfWork.Complete();
                    if (count > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty,ex.Message);  
            }

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var department = await _unitOfWork.DepartmentRepository.GetAsync(id);
            var departmentView = _mapper.Map<DepartmentViewModel>(department);

            //return View(departmentView);
            return await Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int? id, DepartmentViewModel model)
        {
            try
            {
                if (id != model.Id) return BadRequest();
                if (ModelState.IsValid)
                {
                    var department = _mapper.Map<Department>(model);
                     _unitOfWork.DepartmentRepository.Delete(department);
                    var count = _unitOfWork.Complete();
                    
                    if (count > 0)
                    {
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
     
    

