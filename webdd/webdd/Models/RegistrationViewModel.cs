using System.ComponentModel.DataAnnotations;

namespace webdd.Models
{
    

    public class RegistrationViewModel
    {

        [Required]
        [Display(Name = "暱稱")]
        public string Nickname { get; set; }

        [Required]
        [Display(Name = "帳號")]
        public string BattleTag { get; set; }

        [Required]
        [StringLength(12, ErrorMessage = "密碼最多12位")]
        [DataType(DataType.Password)]
        [Display(Name = "註冊密碼")]
        public string Password { get; set; }

        [Required]
        [Phone]
        [Display(Name = "手機號碼")]
        public string CellPhone { get; set; }

        [Required]
        [Display(Name = "圖形驗證碼")]
        public string Captcha { get; set; }
    }
}
