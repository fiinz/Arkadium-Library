using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ArkadiumLibrary.Models;
using ArkadiumLibrary.ViewModels.ContactsBook.ViewModels;
using ArkadiumLibrary.Views;
using Xamarin.Forms;

namespace ArkadiumLibrary.ViewModels
{
    public class BooksViewModel : BaseViewModel
    {
        //realm db 
        private readonly IBookStore _bookStore;


        /// <summary>
        ///     Book that Was Selected in BooksPage
        /// </summary>
        private BookViewModel _selectedBook;

        //navigation and alert services
        private readonly IService _service;

        public BooksViewModel()
        {
            MessagingCenter.Subscribe<BookDetailsViewModel, Book>
                (this, EventType.BookAdded, OnBookAdded);

            MessagingCenter.Subscribe<BookDetailsViewModel, Book>
                (this, EventType.BookUpdated, OnBookUpdated);
        }


        public BooksViewModel(IBookStore bookStore, IService service)
        {
            _bookStore = bookStore;
            _service = service;
            LoadDataCommand = new Command(() => LoadData());
            CreateNewBookCommand = new Command(async () => await CreateNewBook());
            SelectBookCommand = new Command<BookViewModel>(async b => await SelectBook(b));
        }

        public ObservableCollection<BookViewModel> CollectionOfBookViewModels { get; }
            = new ObservableCollection<BookViewModel>();

        public BookViewModel SelectedBook
        {
            get => _selectedBook;
            set => SetValue(ref _selectedBook, value);
        }

        public ICommand LoadDataCommand { get; }
        public ICommand CreateNewBookCommand { get; }
        public ICommand SelectBookCommand { get; }

        private void OnBookAdded(BookDetailsViewModel source, Book book)
        {
            CollectionOfBookViewModels.Add(new BookViewModel(book));
        }

        private void OnBookUpdated(BookDetailsViewModel source, Book book)
        {
            var bookViewModelInList = CollectionOfBookViewModels.Single(bvm => bvm.Id == book.id);

            bookViewModelInList.Id = book.id;
            bookViewModelInList.Title = book.title;
            bookViewModelInList.Description = book.description;
            bookViewModelInList.Year = book.year;
            bookViewModelInList.IsFavorite = book.isFavorite;
        }

        private async Task CreateNewBook()
        {
            await _service.PushAsync(new BookDetailsPage(new BookViewModel()));
        }


        private async Task SelectBook(BookViewModel bookViewModel)
        {
            if (bookViewModel == null)
                return;

            SelectedBook = null;
            await _service.PushAsync(new BookDetailsPage(bookViewModel));
        }

        private void LoadData()
        {
            var books = _bookStore.GetBooks();
            CollectionOfBookViewModels.Clear();
            foreach (var book in books) CollectionOfBookViewModels.Add(new BookViewModel(book));
        }
    }
}