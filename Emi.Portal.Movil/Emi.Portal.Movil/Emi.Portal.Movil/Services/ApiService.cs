namespace Emi.Portal.Movil.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using Emi.Portal.Movil.Logic.Contracts.Domain;
    using Emi.Portal.Movil.Logic.Contracts.Services;
    using Emi.Portal.Movil.Logic.Models.Domain;
    using Emi.Portal.Movil.Logic.Models.Requests;
    using Emi.Portal.Movil.Logic.Models.Responses;
    using Emi.Portal.Movil.Logic.Resources;
    using CommonServiceLocator;
    using Newtonsoft.Json;
    using Xamarin.Forms;

    public class ApiService : IApiService
    {
        INetworkService networkService;
        IDialogService dialogService;
        IExceptionService exceptionService;
        INavigationService navigationService;

        public string Token { get; set; }
        private static string urlBase = AppConfigurations.UrlBaseMiddleware;
        
        protected HttpClient GetHttpClient()
        {
            HttpClient httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
            httpClient.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
            httpClient.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

            return httpClient;
        }

        public async Task<ResponseAddMember> AddMember(RequestAddMember request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        List<KeyValuePair<string, string>> keyValues = new List<KeyValuePair<string, string>>();

                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.Document, request.Document));
                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.DocumentType, request.DocumentType));
                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.Email, request.Email));
                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.IdReference, request.IdReference));
                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.Names, request.Names));
                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.Phone, request.Phone));
                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.Surnames, request.Surnames));

                        FormUrlEncodedContent content = new FormUrlEncodedContent(keyValues);
                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseAddMember
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseAddMember response = JsonConvert.DeserializeObject<ResponseAddMember>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseAddMember
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponseAddMember
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }

        public async Task<ResponseCertificates> GetCertificates(RequestCertificates request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        string values = JsonConvert.SerializeObject(request);
                        StringContent content = new StringContent(values, Encoding.UTF8, "application/json");

                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseCertificates
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseCertificates response = JsonConvert.DeserializeObject<ResponseCertificates>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseCertificates
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponseCertificates
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }

        public async Task<ResponseCertificateBeneficiaries> GetUsersCertificate(Request request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        string values = JsonConvert.SerializeObject(request);
                        StringContent content = new StringContent(values, Encoding.UTF8, "application/json");

                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseCertificateBeneficiaries
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseCertificateBeneficiaries response = JsonConvert.DeserializeObject<ResponseCertificateBeneficiaries>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseCertificateBeneficiaries
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponseCertificateBeneficiaries
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }

        public async Task<ResponseDownloadCertificate> GetDownloadCertAffiliatedPayments(RequestDownloadCertificate request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        string values = JsonConvert.SerializeObject(request);
                        StringContent content = new StringContent(values, Encoding.UTF8, "application/json");

                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseDownloadCertificate
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseDownloadCertificate response = JsonConvert.DeserializeObject<ResponseDownloadCertificate>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseDownloadCertificate
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponseDownloadCertificate
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }

        public async Task<ResponseBase> SendCertAffiliatedPayments(RequestDownloadCertificate request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        string values = JsonConvert.SerializeObject(request);
                        StringContent content = new StringContent(values, Encoding.UTF8, "application/json");

                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseDownloadCertificate
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseDownloadCertificate response = JsonConvert.DeserializeObject<ResponseDownloadCertificate>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseDownloadCertificate
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponseDownloadCertificate
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }

        public async Task<ResponseContractedPlans> GetContractedPlans(Request request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        string values = JsonConvert.SerializeObject(request);
                        StringContent content = new StringContent(values, Encoding.UTF8, "application/json");

                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseContractedPlans
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseContractedPlans response = JsonConvert.DeserializeObject<ResponseContractedPlans>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseContractedPlans
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponseContractedPlans
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }

        public async Task<ResponseHasDebt> GetHastDebt(Request request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        string values = JsonConvert.SerializeObject(request);
                        StringContent content = new StringContent(values, Encoding.UTF8, "application/json");

                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseHasDebt
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseHasDebt response = JsonConvert.DeserializeObject<ResponseHasDebt>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseHasDebt
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponseHasDebt
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }

        public async Task<ResponseCities> GetCitiesRedSiem(RequestCitiesRedSiem request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {


                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        string values = JsonConvert.SerializeObject(request);
                        StringContent content = new StringContent(values, Encoding.UTF8, "application/json");

                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseCities
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseCities response = JsonConvert.DeserializeObject<ResponseCities>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseCities
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponseCities
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }

        public async Task<ResponseCountries> GetCountryRedSiem()
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    var request = new Request
                    {
                        Controller = "DataLists",
                        Action = "GetCountryRedSiem"
                    };

                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        string values = JsonConvert.SerializeObject(request);
                        StringContent content = new StringContent(values, Encoding.UTF8, "application/json");

                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseCountries
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseCountries response = JsonConvert.DeserializeObject<ResponseCountries>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseCountries
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponseCountries
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }

        public async Task<ResponseChangePassword> ChangePassword(RequestChangePassword request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Profile, AppConfigurations.CodeProfile);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        List<KeyValuePair<string, string>> keyValues = new List<KeyValuePair<string, string>>();

                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.OldPassword, request.OldPassword));
                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.NewPassword, request.NewPassword));
                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.UserName, request.UserName));

                        FormUrlEncodedContent content = new FormUrlEncodedContent(keyValues);
                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseChangePassword
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseChangePassword response = JsonConvert.DeserializeObject<ResponseChangePassword>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseChangePassword
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponseChangePassword
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }

        public async Task<ResponseAreas> GetAreas()
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    RequestAreas request = new RequestAreas();

                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Profile, AppConfigurations.CodeProfile);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.GetAsync(url);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseAreas
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseAreas response = JsonConvert.DeserializeObject<ResponseAreas>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseAreas
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }

            return new ResponseAreas
            {
                Message = AppResources.WithoutConnection,
                Success = false,
                Title = AppResources.TitleWithoutConnection,
            };
        }

        public async Task<ResponseEventType> GetEventType()
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    Request request = new Request
                    {
                        Controller = "DataLists",
                        Action = "GetEventType"
                    };
                    using (HttpClient client = new HttpClient())
                    {

                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        string values = JsonConvert.SerializeObject(request);

                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.GetAsync(url);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseEventType
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseEventType response = JsonConvert.DeserializeObject<ResponseEventType>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseEventType
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }

            return new ResponseEventType
            {
                Message = AppResources.WithoutConnection,
                Success = false,
                Title = AppResources.TitleWithoutConnection,
            };
        }

        public async Task<ResponseTermsConditions> GetPQRSTermsConditions()
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    Request request = new Request
                    {
                        Controller = "pqrs",
                        Action = "TermsConditions"
                    };
                    using (HttpClient client = new HttpClient())
                    {

                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);

                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.GetAsync(url);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseTermsConditions
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseTermsConditions response = JsonConvert.DeserializeObject<ResponseTermsConditions>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseTermsConditions
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }

            return new ResponseTermsConditions
            {
                Message = AppResources.WithoutConnection,
                Success = false,
                Title = AppResources.TitleWithoutConnection,
            };
        }

        public async Task<ResponsePQRSCreated> PostPQRS(RequestCreatePQRS request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {

                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);

                        string values = JsonConvert.SerializeObject(request);
                        StringContent content = new StringContent(values, Encoding.UTF8, "application/json");

                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponsePQRSCreated
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponsePQRSCreated response = JsonConvert.DeserializeObject<ResponsePQRSCreated>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponsePQRSCreated
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            return new ResponsePQRSCreated
            {
                Message = AppResources.WithoutConnection,
                Success = false,
                Title = AppResources.TitleWithoutConnection,
            };
        }

        public async Task<ResponseTracingPQRS> GetTracingPQRS(Request request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {

                    using (HttpClient client = new HttpClient())
                    {

                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);

                        string values = JsonConvert.SerializeObject(request);
                        StringContent content = new StringContent(values, Encoding.UTF8, "application/json");

                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseTracingPQRS
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseTracingPQRS response = JsonConvert.DeserializeObject<ResponseTracingPQRS>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseTracingPQRS
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }

            return new ResponseTracingPQRS
            {
                Message = AppResources.WithoutConnection,
                Success = false,
                Title = AppResources.TitleWithoutConnection,
            };
        }

        public async Task<ResponsePQRSValidateUser> GetPQRSUser(RequestPQRSUser request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        string values = JsonConvert.SerializeObject(request);
                        StringContent content = new StringContent(values, Encoding.UTF8, "application/json");

                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponsePQRSValidateUser
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponsePQRSValidateUser response = JsonConvert.DeserializeObject<ResponsePQRSValidateUser>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponsePQRSValidateUser
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponsePQRSValidateUser
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }

        public async Task<RefundTypeResponse> GetRefundType(RefundTypeRequest request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {


                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Profile, AppConfigurations.CodeProfile);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        string values = JsonConvert.SerializeObject(request);
                        StringContent content = new StringContent(values, Encoding.UTF8, "application/json");

                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new RefundTypeResponse
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        RefundTypeResponse response = JsonConvert.DeserializeObject<RefundTypeResponse>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new RefundTypeResponse
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }

            return new RefundTypeResponse
            {
                Message = AppResources.WithoutConnection,
                Success = false,
                Title = AppResources.TitleWithoutConnection,
            };
        }

        public async Task<ResponsePendingCoordinations> ConfirmCoordination(RequestNewMedicalCenterCoordination request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {

                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        List<KeyValuePair<string, string>> keyValues = new List<KeyValuePair<string, string>>();
                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.IdReference, request.IdReference));
                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.Token, request.Token));
                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.RDACode, request.RDACode));
                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.LocalCode, request.LocalCode));
                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.ClinicCode, request.ClinicCode));
                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.SpecialityCode, request.SpecialityCode));
                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.Phone, request.Phone));
                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.Email, request.Email));
                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.YearMonthDay, request.YearMonthDay));
                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.Time, request.Time));
                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.PatientCode, request.PatientCode));
                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.ProductCode, request.ProductCode));
                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.PaymentMethodName, request.PaymentMethodName));
                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.UserEmail, request.UserEmail));
                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.PatientName, request.PatientName));

                        FormUrlEncodedContent content = new FormUrlEncodedContent(keyValues);
                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponsePendingCoordinations
                            {
                                Message = httpResponse.StatusCode.ToString(),
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponsePendingCoordinations response = JsonConvert.DeserializeObject<ResponsePendingCoordinations>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponsePendingCoordinations
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponsePendingCoordinations
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }

        public async Task<ResponseForgotPassword> ForgotPassword(RequestForgotPassword request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Profile, AppConfigurations.CodeProfile);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        string values = JsonConvert.SerializeObject(request);
                        StringContent content = new StringContent(values, Encoding.UTF8, "application/json");

                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseForgotPassword
                            {
                                Error = new ResponseError { Description = httpResponse.Content.ReadAsStringAsync().Result }
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseForgotPassword response = JsonConvert.DeserializeObject<ResponseForgotPassword>(result);
                        response.Success = true;
                        return response;
                    }
                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseForgotPassword
                    {
                        Error = new ResponseError { Description = AppResources.WithoutConnection }
                    };
                }
            }

            return new ResponseForgotPassword
            {
                Message = AppResources.WithoutConnection,
                Title = AppResources.TitleWithoutConnection,
            };
        }

        public async Task<ResponseFile> PostFile(FileRequest request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Profile, AppConfigurations.CodeProfile);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        string values = JsonConvert.SerializeObject(request);
                        StringContent content = new StringContent(values, Encoding.UTF8, "application/json");

                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseFile
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseFile response = JsonConvert.DeserializeObject<ResponseFile>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseFile
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }

            return new ResponseFile
            {
                Message = AppResources.WithoutConnection,
                Success = false,
                Title = AppResources.TitleWithoutConnection,
            };
        }

        public async Task<ResponseAffiliate> GetAffiliate(RequestAffiliate request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        List<KeyValuePair<string, string>> keyValues = new List<KeyValuePair<string, string>>();

                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.Document, request.Document));
                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.DocumentType, request.DocumentType));

                        FormUrlEncodedContent content = new FormUrlEncodedContent(keyValues);
                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseAffiliate
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseAffiliate response = JsonConvert.DeserializeObject<ResponseAffiliate>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseAffiliate
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponseAffiliate
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }

        public async Task<ResponseBeneficiaries> GetBeneficiaries(RequestBeneficiaries request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        List<KeyValuePair<string, string>> keyValues = new List<KeyValuePair<string, string>>();

                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.Document, request.Document));
                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.DocumentType, request.DocumentType));

                        FormUrlEncodedContent content = new FormUrlEncodedContent(keyValues);
                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseBeneficiaries
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseBeneficiaries response = JsonConvert.DeserializeObject<ResponseBeneficiaries>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseBeneficiaries
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponseBeneficiaries
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }

        public async Task<ResponseNearbyClinics> GetClinics(RequestClinics request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {

                        client.DefaultRequestHeaders.Clear();
                        List<KeyValuePair<string, string>> keyValues = new List<KeyValuePair<string, string>>();

                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));
                        FormUrlEncodedContent content = new FormUrlEncodedContent(keyValues);
                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);
                        string result = httpResponse.Content.ReadAsStringAsync().Result;
                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseNearbyClinics
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }
                        ResponseNearbyClinics response = JsonConvert.DeserializeObject<ResponseNearbyClinics>(result);
                        response.Success = true;
                        return response;
                    }
                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseNearbyClinics
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponseNearbyClinics
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }

        public async Task<ResponseDocuments> GetDocuments(RequestDocument request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);

                        FormUrlEncodedContent content = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>());
                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseDocuments
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseDocuments response = JsonConvert.DeserializeObject<ResponseDocuments>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseDocuments
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponseDocuments
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }

        public async Task<ResponseDocumentRegister> GetDocumentTypesRegister(RequestDocumentRegister request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);

                        FormUrlEncodedContent content = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>());
                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", "DataLists", request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseDocumentRegister
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseDocumentRegister response = JsonConvert.DeserializeObject<ResponseDocumentRegister>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseDocumentRegister
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponseDocumentRegister
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }

        public async Task<ResponseFamilyMembers> GetFamilyMembers(RequestFamilyMembers request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();

                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        List<KeyValuePair<string, string>> keyValues = new List<KeyValuePair<string, string>>();
                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.IdReference, request.IdReference));

                        FormUrlEncodedContent content = new FormUrlEncodedContent(keyValues);
                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseFamilyMembers
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseFamilyMembers response = JsonConvert.DeserializeObject<ResponseFamilyMembers>(result);
                        response.Success = true;
                        return response;
                    }
                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseFamilyMembers
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponseFamilyMembers
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }

        public async Task<ResponseLegalContent> GetLegalContent(RequestLegalContent request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        List<KeyValuePair<string, string>> keyValues = new List<KeyValuePair<string, string>>();
                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.Tag, request.Tag));

                        FormUrlEncodedContent content = new FormUrlEncodedContent(keyValues);
                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseLegalContent
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseLegalContent response = JsonConvert.DeserializeObject<ResponseLegalContent>(result);
                        response.Success = true;
                        return response;
                    }
                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseLegalContent
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponseLegalContent
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }

        public async Task<ResponseMedicalCenterSchedules> GetMedicalCenterSchedules(RequestMedicalCenterSchedules request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        List<KeyValuePair<string, string>> keyValues = new List<KeyValuePair<string, string>>();
                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.SpecialityCode, request.SpecialityCode));

                        FormUrlEncodedContent content = new FormUrlEncodedContent(keyValues);
                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseMedicalCenterSchedules
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseMedicalCenterSchedules response = JsonConvert.DeserializeObject<ResponseMedicalCenterSchedules>(result);
                        response.Success = true;
                        return response;
                    }
                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseMedicalCenterSchedules
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponseMedicalCenterSchedules
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }

        public async Task<ResponseServiceTypes> GetServicesTypes(RequestServiceTypes request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();

                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));
                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);

                        HttpResponseMessage httpResponse = await client.PostAsync(url, null);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseServiceTypes
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseServiceTypes response = JsonConvert.DeserializeObject<ResponseServiceTypes>(result);
                        response.Success = true;
                        return response;
                    }
                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseServiceTypes
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponseServiceTypes
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }

        public async Task<ResponsePendingCoordinations> GetPendingCoordinations(RequestPendingCoordinations request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        List<KeyValuePair<string, string>> keyValues = new List<KeyValuePair<string, string>>();
                        ILoginViewModel loginViewModel = ServiceLocator.Current.GetInstance<ILoginViewModel>();
                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.IdReference, loginViewModel.User.IdReference));

                        FormUrlEncodedContent content = new FormUrlEncodedContent(keyValues);
                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponsePendingCoordinations
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponsePendingCoordinations response = JsonConvert.DeserializeObject<ResponsePendingCoordinations>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponsePendingCoordinations
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponsePendingCoordinations
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }

        public async Task<ResponseLogin> Login(RequestLogin request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Profile, AppConfigurations.CodeProfile);
                        List<KeyValuePair<string, string>> keyValues = new List<KeyValuePair<string, string>>();
                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.UserName, request.Login.Email));
                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.Password, request.Login.Password));
                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.GrantType, "password"));
                        FormUrlEncodedContent content = new FormUrlEncodedContent(keyValues);
                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            ResponseErrorLogin errorLogin = JsonConvert.DeserializeObject<ResponseErrorLogin>(httpResponse.Content.ReadAsStringAsync().Result);
                            return new ResponseLogin
                            {
                                Message = errorLogin.Description,
                                StatusCode = errorLogin.Error,
                                Success = (errorLogin.Error == "16" || errorLogin.Error == "17" || errorLogin.Error == "18" || errorLogin.Error == "66" || errorLogin.Error == "401"),
                                Tittle = (errorLogin.Error == "16" || errorLogin.Error == "17" || errorLogin.Error == "66") ? "Datos incorrectos" : errorLogin.Error == "401" || errorLogin.Error == "18" ? "Usuario inactivo" : AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseLogin response = JsonConvert.DeserializeObject<ResponseLogin>(result);
                        response.Success = true;
                        response.StatusCode = "0";
                        return response;
                    }
                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseLogin
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Tittle = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponseLogin
                {
                    Error = new ResponseErrorLogin { Description = AppResources.WithoutConnection, Error = AppResources.WithoutConnection },
                    Success = false,
                    Tittle = AppResources.TitleWithoutConnection,
                };
            }
        }

        public async Task<ResponsePreRegister> Register(RequestRegister request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Profile, AppConfigurations.CodeProfile);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);

                        List<KeyValuePair<string, string>> keyValues = new List<KeyValuePair<string, string>>();
                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.BirthDate, request.Register.BirthDate.ToString(AppConfigurations.DateFormat)));
                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.CellPhone, request.Register.CellPhone));
                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.ConfirmPassword, request.Register.ConfirmPassword));
                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.Email, request.Register.Email));
                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.IdDocument, request.Register.IdDocument));
                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.LastNameOne, request.Register.LastNameOne));
                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.LastNameTwo, request.Register.LastNameTwo));
                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.NameOne, request.Register.NameOne));
                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.NameTwo, request.Register.NameTwo));
                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.Password, request.Register.Password));
                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.TermsAndConditions, request.Register.TermsAndConditions ? "true" : "false"));
                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.TypeDocument, request.Register.TypeDocument));
                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.UpdateEmail, request.Register.UpdateEmail ? "true" : "false"));
                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.Department, request.Register.Department));
                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.City, request.Register.City));
                        keyValues.Add(new KeyValuePair<string, string>("Gender", request.Register.Gender));

                        FormUrlEncodedContent content = new FormUrlEncodedContent(keyValues);
                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponsePreRegister
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponsePreRegister response = JsonConvert.DeserializeObject<ResponsePreRegister>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponsePreRegister
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponsePreRegister
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }

        public async Task<ResponsePreRegister> PreRegister(RequestPreRegister request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Profile, AppConfigurations.CodeProfile);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        List<KeyValuePair<string, string>> keyValues = new List<KeyValuePair<string, string>>();

                        keyValues.Add(new KeyValuePair<string, string>("document", request.Register.IdDocument));
                        keyValues.Add(new KeyValuePair<string, string>("documentType", request.Register.TypeDocument));
                        keyValues.Add(new KeyValuePair<string, string>("email", request.Register.Email));
                        keyValues.Add(new KeyValuePair<string, string>("confirmEmail", request.Register.Email));
                        keyValues.Add(new KeyValuePair<string, string>("termsAndConditions", request.Register.TermsAndConditions ? "true" : "false"));

                        FormUrlEncodedContent content = new FormUrlEncodedContent(keyValues);
                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponsePreRegister
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponsePreRegister response = JsonConvert.DeserializeObject<ResponsePreRegister>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponsePreRegister
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponsePreRegister
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }

        public async Task<ResponseSearchMember> SearchMember(RequestSearchMember request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        List<KeyValuePair<string, string>> keyValues = new List<KeyValuePair<string, string>>();

                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.Document, request.Number));
                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.DocumentType, request.DocumentType));

                        FormUrlEncodedContent content = new FormUrlEncodedContent(keyValues);
                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseSearchMember
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseSearchMember response = JsonConvert.DeserializeObject<ResponseSearchMember>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseSearchMember
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponseSearchMember
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }

        public async Task<ResponseBase> SendActivationEmail(RequestSendActivationEmail request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Profile, AppConfigurations.CodeProfile);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        List<KeyValuePair<string, string>> keyValues = new List<KeyValuePair<string, string>>();
                        keyValues.Add(new KeyValuePair<string, string>("User", request.User));

                        FormUrlEncodedContent content = new FormUrlEncodedContent(keyValues);
                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseBase
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseBase response = JsonConvert.DeserializeObject<ResponseBase>(result);
                        response.Success = true;
                        return response;
                    }
                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseBase
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponseBase
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }

        public async Task<ResponsePreRegister> SendVerificationCode(RequestSendVerificationCode request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        List<KeyValuePair<string, string>> keyValues = new List<KeyValuePair<string, string>>();
                        keyValues.Add(new KeyValuePair<string, string>("documentType", request.Register.TypeDocument));
                        keyValues.Add(new KeyValuePair<string, string>("document", request.Register.IdDocument));
                        keyValues.Add(new KeyValuePair<string, string>("email", request.Register.Email));
                        keyValues.Add(new KeyValuePair<string, string>("CellPhoneNumber", request.Register.CellPhone));

                        FormUrlEncodedContent content = new FormUrlEncodedContent(keyValues);
                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponsePreRegister
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponsePreRegister response = JsonConvert.DeserializeObject<ResponsePreRegister>(result);
                        response.Success = true;
                        return response;
                    }
                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponsePreRegister
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponsePreRegister
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }

        public async Task<ResponsePreRegister> RegisterUpdateEmail(RequestRegisterUpdateEmail request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        List<KeyValuePair<string, string>> keyValues = new List<KeyValuePair<string, string>>();

                        keyValues.Add(new KeyValuePair<string, string>("documentType", request.Register.IdDocument));
                        keyValues.Add(new KeyValuePair<string, string>("document", request.Register.TypeDocument));
                        keyValues.Add(new KeyValuePair<string, string>("email", request.Register.Email));
                        keyValues.Add(new KeyValuePair<string, string>("code", request.Register.CodeVerification));

                        FormUrlEncodedContent content = new FormUrlEncodedContent(keyValues);
                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponsePreRegister
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponsePreRegister response = JsonConvert.DeserializeObject<ResponsePreRegister>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponsePreRegister
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponsePreRegister
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }

        public async Task<ResponseRemoveMember> RemoveMember(RequestRemoveMember request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        List<KeyValuePair<string, string>> keyValues = new List<KeyValuePair<string, string>>();

                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.Document, request.Document));
                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.DocumentType, request.DocumentType));
                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.IdReference, request.IdReference));


                        FormUrlEncodedContent content = new FormUrlEncodedContent(keyValues);
                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseRemoveMember
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseRemoveMember response = JsonConvert.DeserializeObject<ResponseRemoveMember>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseRemoveMember
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponseRemoveMember
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }

        public async Task<ResponseCancelCoordination> CancelPendingCoordination(RequestCancelPendingCoordination request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        ILoginViewModel loginViewModel = ServiceLocator.Current.GetInstance<ILoginViewModel>();
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        List<KeyValuePair<string, string>> keyValues = new List<KeyValuePair<string, string>>();

                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.ClinicCode, request.PendingCoordination.MedicalCenter.ClinicCode));
                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.SpecialityCode, request.PendingCoordination.SpecialityCode));
                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.RDACode, request.PendingCoordination.RDACode));
                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.Document, request.PendingCoordination.Document));
                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.YearMonthDay, request.PendingCoordination.YearMonthDay));
                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.Time, request.PendingCoordination.Time));

                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.IdReference, loginViewModel.User.IdReference));
                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.AgendaType, request.PendingCoordination.AgendaType));

                        FormUrlEncodedContent content = new FormUrlEncodedContent(keyValues);
                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseCancelCoordination
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseCancelCoordination response = JsonConvert.DeserializeObject<ResponseCancelCoordination>(result);
                        response.Success = true;
                        return response;
                    }
                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseCancelCoordination
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponseCancelCoordination
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }

        public async Task<ResponseCancelPreRegister> CancelPreRegister(RequestCancelPreRegister request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        ILoginViewModel loginViewModel = ServiceLocator.Current.GetInstance<ILoginViewModel>();
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        List<KeyValuePair<string, string>> keyValues = new List<KeyValuePair<string, string>>();

                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.DocumentType, request.DocumentType));
                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.Document, request.Document));
                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.Email, request.Email));
                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.PhoneNumber, request.PhoneNumber));

                        FormUrlEncodedContent content = new FormUrlEncodedContent(keyValues);
                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseCancelPreRegister
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseCancelPreRegister response = JsonConvert.DeserializeObject<ResponseCancelPreRegister>(result);
                        response.Success = true;
                        return response;
                    }
                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseCancelPreRegister
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponseCancelPreRegister
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }

        public async Task<ResponseUpdateMember> UpdateMember(RequestUpdateMember request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        List<KeyValuePair<string, string>> keyValues = new List<KeyValuePair<string, string>>();

                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.Document, request.Document));
                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.DocumentType, request.DocumentType));
                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.Email, request.Email));
                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.IdReference, request.IdReference));
                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.Names, request.Names));
                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.Phone, request.Phone));
                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.Surnames, request.Surnames));

                        FormUrlEncodedContent content = new FormUrlEncodedContent(keyValues);
                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseUpdateMember
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseUpdateMember response = JsonConvert.DeserializeObject<ResponseUpdateMember>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseUpdateMember
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponseUpdateMember
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }

        public async Task<ResponseUpdateAffiliate> UpdateAffiliate(RequestUpdateAffiliate request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        List<KeyValuePair<string, string>> keyValues = new List<KeyValuePair<string, string>>();

                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.Channel, request.Channel));
                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.Document, request.Document));
                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.DocumentType, request.DocumentType));
                        keyValues.Add(new KeyValuePair<string, string>("CellPhoneNumber", request.CellPhoneNumber));
                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.Email, request.Email));

                        FormUrlEncodedContent content = new FormUrlEncodedContent(keyValues);
                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseUpdateAffiliate
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseUpdateAffiliate response = JsonConvert.DeserializeObject<ResponseUpdateAffiliate>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseUpdateAffiliate
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponseUpdateAffiliate
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }

        public async Task<ResponsePreConfirmNewMedicalCenterCoordination> PreConfirmNewMedicalCenterCoordination(RequestPreConfirmNewMedicalCenterCoordination request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        string values = JsonConvert.SerializeObject(request);
                        StringContent content = new StringContent(values, Encoding.UTF8, "application/json");

                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponsePreConfirmNewMedicalCenterCoordination
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponsePreConfirmNewMedicalCenterCoordination response = JsonConvert.DeserializeObject<ResponsePreConfirmNewMedicalCenterCoordination>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponsePreConfirmNewMedicalCenterCoordination
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponsePreConfirmNewMedicalCenterCoordination
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }

        public async Task<ResponseCoordinationPaymentMethod> GetPaymentMethods(RequestCoordinationPaymentMethod request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        string values = JsonConvert.SerializeObject(request);
                        StringContent content = new StringContent(values, Encoding.UTF8, "application/json");

                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseCoordinationPaymentMethod
                            {
                                Message = httpResponse.StatusCode.ToString(),
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseCoordinationPaymentMethod response = JsonConvert.DeserializeObject<ResponseCoordinationPaymentMethod>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseCoordinationPaymentMethod
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponseCoordinationPaymentMethod
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }

        public async Task<ResponsePayMedicalCenterCoordination> PayMedicalCenterCoordination(RequestPayMedicalCenterCoordination request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        string values = JsonConvert.SerializeObject(request);
                        StringContent content = new StringContent(values, Encoding.UTF8, "application/json");

                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponsePayMedicalCenterCoordination
                            {
                                Message = httpResponse.StatusCode.ToString(),
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponsePayMedicalCenterCoordination response = JsonConvert.DeserializeObject<ResponsePayMedicalCenterCoordination>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponsePayMedicalCenterCoordination
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponsePayMedicalCenterCoordination
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }

        public async Task<ResponseAllFaqsGroup> GetAllFaqsGroup(RequestAllFaqsGroup request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        string values = JsonConvert.SerializeObject(request);
                        StringContent content = new StringContent(values, Encoding.UTF8, "application/json");

                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseAllFaqsGroup
                            {
                                Message = httpResponse.StatusCode.ToString(),
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseAllFaqsGroup response = JsonConvert.DeserializeObject<ResponseAllFaqsGroup>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseAllFaqsGroup
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponseAllFaqsGroup
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }

        public async Task<ResponseFaqs> GetAllFaqs()
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        List<KeyValuePair<string, string>> keyValues = new List<KeyValuePair<string, string>>();
                        FormUrlEncodedContent content = new FormUrlEncodedContent(keyValues);

                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", AppConfigurations.ContentsController, "GetAllFaqs");
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseFaqs
                            {
                                Message = httpResponse.StatusCode.ToString(),
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseFaqs response = JsonConvert.DeserializeObject<ResponseFaqs>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseFaqs
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponseFaqs
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }

        public async Task<ResponseContactPhones> GetContactPhones(RequestContactPhones request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);

                        string values = JsonConvert.SerializeObject(request);
                        StringContent content = new StringContent(values, Encoding.UTF8, "application/json");

                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseContactPhones
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseContactPhones response = JsonConvert.DeserializeObject<ResponseContactPhones>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseContactPhones
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponseContactPhones
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }

        public async Task<ResponseServicesHistoryLists> GetServicesHistoryLists(RequestServicesHistoryLists request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);

                        string values = JsonConvert.SerializeObject(request);
                        StringContent content = new StringContent(values, Encoding.UTF8, "application/json");

                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseServicesHistoryLists
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseServicesHistoryLists response = JsonConvert.DeserializeObject<ResponseServicesHistoryLists>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseServicesHistoryLists
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponseServicesHistoryLists
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }

        public async Task<ResponseServicesHistory> GetServicesHistory(RequestServicesHistory request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);

                        string values = JsonConvert.SerializeObject(request);
                        StringContent content = new StringContent(values, Encoding.UTF8, "application/json");

                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseServicesHistory
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseServicesHistory response = JsonConvert.DeserializeObject<ResponseServicesHistory>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseServicesHistory
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponseServicesHistory
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }

        public async Task<ResponseSendServiceFile> SendServiceFile(RequestSendServiceFile request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);

                        string values = JsonConvert.SerializeObject(request);
                        StringContent content = new StringContent(values, Encoding.UTF8, "application/json");

                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseSendServiceFile
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseSendServiceFile response = JsonConvert.DeserializeObject<ResponseSendServiceFile>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseSendServiceFile
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponseSendServiceFile
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }

        public async Task<ResponseServiceFile> GetServiceFile(RequestServiceFile request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);

                        string values = JsonConvert.SerializeObject(request);
                        StringContent content = new StringContent(values, Encoding.UTF8, "application/json");

                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseServiceFile
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseServiceFile response = JsonConvert.DeserializeObject<ResponseServiceFile>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseServiceFile
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponseServiceFile
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }

        public async Task<ResponseSheduledServices> GetSheduledServices(RequestSheduledServices request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);

                        string values = JsonConvert.SerializeObject(request);
                        StringContent content = new StringContent(values, Encoding.UTF8, "application/json");

                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseSheduledServices
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseSheduledServices response = JsonConvert.DeserializeObject<ResponseSheduledServices>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseSheduledServices
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponseSheduledServices
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }

        public async Task<ResponseCancelService> CancelService(RequestCancelService request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);

                        string values = JsonConvert.SerializeObject(request);
                        StringContent content = new StringContent(values, Encoding.UTF8, "application/json");

                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseCancelService
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseCancelService response = JsonConvert.DeserializeObject<ResponseCancelService>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseCancelService
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponseCancelService
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }

        public async Task<ResponseServiceQualify> GetServiceQualify(RequestServiceQualify request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);

                        string values = JsonConvert.SerializeObject(request);
                        StringContent content = new StringContent(values, Encoding.UTF8, "application/json");

                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseServiceQualify
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseServiceQualify response = JsonConvert.DeserializeObject<ResponseServiceQualify>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseServiceQualify
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponseServiceQualify
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }

        public async Task<ResponseQualifyQuestion> QualifyQuestion(RequestQualifyQuestion request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        List<KeyValuePair<string, string>> keyValues = new List<KeyValuePair<string, string>>();

                        Qualifications serviceQualification = new Qualifications
                        {
                            ServiceQualification = request.ServiceQualification
                        };

                        string values = JsonConvert.SerializeObject(serviceQualification);
                        StringContent content = new StringContent(values, Encoding.UTF8, "application/json");


                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage response = await client.PostAsync(url, content);


                        if (!response.IsSuccessStatusCode)
                        {
                            return new ResponseQualifyQuestion
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await response.Content.ReadAsStringAsync();
                        ResponseQualifyQuestion response1 = JsonConvert.DeserializeObject<ResponseQualifyQuestion>(result);
                        response1.Success = true;
                        return response1;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseQualifyQuestion
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponseQualifyQuestion
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }

        public async Task<ResponseBeneficiaries> GetPersons(RequestPeople request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        string values = JsonConvert.SerializeObject(request);
                        StringContent content = new StringContent(values, Encoding.UTF8, "application/json");

                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseBeneficiaries
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseBeneficiaries response = JsonConvert.DeserializeObject<ResponseBeneficiaries>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseBeneficiaries
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponseBeneficiaries
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }

        public async Task<ResponseServicesEnabled> GetServicesEnabled(RequestServicesEnabled request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        string values = JsonConvert.SerializeObject(request);
                        StringContent content = new StringContent(values, Encoding.UTF8, "application/json");

                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseServicesEnabled
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseServicesEnabled response = JsonConvert.DeserializeObject<ResponseServicesEnabled>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseServicesEnabled
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponseServicesEnabled
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }

        public async Task<ResponseStandarizedAddressLists> GetStandarizedAddressLists(RequestStandarizedAddressLists request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        string values = JsonConvert.SerializeObject(request);
                        StringContent content = new StringContent(values, Encoding.UTF8, "application/json");

                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseStandarizedAddressLists
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseStandarizedAddressLists response = JsonConvert.DeserializeObject<ResponseStandarizedAddressLists>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseStandarizedAddressLists
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponseStandarizedAddressLists
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }

        public async Task<ResponseDepartments> GetDepartments(RequestDepartments request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        List<KeyValuePair<string, string>> keyValues = new List<KeyValuePair<string, string>>();

                        FormUrlEncodedContent content = new FormUrlEncodedContent(keyValues);
                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseDepartments
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseDepartments response = JsonConvert.DeserializeObject<ResponseDepartments>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseDepartments
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponseDepartments
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }

        public async Task<ResponseCities> GetCities(RequestCities request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        string values = JsonConvert.SerializeObject(request);
                        StringContent content = new StringContent(values, Encoding.UTF8, "application/json");

                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseCities
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseCities response = JsonConvert.DeserializeObject<ResponseCities>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseCities
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponseCities
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }

        public async Task<ResponseNeighborhoods> GetNeighborhoods(RequestNeighborhoods request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        List<KeyValuePair<string, string>> keyValues = new List<KeyValuePair<string, string>>();
                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.Value, request.CityCode));

                        FormUrlEncodedContent content = new FormUrlEncodedContent(keyValues);
                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseNeighborhoods
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseNeighborhoods response = JsonConvert.DeserializeObject<ResponseNeighborhoods>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseNeighborhoods
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponseNeighborhoods
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }

        public async Task<ResponseCoverage> GetCoverage(RequestCoverage request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        List<KeyValuePair<string, string>> keyValues = new List<KeyValuePair<string, string>>();
                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.Code, request.Code));

                        FormUrlEncodedContent content = new FormUrlEncodedContent(keyValues);
                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseCoverage
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseCoverage response = JsonConvert.DeserializeObject<ResponseCoverage>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseCoverage
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponseCoverage
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }

        public async Task<ResponseLocationInformationService> GetLocationInformationService(RequestLocationInformationService request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        string values = JsonConvert.SerializeObject(request);
                        StringContent content = new StringContent(values, Encoding.UTF8, "application/json");

                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseLocationInformationService
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseLocationInformationService response = JsonConvert.DeserializeObject<ResponseLocationInformationService>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseLocationInformationService
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponseLocationInformationService
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }

        public async Task<ResponseValidateCoverage> ValidateCoverage(RequestValidateCoverage request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        string values = JsonConvert.SerializeObject(request);
                        StringContent content = new StringContent(values, Encoding.UTF8, "application/json");

                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseValidateCoverage
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseValidateCoverage response = JsonConvert.DeserializeObject<ResponseValidateCoverage>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseValidateCoverage
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponseValidateCoverage
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }

        public async Task<ResponseMedicalService> HomeHealthCare(RequestMedicalService request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        string values = JsonConvert.SerializeObject(request);
                        StringContent content = new StringContent(values, Encoding.UTF8, "application/json");

                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseMedicalService
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseMedicalService response = JsonConvert.DeserializeObject<ResponseMedicalService>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseMedicalService
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponseMedicalService
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }

        public async Task<ResponseMedicalService> MedicalOrientation(RequestMedicalService request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        string values = JsonConvert.SerializeObject(request);
                        StringContent content = new StringContent(values, Encoding.UTF8, "application/json");

                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseMedicalService
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseMedicalService response = JsonConvert.DeserializeObject<ResponseMedicalService>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseMedicalService
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponseMedicalService
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }

        public async Task<ResponseGenders> GetGenders()
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    Request request = new Request
                    {
                        Controller = "DataLists",
                        Action = "GetGenders"
                    };
                    using (HttpClient client = new HttpClient())
                    {

                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.GetAsync(url);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseGenders
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseGenders response = JsonConvert.DeserializeObject<ResponseGenders>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseGenders
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }

            return new ResponseGenders
            {
                Message = AppResources.WithoutConnection,
                Success = false,
                Title = AppResources.TitleWithoutConnection,
            };
        }

        public async Task<ResponseServiceChatAgent> GetServiceChatAgent(RequestServiceChatAgent request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        List<KeyValuePair<string, string>> keyValues = new List<KeyValuePair<string, string>>();

                        keyValues.Add(new KeyValuePair<string, string>("Value", request.Value));

                        FormUrlEncodedContent content = new FormUrlEncodedContent(keyValues);
                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);


                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseServiceChatAgent
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseServiceChatAgent response = JsonConvert.DeserializeObject<ResponseServiceChatAgent>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseServiceChatAgent
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponseServiceChatAgent
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }

        public async Task<ResponseSpecialities> GetSpecialties(RequestSpecialities request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        string values = JsonConvert.SerializeObject(request);
                        StringContent content = new StringContent(values, Encoding.UTF8, "application/json");

                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseSpecialities
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseSpecialities response = JsonConvert.DeserializeObject<ResponseSpecialities>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseSpecialities
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponseSpecialities
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }

        }

        public async Task<ResponseSoftwareVersion> GetSoftwareVersion(RequestSoftwareVersion request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        string values = JsonConvert.SerializeObject(request);
                        StringContent content = new StringContent(values, Encoding.UTF8, "application/json");

                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseSoftwareVersion
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseSoftwareVersion response = JsonConvert.DeserializeObject<ResponseSoftwareVersion>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseSoftwareVersion
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponseSoftwareVersion
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }

        public async Task<ResponseExistsMedicalHomeService> GetExistsMedicalHomeService(RequestExistsMedicalHomeService request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        string values = JsonConvert.SerializeObject(request);
                        StringContent content = new StringContent(values, Encoding.UTF8, "application/json");

                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseExistsMedicalHomeService
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseExistsMedicalHomeService response = JsonConvert.DeserializeObject<ResponseExistsMedicalHomeService>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseExistsMedicalHomeService
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponseExistsMedicalHomeService
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }

        public async Task<ResponseCancelMedicalHomeService> CancelMedicalHomeService(RequestCancelMedicalHomeService request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        List<KeyValuePair<string, string>> keyValues = new List<KeyValuePair<string, string>>();

                        keyValues.Add(new KeyValuePair<string, string>("IdService", request.IdService));

                        FormUrlEncodedContent content = new FormUrlEncodedContent(keyValues);
                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseCancelMedicalHomeService
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseCancelMedicalHomeService response = JsonConvert.DeserializeObject<ResponseCancelMedicalHomeService>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseCancelMedicalHomeService
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponseCancelMedicalHomeService
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }

        public async Task<ResponseStatusInvoices> GetStatusInvoices(RequestStatusInvoice request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        List<KeyValuePair<string, string>> keyValues = new List<KeyValuePair<string, string>>();

                        FormUrlEncodedContent content = new FormUrlEncodedContent(keyValues);
                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);


                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseStatusInvoices
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseStatusInvoices response = JsonConvert.DeserializeObject<ResponseStatusInvoices>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseStatusInvoices
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponseStatusInvoices
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }

        public async Task<ResponseValidateUserYoung> ValidateUserYoung(RequestValidateUserYoung request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        List<KeyValuePair<string, string>> keyValues = new List<KeyValuePair<string, string>>();

                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.Document, request.Document));
                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.DocumentType, request.DocumentType));

                        FormUrlEncodedContent content = new FormUrlEncodedContent(keyValues);
                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseValidateUserYoung
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseValidateUserYoung response = JsonConvert.DeserializeObject<ResponseValidateUserYoung>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseValidateUserYoung
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponseValidateUserYoung
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }

        public async Task<ResponseInvoices> GetInvoices(RequestInvoices request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        List<KeyValuePair<string, string>> keyValues = new List<KeyValuePair<string, string>>();

                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.Document, request.Document));
                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.DocumentType, request.DocumentType));
                        keyValues.Add(new KeyValuePair<string, string>("IdStatusInvoice", request.IdStatusInvoice));
                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.InitDate, request.InitDate));
                        keyValues.Add(new KeyValuePair<string, string>(AppConfigurations.EndDate, request.EndDate));

                        FormUrlEncodedContent content = new FormUrlEncodedContent(keyValues);
                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseInvoices
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseInvoices response = JsonConvert.DeserializeObject<ResponseInvoices>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseInvoices
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponseInvoices
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }

        public async Task<ResponseInvoiceDetail> GetInvoiceDetail(RequestInvoiceDetail request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);

                        string values = JsonConvert.SerializeObject(request);
                        StringContent content = new StringContent(values, Encoding.UTF8, "application/json");

                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseInvoiceDetail
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseInvoiceDetail response = JsonConvert.DeserializeObject<ResponseInvoiceDetail>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseInvoiceDetail
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponseInvoiceDetail
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }

        public async Task<ResponseMinorAuthorizations> GetMinorAuthorizations(RequestMinorAuthorizations request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        string values = JsonConvert.SerializeObject(request);
                        StringContent content = new StringContent(values, Encoding.UTF8, "application/json");

                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseMinorAuthorizations
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseMinorAuthorizations response = JsonConvert.DeserializeObject<ResponseMinorAuthorizations>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseMinorAuthorizations
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }

            return new ResponseMinorAuthorizations
            {
                Message = AppResources.WithoutConnection,
                Success = false,
                Title = AppResources.TitleWithoutConnection,
            };
        }

        public ApiService(INetworkService networkService, IExceptionService exceptionService, ILoginViewModel loginViewModel, INavigationService navigationService, IDialogService dialogService)
        {
            this.exceptionService = exceptionService;
            this.networkService = networkService;
            this.navigationService = navigationService;
            this.dialogService = dialogService;
        }


        public async Task<RefundTypeResponse> GetRefundType()
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    RefundTypeRequest request = new RefundTypeRequest();

                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Profile, AppConfigurations.CodeProfile);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.GetAsync(url);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new RefundTypeResponse
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        RefundTypeResponse response = JsonConvert.DeserializeObject<RefundTypeResponse>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new RefundTypeResponse
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }

            return new RefundTypeResponse
            {
                Message = AppResources.WithoutConnection,
                Success = false,
                Title = AppResources.TitleWithoutConnection,
            };
        }

        public async Task<ResponseOpenTokDataForAffiliate> GetOpenTokDataForAffiliate(RequestMedicalService request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        string values = JsonConvert.SerializeObject(request);
                        StringContent content = new StringContent(values, Encoding.UTF8, "application/json");

                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseOpenTokDataForAffiliate
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseOpenTokDataForAffiliate response = JsonConvert.DeserializeObject<ResponseOpenTokDataForAffiliate>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseOpenTokDataForAffiliate
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponseOpenTokDataForAffiliate
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }

        public async Task<ResponseBase> ChangeEmail(RequestEmail request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Profile, AppConfigurations.CodeProfile);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        string values = JsonConvert.SerializeObject(request);
                        StringContent content = new StringContent(values, Encoding.UTF8, "application/json");

                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseBase
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseBase response = JsonConvert.DeserializeObject<ResponseBase>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseBase
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }

            return new ResponseBase
            {
                Message = AppResources.WithoutConnection,
                Success = false,
                Title = AppResources.TitleWithoutConnection,
            };
        }

        public async Task<ResponseDeactivationType> DeactivationType()
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    RequestDeactivationType request = new RequestDeactivationType();

                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Profile, AppConfigurations.CodeProfile);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.GetAsync(url);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseDeactivationType
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseDeactivationType response = JsonConvert.DeserializeObject<ResponseDeactivationType>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseDeactivationType
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }

            return new ResponseDeactivationType
            {
                Message = AppResources.WithoutConnection,
                Success = false,
                Title = AppResources.TitleWithoutConnection,
            };
        }

        public async Task<ResponseBase> DisableAccount(RequestDeactivateUserAccount request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Profile, AppConfigurations.CodeProfile);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        string values = JsonConvert.SerializeObject(request);
                        StringContent content = new StringContent(values, Encoding.UTF8, "application/json");

                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseBase
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseBase response = JsonConvert.DeserializeObject<ResponseBase>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseBase
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }

            return new ResponseBase
            {
                Message = AppResources.WithoutConnection,
                Success = false,
                Title = AppResources.TitleWithoutConnection,
            };
        }

        public async Task<ResponseAsociatedUserAccounts> GetAsociatedUserAccounts(RequestAsociatedUserAccounts request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Profile, AppConfigurations.CodeProfile);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        string values = JsonConvert.SerializeObject(request);
                        StringContent content = new StringContent(values, Encoding.UTF8, "application/json");

                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseAsociatedUserAccounts
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseAsociatedUserAccounts response = JsonConvert.DeserializeObject<ResponseAsociatedUserAccounts>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseAsociatedUserAccounts
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }

            return new ResponseAsociatedUserAccounts
            {
                Message = AppResources.WithoutConnection,
                Success = false,
                Title = AppResources.TitleWithoutConnection,
            };
        }

        public async Task<ResponseBase> VerifyCodeForgortPassword(RequestVerifyCodeForgortPassword request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Profile, AppConfigurations.CodeProfile);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        string values = JsonConvert.SerializeObject(request);
                        StringContent content = new StringContent(values, Encoding.UTF8, "application/json");

                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseBase
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseBase response = JsonConvert.DeserializeObject<ResponseBase>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseBase
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }

            return new ResponseBase
            {
                Message = AppResources.WithoutConnection,
                Success = false,
                Title = AppResources.TitleWithoutConnection,
            };
        }

        public async Task<ResponseBase> SetPassword(RequestSetPassword request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Profile, AppConfigurations.CodeProfile);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        string values = JsonConvert.SerializeObject(request);
                        StringContent content = new StringContent(values, Encoding.UTF8, "application/json");

                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseBase
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseBase response = JsonConvert.DeserializeObject<ResponseBase>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseBase
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }

            return new ResponseBase
            {
                Message = AppResources.WithoutConnection,
                Success = false,
                Title = AppResources.TitleWithoutConnection,
            };
        }

        public async Task<ResponseAuthorizeChangePassword> AuthorizeChangePasswordByCellPhone(RequestAuthorizeChangePassword request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Profile, AppConfigurations.CodeProfile);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        string values = JsonConvert.SerializeObject(request);
                        StringContent content = new StringContent(values, Encoding.UTF8, "application/json");

                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseAuthorizeChangePassword
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseAuthorizeChangePassword response = JsonConvert.DeserializeObject<ResponseAuthorizeChangePassword>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseAuthorizeChangePassword
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }

            return new ResponseAuthorizeChangePassword
            {
                Message = AppResources.WithoutConnection,
                Success = false,
                Title = AppResources.TitleWithoutConnection,
            };
        }

        public async Task<ResponseOpenTokDataForAffiliate> GetOpenTokRoutedSession(int retryNumber = 0)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", AppConfigurations.VideoCallController, AppConfigurations.GetOpenTokRoutedSession);
                        HttpResponseMessage httpResponse = await client.GetAsync(url);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseOpenTokDataForAffiliate
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseOpenTokDataForAffiliate response = JsonConvert.DeserializeObject<ResponseOpenTokDataForAffiliate>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    if (retryNumber < 2)
                        return await GetOpenTokRoutedSession(retryNumber++);
                    exceptionService.RegisterException(ex);
                    return new ResponseOpenTokDataForAffiliate
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponseOpenTokDataForAffiliate
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }

        public async Task<ResponseOpenTokDataForAffiliate> SendRatingCall(RequestSendRatingCall request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    string url = string.Format("/{0}/{1}", AppConfigurations.VideoCallController, AppConfigurations.PostCallRating);
                    HttpResponseMessage httpResponse = default(HttpResponseMessage);

                    string values = JsonConvert.SerializeObject(request);
                    StringContent content = new StringContent(values, Encoding.UTF8, "application/json");

                    using (HttpClient client = GetHttpClient())
                    {
                        client.BaseAddress = new Uri(urlBase);
                        httpResponse = await client.PostAsync(url, content);
                    }

                    if (!httpResponse.IsSuccessStatusCode)
                    {
                        return new ResponseOpenTokDataForAffiliate
                        {
                            Message = AppResources.WithoutConnection,
                            Success = false,
                            Title = AppResources.TitleWithoutConnection,
                        };
                    }

                    string result = await httpResponse.Content.ReadAsStringAsync();
                    ResponseOpenTokDataForAffiliate response = JsonConvert.DeserializeObject<ResponseOpenTokDataForAffiliate>(result);
                    response.Success = true;
                    return response;

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseOpenTokDataForAffiliate
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponseOpenTokDataForAffiliate
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }

        public async Task<ResponseVersion> ValidateVersion()
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    RequestVersion request = new RequestVersion();

                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Profile, AppConfigurations.CodeProfile);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, $"{request.Action}?devicePlatform={(Device.RuntimePlatform == Device.iOS ? 1 : 0)}");
                        HttpResponseMessage httpResponse = await client.GetAsync(url);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseVersion
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseVersion response = JsonConvert.DeserializeObject<ResponseVersion>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseVersion
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }

            return new ResponseVersion
            {
                Message = AppResources.WithoutConnection,
                Success = false,
                Title = AppResources.TitleWithoutConnection,
            };
        }

        public async Task<RealTimeConfigurationResponse> GetFirebaseConfiguration()
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    RequestRealTimeConfiguration request = new RequestRealTimeConfiguration();

                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Profile, AppConfigurations.CodeProfile);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.GetAsync(url);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new RealTimeConfigurationResponse
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        RealTimeConfigurationResponse response = JsonConvert.DeserializeObject<RealTimeConfigurationResponse>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new RealTimeConfigurationResponse
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }

            return new RealTimeConfigurationResponse
            {
                Message = AppResources.WithoutConnection,
                Success = false,
                Title = AppResources.TitleWithoutConnection,
            };
        }

        public async Task<PostPatientServiceTypeResponse> PostPatientServiceType(RequestPostPatientServiceType request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Profile, AppConfigurations.CodeProfile);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        string values = JsonConvert.SerializeObject(request);
                        StringContent content = new StringContent(values, Encoding.UTF8, "application/json");

                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new PostPatientServiceTypeResponse
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        PostPatientServiceTypeResponse response = JsonConvert.DeserializeObject<PostPatientServiceTypeResponse>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new PostPatientServiceTypeResponse
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }

            return new PostPatientServiceTypeResponse
            {
                Message = AppResources.WithoutConnection,
                Success = false,
                Title = AppResources.TitleWithoutConnection,
            };
        }

        public async Task<ReasonsAbandonmentResponse> GetReasonsAbandonment()
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    RequestReasonsAbandonment request = new RequestReasonsAbandonment();
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Profile, AppConfigurations.CodeProfile);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));
                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.GetAsync(url);
                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ReasonsAbandonmentResponse
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }
                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ReasonsAbandonmentResponse response = JsonConvert.DeserializeObject<ReasonsAbandonmentResponse>(result);
                        response.Success = true;
                        return response;
                    }
                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ReasonsAbandonmentResponse
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            return new ReasonsAbandonmentResponse
            {
                Message = AppResources.WithoutConnection,
                Success = false,
                Title = AppResources.TitleWithoutConnection,
            };
        }

        public async Task<PostIntentResponse> PostIntent(RequestMedicalService request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Profile, AppConfigurations.CodeProfile);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));
                        string values = JsonConvert.SerializeObject(request);
                        StringContent content = new StringContent(values, Encoding.UTF8, "application/json");
                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);
                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new PostIntentResponse
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }
                        string result = await httpResponse.Content.ReadAsStringAsync();
                        PostIntentResponse response = JsonConvert.DeserializeObject<PostIntentResponse>(result);
                        response.Success = true;
                        return response;
                    }
                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new PostIntentResponse
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            return new PostIntentResponse
            {
                Message = AppResources.WithoutConnection,
                Success = false,
                Title = AppResources.TitleWithoutConnection,
            };
        }

        public async Task<ResponseMenu> GetMenuDELETE(string token)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    var request = new Request
                    {
                        Action = "GetListAssociatedMenus",
                        Controller = "common"
                    };

                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Profile, AppConfigurations.CodeProfile);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));
                        client.DefaultRequestHeaders.Add("AppName", "AppUcm");
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.GetAsync(url);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseMenu
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseMenu response = JsonConvert.DeserializeObject<ResponseMenu>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseMenu
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }

            return new ResponseMenu
            {
                Message = AppResources.WithoutConnection,
                Success = false,
                Title = AppResources.TitleWithoutConnection,
            };
        }

        public async Task<ResponseMenu> GetMenu(Request request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Profile, AppConfigurations.CodeProfile);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));
                        client.DefaultRequestHeaders.Add("AppName", "AppUcm");

                        string values = JsonConvert.SerializeObject(request);
                        StringContent content = new StringContent(values, Encoding.UTF8, "application/json");

                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
                            {
                                ILoginViewModel log = ServiceLocator.Current.GetInstance<ILoginViewModel>();
                                return await GetMenuDELETE(log.User.Access_token);
                            }
                            return new ResponseMenu
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseMenu response = JsonConvert.DeserializeObject<ResponseMenu>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseMenu
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }

            return new ResponseMenu
            {
                Message = AppResources.WithoutConnection,
                Success = false,
                Title = AppResources.TitleWithoutConnection,
            };
        }

        public async Task<ResponseCards> GetAffiliateCard(Request request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        string values = JsonConvert.SerializeObject(request);
                        StringContent content = new StringContent(values, Encoding.UTF8, "application/json");

                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseCards
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseCards response = JsonConvert.DeserializeObject<ResponseCards>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseCards
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponseCards
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }


        public async Task<ResponseValidateRPWithdrawal> ValidateRPWithdrawalRetired(Request request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Profile, AppConfigurations.CodeProfile);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        string values = JsonConvert.SerializeObject(request);
                        StringContent content = new StringContent(values, Encoding.UTF8, "application/json");

                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseValidateRPWithdrawal
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseValidateRPWithdrawal response = JsonConvert.DeserializeObject<ResponseValidateRPWithdrawal>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseValidateRPWithdrawal
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }

            return new ResponseValidateRPWithdrawal
            {
                Message = AppResources.WithoutConnection,
                Success = false,
                Title = AppResources.TitleWithoutConnection,
            };
        }

        public async Task<ResponseExpiredMedicalServices> GetExpiredProducts(RequestExpiredMedicalServices request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        string values = JsonConvert.SerializeObject(request);
                        StringContent content = new StringContent(values, Encoding.UTF8, "application/json");

                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseExpiredMedicalServices
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseExpiredMedicalServices response = JsonConvert.DeserializeObject<ResponseExpiredMedicalServices>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseExpiredMedicalServices
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponseExpiredMedicalServices
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }

        public async Task<ResponseNewCoverage> GetNewCoverage(RequestNewCoverage request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        string values = JsonConvert.SerializeObject(request);
                        StringContent content = new StringContent(values, Encoding.UTF8, "application/json");

                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseNewCoverage
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseNewCoverage response = JsonConvert.DeserializeObject<ResponseNewCoverage>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseNewCoverage
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponseNewCoverage
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }


        public async Task<ResponseNewCoverage> GetCoverageLatLong(RequestCoverageLatLong request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        string values = JsonConvert.SerializeObject(request);
                        StringContent content = new StringContent(values, Encoding.UTF8, "application/json");

                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseNewCoverage
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseNewCoverage response = JsonConvert.DeserializeObject<ResponseNewCoverage>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseNewCoverage
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponseNewCoverage
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }

        public async Task<ResponseNewVideocall> NewVideocallPetition(RequestNewVideoCall request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        string values = JsonConvert.SerializeObject(request);
                        StringContent content = new StringContent(values, Encoding.UTF8, "application/json");

                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseNewVideocall
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseNewVideocall response = JsonConvert.DeserializeObject<ResponseNewVideocall>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseNewVideocall
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponseNewVideocall
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }

        public async Task<ResponsePediatricAgendas> GetPediatricAgendas(RequestPediatricAgendas request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        string values = JsonConvert.SerializeObject(request);
                        StringContent content = new StringContent(values, Encoding.UTF8, "application/json");

                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponsePediatricAgendas
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponsePediatricAgendas response = JsonConvert.DeserializeObject<ResponsePediatricAgendas>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponsePediatricAgendas
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponsePediatricAgendas
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }

        public async Task<ResponsePediatricPetition> CreatePediatricPetition(RequestPediatricAttention request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        string values = JsonConvert.SerializeObject(request);
                        StringContent content = new StringContent(values, Encoding.UTF8, "application/json");

                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponsePediatricPetition
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponsePediatricPetition response = JsonConvert.DeserializeObject<ResponsePediatricPetition>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponsePediatricPetition
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponsePediatricPetition
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }

        public async Task<ResponseValidatePediatricServices> ValidatePediatricServices(Request request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        string values = JsonConvert.SerializeObject(request);
                        StringContent content = new StringContent(values, Encoding.UTF8, "application/json");

                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseValidatePediatricServices
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseValidatePediatricServices response = JsonConvert.DeserializeObject<ResponseValidatePediatricServices>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseValidatePediatricServices
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponseValidatePediatricServices
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }
public async Task<ResponseRegisterMinor> RegisterMinor(RequestRegisterMinor request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        string values = JsonConvert.SerializeObject(request);
                        StringContent content = new StringContent(values, Encoding.UTF8, "application/json");

                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseRegisterMinor
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseRegisterMinor response = JsonConvert.DeserializeObject<ResponseRegisterMinor>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseRegisterMinor
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponseRegisterMinor
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }

        public async Task<ReponseYoungMembers> GetYoungMembers(Request request)
        {
            if (networkService.IsNetworkAvailable)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        string values = JsonConvert.SerializeObject(request);
                        StringContent content = new StringContent(values, Encoding.UTF8, "application/json");

                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.PostAsync(url, content);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ReponseYoungMembers
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ReponseYoungMembers response = JsonConvert.DeserializeObject<ReponseYoungMembers>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ReponseYoungMembers
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ReponseYoungMembers
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }
        public async Task<ResponseContingencyMessages> GetContingencyMessages()
        {
            if (networkService.IsNetworkAvailable)
            {
                var request = new Request
                {
                    Action = "EnableMessages",
                    Controller = "ContingencyMessages"
                };
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add(AppConfigurations.EnvironmentString, AppConfigurations.Environment);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        client.DefaultRequestHeaders.Add(AppConfigurations.OcpApimSubscriptionKey, AppConfigurations.Subscriptionkey);
                        client.DefaultRequestHeaders.Add(AppConfigurations.Channel, AppConfigurations.ChannelKey);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppConfigurations.ApplicationJson));

                        client.BaseAddress = new Uri(urlBase);
                        string url = string.Format("/{0}/{1}", request.Controller, request.Action);
                        HttpResponseMessage httpResponse = await client.GetAsync(url);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                await dialogService.ShowMessage(AppConfigurations.UnauthorizedUserTitle, AppConfigurations.UnauthorizedUser);
                                await navigationService.Navigate(Logic.Enumerations.AppPages.LoginPage);
                            }
                            return new ResponseContingencyMessages
                            {
                                Message = AppResources.WithoutConnection,
                                Success = false,
                                Title = AppResources.TitleWithoutConnection,
                            };
                        }

                        string result = await httpResponse.Content.ReadAsStringAsync();
                        ResponseContingencyMessages response = JsonConvert.DeserializeObject<ResponseContingencyMessages>(result);
                        response.Success = true;
                        return response;
                    }

                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                    return new ResponseContingencyMessages
                    {
                        Message = AppResources.WithoutConnection,
                        Success = false,
                        Title = AppResources.TitleWithoutConnection,
                    };
                }
            }
            else
            {
                return new ResponseContingencyMessages
                {
                    Message = AppResources.WithoutConnection,
                    Success = false,
                    Title = AppResources.TitleWithoutConnection,
                };
            }
        }
    }
}
