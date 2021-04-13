namespace Emi.Portal.Movil.Logic.Helpers
{
    using System.Collections.ObjectModel;
    using System.Linq;
    using Emi.Portal.Movil.Logic.Contracts.Domain;
    using Emi.Portal.Movil.Logic.Enumerations;
    using Emi.Portal.Movil.Logic.Models.Domain;
    using Emi.Portal.Movil.Logic.Models.Responses;
    using Emi.Portal.Movil.Logic.Resources;
    using Emi.Portal.Movil.Logic.ViewModels.Domain;
    using Emi.Portal.Movil.Logic.ViewModels.Pages;
    using Emi.Portal.Movil.Logic.ViewModels.Pages.MyAccount;
    using CommonServiceLocator;
    using System;

    public static class ViewModelHelper
    {
        static double rad2deg(double rad)
        {
            return rad / Math.PI * 180.0;
        }

        static double deg2rad(double deg)
        {
            return deg * Math.PI / 180.0;
        }

        public static double Distance(double latitudeInitial, double longitudeInitial, double latitudeFinal, double longitudeFinal)
        {
            double theta = longitudeInitial - longitudeFinal;
            double distance = Math.Sin(deg2rad(latitudeInitial)) * Math.Sin(deg2rad(latitudeFinal)) + Math.Cos(deg2rad(latitudeInitial)) * Math.Cos(deg2rad(latitudeFinal)) * Math.Cos(deg2rad(theta));

            distance = Math.Acos(distance);
            distance = rad2deg(distance);
            distance = distance * 60 * 1.1515;
            distance = distance * 1.609344;

            return Math.Round(distance, 2);
        }

        internal static void SetClinicToClinicViewModel(ClinicViewModel viewModel, Clinic model, string decimalSeparator)
        {
            viewModel.Address = model.Addres;
            viewModel.Icon = "gps2";            
            viewModel.Latitude = double.Parse(model.Latitude.Replace(".", decimalSeparator));
            viewModel.Longitude = double.Parse(model.Longitude.Replace(".", decimalSeparator));
            viewModel.Name = model.Name;
            viewModel.Schedule = model.Schedule;
            viewModel.Services = model.Services;
            viewModel.Distance = model.Distance;
            viewModel.AdultTime = model.AdultTime;
            viewModel.PediatricTime = model.PediatricTime;
        }

        internal static void SetCoordinationToCoordinationViewModel(CoordinationViewModel viewModel, PendingCoordination model, string decimalSeparator)
        {
            viewModel.Address = model.MedicalCenter.Address;
            viewModel.AgendaName = model.AgendaName;
            viewModel.ClinicCode = model.MedicalCenter.ClinicCode;
            viewModel.RDACode = model.RDACode;
            viewModel.SpecialityCode = model.SpecialityCode;
            viewModel.Latitude = double.Parse(model.MedicalCenter.Latitude.Replace(".", decimalSeparator));
            viewModel.Longitude = double.Parse(model.MedicalCenter.Longitude.Replace(".", decimalSeparator));
            viewModel.Day = model.Day;
            viewModel.Document = model.Document;
            viewModel.FullAddress = string.Format("{0} - {1}", model.MedicalCenter.ClinicName, model.MedicalCenter.Address);
            viewModel.Date = model.Date;
            viewModel.Hour = model.Time;
            viewModel.ClinicName = model.MedicalCenter.ClinicName;
            viewModel.Names = model.Names;
            viewModel.NameSpecialty = model.SpecialityName;
            viewModel.TypeCoordination = model.AgendaType;
            viewModel.YearMonthDay = model.YearMonthDay;
            viewModel.Time = model.Time;
            viewModel.Recommendations = model.Recommendations;
            viewModel.Price = model.Price;
        }        

        internal static void SetPersonToPersonViewModel(PersonViewModel viewModel, Person model)
        {
            viewModel.Active = model.Active;
            viewModel.AffiliateType = model.AffiliateType;
            viewModel.Beneficiary = (model.Beneficiary != null && model.Beneficiary.ToUpper() == "SI") ? true : false;
            viewModel.CellPhone = string.IsNullOrEmpty(model.CellPhoneNumber) ? model.CellPhone : model.CellPhoneNumber;
            viewModel.Document = model.Document;
            viewModel.DocumentType = model.DocumentType;
            viewModel.DocumentTypeShort = model.DocumentTypeShort;
            viewModel.Email = model.Email;
            viewModel.FullDocument = string.Format("{0} {1}", model.DocumentTypeShort, model.Document);
            viewModel.FullNames = model.FullNames;
            viewModel.IdReference = model.IdReference;
            viewModel.IsVisiblePersonalData = AppResources.ApplicationName == "ucm";
            viewModel.Names = model.Names;
            viewModel.Phone = model.Phone;
            viewModel.Status = viewModel.Active ? AppResources.ActivePerson : AppResources.InactivePerson;
            viewModel.Surnames = model.Surnames;
        }

		internal static void SetServiceEnabledToServiceEnabledViewModel(EnabledService model, ServicesEnabledViewModel viewModel)
		{
            viewModel.Code = model.Code;
            viewModel.Description = model.Description;
            viewModel.Icon = model.IconApp;
            viewModel.Message = model.Message;
            viewModel.Name = model.Name;
            viewModel.ServiceType = model.ServiceType;
            viewModel.EstimatedTime = model.EstimatedTime;
            viewModel.EstimatedTimeText = model.ServiceType.Equals(ServiceType.Urgency) ? AppResources.EstimatedTimeZone : AppResources.EstimatedTime;
        }

		internal static void SetServiceHistoryToServiceHistoryViewModel(ServiceHistoryViewModel viewModel, ServiceHistory model)
        {
            viewModel.Address = model.Address;
            viewModel.Code = model.Code;
            viewModel.Cost = model.Cost;
            viewModel.Coordination = model.Coordination;
            viewModel.CityName = model.CityName;
            viewModel.Date = model.Date;
            viewModel.DoctorName = model.DoctorName;
            viewModel.FileCode = model.FileCode;
            viewModel.ServiceNumber = model.ServiceNumber;
            viewModel.ServiceType = model.ServiceType;
            viewModel.ServiceTypeDescription = model.ServiceTypeDescription;
            viewModel.SpecialityName = model.SpecialityName;
            viewModel.Cancelable = model.Cancelable;
            viewModel.DescriptionState = model.DescriptionState;
            viewModel.Canceled = model.Canceled;
            if (model.ServiceType == ServiceType.DoctorHome && string.IsNullOrEmpty(model.CodeState) == false)
            {
                viewModel.Canceled = (int.Parse(model.CodeState) == (int)StatusServices.InAtention || int.Parse(model.CodeState) == (int)StatusServices.Cancel) ? true : false;
            }
            viewModel.IdService = model.IdService;
            viewModel.UserName = model.UserName;
            viewModel.UserDocument = model.UserDocument;
            viewModel.UserDocumentType = model.UserDocumentType;
            viewModel.UserDocumentTypeStr = model.UserDocumentTypeStr;
        }

        internal static void SetScheduleToScheduleViewModel(ScheduleViewModel viewModel, Schedule model)
        {
            viewModel.ClinicCode = model.ClinicCode;
            viewModel.Date = model.Date;
            viewModel.DateMobile = model.DateMobile;
            viewModel.Day = model.Day;
            viewModel.Month = model.Month;            
            viewModel.Time = model.Time;
            viewModel.TimeMobile = model.TimeMobile;
            viewModel.Year = model.Year;
            viewModel.YearMonthDay = model.YearMonthDay;
        }       

        internal static void SetResponseLoginToLoginViewModel(LoginPageViewModel viewModel, ResponseLogin model)
        {
            ILoginViewModel loginViewModel = ServiceLocator.Current.GetInstance<ILoginViewModel>();
            loginViewModel.User = new ResponseLogin();
            loginViewModel.User.Access_token = model.Access_token;
            loginViewModel.User.AffiliateType = model.AffiliateType;
            loginViewModel.User.Document = model.Document;
            loginViewModel.User.DocumentType = model.DocumentType;
            loginViewModel.User.DocumentTypeName = model.DocumentTypeName;
            loginViewModel.User.Expires = model.Access_token;
            loginViewModel.User.Expires_in = model.Expires_in;
            loginViewModel.User.Issued = model.Issued;
            loginViewModel.User.LastNameOne = model.LastNameOne;
            loginViewModel.User.LastNameTwo = model.LastNameTwo;
            loginViewModel.User.Message = model.Message;
            loginViewModel.User.NameOne = model.NameOne;
            loginViewModel.User.NameTwo = model.NameTwo;
            loginViewModel.User.Success = model.Success;
            loginViewModel.User.Token_type = model.Token_type;
            loginViewModel.User.UserName = model.UserName;
            loginViewModel.User.IdReference = model.IdReference;
            loginViewModel.User.CellPhone = model.CellPhone;
            loginViewModel.User.SessionCode = model.SessionCode;
        }
        internal static void SetAffiliateToPersonalDataViewModel(PersonalDataPageViewModel viewModel, Person model)
        {
            viewModel.CellPhoneNumber = model.CellPhoneNumber;
            viewModel.DataCoveragePercentage = model.DataCoveragePercentage;            
            viewModel.Document = model.Document;
            viewModel.DocumentType = model.DocumentType;
            viewModel.Email = model.Email;
            viewModel.FirstName = model.FirstName;
            viewModel.FirstSurname = model.FirstSurname;
            viewModel.Percentage = model.DataCoveragePercentage / 100;
            viewModel.SecondName = model.SecondName;
            viewModel.SecondSurname = model.SecondSurname;
        }
        internal static void SetLoginPageViemModelToLogin(LoginPageViewModel viewModel, Login model)
        {
            model.Email = viewModel.Email;
            model.Password = viewModel.Password;
        }
        internal static void SetRegisterViemModelToRegister(RegisterPageViewModel viewModel, Register model)
        {
            model.BirthDate = viewModel.BirthDate;
            model.CellPhone = viewModel.CellPhone;
            model.CodeVerification = viewModel.CodeVerification;
            model.ConfirmPassword = viewModel.ConfirmationPassword;
            model.Email = viewModel.Email;
            model.IdDocument = viewModel.DocumentNumber;
            model.LastNameOne = viewModel.LastNameOne;
            model.LastNameTwo = viewModel.LastNameTwo;
            model.NameOne = viewModel.NameOne;
            model.NameTwo = viewModel.NameTwo;
            model.Password = viewModel.Password;
            model.TermsAndConditions = viewModel.IsTermsAndConditions;
            model.TypeDocument = viewModel.DocumentSelected.Code;
            model.UpdateEmail = viewModel.UpdateEmail;
            model.Department = viewModel.DepartamentSelected?.Code;
            model.City = viewModel.CitySelected?.Code;
            model.PhoneNumber = viewModel.Phone;
            model.Gender = viewModel.GenderSelected?.Code.ToString();
        }
        internal static void SetCoordinationPaymentMethodToCoordinationPaymentMethodViewModel(CoordinationPaymentMethod model, CoordinationPaymentMethodViewModel viewModel)
        {            
            viewModel.ExternalMethod = model.ExternalMethod;
            viewModel.Icon = model.IconApp;
            viewModel.Installments = model.Installments;
            viewModel.InstallmentSelected = model.Installments.First();
            viewModel.PaymentMethodCode = model.PaymentMethodCode;
            viewModel.PaymentMethodName = model.PaymentMethodName;
            viewModel.PaymentMethodDescription = model.PaymentMethodDescription;           
        }

        internal static void SetCategoryToCategoryViewModel(CategoryViewModel viewModel, Category model)
        {
            viewModel.Name = model.Name;
            viewModel.Subcategories = new ObservableCollection<SubcategoryViewModel>();            

            foreach (Subcategory subcategory in model.Subcategories)
            {
                SubcategoryViewModel subcategoryViewModel = new SubcategoryViewModel
                {
                    SubName = subcategory.Name,
                    Faqs = new ObservableCollection<FaqViewModel>(),
                };
                foreach (Faq faq in subcategory.Faqs)
                {
                    FaqViewModel faqViewModel = new FaqViewModel
                    {
                        AnswerText = faq.AnswerText,
                        Question = faq.Question
                    };
                    subcategoryViewModel.Faqs.Add(faqViewModel);
                }
                viewModel.Subcategories.Add(subcategoryViewModel);
            }
        }
        internal static void SetQuestionToQuestionViewModel(QuestionViewModel viewModel, Question model)
        {
            viewModel.Code = model.Code;
            viewModel.Description = model.DescriptionResponse;
            viewModel.IsTogglesYesNo = true;
            viewModel.IsVisibleOneFive = model.QuestionType == QuestionType.OneFive;
            viewModel.IsVisibleOneTen = model.QuestionType == QuestionType.OneTen;
            viewModel.IsVisibleYesNo = model.QuestionType == QuestionType.YesNo;
            viewModel.LabelYesNo = model.LabelTrue;
            viewModel.LabelTrue = model.LabelTrue;
            viewModel.LabelFalse = model.LabelFalse;
            viewModel.QuestionType = model.QuestionType;
            viewModel.Title = model.Description;
            viewModel.ValueOneFive = 1;
            viewModel.ValueOneTen = 1;            
        }
        internal static void CloneUser(ILoginViewModel loginViewModel, ResponseLogin userSaved)
        {
            loginViewModel.User.Access_token = userSaved.Access_token;
            loginViewModel.User.AffiliateType = userSaved.AffiliateType;
            loginViewModel.User.CellPhone = userSaved.CellPhone;
            loginViewModel.User.Document = userSaved.Document;
            loginViewModel.User.DocumentType = userSaved.DocumentType;
            loginViewModel.User.DocumentTypeName = userSaved.DocumentTypeName;
            loginViewModel.User.Expires = userSaved.Expires;
            loginViewModel.User.Expires_in = userSaved.Expires_in;
            loginViewModel.User.IdReference = userSaved.IdReference;
            loginViewModel.User.Issued = userSaved.Issued;
            loginViewModel.User.LastNameOne = userSaved.LastNameOne;
            loginViewModel.User.LastNameTwo = userSaved.LastNameTwo;
            loginViewModel.User.NameOne = userSaved.NameOne;
            loginViewModel.User.NameTwo = userSaved.NameTwo;
            loginViewModel.User.Token_type = userSaved.Token_type;
            loginViewModel.User.UserName = userSaved.UserName;
            loginViewModel.User.SessionCode = userSaved.SessionCode;
        }
    }
}
