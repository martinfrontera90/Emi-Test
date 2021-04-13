namespace Emi.Portal.Movil.Logic.ViewModels.Domain
{
    using System.Collections.Generic;
    using System.Windows.Input;
    using Emi.Portal.Movil.Logic.Contracts.Domain;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using CommonServiceLocator;

    public class CoordinationPaymentMethodViewModel : ViewModelBase, ICoordinationPaymentMethodViewModel
    {

        private string paymentMethodCode;
        public string PaymentMethodCode
        {
            get { return paymentMethodCode; }
            set
            {
                if (paymentMethodCode != value)
                {
                    paymentMethodCode = value;
                    RaisePropertyChanged("PaymentMethodCode");
                }
            }
        }

        private string paymentMethodDescription;
        public string PaymentMethodDescription
        {
            get { return paymentMethodDescription; }
            set
            {
                if (paymentMethodDescription != value)
                {
                    paymentMethodDescription = value;
                    RaisePropertyChanged("PaymentMethodDescription");
                }
            }
        }

        private string paymentMethodName;
        public string PaymentMethodName
        {
            get { return paymentMethodName; }
            set
            {
                if (paymentMethodName != value)
                {
                    paymentMethodName = value;
                    RaisePropertyChanged("PaymentMethodName");
                }
            }
        }

        private List<int> installments;
        public List<int> Installments
        {
            get { return installments; }
            set
            {
                if (installments != value)
                {
                    installments = value;
                    RaisePropertyChanged("Installments");
                }
            }
        }

        private bool externalMethod;
        public bool ExternalMethod
        {
            get { return externalMethod; }
            set
            {
                if (externalMethod != value)
                {
                    externalMethod = value;
                    RaisePropertyChanged("ExternalMethod");
                }
            }
        }

        private string icon;
        public string Icon
        {
            get { return icon; }
            set
            {
                icon = value;
                RaisePropertyChanged("Icon");
            }
        }

        private string descriptionPaymentMethod;
        public string DescriptionPaymentMethod
        {
            get { return descriptionPaymentMethod; }
            set
            {
                if (descriptionPaymentMethod != value)
                {
                    descriptionPaymentMethod = value;
                    RaisePropertyChanged("DescriptionPaymentMethod");
                }
            }
        }

        private int installmentSelected;
        public int InstallmentSelected
        {
            get { return installmentSelected; }
            set
            {
                if (installmentSelected != value)
                {
                    installmentSelected = value;
                    RaisePropertyChanged("InstallmentSelected");
                }
            }
        }

        public ICommand CoordinationPaymentMethodSelectedCommand { get { return new RelayCommand(CoordinationPaymentMethodSelected); } }

        private void CoordinationPaymentMethodSelected()
        {
            INewMedicalCenterCoordinationPageViewModel newMedicalCenterCoordinationPageViewModel = ServiceLocator.Current.GetInstance<INewMedicalCenterCoordinationPageViewModel>();
            newMedicalCenterCoordinationPageViewModel.PaymentMethodSelected = this;
            newMedicalCenterCoordinationPageViewModel.Payment();
        }
    }
}
