namespace Emi.Portal.Movil.Logic.ViewModels.Pages.MyAccount
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using CommonServiceLocator;
    using Emi.Portal.Movil.Logic.Contracts.Domain;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels.MyAccount;
    using Emi.Portal.Movil.Logic.Contracts.Services;
    using Emi.Portal.Movil.Logic.Models.Domain;
    using Emi.Portal.Movil.Logic.Models.Requests;
    using Emi.Portal.Movil.Logic.ViewModels.Domain;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using Xamarin.Essentials;

    public class ProductsAndPlansPageViewModel : ViewModelBase, IProductsAndPlansPageViewModel
    {
        IDialogService dialogService;
        IApiService apiService;
        ILoginViewModel loginViewModel;
        INavigationService navigationService;

        private ObservableCollection<ContractedPlanCard> beneficiaries;
        public ObservableCollection<ContractedPlanCard> Beneficiaries
        {
            get { return beneficiaries; }
            set
            {
                beneficiaries = value;
                RaisePropertyChanged();
            }
        }

        private bool hasNoProducts;
        public bool HasNoProducts
        {
            get { return hasNoProducts; }
            set
            {
                hasNoProducts = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<ContractedPlanCard> responsableCards;
        public ObservableCollection<ContractedPlanCard> ResponsableCards
        {
            get { return responsableCards; }
            set
            {
                responsableCards = value;
                RaisePropertyChanged();
            }
        }

        private int height;
        public int HeighBeneficiary
        {
            get { return height; }
            set
            {
                height = value;
                RaisePropertyChanged();
            }
        }

        private int heightCard;
        public int HeightCard
        {
            get { return heightCard; }
            set
            {
                heightCard = value;
                RaisePropertyChanged();
            }
        }




        public ICommand BuyProductTestCommand { get { return new RelayCommand(BuyProductTest); } }
        public ICommand BuyProductBeneficiaryCommand { get { return new RelayCommand(BuyProductBeneficiary); } }
        public ICommand BuyProductCommand { get { return new RelayCommand(BuyProduct); } }

        public async void LoadData()
        {
            try
            {
                Request request = new Request
                {
                    DocumentType = loginViewModel.User.DocumentType,
                    Document = loginViewModel.User.Document,
                    Controller = "PaymentGateway",
                    Action = "GetContractedPlans"
                };
                var response = await apiService.GetContractedPlans(request);

                if (response.Success)
                {
                    ResponsableCards = new ObservableCollection<ContractedPlanCard>();
                    Beneficiaries = new ObservableCollection<ContractedPlanCard>();
                    foreach (var card in response.ContractedPlans.Family)
                    {
                        if (card.ConsultType.Equals("RP"))
                        {
                            var benList = new ObservableCollection<PlanBeneficiaryGroup>();

                            foreach (var ben in card.Beneficiaries)
                            {
                                benList.Add(
                                    new PlanBeneficiaryGroup
                                    (
                                        ben.Document,
                                        ben.DocumentType,
                                        $"{ben.Name1} {ben.Name2} {ben.LastName1} {ben.LastName2}",
                                        new ObservableCollection<HiredProduct>(ben.HiredProducts)
                                    ));
                            }

                            ResponsableCards.Add(new ContractedPlanCard
                            {
                                FullName = $"{card.Name1} {card.Name2} {card.LastName1} {card.LastName2}",
                                FullAddress = $"{card.PaymentAddress.NameStreet} {card.PaymentAddress.NameCorner} {card.PaymentAddress.DoorNumber}, {card.PaymentAddress.Neighborhood} {card.PaymentAddress.City}",
                                Name1 = card.Name1,
                                Name2 = card.Name2,
                                CreditCard = card.Bill.Equals("Tarjeta de Crédito", StringComparison.InvariantCultureIgnoreCase),
                                Debit = card.Bill.Equals("Débito bancario", StringComparison.InvariantCultureIgnoreCase),
                                BillAddress = card.Bill.Equals("Cobro domiciliario", StringComparison.InvariantCultureIgnoreCase),
                                Bank = card.Bank,
                                Beneficiaries = benList,
                                BillDate = card.Billing.InvoiceDescription,
                                Bill = card.Bill,
                                Billing = card.Billing,
                                Card = card.Card,
                                ConsultType = card.ConsultType,
                                FamilyEnrollment = card.FamilyEnrollment,
                                LastName1 = card.LastName1,
                                LastName2 = card.LastName2,
                                LifeRp = card.LifeRp,
                                PaymentAddress = card.PaymentAddress,
                                Periodicity = card.Periodicity
                            });
                        }
                        else
                        {
                            var benList = new ObservableCollection<PlanBeneficiaryGroup>();

                            foreach (var ben in card.Beneficiaries)
                            {
                                benList.Add(
                                    new PlanBeneficiaryGroup
                                    (
                                        ben.Document,
                                        ben.DocumentType,
                                        $"{ben.Name1} {ben.Name2} {ben.LastName1} {ben.LastName2}",
                                        new ObservableCollection<HiredProduct>(ben.HiredProducts)
                                    ));
                            }

                            Beneficiaries.Add(new ContractedPlanCard
                            {
                                FullName = $"{card.Name1} {card.Name2} {card.LastName1} {card.LastName2}",
                                FullAddress = $"{card.PaymentAddress.NameStreet}, {card.PaymentAddress.Neighborhood} {card.PaymentAddress.State}",
                                Name1 = card.Name1,
                                Name2 = card.Name2,
                                Bank = card.Bank,
                                Beneficiaries = benList,
                                Bill = card.Bill,
                                Billing = card.Billing,
                                Card = card.Card,
                                ConsultType = card.ConsultType,
                                FamilyEnrollment = card.FamilyEnrollment,
                                LastName1 = card.LastName1,
                                LastName2 = card.LastName2,
                                LifeRp = card.LifeRp,
                                PaymentAddress = card.PaymentAddress,
                                Periodicity = card.Periodicity
                            });
                        }
                    }

                    HasNoProducts = Beneficiaries.Count < 1 && ResponsableCards.Count < 1;
                }
                else
                {
                    await dialogService.ShowMessage(response.Title, response.Message);
                    await navigationService.Back();
                }
            }
            catch (Exception e)
            {
                ServiceLocator.Current.GetInstance<IExceptionService>().RegisterException(e);
            }
        }



        private void BuyProductBeneficiary()
        {
            string url = "https://shop.ucm.com.uy/ShopOnline/affiliate/";
            string life = Beneficiaries.First().LifeRp;
            string doc = loginViewModel.User.Document;
            string docType = loginViewModel.User.DocumentType;
            string complement = $"{life}{docType}{doc}|{life.Length}1";
            Launcher.OpenAsync($"{url}{Base64Encode(complement)}");
        }

        private async void BuyProduct()
        {
            var request = new Request
            {
                Document = loginViewModel.User.Document,
                DocumentType = loginViewModel.User.DocumentType,
                Controller = "Affiliate",
                Action = "HasDebt"
            };
            var response = await apiService.GetHastDebt(request);
            if (response.Success)
            {
                if (!response.HasDebt)
                {
                    string url = "https://shop.ucm.com.uy/ShopOnline/affiliate/";
                    string life = ResponsableCards.First().LifeRp;
                    string docType = loginViewModel.User.DocumentType;
                    string doc = loginViewModel.User.Document;
                    string complement = $"{life}{docType}{doc}|{life.Length}1";
                    await Launcher.OpenAsync($"{url}{Base64Encode(complement)}");
                }
                else
                {
                    if (await dialogService.ShowConfirm("", "Para comprar un nuevo plan debes cancelar las facturas que tienes pendientes si deseas puedes pagar o chatea con uno de nuestros representantes de Servicio al Cliente.", "Pagar", "Cancelar"))
                    {
                        await navigationService.Navigate(Enumerations.AppPages.SearchInvoicesPage);
                    }
                }
            }
            else
            {
                await dialogService.ShowMessage(response.Title, response.Message);

            }
        }

        private void BuyProductTest()
        {
            string url = "https://shop.ucm.com.uy/";
            Launcher.OpenAsync(url);

        }



        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }



        public ProductsAndPlansPageViewModel(IDialogService dialogService, IApiService apiService, ILoginViewModel loginViewModel, INavigationService navigationService)
        {
            this.loginViewModel = loginViewModel;
            this.apiService = apiService;
            this.dialogService = dialogService;
            this.navigationService = navigationService;
        }
    }
}
