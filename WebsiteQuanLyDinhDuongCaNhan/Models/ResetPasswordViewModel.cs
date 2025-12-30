using System.ComponentModel.DataAnnotations;

public class ResetPasswordViewModel
{
    public string Token { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string NewPassword { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Compare("NewPassword", ErrorMessage = "Mật khẩu không khớp.")]
    public string ConfirmPassword { get; set; }
}
