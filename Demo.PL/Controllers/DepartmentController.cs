using AutoMapper;
using Demo.BLL.Interfaces;
using Demo.DAL.Entities;
using Demo.PL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public DepartmentController(IUnitOfWork unitOfWork , IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var MappedDepart = mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(await unitOfWork.DepartmentRepository.GetAll());
            return View(MappedDepart);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(DepartmentViewModel department)
        {
            if(ModelState.IsValid)
            {
                var MappedDepart = mapper.Map<DepartmentViewModel, Department>(department);
                await unitOfWork.DepartmentRepository.Add(MappedDepart);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(department);
            }
        }

        public async Task<IActionResult> Details(int? id , string ViewName = "Details")
        {
            if (id == null)
                return NotFound();
            var Department = mapper.Map<Department , DepartmentViewModel>(await unitOfWork.DepartmentRepository.Get(id));
            if (Department == null)
                return NotFound();
            return View(ViewName , Department);
        }

        public async Task<IActionResult> Edit(int? id )
        {
            return await Details(id, "Edit");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute]int? id , DepartmentViewModel department)
        {
            if(id != department.Id)
                return BadRequest();
            if(ModelState.IsValid)
            {
                try
                {
                    var mappedDepart = mapper.Map<DepartmentViewModel, Department>(department);
                    await unitOfWork.DepartmentRepository.Update(mappedDepart);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View(department);
                }
            }
            return View(department);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute]int? id , DepartmentViewModel department)
        {
            if (id != department.Id)
                return BadRequest();
            try
            {
                var mappedDepart = mapper.Map<DepartmentViewModel, Department>(department);
                await unitOfWork.DepartmentRepository.Delete(mappedDepart);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(department);
            }
        }
    }
}
