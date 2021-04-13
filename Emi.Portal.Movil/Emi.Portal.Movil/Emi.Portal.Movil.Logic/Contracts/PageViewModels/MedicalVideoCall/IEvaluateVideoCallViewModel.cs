namespace Emi.Portal.Movil.Logic.Contracts.PageViewModels.MedicalVideoCall
{
    using System.Threading.Tasks;
    using System.Windows.Input;

    public interface IEvaluateVideoCallViewModel
    {
        ICommand CallCategoryCommand { get; }

        ICommand CancelEvaluateCommand { get; }

        ICommand SendEvaluateCommand { get; }

        string CommentEvaluateVideoCall { get; set; }

        int RatingQ2 { get; set; }

        Task GoToServicePage();

        void RemovePage();
    }
}
