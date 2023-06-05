using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage="이메일주소를 입력하세요")]
        [EmailAddress]
        [DisplayName("이메일주소")]
        public string Email { get; set; }

        [Required(ErrorMessage = "패스워드를 입력하세요")]
        [DataType(DataType.Password)]
        [DisplayName("패스워드")]
        public string Password { get; set; }

        [DisplayName("메일주소기억")]
        public bool RememberMe { get; set; }
    }
}
