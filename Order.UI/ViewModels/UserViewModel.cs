using System.ComponentModel.DataAnnotations;

namespace Order.Entity.ViewModels
{
    public class UserViewModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public class EditUserViewModel
    {
        public string Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

      
    }

    // DeleteUserViewModel.cs
    public class DeleteUserViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
    }

    public class CreateUserViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
