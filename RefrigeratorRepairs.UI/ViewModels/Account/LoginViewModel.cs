using System.ComponentModel.DataAnnotations;

namespace RefrigeratorRepairs.UI.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "لطفا نام کاربری خود را وارد کنید.")]
        public string UserName { get; set; }

        [Required(ErrorMessage ="لطفا رمز عبور خود را وارد کنید.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
