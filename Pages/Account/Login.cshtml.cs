using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebAppSecTraining.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Credential Credential { get; set; }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if(Credential.Username=="admin" && Credential.Password == "123")
            {
                var claim = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name,"admin"),
                    new Claim(ClaimTypes.Email,"admin@email.com"),
                    new Claim("Department","HR"),
                    new Claim("HRManager","true"),
                    new Claim("EmplymentDate","2023-02-01")
                };
                var identity = new ClaimsIdentity(claim, "myAuthCookie");
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = Credential.RememberMe
                };
              await  HttpContext.SignInAsync("myAuthCookie", principal, authProperties);
                return Redirect("/index");
            }
            return Page();
        }
    }
    public class Credential
    {
        [Required, Display(Name = "Username")]
        public string Username { get; set; }
        [Required, DataType(DataType.Password), Display(Name = "Password")]
        public string Password { get; set; }
        [Display(Name ="Remember Me")]
        public bool RememberMe { get; set; }
    }
}
