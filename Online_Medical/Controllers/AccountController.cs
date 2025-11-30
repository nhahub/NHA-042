using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Online_Medical.ALL_DATA;
using Online_Medical.EServices;
using Online_Medical.Models;
using Online_Medical.ViewModel;

namespace Online_Medical.Controllers
{
    public class AccountController : Controller

    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        
        private readonly IEmailSender emailSender;
        private readonly AppDbContext _context;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailSender emailSender, AppDbContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.emailSender = emailSender;
            this._context = context;
        }
        [HttpGet]

        public IActionResult Register()

        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel newUser)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = newUser.UserName;
                user.Email = newUser.Email;
                user.FirstName = newUser.FirstName;
                user.LastName = newUser.LastName;
                user.PhoneNumber = newUser.PhoneNumber;
                user.DateOfBirth = newUser.DateOfBirth;
                user.Gender = newUser.Gender;
                user.JoinDate = DateTime.Now;

                IdentityResult Results = await userManager.CreateAsync(user, newUser.Password);

                if (Results.Succeeded)
                {
                   
                    await userManager.AddToRoleAsync(user, "Patient");

                    Patient patientProfile = new Patient
                    {
                        // الحل: يجب تعيين خاصية الـ Key المشتركة 'Id'
                        Id = user.Id,

                        // تعيين الـ Navigation Property لمزيد من الوضوح
                        ApplicationUser = user

                        // ... لو عندك أي خصائص زيادة زي 'ProfileImage' ضيفيها هنا ...
                    };

                    _context.Patients.Add(patientProfile);
                    await _context.SaveChangesAsync();

                    // 2. Generate Token & Link
                    //var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    //var confirmationLink = Url.Action("ConfirmEmail", "Account",
                           //new { userId = user.Id, token = token }, Request.Scheme);

                    
                    //await emailSender.SendEmailAsync(user.Email, "Confirm Your Account",
                        //$"Welcome to Online Medical! <br> Please click the link below to confirm your account: <br> <a href='{confirmationLink}'>Confirm Account</a>");


                    //ViewBag.Message = "Registration successful! We have sent a confirmation link to your email. Please check your inbox.";
                    return View("RegistrationSuccess");
                }

              
                foreach (var error in Results.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(newUser);
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginUserViewModel loginVM)
        {
            
                if (ModelState.IsValid)
                {
                    ApplicationUser user = null;

                   
                    if (loginVM.UserName.Contains("@"))
                    {
                        user = await userManager.FindByEmailAsync(loginVM.UserName);
                    }
                    else
                    {
                        user = await userManager.FindByNameAsync(loginVM.UserName);
                    }

                    if (user != null)
                    {
                        
                        bool found = await userManager.CheckPasswordAsync(user, loginVM.Password);

                        if (found)
                        {
                            
                            await signInManager.SignInAsync(user, loginVM.RememberMe);

                    
                            var roles = await userManager.GetRolesAsync(user);

                            if (roles.Contains("Admin"))
                            {
                                //تتعدل لما نعمل admin(سبتها كدة دلوقتي)
                                return RedirectToAction("Index", "Home");
                            }
                            else if (roles.Contains("Doctor"))
                            {
                                //نفس الكلام هنا
                                return RedirectToAction("Index", "Home");
                            }
                            else
                            {
                               
                                return RedirectToAction("Index", "Home");
                            }
                        }
                    }

                  
                    ModelState.AddModelError("", "Invalid Username/Email or Password");
                }

            return View(loginVM);
        }
        [HttpPost]

        public async Task<IActionResult> Logout()
        {
         
            await signInManager.SignOutAsync();

            
            return RedirectToAction("Login");
        }
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);

               
                if (user != null)
                {
                   
                    var token = await userManager.GeneratePasswordResetTokenAsync(user);

             
                    var resetLink = Url.Action("ResetPassword", "Account",
                        new { email = model.Email, token = token }, Request.Scheme);

                   
                    await emailSender.SendEmailAsync(model.Email, "Reset Your Password",
                        $"Click the link below to reset your password: <br> <a href='{resetLink}'>Reset Password</a>");
                }

                return View("ForgotPasswordConfirmation");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string email, string token)
        {
           
            if (email == null || token == null)
            {
                return Content("Invalid password reset token.");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    
                    var result = await userManager.ResetPasswordAsync(user, model.Token, model.Password);

                    if (result.Succeeded)
                    {
                      
                        return View("ResetPasswordConfirmation");
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Email not found.");
                }
            }
            return View(model);
        }

       
        [HttpGet]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }
    } 
}