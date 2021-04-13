namespace Emi.Portal.Movil.Logic.ViewModels.Pages.MyAccount.Certificates
{
    using System;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using CommonServiceLocator;
    using Emi.Portal.Movil.Logic.Contracts.Domain;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels.MyAccount;
    using Emi.Portal.Movil.Logic.Contracts.Services;
    using Emi.Portal.Movil.Logic.Enumerations;
    using Emi.Portal.Movil.Logic.Models.Domain;
    using Emi.Portal.Movil.Logic.Models.Requests;
    using Emi.Portal.Movil.Logic.Models.Responses;
    using Emi.Portal.Movil.Logic.Resources;
    using Emi.Portal.Movil.Logic.ViewModels.Domain;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;

    public class CertificatesPageViewModel : ViewModelBase, ICertificatesPageViewModel
    {
        IApiService apiService;
        ILoginViewModel loginViewModel;
        IDialogService dialogService;
        INavigationService navigationService;
        public string GroupNames { get; set; }
        public string GroupCode { get; set; }

        private string titlePage;
        public string TitlePage
        {
            get { return titlePage; }
            set
            {
                titlePage = value;
                RaisePropertyChanged("TitlePage");
            }
        }

        private ObservableCollection<BeneficiaryCertificates> beneficiaries;
        public ObservableCollection<BeneficiaryCertificates> Beneficiaries
        {
            get { return beneficiaries; }
            set
            {
                beneficiaries = value;
                RaisePropertyChanged(nameof(Beneficiaries));
            }
        }

        private BeneficiaryCertificates beneficiarySelected;
        public BeneficiaryCertificates BeneficiarySelected
        {
            get { return beneficiarySelected; }
            set
            {
                if (beneficiarySelected != value)
                {
                    beneficiarySelected = value;
                    RaisePropertyChanged(nameof(BeneficiarySelected));
                    if (beneficiarySelected != null)
                        LoadCards();
                    GroupNames = string.Empty;
                }
            }
        }

        private ObservableCollection<CertificateCardViewModel> certificateCards;
        public ObservableCollection<CertificateCardViewModel> CertificateCards
        {
            get { return certificateCards; }
            set
            {
                certificateCards = value;
                RaisePropertyChanged(nameof(CertificateCards));
            }
        }

        private CertificateCardViewModel cardSelected;
        public CertificateCardViewModel CardSelected
        {
            get { return cardSelected; }
            set
            {
                cardSelected = value;
                RaisePropertyChanged(nameof(CardSelected));
            }
        }

        private ObservableCollection<GroupCertificate> groupCertificates;
        public ObservableCollection<GroupCertificate> GroupCertificates
        {
            get { return groupCertificates; }
            set
            {
                groupCertificates = value;
                RaisePropertyChanged(nameof(GroupCertificates));
            }
        }

        private GroupCertificate groupCertificateSelected;
        public GroupCertificate GroupCertificateSelected
        {
            get { return groupCertificateSelected; }
            set
            {
                if (groupCertificateSelected != value)
                {
                    groupCertificateSelected = value;
                    RaisePropertyChanged(nameof(GroupCertificateSelected));
                    if (groupCertificateSelected != null)
                    {
                        LoadCards();
                        GroupCode = groupCertificateSelected.GroupCode;
                        GroupNames = String.Join("\n", groupCertificateSelected.User);
                    }
                }
            }
        }

        private bool isVisibleGroup;
        public bool IsVisibleGroup
        {
            get { return isVisibleGroup; }
            set
            {
                isVisibleGroup = value;
                RaisePropertyChanged(nameof(IsVisibleGroup));
            }
        }

        public ICommand AddFamilyCommand { get { return new RelayCommand(AddFamily); } }
        public ICommand InformationCommand { get { return new RelayCommand<string>(Information); } }

        private async void Information(string option)
        {
            await dialogService.ShowMessage(AppResources.TitleCertificates, AppResources.AddFamiliyServicePage);
        }

        private void AddFamily()
        {
            navigationService.Navigate(Enumerations.AppPages.SearchFamilyPage);
        }

        public void Clean()
        {
            IsVisibleGroup = false;
            GroupCertificates = new ObservableCollection<GroupCertificate>();
            CertificateCards = new ObservableCollection<CertificateCardViewModel>();
            Beneficiaries = new ObservableCollection<BeneficiaryCertificates>();
            CertificateCards = new ObservableCollection<CertificateCardViewModel>();
            GroupCertificateSelected = null;
            CardSelected = null;
            BeneficiarySelected = null;
            GroupNames = string.Empty;
            GroupCode = string.Empty;
        }

        public async Task LoadData()
        {
            try
            {
                dialogService.ShowProgress();
                Clean();
                if (loginViewModel.User.AffiliateType == Enumerations.AffiliateType.Beneficiary)
                {
                    Beneficiaries.Add(
                        new BeneficiaryCertificates
                        {
                            Document = loginViewModel.User.Document,
                            CellPhone = loginViewModel.User.CellPhone,
                            DocumentType = loginViewModel.User.DocumentType,
                            FullNames = $"{loginViewModel.User.NameOne} {loginViewModel.User.NameTwo} {loginViewModel.User.LastNameOne} {loginViewModel.User.LastNameTwo}",
                            DocumentTypeShort = loginViewModel.User.DocumentTypeName,
                            Email = loginViewModel.User.UserName,
                        });
                }
                else
                {
                    Request request = new Request
                    {
                        Document = loginViewModel.User.Document,
                        DocumentType = loginViewModel.User.DocumentType,
                        IdReference = loginViewModel.User.IdReference,
                        Controller = "Affiliate",
                        Action = "GetUsersCertificate"
                    };
                    ResponseCertificateBeneficiaries response = new ResponseCertificateBeneficiaries();
                    response = await apiService.GetUsersCertificate(request);
                    if (response.Success)
                    {
                        Beneficiaries = new ObservableCollection<BeneficiaryCertificates>(response.Beneficiaries);
                        if (loginViewModel.User.AffiliateType == Enumerations.AffiliateType.ResponsiblePayment)
                            Beneficiaries.Add(
                                new BeneficiaryCertificates
                                {
                                    Document = loginViewModel.User.Document,
                                    DocumentType = loginViewModel.User.DocumentType,
                                    DocumentTypeShort = loginViewModel.User.DocumentTypeName,
                                    TypeCertificate = "AFILIACION",
                                    FullNames = "Grupo Familiar"
                                });
                    }
                    else
                    {
                        await dialogService.ShowMessage(response.Title, response.Message);
                    }

                }
                await navigationService.Navigate(AppPages.CertificatesPage);
            }
            catch (Exception e)
            {
                ServiceLocator.Current.GetInstance<IExceptionService>().RegisterException(e);
            }
            finally
            {
                dialogService.HideProgress();
            }
        }

        private async void LoadCards()
        {
            try
            {
                CertificateCards = new ObservableCollection<CertificateCardViewModel>();

                dialogService.ShowProgress();
                var loginUser = BeneficiarySelected.Document.Equals(loginViewModel.User.Document);
                IsVisibleGroup = BeneficiarySelected.FullNames.Equals("Grupo Familiar");
                RequestCertificates request = new RequestCertificates
                {
                    Controller = "Affiliate",
                    Action = "GetCertificates",
                    LoginUser = loginUser,
                    Document = BeneficiarySelected.Document,
                    DocumentType = BeneficiarySelected.DocumentType,
                    GroupCode = GroupCertificateSelected != null ? GroupCertificateSelected.GroupCode : string.Empty,
                    TypeCertificate = BeneficiarySelected.TypeCertificate,
                    RequestGroup = BeneficiarySelected.FullNames.Equals("Grupo Familiar"),
                    LoggedUserDocument = loginViewModel.User.Document,
                    LoggedUserDocumentType = loginViewModel.User.DocumentType
                };

                var response = await apiService.GetCertificates(request);
                if (response.Success)
                {
                    if (response.Groups != null && GroupCertificates.Count == 0)
                    {
                        GroupCertificates = new ObservableCollection<GroupCertificate>(response.Groups);
                    }
                    if (response.Certificates != null)
                    {
                        CertificateCards = new ObservableCollection<CertificateCardViewModel>();
                        foreach (var a in response.Certificates)
                        {
                            CertificateCards.Add
                            (
                                new CertificateCardViewModel
                                {
                                    CertificateName = a.CertificateName,
                                    CertificateCode = a.CertificateCode,
                                    FileName = a.FileName,
                                    TypeCertificate = a.TypeCertificate,
                                    Document = beneficiarySelected.Document,
                                    DocumentType = beneficiarySelected.DocumentType,
                                    Name = beneficiarySelected.FullNames,
                                    IsVisibleGenerate = a.CertificateCode.Equals("5"),
                                    HasDebt = !response.PeaceSafe,
                                    GroupCode = GroupCode
                                }
                            );
                        }
                    }
                }
                else
                {
                    await dialogService.ShowMessage(response.Title, response.Message);
                }
                dialogService.HideProgress();
                if (BeneficiarySelected.FullNames.Equals("Grupo Familiar") && !string.IsNullOrWhiteSpace(GroupNames))
                    await dialogService.AlertIcon($"Estos son tus familiares en {GroupCertificateSelected.GroupName}", GroupNames);
            }
            catch (Exception e)
            {
                ServiceLocator.Current.GetInstance<IExceptionService>().RegisterException(e);
            }
            finally
            {
                dialogService.HideProgress();
            }
        }

        public CertificatesPageViewModel(IApiService apiService, ILoginViewModel loginViewModel, IDialogService dialogService, INavigationService navigationService)
        {
            this.loginViewModel = loginViewModel;
            this.apiService = apiService;
            this.dialogService = dialogService;
            this.navigationService = navigationService;
        }

    }
}
