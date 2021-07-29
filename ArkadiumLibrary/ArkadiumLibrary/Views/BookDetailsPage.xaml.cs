using ArkadiumLibrary.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ArkadiumLibrary.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookDetailsPage : ContentPage
    {
        public BookDetailsPage()
        {
            InitializeComponent();
        }

        public BookDetailsPage(BookViewModel bookViewModel)
        {
            InitializeComponent();
            var bookStore = new BookStore();
            var service = new Service();
            Title = "New Book";
            BindingContext = new BookDetailsViewModel(bookViewModel ?? new BookViewModel(), bookStore, service);

            if (bookViewModel.Title != null)
            {
                Title = "Edit Book";
                var item = new ToolbarItem
                {
                    Text = "Delete",
                    Order = ToolbarItemOrder.Primary,
                    Command = (BindingContext as BookDetailsViewModel).DeleteBookCommand,
                    Priority = 0
                };
                ToolbarItems.Add(item);
            }
        }
    }
}