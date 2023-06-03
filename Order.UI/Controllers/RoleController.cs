using Microsoft.AspNetCore.Mvc;
using Order.Entity.Entities;
using Order.Entity.NewLayer.interfaces;
using Order.Entity.Repositories;
using Order.Entity.UnitOfWork;
using Order.Entity.ViewModels;

namespace Role.Entity.NewLayer;

public class RoleController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRoleRepository _roleRepository;


    public RoleController(IUnitOfWork unitOfWork, IRoleRepository roleRepository)
    {
       
        _unitOfWork = unitOfWork;
        _roleRepository = roleRepository;

    }

    public IActionResult Index()
    {
        var roles = _unitOfWork.Roles.GetAll();
        var roleViewModels = roles.Select(r => new RoleViewModel
        {
            Id = r.Id,
            Name = r.Name
        }).ToList();

        return View(roleViewModels);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(RoleViewModel model)
    {
        if (ModelState.IsValid)
        {
            var role = new Order.Entity.Entities.Role
            {
                Name = model.Name
            };

            _unitOfWork.Roles.Add(role);
            _unitOfWork.Commit();

            return RedirectToAction("Index");
        }

        return View(model);
    }

    public IActionResult Edit(int id)
    {
        var role = _unitOfWork.Roles.GetById(id);

        if (role == null)
        {
            return NotFound();
        }

        var roleViewModel = new RoleViewModel
        {
            Id = role.Id,
            Name = role.Name
        };

        return View(roleViewModel);
    }

    [HttpPost]
    public IActionResult Edit(RoleViewModel model)
    {
        if (ModelState.IsValid)
        {
            var role = _unitOfWork.Roles.GetById(model.Id);

            if (role == null)
            {
                return NotFound();
            }

            role.Name = model.Name;

            _unitOfWork.Roles.Update(role);

            return RedirectToAction("Index");
        }

        return View(model);
    }

    public IActionResult Delete(int id)
    {
        var role = _unitOfWork.Roles.GetById(id);

        if (role == null)
        {
            return NotFound();
        }

        var roleViewModel = new RoleViewModel
        {
            Id = role.Id,
            Name = role.Name
        };

        return View(roleViewModel);
    }

    [HttpPost]
    public IActionResult DeleteConfirmed(int id)
    {
        var role = _unitOfWork.Roles.GetById(id);

        if (role == null)
        {
            return NotFound();
        }

        _unitOfWork.Roles.Remove(role);

        return RedirectToAction("Index");
    }

    public IActionResult Details(int id)
    {
        var role = _unitOfWork.Roles.GetById(id);

        if (role == null)
        {
            return NotFound();
        }

        var roleViewModel = new RoleViewModel
        {
            Id = role.Id,
            Name = role.Name
        };

        return View(roleViewModel);
    }
}
