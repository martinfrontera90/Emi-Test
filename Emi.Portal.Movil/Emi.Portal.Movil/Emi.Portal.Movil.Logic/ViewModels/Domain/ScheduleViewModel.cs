namespace Emi.Portal.Movil.Logic.ViewModels.Domain
{
    using System.Windows.Input;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using CommonServiceLocator;

    public class ScheduleViewModel : ViewModelBase
    {
        private int year;
        public int Year
        {
            get { return year; }
            set
            {
                if (year != value)
                {
                    year = value;
                    RaisePropertyChanged("Year");
                }
            }
        }

        private int month;
        public int Month
        {
            get { return month; }
            set
            {
                if (month != value)
                {
                    month = value;
                    RaisePropertyChanged("Month");
                }
            }
        }

        private int day;
        public int Day
        {
            get { return day; }
            set
            {
                if (day != value)
                {
                    day = value;
                    RaisePropertyChanged("Day");
                }
            }
        }

        private string time;
        public string Time
        {
            get { return time; }
            set
            {
                time = value;
                RaisePropertyChanged("Time");
            }
        }

        private string date;
        public string Date
        {
            get { return date; }
            set
            {
                if (date != value)
                {
                    date = value;
                    RaisePropertyChanged("Date");
                }
            }
        }

        private string timeMobile;
        public string TimeMobile
        {
            get { return timeMobile; }
            set
            {
                if (timeMobile != value)
                {
                    timeMobile = value;
                    RaisePropertyChanged("TimeMobile");
                }
            }
        }

        private string dateMobile;
        public string DateMobile
        {
            get { return dateMobile; }
            set
            {
                if (dateMobile != value)
                {
                    dateMobile = value;
                    RaisePropertyChanged("DateMobile");
                }
            }
        }

        private string yearMonthDay;
        public string YearMonthDay
        {
            get { return yearMonthDay; }
            set
            {
                if (yearMonthDay != value)
                {
                    yearMonthDay = value;
                    RaisePropertyChanged("YearMonthDay");
                }
            }
        }

        private string clinicCode;
        public string ClinicCode
        {
            get { return clinicCode; }
            set
            {
                if (clinicCode != value)
                {
                    clinicCode = value;
                    RaisePropertyChanged("ClinicCode");
                }
            }
        }

        private ScheduleViewModel scheduleSelected;
        public ScheduleViewModel ScheduleSelected
        {
            get { return scheduleSelected; }
            set
            {
                scheduleSelected = value;
                RaisePropertyChanged("ScheduleSelected");
            }
        }

        public ICommand ConfirmNewCoordinationCommand { get { return new RelayCommand(ConfirmNewCoordination); } }

        private void ConfirmNewCoordination()
        {            
            INewMedicalCenterCoordinationPageViewModel newMedicalCenterCoordinationPageViewModel = ServiceLocator.Current.GetInstance<INewMedicalCenterCoordinationPageViewModel>();
            newMedicalCenterCoordinationPageViewModel.ScheduleSelected = this;
            newMedicalCenterCoordinationPageViewModel.PreConfirmNewCoordination();            
        }
    }
}
