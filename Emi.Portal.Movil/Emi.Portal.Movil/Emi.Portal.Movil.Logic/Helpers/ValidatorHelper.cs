namespace Emi.Portal.Movil.Logic.Helpers
{
    using CommonServiceLocator;
    using Emi.Portal.Movil.Logic.Contracts.Services;
    using Emi.Portal.Movil.Logic.Resources;
    using Plugin.Permissions;
    using Plugin.Permissions.Abstractions;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Xamarin.Forms;

    public static class ValidatorHelper
    {

        public static bool IsValidCellPhone(string cellPhone)
        {
            if (string.IsNullOrEmpty(cellPhone))
                return false;

            Regex regex = new Regex(AppConfigurations.RegexCellPhone);
            Match match = regex.Match(cellPhone);
            return match.Success;
        }

        public static bool IsValidPhone(string phone)
        {
            if (string.IsNullOrEmpty(phone))
                return false;

            Regex regex = new Regex(AppConfigurations.RegexPhone);
            Match match = regex.Match(phone);
            return match.Success;
        }

        /// <summary>
        /// Validate if email is valid.
        /// </summary>        
        /// <returns></returns>
        public static bool IsValidEmail(string email)
        {
            Regex regex = new Regex(AppConfigurations.RegexEmail);
            Match match = regex.Match(email);
            return match.Success;
        }


        public static bool IsValidName(string name)
        {
            Regex regex = new Regex(AppConfigurations.RegexName);
            Match match = regex.Match(name);
            return match.Success;
        }
        /// <summary>
        /// Validate if two data are equal
        /// </summary>
        public static bool IsEqualData(string data, string dataToConfirm)
        {
            return data == dataToConfirm;
        }

        public static async Task<bool> CheckPermissions(Permission permission)
        {
            PermissionStatus permissionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(permission);
            IDialogService dialogService = ServiceLocator.Current.GetInstance<IDialogService>();
            IPhoneService phoneService = ServiceLocator.Current.GetInstance<IPhoneService>();

            permissionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(permission);

            bool denied = false;

            if (permissionStatus == PermissionStatus.Denied)
            {
                if (phoneService.IsiOS)
                {
                    string title = "Ubicación";
                    string question = "Esta funcionalidad requiere permisos de ubicación. Por favor activa los permisos para esta app.";
                    Task<bool> task = Application.Current?.MainPage?.DisplayAlert(title, question, AppResources.YesButtonText, AppResources.NoButtonText);

                    if (task == null)
                        return false;

                    bool result = await task;

                    if (result)
                    {
                        CrossPermissions.Current.OpenAppSettings();
                    }

                    return false;
                }
                denied = true;
            }

            if (denied || permissionStatus != PermissionStatus.Granted)
            {
                Dictionary<Permission, PermissionStatus> newStatus = await CrossPermissions.Current.RequestPermissionsAsync(permission);
                if (newStatus.ContainsKey(permission) && newStatus[permission] != PermissionStatus.Granted)
                {
                    string title = "Ubicación";
                    string question = "Esta funcionalidad requiere permisos de ubicación.";
                    Application.Current?.MainPage?.DisplayAlert(title, question, "Aceptar");
                    CrossPermissions.Current.OpenAppSettings();
                    return false;
                }
            }

            return true;
        }
    }
}
