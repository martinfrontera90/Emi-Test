using Emi.Portal.Movil.Droid.Services;

[assembly: Xamarin.Forms.Dependency(typeof(FileService))]
namespace Emi.Portal.Movil.Droid.Services
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Emi.Portal.Movil.Logic.Contracts.Services;
    using Newtonsoft.Json;
    public  class FileService : IFileService
    {
        public async Task SaveAsync<T>(string fileName, T content)
        {
            await Task.Run(() =>
            {
                var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                var filePath = Path.Combine(documentsPath, fileName);

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                string result = JsonConvert.SerializeObject(content);
                File.WriteAllText(filePath, result);
            });
        }

        public async Task<TResponse> LoadAsync<TResponse>(string fileName)
        {
            try
            {
                return await Task.Run(async () =>
                {
                    if (await FileExists(fileName))
                    {
                        var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                        var filePath = Path.Combine(documentsPath, fileName);

                        string result = System.IO.File.ReadAllText(filePath);
                        if (result.Equals("{}") || string.IsNullOrEmpty(result.Trim()) || result.Equals("\"\""))
                        {
                            return default(TResponse);
                        }
                        else
                        {
                            TResponse serializedResponse = JsonConvert.DeserializeObject<TResponse>(result);
                            return serializedResponse;
                        }
                    }
                    else
                        return default(TResponse);
                });
            }
            catch (Exception ex)
            {
                return default(TResponse);
            }
        }

        public void Delete(string fileName)
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(documentsPath, fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        public bool ExistRecentCache(string fileName, int cacheTime)
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

            var filePath = Path.Combine(documentsPath, fileName);

            if (File.Exists(filePath))
            {
                var creationTime = File.GetCreationTime(filePath);

                if (creationTime.Add(new TimeSpan(0, 0, cacheTime, 0)) > DateTime.Now)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> FileExists(string fileName)
        {
            return await Task.Run<bool>(() =>
            {
                var folderCourse = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

                var filename = Path.Combine(folderCourse, fileName);
                return File.Exists(filename);
            });
        }

        public async Task DeleteFiles()
        {
            await Task.Run(() =>
            {
                var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                var filePath = documentsPath;

                if (Directory.Exists(filePath))
                {
                    string[] files = Directory.GetFiles(filePath);
                    foreach (string file in files)
                    {
                        File.SetAttributes(file, FileAttributes.Normal);
                        File.Delete(file);
                    }
                }
            });
        }
    }
}