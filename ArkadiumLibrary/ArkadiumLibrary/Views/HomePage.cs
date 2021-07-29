using Xamarin.Forms;

namespace ArkadiumLibrary.Views
{
    public class HomePage : ContentPage
    {
        public Command NavToListBooks
        {
            get
            {
                return new Command(async () =>
                {
                    await Application.Current.MainPage.Navigation.PushAsync(new BooksPage());
                });
            }
        }
    }
}