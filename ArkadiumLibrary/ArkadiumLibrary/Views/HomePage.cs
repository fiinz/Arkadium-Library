using Xamarin.Forms;

namespace ArkadiumLibrary.Views
{
    public class HomePage : ContentPage
    {
        public HomePage()
        {
            /*
            Title = "Arkadium Library";
            var stackLayout = new StackLayout {Padding = new Thickness(10)};
            var instructions = new Label
                {Text = "Select an Option:", HorizontalOptions = LayoutOptions.Center, Padding = new Thickness(10)};
            var listBooks = new Button {Text = "List Books"};
            listBooks.Command = NavToListBooks;
            var addNewBookBtn = new Button {Text = "Add Book"};
            // addNewBookBtn.Clicked += async (sender, args) => await (Navigation.PushAsync(new BookCreationPage()));
            // addNewBookBtn.Command = NavToCreateCommand;

            //add Components to Layout
            stackLayout.Children.Add(instructions);
            stackLayout.Children.Add(listBooks);
            stackLayout.Children.Add(addNewBookBtn);

            //set content to this view
            Content = stackLayout;
            */
        }

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