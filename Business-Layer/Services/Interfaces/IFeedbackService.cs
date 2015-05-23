using nmct.ssa.labo.webshop.models;
using System;
using System.Collections.Generic;
namespace nmct.ssa.labo.webshop.businesslayer.Services.Interfaces
{
    public interface IFeedbackService
    {
        List<Mode> GetAllModes(); 
        Mode GetModeById(int id);
        void AddPersonFormToDatabase(PersonForm personForm);
        void AddPersonFormToServiceBus(PersonForm personForm);
        List<PersonForm> GetAllQuestions();
        List<PersonForm> GetAllProblems();
        void UpdatePersonForms(params int[] ids);
        PersonForm GetPersonFormById(int id);
        void MailToClient(string reply, PersonForm personForm);
    }
}
