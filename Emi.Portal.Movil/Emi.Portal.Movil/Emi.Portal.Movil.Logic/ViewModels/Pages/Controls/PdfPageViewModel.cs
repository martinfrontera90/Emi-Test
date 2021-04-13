namespace Emi.Portal.Movil.Logic.ViewModels.Pages.Controls
{
    using System.Net;
    using System.Windows.Input;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels.Controls;
    using Emi.Portal.Movil.Logic.Contracts.Services;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using Xamarin.Essentials;
    using Xamarin.Forms;

    public class PdfPageViewModel : ViewModelBase, IPdfPageViewModel
    {
        IFileSelectService fileSelectService;
        IDialogService dialogService;
        INavigationService navigationService;

        public string PdfPath { get; set; }

        private string pdfSource;
        public string PdfSource
        {
            get { return pdfSource; }
            set
            {
                pdfSource = value;
                RaisePropertyChanged(nameof(PdfSource));
            }
        }

        private string title;
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                RaisePropertyChanged(nameof(Title));
            }
        }

        private bool isVisibleShare;
        public bool IsVisibleShare
        {
            get { return isVisibleShare; }
            set
            {
                isVisibleShare = value;
                RaisePropertyChanged(nameof(IsVisibleShare));
            }
        }

        public ICommand SharePdfCommand { get { return new RelayCommand(SharePdf); } }

        private async void SharePdf()
        {
            await Share.RequestAsync(new ShareFileRequest
            {
                Title = Title,
                File = new ShareFile(PdfPath)
            });
        }

        public async void OpenPdf(string urlPdf = "", string name = "", byte[] array = null, bool share = false, string title = "")
        {
            IsVisibleShare = share;
            Title = title;
            PdfPath = await fileSelectService.SavePdf(urlPdf, name, array);
            PdfSource = Device.RuntimePlatform == Device.Android ? $"file:///android_asset/pdfjs/web/viewer.html?file=file://{WebUtility.UrlEncode(PdfPath)}" : PdfPath;
            if (!string.IsNullOrWhiteSpace(PdfSource))
                await navigationService.Navigate(Enumerations.AppPages.PdfPage);
            if (Device.RuntimePlatform != Device.Android)
                await dialogService.ShowMessage("", "En caso de no poder visualizar el pdf, lo puedes descargar o compartir en el icono que encontrás en la parte inferior de tu pantalla.");
        }

        public PdfPageViewModel(IFileSelectService fileSelectService, INavigationService navigationService, IDialogService dialogService)
        {
            this.dialogService = dialogService;
            this.navigationService = navigationService;
            this.fileSelectService = fileSelectService;
        }
    }
}
