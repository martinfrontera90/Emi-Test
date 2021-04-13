namespace Emi.Portal.Movil.Services
{
    using Emi.Portal.Movil.Logic.Contracts.Services;
    using CommonServiceLocator;
    using Plugin.Permissions;
    using Plugin.Permissions.Abstractions;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xamarin.Forms;

    public class PermissionService : IPermissionService
    {
        public async Task<bool> CheckPermissions(Permission permission)
        {
            PermissionStatus permissionStatus = await CheckPermissionStatusAsync(permission);
            IDialogService dialogService = ServiceLocator.Current.GetInstance<IDialogService>();
            IPhoneService phoneService = ServiceLocator.Current.GetInstance<IPhoneService>();

            permissionStatus = await CheckPermissionStatusAsync(permission);
            bool request = false;
            if (permissionStatus == PermissionStatus.Denied)
            {
                if (phoneService.IsiOS)
                {
                    string title = permission.ToString().ToUpper();
                    string question = $"Esta funcionalidad requiere permisos de {permission}. Por favor activa los permisos para esta app.";
                    Task<bool> task = Application.Current?.MainPage?.DisplayAlert(title, question, "Si", "No");

                    if (task == null)
                        return false;

                    bool result = await task;

                    if (result)
                    {
                        OpenAppSettings();
                    }

                    return false;
                }
                request = true;
            }

            if (request || permissionStatus != PermissionStatus.Granted)
            {
                Dictionary<Permission, PermissionStatus> newStatus = await RequestPermissionsAsync(permission);
                if (newStatus.ContainsKey(permission) && newStatus[permission] != PermissionStatus.Granted)
                {
                    string title = permission.ToString().ToUpper();
                    string question = $"Esta funcionalidad requiere permisos de {permission}.";
                    await Application.Current?.MainPage?.DisplayAlert(title, question, "Aceptar");
                    OpenAppSettings();
                    return false;
                }
            }
            return true;
        }

        public async Task<PermissionStatus> CheckPermissionStatusAsync(Permission permission)
        {
            return await CrossPermissions.Current.CheckPermissionStatusAsync(permission);
        }

        public bool OpenAppSettings()
        {
            return CrossPermissions.Current.OpenAppSettings();
        }

        public async Task<Dictionary<Permission, PermissionStatus>> RequestPermissionsAsync(params Permission[] permissions)
        {
            return await CrossPermissions.Current.RequestPermissionsAsync(permissions);
        }
    }
}
