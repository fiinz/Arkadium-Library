using System.Threading.Tasks;
using Xamarin.Forms;

namespace ArkadiumLibrary.ViewModels
{
    public class Service : IService
    {
        private Page MainPage => Application.Current.MainPage;

        public async Task PushAsync(Page page)
        {
            await MainPage.Navigation.PushAsync(page);
        }

        public async Task<Page> PopAsync()
        {
            return await MainPage.Navigation.PopAsync();
        }

        public async Task<bool> DisplayAlert(string title, string message, string ok, string cancel)
        {
            //captures feedback from user true or false 
            return await MainPage.DisplayAlert(title, message, ok, cancel);
        }

        public async Task DisplayAlert(string title, string message, string ok)
        {
            await MainPage.DisplayAlert(title, message, ok);
        }
    }
}