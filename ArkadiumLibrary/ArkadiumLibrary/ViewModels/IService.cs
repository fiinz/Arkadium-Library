using System.Threading.Tasks;
using Xamarin.Forms;

namespace ArkadiumLibrary.ViewModels
{
    /// <summary>
    ///     Interface to control and add logic to Services such as Navigation and Alerts
    /// </summary>
    public interface IService
    {
        Task PushAsync(Page page);
        Task<Page> PopAsync();
        Task<bool> DisplayAlert(string title, string message, string ok, string cancel);
        Task DisplayAlert(string title, string message, string ok);
    }
}