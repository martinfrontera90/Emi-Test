using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Emi.Portal.Movil.Logic.Models.Domain;
using Emi.Portal.Movil.Logic.Models.Requests;
using Emi.Portal.Movil.Logic.Models.Responses;

namespace Emi.Portal.Movil.Logic.Contracts.PageViewModels
{
    public interface ISurveyQueuingViewModel
    {
        void RemovePage();
        Task LoadPage(List<ReasonsAbandonment> reasons);
        ReasonsAbandonment ReasonSelect { get; set; }
        RequestMedicalService requestMedicalService { get; set; }
        PostPatientServiceTypeResponse patient { get; set; }
        Task SendReason(bool showModal);
        List<ReasonsAbandonment> Reasons { get; set; }
    }
}
