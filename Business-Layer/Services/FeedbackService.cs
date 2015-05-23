using nmct.ssa.labo.webshop.businesslayer.Services.Interfaces;
using nmct.ssa.labo.webshop.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using nmct.ssa.labo.webshop.businesslayer.Repositories.Interfaces;
using nmct.ssa.labo.webshop.businesslayer.ServiceBusHelper;
using nmct.ssa.labo.webshop.businesslayer.Mail;

namespace nmct.ssa.labo.webshop.businesslayer.Services
{
    public class FeedbackService : IFeedbackService
    {
        private IFeedbackRepository feedbackRepository = null;
        private IGenericRepository<Mode> modeRepository = null;
        private IServiceBus servicebus = null;
        private ISendGridHelper sendgridHelper = null;
        private string servicebusConnectionstring = "Microsoft.ServiceBus.ConnectionString";
        private string topic = "WebsiteMessages";
        private enum Subscriptions { Problem, Question }

        public FeedbackService(IFeedbackRepository feedbackRepository, IServiceBus servicebus, 
            IGenericRepository<Mode> modeRepository, ISendGridHelper sendgridHelper)
        {
            this.feedbackRepository = feedbackRepository;
            this.servicebus = servicebus;
            this.servicebus.Connectionstring = servicebusConnectionstring;
            this.modeRepository = modeRepository;
            this.sendgridHelper = sendgridHelper;
        }

        public List<Mode> GetAllModes()
        {
            return modeRepository.All().ToList<Mode>();
        }

        public Mode GetModeById(int id)
        {
            return modeRepository.GetItemByID(id);
        }

        public void AddPersonFormToDatabase(PersonForm personForm)
        {
            feedbackRepository.Insert(personForm);
            feedbackRepository.SaveChanges();
        }

        public void AddPersonFormToServiceBus(PersonForm personForm)
        {
            servicebus.AddToServiceBus<PersonForm>(personForm, topic);
            servicebus.AddNewSubscription(Subscriptions.Problem.ToString(), topic);
            servicebus.AddNewSubscription(Subscriptions.Question.ToString(), topic);
        }

        public List<PersonForm> GetAllQuestions()
        {
            return feedbackRepository.GetFeedbackFrom(Subscriptions.Question.ToString());
        }

        public List<PersonForm> GetAllProblems()
        {
            return feedbackRepository.GetFeedbackFrom(Subscriptions.Problem.ToString());
        }

        public void UpdatePersonForms(params int[] ids)
        {
            for (int i = 0, l = ids.Length; i < l; i++)
                GetPersonFormToUpdate(ids, i);
        }

        private void GetPersonFormToUpdate(int[] ids, int i)
        {
            PersonForm form = feedbackRepository.GetItemByID(ids[i]);
            form.Checked = true;
            feedbackRepository.Update(form);
            feedbackRepository.SaveChanges();
        }

        public PersonForm GetPersonFormById(int id)
        {
            return feedbackRepository.GetItemByID(id);
        }

        public void MailToClient(string reply, PersonForm personForm)
        {
            SendGridModel model = new SendGridModel
                (personForm.Email, "stijn.moreels@student.howest.be", personForm.Mode.Name, reply);
            sendgridHelper.SendMail(model);
        }
    }
}
