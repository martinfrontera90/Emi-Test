namespace Emi.Portal.Movil.Services
{
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using CommonServiceLocator;
    using Emi.Portal.Movil.Logic.Contracts.Services;
    using Emi.Portal.Movil.Logic.Models.Domain;
    using Emi.Portal.Movil.Logic.Models.Requests;
    using Emi.Portal.Movil.Logic.Resources;
    using Plugin.FilePicker;
    using Plugin.FilePicker.Abstractions;
    using Plugin.Media;
    using Plugin.Media.Abstractions;
    using Plugin.Permissions;
    using Plugin.Permissions.Abstractions;
    using Xamarin.Forms;

    public class FileSelectService : IFileSelectService
    {

        IDialogService dialogService;
        IPermissionService permissionService;
        readonly string rootDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "pdfjs");

        public string MimeType(string extension)
        {
            switch (extension)
            {
                case ".png":
                    return "image/png";
                case ".jpg":
                    return "image/jpg";
                case ".jpeg":
                    return "image/jpeg";
                case ".pdf":
                    return "application/pdf";
                default:
                    return string.Empty;
            }
        }

        public async Task<FileRequest> AddFile(string serviceDate, string serviceNumber)
        {
            FileSelected file = await SelectFile();
            FileRequest requestFile = new FileRequest();
            try
            {
                requestFile = new FileRequest
                {
                    ServiceMiddlewareId = serviceNumber,
                    ServiceDate = serviceDate,
                    FileName = file.Name,
                    FileContentType = MimeType(file.Extension),
                    FileData = file.File
                };
            }
            catch (Exception e)
            {
                ServiceLocator.Current.GetInstance<IExceptionService>().RegisterException(e);
            }
            return requestFile;
        }

        public async Task<FileSelected> SelectFile()
        {
            FileSelected requestFile = new FileSelected();
            try
            {
                if (await permissionService.CheckPermissions(Plugin.Permissions.Abstractions.Permission.Photos))
                {
                    var option = await dialogService.ShowListActionsAsync(null, AppResources.Cancel, null, AppResources.SelectedPhotoGalery, AppResources.PickDocument);

                    if (option == AppResources.SelectedPhotoGalery)
                    {
                        var photo = await SelectedPhotoAsync();
                        if (photo != null)
                        {
                            string ex = Path.GetExtension(photo.Path);
                            if (ex.Equals(".png", StringComparison.InvariantCultureIgnoreCase) || ex.Equals(".jpg", StringComparison.InvariantCultureIgnoreCase) || ex.Equals("jpeg", StringComparison.InvariantCultureIgnoreCase))
                            {
                                if (await ValidateFile(new FileInfo(photo.Path).Length))
                                {
                                    requestFile = new FileSelected
                                    {
                                        Name = Path.GetFileName(photo.Path),
                                        Extension = ex,
                                        File = Convert.ToBase64String(File.ReadAllBytes(photo.Path))
                                    };
                                }
                            }
                            else
                            {
                                await dialogService.ShowMessage("", "Formato de archivo no soportado");
                            }
                        }

                    }
                    else if (option == AppResources.PickDocument)
                    {
                        FileData fileData = await CrossFilePicker.Current.PickFile();
                        await Task.WhenAll(Task.Delay(200));
                        if (fileData != null)
                        {
                            string ex = Path.GetExtension(fileData.FileName);
                            if (ex.Equals(".pdf", StringComparison.InvariantCultureIgnoreCase))
                            {
                                if (await ValidateFile(fileData.DataArray.Length))
                                {
                                    requestFile = new FileSelected
                                    {
                                        Name = fileData.FileName,
                                        Extension = ex,
                                        File = Convert.ToBase64String(fileData.DataArray)
                                    };
                                }
                            }
                            else
                            {
                                await dialogService.ShowMessage("", "Formato de archivo no soportado");
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ServiceLocator.Current.GetInstance<IExceptionService>().RegisterException(e);
            }
            return requestFile;
        }

        public async Task<MediaFile> SelectedPhotoAsync()
        {
            TaskCompletionSource<MediaFile> tcs = new TaskCompletionSource<MediaFile>();
            Device.BeginInvokeOnMainThread(async () =>
            {
                tcs.SetResult(await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
                {
                    CompressionQuality = 50,
                    PhotoSize = PhotoSize.Full,
                    CustomPhotoSize = 100
                }));
            });
            return await tcs.Task;
        }

        public async Task<bool> ValidateFile(long length)
        {
            if (length < 2000001)
            {
                return true;
            }
            else
            {
                await dialogService.ShowMessage("", "El archivo excede el tamaño máximo permitido");
                return false;
            }

        }

        public async Task<string> SavePdf(string urlPdf = "", string name = "", byte[] array = null)
        {
            string UrlPdf = null;
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
                if (status != PermissionStatus.Granted)
                {
                    var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Storage });
                    status = results[Permission.Storage];
                }


                var fileName = Guid.NewGuid().ToString();

                var localPath = string.Empty;
                if (array == null)
                {
                    using (var httpClient = new HttpClient())
                    {
                        using (dialogService.ShowProgress())
                        {
                            var pdfStream = await httpClient.GetStreamAsync(urlPdf);
                            localPath = await SaveFileToDisk(pdfStream, $"{name}.pdf");
                        }
                    }
                }
                else
                {
                    localPath = await SaveFileArrayToDisk(array, $"{name}.pdf");
                }

                UrlPdf = localPath;

            }
            catch (Exception ex)
            {
                ServiceLocator.Current.GetInstance<IExceptionService>().RegisterException(ex);
            }
            return UrlPdf;
        }

        public async Task<string> SaveFileToDisk(Stream stream, string fileName)
        {
            if (!Directory.Exists(rootDir))
            {
                Directory.CreateDirectory(rootDir);
            }

            var filePath = Path.Combine(rootDir, fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            using (var memoryStream = new MemoryStream())
            {
                await stream.CopyToAsync(memoryStream);
                File.WriteAllBytes(filePath, memoryStream.ToArray());
            }

            return filePath;
        }

        public async Task<string> SaveFileArrayToDisk(byte[] array, string fileName)
        {
            if (!Directory.Exists(rootDir))
            {
                Directory.CreateDirectory(rootDir);
            }

            var filePath = Path.Combine(rootDir, fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            File.WriteAllBytes(filePath, array);

            return filePath;
        }

        public FileSelectService(IDialogService dialogService, IPermissionService permissionService)
        {
            this.dialogService = dialogService;
            this.permissionService = permissionService;
        }
    }
}
