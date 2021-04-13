namespace Emi.Portal.Movil.Logic.ViewModels.Domain
{
    using System;
    using System.Net;
    using System.Windows.Input;
    using CommonServiceLocator;
    using Emi.Portal.Movil.Logic.Contracts.Services;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using Xamarin.Essentials;
    using Xamarin.Forms;

    public class MessageChatViewModel : ViewModelBase
    {
        IMedicalVideoCallViewModel medicalVideoCall = ServiceLocator.Current.GetInstance<IMedicalVideoCallViewModel>();
        IFileSelectService fileSelectService = ServiceLocator.Current.GetInstance<IFileSelectService>();
        IDialogService dialogService = ServiceLocator.Current.GetInstance<IDialogService>();

        private string text;
        public string Text
        {
            get { return text; }
            set
            {
                text = value;
                RaisePropertyChanged("Text");
            }
        }

        private string user;
        public string User
        {
            get { return user; }
            set
            {
                user = value;
                RaisePropertyChanged("User");
            }
        }

        private bool isFile;
        public bool IsFile
        {
            get { return isFile; }
            set
            {
                isFile = value;
                RaisePropertyChanged("IsFile");
            }
        }

        private string urlFile = string.Empty;
        public string UrlFile
        {
            get { return urlFile; }
            set
            {
                urlFile = value;
                RaisePropertyChanged("UrlFile");
            }
        }

        private string completeName;
        public string CompleteName
        {
            get { return completeName; }
            set
            {
                completeName = value;
                RaisePropertyChanged("CompleteName");
            }
        }



        public ICommand OpenFileCommand { get { return new RelayCommand(OpenFile); } }

        private async void OpenFile()
        {
            try
            {
                if (UrlFile != string.Empty)
                {
                    dialogService.ShowProgress();
                    string ext = System.IO.Path.GetExtension(UrlFile).ToLower();

                    switch (ext)
                    {
                        case ".pdf":
                            var pdf = await fileSelectService.SavePdf(UrlFile, System.IO.Path.GetRandomFileName());
                            medicalVideoCall.UrlPdf = Device.RuntimePlatform == Device.Android ? $"file:///android_asset/pdfjs/web/viewer.html?file=file://{WebUtility.UrlEncode(pdf)}" : pdf;
                            medicalVideoCall.IsVisiblePdf = true;
                            medicalVideoCall.Close = true;
                            break;
                        case ".png":
                        case ".jpg":
                        case ".jpeg":
                            medicalVideoCall.UrlImage = urlFile;
                            medicalVideoCall.IsVisibleImage = true;
                            medicalVideoCall.Close = true;
                            break;
                        case ".xlsx":
                        case ".docx":
                            await dialogService.ShowMessage("Atención", "Se abrirá una pantalla nueva de la cual podrás descargar el archivo");
                            await Browser.OpenAsync(UrlFile, BrowserLaunchMode.SystemPreferred);
                            break;
                        default:
                            await dialogService.ShowMessage("", "Formato de archivo no soportado");
                            break;
                    }
                    medicalVideoCall.UrlFile = UrlFile;
                }
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

        public MessageChatViewModel()
        {
        }
    }
}
