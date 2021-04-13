using System;
using System.Collections.Generic;
using System.Text;

namespace Emi.Portal.Movil.Logic.Contracts.Views
{
    using System.Threading.Tasks;
    using System.Windows.Input;
    public interface ICurrentServiceViewViewModel
    {
        bool InProgress { get; set; }
        Task GetMedicalHomeService();
        string MessageHomeMedicalCare { get; set; }
        ICommand ScheduledServicesCommand { get; }
    }
}
