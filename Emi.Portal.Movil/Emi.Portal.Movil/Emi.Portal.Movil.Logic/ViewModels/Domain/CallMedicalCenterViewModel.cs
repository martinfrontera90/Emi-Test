using System;
using System.Windows.Input;
using Emi.Portal.Movil.Logic.Contracts.Domain;
using Emi.Portal.Movil.Logic.Contracts.Services;
using Emi.Portal.Movil.Logic.Resources;
using GalaSoft.MvvmLight.Command;

namespace Emi.Portal.Movil.Logic.ViewModels.Domain
{
    public class CallMedicalCenterViewModel : ICallMedicalCenterViewModel
    {
        IPhoneService phoneService;
        public CallMedicalCenterViewModel(IPhoneService phoneService) 
        {
            this.phoneService = phoneService;
        }

        public ICommand CallMedicalCenterCommand { get { return new RelayCommand(CallMedicalCenter); }}

        private void CallMedicalCenter()
        {
            phoneService.Call(AppConfigurations.MedicalCenterLine);
        
        }
    }
}
