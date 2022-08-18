using Demo.BLL.Interfaces;
using Demo.DAL.Entities;
using Demo.BLL.Repositories;
using Microsoft.AspNetCore.Mvc;
using Demo.PL.Models;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Demo.PL.Helper;
using Microsoft.AspNetCore.Authorization;

namespace Demo.PL.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public EmployeeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<IActionResult> Index(string SearchValue)
        {
            if (string.IsNullOrEmpty(SearchValue))
            {
                var MappedEmp = mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(await unitOfWork.EmployeeRepository.GetAll());
                return View(MappedEmp);
            }
            else
            {
                var MappedEmp = mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(await unitOfWork.EmployeeRepository.SearchEmployee(SearchValue));
                return View(MappedEmp);
            }

        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Departments = await unitOfWork.DepartmentRepository.GetAll();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel employee)
        {
            if (ModelState.IsValid)
            {
                employee.ImageName = DocumentSettings.UploadFile(employee.Image, "imgs");
                var mappedEmp = mapper.Map<EmployeeViewModel, Employee>(employee);
                await unitOfWork.EmployeeRepository.Add(mappedEmp);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.Departments = await unitOfWork.DepartmentRepository.GetAll();
                return View(employee);
            }
        }

        public async Task<IActionResult> Details(int? id, string ViewName = "Details")
        {
            if (id == null)
                return NotFound();
            var Employee = mapper.Map<Employee, EmployeeViewModel>(await unitOfWork.EmployeeRepository.Get(id));
            if (Employee == null)
                return NotFound();
            return View(ViewName, Employee);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Departments =await unitOfWork.DepartmentRepository.GetAll();
            return await Details(id, "Edit");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int? id, EmployeeViewModel employee)
        {
            if (id != employee.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var mappedEmp = mapper.Map<EmployeeViewModel, Employee>(employee);
                    await unitOfWork.EmployeeRepository.Update(mappedEmp);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View(employee);
                }
            }
            ViewBag.Departments = await unitOfWork.DepartmentRepository.GetAll();
            return View(employee);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] int? id, EmployeeViewModel employee)
        {
            if (id != employee.Id)
                return BadRequest();
            try
            {
                
                var mappedEmp = mapper.Map<EmployeeViewModel, Employee>(employee);
                DocumentSettings.DeleteFile(employee.ImageName, "imgs");
                await unitOfWork.EmployeeRepository.Delete(mappedEmp);               
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(employee);
            }
        }
    }
}
