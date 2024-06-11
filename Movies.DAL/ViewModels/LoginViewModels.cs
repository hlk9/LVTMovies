using System.ComponentModel.DataAnnotations.Schema;

namespace Movies.DAL.ViewModel
{
    public class LoginViewModels
    {
        public Guid? Id { get; set; } = Guid.NewGuid();
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
         public string? RoleName { get; set; }
    }
}
