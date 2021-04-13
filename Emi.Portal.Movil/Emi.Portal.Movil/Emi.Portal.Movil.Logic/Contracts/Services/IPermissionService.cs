namespace Emi.Portal.Movil.Logic.Contracts.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Plugin.Permissions.Abstractions;

    public interface IPermissionService
    {
        Task<bool> CheckPermissions(Permission permission);
        Task<PermissionStatus> CheckPermissionStatusAsync(Permission permission);
        bool OpenAppSettings();
        Task<Dictionary<Permission, PermissionStatus>> RequestPermissionsAsync(params Permission[] permissions);
    }
}
