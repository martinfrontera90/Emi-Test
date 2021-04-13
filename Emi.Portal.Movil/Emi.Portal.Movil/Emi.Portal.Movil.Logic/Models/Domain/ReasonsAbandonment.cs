using System;
using System.Windows.Input;
using CommonServiceLocator;
using Emi.Portal.Movil.Logic.Contracts.PageViewModels;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace Emi.Portal.Movil.Logic.Models.Domain
{
    public class ReasonsAbandonment : ViewModelBase
    {
        public string Type { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Comment { get; set; }

        bool isVisible;
        public bool IsVisible
        {
            get { return isVisible; }
            set
            {
                if (isVisible != value)
                {
                    isVisible = value;
                    RaisePropertyChanged();
                }
            }
        }

        string errorOther;
        public string ErrorOther
        {
            get { return errorOther; }
            set
            {
                if (errorOther != value)
                {
                    errorOther = value;
                    RaisePropertyChanged();
                }
            }
        }

        public ICommand SelectCommand { get { return new RelayCommand(Select); } }
        private void Select()
        {
            ISurveyQueuingViewModel surveyQueuing = ServiceLocator.Current.GetInstance<ISurveyQueuingViewModel>();
            surveyQueuing.ReasonSelect = this;
        }
    }
}
