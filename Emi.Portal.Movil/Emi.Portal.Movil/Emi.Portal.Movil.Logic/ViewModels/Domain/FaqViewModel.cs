namespace Emi.Portal.Movil.Logic.ViewModels.Domain
{
    using GalaSoft.MvvmLight;
    public class FaqViewModel : ViewModelBase
    {
        private string answer;
        public string AnswerText
        {
            get { return answer; }
            set
            {
                if (answer != value)
                {
                    answer = value;
                    RaisePropertyChanged("AnswerText");
                }
            }
        }

        private string question;
        public string Question
        {
            get { return question; }
            set
            {
                if (question != value)
                {
                    question = value;
                    RaisePropertyChanged("Question"); 
                }
            }
        }
    }
}
