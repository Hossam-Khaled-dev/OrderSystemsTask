using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order.Entity.NewLayer.interfaces;
using Order.Entity.UnitOfWork;
using Order.Entity.ViewModels;

namespace Order.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IRoleRepository _roleRepository;


        public RoleController(IUnitOfWork unitOfWork, IRoleRepository roleRepository)
        {

            _unitOfWork = unitOfWork;
            _roleRepository = roleRepository;

        }

        [HttpGet("GetAll")]

        public IActionResult GetAll()
        {
            var roles = _unitOfWork.Roles.GetAll();
            var roleViewModels = roles.Select(r => new RoleViewModel
            {
                Id = r.Id,
                Name = r.Name
            }).ToList();

            return Ok(roleViewModels);
        }



        [HttpPost("Create")]
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

            }

            return Ok(model);
        }


        [HttpPut("Edit")]
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

            }

            return Ok(model);
        }


        [HttpDelete("Delete")]
        public IActionResult Delete(int id)
        {
            var role = _unitOfWork.Roles.GetById(id);

            if (role == null)
            {
                return NotFound();
            }

            _unitOfWork.Roles.Remove(role);
            _unitOfWork.Commit(); // Save changes

            return Ok();
        }

    }
}
