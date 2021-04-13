namespace Emi.Portal.Movil.Logic.Resources
{
    public class LocalConfigurations
    {
        public string this[string key]
        {
            get
            {
                return AppConfigurations.ResourceManager.GetString(key);
            }
        }
    }
}
