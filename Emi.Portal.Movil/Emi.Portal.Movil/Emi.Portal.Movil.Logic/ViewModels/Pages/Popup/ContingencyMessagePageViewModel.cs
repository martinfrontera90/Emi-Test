namespace Emi.Portal.Movil.Logic.ViewModels.Pages.Popup
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Windows.Input;
    using CommonServiceLocator;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels.Popup;
    using Emi.Portal.Movil.Logic.Contracts.Services;
    using Emi.Portal.Movil.Logic.Models.Domain;
    using Emi.Portal.Movil.Logic.Resources;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using Rg.Plugins.Popup.Services;
    using Xamarin.Essentials;
    using System.Collections.Generic;

    public class ContingencyMessagePageViewModel : ViewModelBase, IContingencyMessagePageViewModel
    {
        INavigationService navigationService;
        IApiService apiService;
        IDialogService dialogService;

        private ObservableCollection<ContingencyMessages> messages;
        public ObservableCollection<ContingencyMessages> Messages
        {
            get { return messages; }
            set
            {
                messages = value;
                RaisePropertyChanged(nameof(Messages));
            }
        }


        public ICommand CloseCommand { get { return new RelayCommand(async () => await PopupNavigation.Instance.PopAsync()); } }
        public ICommand OpenUrlCommand { get { return new RelayCommand<ContingencyMessages>(OpenUrl); } }

        private async void OpenUrl(ContingencyMessages obj)
        {
            var res = Find(obj.Message);
            var urlText = res.Select(x => System.Net.WebUtility.HtmlDecode(x.Text));

            if (res.Count == 1)
            {
                await Launcher.OpenAsync(res.FirstOrDefault().Href);
            }
            else if (res.Count > 1)
            {
                var option = await dialogService.ShowListActionsAsync(null, AppResources.Cancel, null, urlText.ToArray());
                if (!option.Equals("Cancelar"))
                    await Launcher.OpenAsync(System.Net.WebUtility.HtmlDecode(res.Where(x => option.Equals(System.Net.WebUtility.HtmlDecode(x.Text))).FirstOrDefault().Href));
            }
        }

        public struct LinkItem
        {
            public string Href;
            public string Text;

            public override string ToString()
            {
                return Href + "\n\t" + Text;
            }
        }

        public static List<LinkItem> Find(string file)
        {
            List<LinkItem> list = new List<LinkItem>();

            // 1.
            // Find all matches in file.
            MatchCollection m1 = Regex.Matches(file, @"(<a.*?>.*?</a>)",
                RegexOptions.Singleline);

            // 2.
            // Loop over each match.
            foreach (Match m in m1)
            {
                string value = m.Groups[1].Value;
                LinkItem i = new LinkItem();

                // 3.
                // Get href attribute.
                Match m2 = Regex.Match(value, @"href=\""(.*?)\""",
                    RegexOptions.Singleline);
                if (m2.Success)
                {
                    i.Href = m2.Groups[1].Value;
                }

                // 4.
                // Remove inner tags from text.
                string t = Regex.Replace(value, @"\s*<.*?>\s*", "",
                    RegexOptions.Singleline);
                i.Text = t;

                list.Add(i);
            }
            return list;
        }

        public async void LoadData()
        {
            try
            {
                Clean();
                dialogService.ShowProgress();
                var response = await apiService.GetContingencyMessages();
                dialogService.HideProgress();
                if (response.Success && response.StatusCode != -1)
                {
                    if (response.Messages.Count > 0)
                    {
                        Messages = new ObservableCollection<ContingencyMessages>(response.Messages);
                        if (Messages.Count > 1)
                        {
                            Messages.Last().Last = true;
                        }
                        else
                        {
                            Messages.FirstOrDefault().Last = true;
                        }
                        await navigationService.Navigate(Enumerations.AppPages.ContingencyMessagePage);
                    }
                }
            }
            catch (Exception e)
            {
                dialogService.HideProgress();
                ServiceLocator.Current.GetInstance<IExceptionService>().RegisterException(e);
            }
        }

        private void Clean()
        {
            Messages = new ObservableCollection<ContingencyMessages>();
        }

        public ContingencyMessagePageViewModel(INavigationService navigationService, IApiService apiService, IDialogService dialogService)
        {
            this.dialogService = dialogService;
            this.apiService = apiService;
            this.navigationService = navigationService;
        }
    }
}
