namespace Emi.Portal.Movil.Logic.ViewModels.Domain
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using CommonServiceLocator;
    using Emi.Portal.Movil.Logic.Contracts.Domain;
    using Emi.Portal.Movil.Logic.Contracts.Services;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;

    public class DocumentRequiredViewModel : ViewModelBase, IDocumentsRequiredViewModel
    {
        readonly IFileSelectService fileSelectService = ServiceLocator.Current.GetInstance<IFileSelectService>();
        readonly IDialogService dialogService = ServiceLocator.Current.GetInstance<IDialogService>();

        public int MaxFiles { get; set; } = 0;

        public int Code { get; set; }

        private string nameDocument;
        public string NameDocument
        {
            get { return nameDocument; }
            set
            {
                nameDocument = value;
                RaisePropertyChanged(nameof(NameDocument));
                IsVisibleAddFile = string.IsNullOrWhiteSpace(NameDocument);
            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }

        private string informationFile;
        public string InformationFile
        {
            get { return informationFile; }
            set
            {
                informationFile = value;
                RaisePropertyChanged(nameof(InformationFile));
            }
        }

        private bool isVisibleFileList;
        public bool IsVisibleFileList
        {
            get { return isVisibleFileList; }
            set
            {
                isVisibleFileList = value;
                RaisePropertyChanged(nameof(IsVisibleFileList));
            }
        }

        private bool listFilled = false;
        public bool ListFilled
        {
            get { return listFilled; }
            set
            {
                listFilled = value;
                RaisePropertyChanged(nameof(ListFilled));
            }
        }

        private ObservableCollection<DocumentRequiredViewModel> filesSelected;
        public ObservableCollection<DocumentRequiredViewModel> FilesSelected
        {
            get { return filesSelected; }
            set
            {
                filesSelected = value;
                RaisePropertyChanged(nameof(FilesSelected));
            }
        }

        private bool isVisibleAddFile;
        public bool IsVisibleAddFile
        {
            get { return isVisibleAddFile; }
            set
            {
                isVisibleAddFile = value;
                RaisePropertyChanged(nameof(IsVisibleAddFile));
            }
        }

        private bool isVisibleError = false;
        public bool IsVisibleError
        {
            get { return isVisibleError; }
            set
            {
                isVisibleError = value;
                RaisePropertyChanged(nameof(IsVisibleError));
            }
        }

        private bool isRequired;
        public bool IsRequired
        {
            get { return isRequired; }
            set
            {
                isRequired = value;
                RaisePropertyChanged(nameof(IsRequired));
            }
        }

        private string base64;
        public string Base64
        {
            get { return base64; }
            set
            {
                base64 = value;
                RaisePropertyChanged(nameof(Base64));
            }
        }

        private string extension;
        public string Extension
        {
            get { return extension; }
            set
            {
                extension = value;
                RaisePropertyChanged(nameof(Extension));
            }
        }

        private bool isVisibleInformationFile = false;
        public bool IsVisibleInformationFile
        {
            get { return isVisibleInformationFile; }
            set
            {
                isVisibleInformationFile = value;
                RaisePropertyChanged(nameof(IsVisibleInformationFile));
            }
        }

        public ICommand AddFileCommand { get { return new RelayCommand<string>(AddFile); } }
        public ICommand DeleteFileCommand { get { return new RelayCommand(DeleteFile); } }
        public ICommand InformationFileCommand { get { return new RelayCommand(EnableFileInformation); } }
        public ICommand AddFileListCommand { get { return new RelayCommand(AddFileList); } }
        public ICommand DeleteFileListCommand { get { return new RelayCommand<DocumentRequiredViewModel>((file) => DeleteFileList(file)); } }

        private async void DeleteFileList(DocumentRequiredViewModel file)
        {
            if (await dialogService.ShowConfirm("", "¿Deseas eliminar este archivo?"))
            {
                FilesSelected.Remove(file);
                ListFilled = false;
            }
        }

        private async void AddFileList()
        {
            try
            {
                var result = await fileSelectService.SelectFile();
                if (result.Name != null)
                {
                    FilesSelected.Add(
                        new DocumentRequiredViewModel
                        {
                            Base64 = result.File,
                            NameDocument = result.Name,
                            Extension = fileSelectService.MimeType(result.Extension)

                        });
                    if (FilesSelected.Count() >= MaxFiles)
                        ListFilled = true;
                }
            }
            catch (Exception ex)
            {
                ServiceLocator.Current.GetInstance<IExceptionService>().RegisterException(ex);

            }
        }

        private async void AddFile(string op)
        {
            try
            {
                var result = await fileSelectService.SelectFile();
                if (result.Name != null)
                {

                    Base64 = result.File;
                    NameDocument = result.Name;
                    Extension = fileSelectService.MimeType(result.Extension);
                    IsVisibleAddFile = false;
                }
            }
            catch (Exception ex)
            {
                ServiceLocator.Current.GetInstance<IExceptionService>().RegisterException(ex);

            }

        }

        private void EnableFileInformation()
        {
            IsVisibleInformationFile = !IsVisibleInformationFile;
        }

        private async void DeleteFile()
        {
            if (await dialogService.ShowConfirm("", "¿Deseas eliminar este archivo?"))
            {
                NameDocument = string.Empty;
                IsVisibleAddFile = true;
            }
        }

        public DocumentRequiredViewModel()
        {
            FilesSelected = new ObservableCollection<DocumentRequiredViewModel>();
        }

    }
}
