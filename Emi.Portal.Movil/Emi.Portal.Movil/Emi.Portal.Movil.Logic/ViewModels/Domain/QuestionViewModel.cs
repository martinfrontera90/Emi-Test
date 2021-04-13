namespace Emi.Portal.Movil.Logic.ViewModels.Domain
{
    using Emi.Portal.Movil.Logic.Enumerations;
    using GalaSoft.MvvmLight;
    public class QuestionViewModel : ViewModelBase
    {
        private string code;
        public string Code
        {
            get { return code; }
            set
            {
                if (code != value)
                {
                    code = value;
                    RaisePropertyChanged("Code");
                }
            }
        }


        private bool isVisibleOneTen;
        public bool IsVisibleOneTen
        {
            get { return isVisibleOneTen; }
            set
            {
                if (isVisibleOneTen != value)
                {
                    isVisibleOneTen = value;
                    RaisePropertyChanged("IsVisibleOneTen");
                }
            }
        }

        private bool isVisibleOneFive;
        public bool IsVisibleOneFive
        {
            get { return isVisibleOneFive; }
            set
            {
                if (isVisibleOneFive != value)
                {
                    isVisibleOneFive = value;
                    RaisePropertyChanged("IsVisibleOneFive");
                }
            }
        }

        private bool isVisibleYesNo;
        public bool IsVisibleYesNo
        {
            get { return isVisibleYesNo; }
            set
            {
                if (isVisibleYesNo != value)
                {
                    isVisibleYesNo = value;
                    RaisePropertyChanged("IsVisibleYesNo");
                }
            }
        }

        private bool isTogglesYesNo;
        public bool IsTogglesYesNo
        {
            get { return isTogglesYesNo; }
            set
            {
                if (isTogglesYesNo != value)
                {
                    isTogglesYesNo = value;
                    LabelYesNo = isTogglesYesNo ? LabelTrue : LabelFalse;
                    RaisePropertyChanged("IsTogglesYesNo");
                }
            }
        }

        private int valueOneTen;
        public int ValueOneTen
        {
            get { return valueOneTen; }
            set
            {
                if (valueOneTen != value)
                {
                    valueOneTen = value;
                    RaisePropertyChanged("ValueOneTen");
                }
            }
        }

        private int valueOneFive;
        public int ValueOneFive
        {
            get { return valueOneFive; }
            set
            {
                if (valueOneFive != value)
                {
                    valueOneFive = value;
                    RaisePropertyChanged("ValueOneFive");
                }
            }
        }

        private string title;
        public string Title
        {
            get { return title; }
            set
            {
                if (title != value)
                {
                    title = value;
                    RaisePropertyChanged("Title");
                }
            }
        }

        private string description;

        public string Description
        {
            get { return description; }
            set
            {
                if (description != value)
                {
                    description = value;
                    RaisePropertyChanged("Description");
                }
            }
        }
                
        private string labelYesNo;
        public string LabelYesNo
        {
            get { return labelYesNo; }
            set {
                if (labelYesNo != value)
                {
                    labelYesNo = value;
                    RaisePropertyChanged("LabelYesNo"); 
                }
            }
        }

        private QuestionType questionType;
        public QuestionType QuestionType
        {
            get { return questionType; }
            set
            {
                if (questionType != value)
                {
                    questionType = value;
                    RaisePropertyChanged("QuestionType");
                }
            }
        }

        private string labelTrue;
        public string LabelTrue
        {
            get { return labelTrue; }
            set
            {
                if (labelTrue != value)
                {
                    labelTrue = value;
                    RaisePropertyChanged("LabelTrue");
                }
            }
        }

        private string labelFalse;
        public string LabelFalse
        {
            get { return labelFalse; }
            set
            {
                if (labelFalse != value)
                {
                    labelFalse = value;
                    RaisePropertyChanged("LabelFalse");
                }
            }
        }

    }
}
