using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Order.Entity.Entities;
using Order.Entity.NewLayer.interfaces;
using Order.Entity.UnitOfWork;
using Order.Entity.ViewModels;
using System.Reflection.Metadata.Ecma335;

namespace Order.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRoleRepository _userRepository;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;


        public UserController(IUnitOfWork unitOfWork, IRoleRepository userRepository, SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("Login")]


       
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }

            return Ok(model);
        }
        [HttpPost("Logout")]
        public IActionResult Logout()
        {
            _signInManager.SignOutAsync();

            return Ok();
        }



       
        [HttpPost("Signup")]
        public async Task<IActionResult> Signup(SignupViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return Ok();
                }

                else
                    return BadRequest();
                
            }

            return Ok(model);
        }

        // GET: Users/Create
       

        // POST: Users/Create
        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return Ok();
                }

                else
                    return BadRequest();

            }

            return Ok(model);
        }



        // POST: Users/Edit/5
        [HttpPost("Edit")]

        public async Task<IActionResult> Edit(string id, EditUserViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(id);

                if (user == null)
                {
                    return NotFound();
                }

                user.Email = model.Email;
                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return Ok();
                }

                else
                    return BadRequest();

            }

            return Ok(model);
        }

        
        [HttpPost("Delete")]
        
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return Ok();
            }

            else
                return BadRequest();


        
        }
    }
}
