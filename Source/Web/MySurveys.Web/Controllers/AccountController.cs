namespace MySurveys.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security;
    using Models;
    using ViewModels.Account;

    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager signInManager;
        private ApplicationUserManager userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            this.UserManager = userManager;
            this.SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return this.signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }

            private set
            {
                this.signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return this.userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }

            private set
            {
                this.userManager = value;
            }
        }

        //// GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return this.View();
        }

        //// POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            var result = await this.SignInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return this.RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return this.View("Lockout");
                case SignInStatus.RequiresVerification:
                    return this.RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    this.ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return this.View(model);
            }
        }

        //// GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return this.View();
        }

        //// POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.UserName, Email = model.Email };
                var result = await this.UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await this.SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    return this.RedirectToAction("Index", "Home");
                }

                this.AddErrors(result);
            }

            //// If we got this far, something failed, redisplay form
            return this.View(model);
        }

        //// POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            this.AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return this.RedirectToAction("Index", "Home");
        }

        //// GET: /Account/Manage/Index
        public ActionResult Index(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.ChangeEmailSuccess ? "Your email has been changed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : string.Empty;

            var user = this.UserManager.FindById(User.Identity.GetUserId());

            var model = new IndexViewModel
            {
                UserName = user.UserName,
                Email = user.Email,
                CreateOn = user.CreatedOn,
                TotalSurveys = user.Surveys.Count
            };

            return this.View(model);
        }

        //// GET: /Account/Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return this.View();
        }

        //// POST: /Account/Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            var result = await this.UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await this.UserManager.FindByIdAsync(User.Identity.GetUserId());
                await this.SignInAsync(user, isPersistent: false);

                return this.RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            else
            {
                this.AddErrors(result);
            }

            return this.View(model);
        }

        //// GET: /Account/Manage/ChangeEmail
        public ActionResult ChangeEmail()
        {
            return this.View();
        }

        //// POST: /Account/Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangeEmail(ChangeEmailViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                if (this.UserManager.Users.Any(x => x.Email == model.NewEmail))
                {
                    this.AddErrors(new IdentityResult(new string[] { "This email is already registered." }));
                }

                var user = await this.UserManager.FindByIdAsync(this.User.Identity.GetUserId());

                var passwordVerificationResult = this.UserManager.PasswordHasher.VerifyHashedPassword(user.PasswordHash, model.Password);

                if (passwordVerificationResult != PasswordVerificationResult.Success)
                {
                    this.AddErrors(new IdentityResult(new string[] { "Incorrect password." }));
                }

                if (this.ModelState.IsValid)
                {
                    var result = await this.UserManager.SetEmailAsync(User.Identity.GetUserId(), model.NewEmail);

                    if (result.Succeeded)
                    {
                        return this.RedirectToAction("Index", new { Message = ManageMessageId.ChangeEmailSuccess });
                    }
                    else
                    {
                        this.AddErrors(result);
                    }
                }
            }

            return this.View(model);
        }

        #region Helpers
        //// Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.userManager != null)
                {
                    this.userManager.Dispose();
                    this.userManager = null;
                }

                if (this.signInManager != null)
                {
                    this.signInManager.Dispose();
                    this.signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        private async Task SignInAsync(User user, bool isPersistent)
        {
            this.AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            this.AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, await user.GenerateUserIdentityAsync(this.UserManager));
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error);
            }
        }

        private bool HasPassword()
        {
            var user = this.UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }

            return false;
        }

        private bool HasPhoneNumber()
        {
            var user = this.UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PhoneNumber != null;
            }

            return false;
        }

        public enum ManageMessageId
        {
            ChangeEmailSuccess,
            ChangePasswordSuccess,
            Error
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return this.Redirect(returnUrl);
            }

            return this.RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                this.LoginProvider = provider;
                this.RedirectUri = redirectUri;
                this.UserId = userId;
            }

            public string LoginProvider { get; set; }

            public string RedirectUri { get; set; }

            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = this.RedirectUri };
                if (this.UserId != null)
                {
                    properties.Dictionary[XsrfKey] = this.UserId;
                }

                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, this.LoginProvider);
            }
        }
        #endregion
    }
}