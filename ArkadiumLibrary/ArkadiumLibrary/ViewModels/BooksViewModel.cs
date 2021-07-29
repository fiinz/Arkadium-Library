using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ArkadiumLibrary.Models;
using ArkadiumLibrary.ViewModels.ContactsBook.ViewModels;
using ArkadiumLibrary.Views;
using Realms;
using Xamarin.Forms;

namespace ArkadiumLibrary.ViewModels
{
    public class BooksViewModel : BaseViewModel
    {
   
        //realm db 
        private IBookStore _bookStore;
        //navigation and alert services
        private IService _service;
        
        public ObservableCollection<BookViewModel> CollectionOfBookViewModels { get; private set; }
            = new ObservableCollection<BookViewModel>();
            
        
        /// <summary>
        /// Book that Was Selected in BooksPage
        /// </summary>
        private BookViewModel _selectedBook;
        public BookViewModel SelectedBook
        {
            get => _selectedBook;
            set => SetValue(ref _selectedBook, value);
        }
        public ICommand LoadDataCommand { get; private set; }
        public ICommand CreateNewBookCommand { get; private set; }
        public ICommand SelectBookCommand { get; private set; }

        public BooksViewModel()
        {
            MessagingCenter.Subscribe<BookDetailsViewModel,Book>
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
            var books= _bookStore.GetBooks();
            CollectionOfBookViewModels.Clear();
            foreach (var book in books)
            {
     
                CollectionOfBookViewModels.Add(new BookViewModel(book));
            }
        }

        /*
        public ContactsPageViewModel(IBooksStore contactStore, IPageService pageService)
        {
            _contactStore = contactStore;
            _pageService = pageService;

            LoadDataCommand = new Command(async () => await LoadData());
            AddContactCommand = new Command(async () => await AddContact());
            SelectContactCommand = new Command<ContactViewModel>(async c => await SelectContact(c));
            DeleteContactCommand = new Command<ContactViewModel>(async c => await DeleteContact(c));
            CallContactCommand = new Command<ContactViewModel>(async c => await CallContact(c));
        }
*/
   
   
        /*
        public Command NavToEditDeletePage
        {
            get
            {
                return new Command(async () =>
                {
                    await App.Current.MainPage.Navigation.PushAsync(new EditDeleteBookPage());
                });
            }
        }*/
    }
}