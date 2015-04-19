using System;
namespace nmct.ssa.labo.webshop.Controllers
{
    public interface IAccountController
    {
        System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> ConfirmEmail(string userId, string code);
        System.Web.Mvc.ActionResult ExternalLogin(string provider, string returnUrl);
        System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> ExternalLoginCallback(string returnUrl);
        System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> ExternalLoginConfirmation(nmct.ssa.labo.webshop.Models.ExternalLoginConfirmationViewModel model, string returnUrl);
        System.Web.Mvc.ActionResult ExternalLoginFailure();
        System.Web.Mvc.ActionResult ForgotPassword();
        System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> ForgotPassword(nmct.ssa.labo.webshop.Models.ForgotPasswordViewModel model);
        System.Web.Mvc.ActionResult ForgotPasswordConfirmation();
        System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> Login(nmct.ssa.labo.webshop.Models.LoginViewModel model, string returnUrl);
        System.Web.Mvc.ActionResult Login(string returnUrl);
        System.Web.Mvc.ActionResult LogOff();
        System.Web.Mvc.ActionResult Register();
        System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> Register(nmct.ssa.labo.webshop.Models.RegisterViewModel model);
        System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> ResetPassword(nmct.ssa.labo.webshop.Models.ResetPasswordViewModel model);
        System.Web.Mvc.ActionResult ResetPassword(string code);
        System.Web.Mvc.ActionResult ResetPasswordConfirmation();
        System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> SendCode(nmct.ssa.labo.webshop.Models.SendCodeViewModel model);
        System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> SendCode(string returnUrl, bool rememberMe);
        nmct.ssa.labo.webshop.ApplicationSignInManager SignInManager { get; }
        nmct.ssa.labo.webshop.ApplicationUserManager UserManager { get; }
        System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> VerifyCode(nmct.ssa.labo.webshop.Models.VerifyCodeViewModel model);
        System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe);
    }
}
