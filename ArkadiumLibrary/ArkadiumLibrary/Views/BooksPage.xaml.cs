using ArkadiumLibrary.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ArkadiumLibrary.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BooksPage : ContentPage
    {
        public BooksPage()
        {
            var bookStore = new BookStore();
            var service = new Service();
            ViewModel = new BooksViewModel(bookStore, service);

            InitializeComponent();
        }

        public BooksViewModel ViewModel
        {
            get => BindingContext as BooksViewModel;
            set => BindingContext = value;
        }


        protected override void OnAppearing()
        {
            ViewModel.LoadDataCommand.Execute(null); // loads data when 
            base.OnAppearing();
        }

        private void OnBookSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ViewModel.SelectBookCommand.Execute(e.SelectedItem);
        }
    }
}