namespace Emi.Portal.Movil.Logic.Resources
{
    public class LocalizedStrings
    {
        public string this[string key]
        {
            get
            {
                return AppResources.ResourceManager.GetString(key);
            }
        }
    }
}
