using nmct.ssa.labo.webshop.businesslayer.Services.Interfaces;
using nmct.ssa.labo.webshop.models;
using nmct.ssa.labo.webshop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace nmct.ssa.labo.webshop.Controllers
{
    public class FeedbackController : Controller
    {
        private IFeedbackService feedbackService = null;
        private IOrderService orderService = null;
        private IUserService userService = null;

        public FeedbackController(IFeedbackService feedbackService, IOrderService orderService, IUserService userService)
        {
            this.feedbackService = feedbackService;
            this.orderService = orderService;
            this.userService = userService;
        }

        [HttpGet]
        public ViewResult Create()
        {
            ApplicationUser user = userService.GetAllUserValues(User.Identity.Name);
            PersonFormVM vm = new PersonFormVM();
            FillVMValues(user, vm);
            return View(vm);
        }

        private void FillVMValues(ApplicationUser user, PersonFormVM vm)
        {
            vm.PersonForm = new PersonForm()
            {
                Name = user.Name,
                Email = user.Email,
                Firstname = user.Fristname
            };
            vm.Modes = feedbackService.GetAllModes();
        }

        [HttpPost]
        public RedirectToRouteResult Create(PersonFormVM vm)
        {
            //vm.PersonForm.Order = orderService.GetOrderById(vm.PersonForm.OrderId);
            vm.PersonForm.Mode = feedbackService.GetModeById(vm.SelectedMode);  
            feedbackService.AddPersonFormToServiceBus(vm.PersonForm);
            return RedirectToAction("Index", "Catalog");
        }

        [HttpGet]
        public ViewResult Problems()
        {
            FeedbackVM vm = new FeedbackVM();
            vm.PersonForms = feedbackService.GetAllProblems() ?? new List<PersonForm>();
            vm.Mode = "Problems";
            return View("Feedback", vm);
        }

        [HttpGet]
        public ViewResult Questions()
        {
            FeedbackVM vm = new FeedbackVM();
            vm.PersonForms = feedbackService.GetAllQuestions() ?? new List<PersonForm>();
            vm.Mode = "Questions";
            return View("Feedback", vm);
        }

        [HttpPost]
        public RedirectToRouteResult Update(FeedbackVM vm)
        {
            feedbackService.UpdatePersonForms(vm.Seen);
            return RedirectToAction(vm.Mode);
        }

        [HttpGet]
        public ViewResult Reply(int id)
        {
            ReplyVM vm = new ReplyVM();
            vm.PersonFormId = id;
            vm.PersonForm = feedbackService.GetPersonFormById(vm.PersonFormId);
            return View(vm);
        }

        [HttpPost]
        public RedirectToRouteResult Reply(ReplyVM vm)
        {
            //MAIL TO CLIENT
            PersonForm form = feedbackService.GetPersonFormById(vm.PersonFormId);
            feedbackService.MailToClient(vm.Reply, form);
            //NOG MODE KUNNEN INVULLEN IN ONDERSTAANDE METHOD
            return RedirectToAction(vm.PersonForm.Mode.Name);
        }
    }
}