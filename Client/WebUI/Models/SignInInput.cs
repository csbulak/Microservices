using System.ComponentModel.DataAnnotations;

namespace WebUI.Models;

public class SignInInput
{
    [Display(Name = "Email Adresi")]
    public string Email { get; set; }
    [Display(Name = "Şifre")]
    public string Password { get; set; }
    [Display(Name = "Beni Hatırla")]
    public bool IsRemember { get; set; }
}