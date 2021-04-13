namespace Emi.Portal.Movil.Logic.ViewModels.Domain
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using CommonServiceLocator;
    using Emi.Portal.Movil.Logic.Contracts.Services;
    using Emi.Portal.Movil.Logic.Models.Domain;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using Xamarin.Forms.Internals;

    public class ContractedPlanCard : ViewModelBase
    {
        private BankPlans bank;
        public BankPlans Bank
        {
            get { return bank; }
            set
            {
                bank = value;
                RaisePropertyChanged();
            }
        }

        private int heightTest = 380;
        public int HeightTest
        {
            get { return heightTest; }
            set
            {
                heightTest = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<PlanBeneficiaryGroup> beneficiaries;
        public ObservableCollection<PlanBeneficiaryGroup> Beneficiaries
        {
            get { return beneficiaries; }
            set
            {
                beneficiaries = value;
                HeighBeneficiary = (Beneficiaries.Count * 60) + (Beneficiaries.Count + 10);
                RaisePropertyChanged();
            }
        }

        private PlanBeneficiaryGroup lastExpanded;

        private int height = 0;
        public int HeighBeneficiary
        {
            get { return height; }
            set
            {
                height = value;
                RaisePropertyChanged();
            }
        }

        bool beneficiaryExpanded = false;
        public bool BeneficiaryExpanded

        {
            get { return beneficiaryExpanded; }
            set
            {
                if (beneficiaryExpanded != value)
                {
                    beneficiaryExpanded = value;
                    RaisePropertyChanged();
                    if (HeighBeneficiary == 0)
                        HeighBeneficiary = (Beneficiaries.Count * 60) + (Beneficiaries.Count + 10);
                }
            }
        }

        private string bill;
        public string Bill
        {
            get { return bill; }
            set
            {
                bill = value;
                RaisePropertyChanged();
            }
        }

        private string billDate;
        public string BillDate
        {
            get { return billDate; }
            set
            {
                billDate = value;
                RaisePropertyChanged();
            }
        }

        private bool billAddress;
        public bool BillAddress
        {
            get { return billAddress; }
            set
            {
                billAddress = value;
                RaisePropertyChanged();
            }
        }

        private bool creditCard;
        public bool CreditCard
        {
            get { return creditCard; }
            set
            {
                creditCard = value;
                RaisePropertyChanged();
            }
        }

        private bool debit;
        public bool Debit
        {
            get { return debit; }
            set
            {
                debit = value;
                RaisePropertyChanged();
            }
        }

        private string fullAddress;
        public string FullAddress
        {
            get { return fullAddress; }
            set
            {
                fullAddress = value;
                RaisePropertyChanged();
            }
        }

        private string fullName;
        public string FullName
        {
            get { return fullName; }
            set
            {
                fullName = value;
                RaisePropertyChanged();
            }
        }

        private BillingPlans billing;
        public BillingPlans Billing
        {
            get { return billing; }
            set
            {
                billing = value;
                RaisePropertyChanged();
            }
        }

        private CardPlans card;
        public CardPlans Card
        {
            get { return card; }
            set
            {
                card = value;
                RaisePropertyChanged();
            }
        }

        private string consultType;
        public string ConsultType
        {
            get { return consultType; }
            set
            {
                consultType = value;
                RaisePropertyChanged();
            }
        }

        private string familyEnrollment;
        public string FamilyEnrollment
        {
            get { return familyEnrollment; }
            set
            {
                familyEnrollment = value;
                RaisePropertyChanged();
            }
        }

        private string lastName1;
        public string LastName1
        {
            get { return lastName1; }
            set
            {
                lastName1 = value;
                RaisePropertyChanged();
            }
        }

        private string lastName2;
        public string LastName2
        {
            get { return lastName2; }
            set
            {
                lastName2 = value;
                RaisePropertyChanged();
            }
        }

        private string lifeRp;
        public string LifeRp
        {
            get { return lifeRp; }
            set
            {
                lifeRp = value;
                RaisePropertyChanged();
            }
        }

        private string name1;
        public string Name1
        {
            get { return name1; }
            set
            {
                name1 = value;
                RaisePropertyChanged();
            }
        }

        private string name2;
        public string Name2
        {
            get { return name2; }
            set
            {
                name2 = value;
                RaisePropertyChanged();
            }
        }

        private PaymentAddressPlans paymentAddress;
        public PaymentAddressPlans PaymentAddress
        {
            get { return paymentAddress; }
            set
            {
                paymentAddress = value;
                RaisePropertyChanged();
            }
        }

        private string periodicity;
        public string Periodicity
        {
            get { return periodicity; }
            set
            {
                periodicity = value;
                RaisePropertyChanged();
            }
        }

        private bool showDetail;
        public bool ShowDetail
        {
            get { return showDetail; }
            set
            {
                showDetail = value;
                RaisePropertyChanged();
            }
        }

        public ICommand ExpandBeneficiaryCommand { get { return new RelayCommand(ExpandBeneficiart); } }
        public ICommand RefreshItemsCommand { get { return new RelayCommand<PlanBeneficiaryGroup>((item) => ExecuteRefreshItemsCommand(item)); } }
        public ICommand ExpandAllBeneficiariesCommand { get { return new RelayCommand(ExpandAllBeneficiaries); } }

        private void ExpandAllBeneficiaries()
        {
            ShowDetail = !ShowDetail;
            Beneficiaries.ForEach(ben => ben.IsExpanded = ShowDetail);
        }

        private void ExecuteRefreshItemsCommand(PlanBeneficiaryGroup item)
        {
            try
            {
                if (lastExpanded == item)
                {
                    item.IsExpanded = !item.IsExpanded;
                }
                else
                {
                    if (lastExpanded != null)
                    {
                        // hide previous selected item
                        lastExpanded.IsExpanded = false;
                    }
                    // show selected item
                    item.IsExpanded = true;
                }

                lastExpanded = item;
                if (Beneficiaries.All(x => !x.IsExpanded))
                {
                    HeighBeneficiary = (Beneficiaries.Count * 50);
                    HeightTest = 380;
                    return;
                }
                HeighBeneficiary = (item.Count * 100) + (item.Count * 30);
                HeightTest = 500;
            }
            catch (Exception e)
            {
                ServiceLocator.Current.GetInstance<IExceptionService>().RegisterException(e);
            }

        }

        private void ExpandBeneficiart()
        {
            BeneficiaryExpanded = !BeneficiaryExpanded;

        }

    }

}
