using System;
using System.Threading.Tasks;
using System.Windows.Input;
using ArkadiumLibrary.Models;
using ArkadiumLibrary.ViewModels.ContactsBook.ViewModels;
using Xamarin.Forms;

namespace ArkadiumLibrary.ViewModels
{
    public class BookDetailsViewModel
    {
        private readonly IBookStore _bookStore;
        private readonly IService _service;

        public BookDetailsViewModel(BookViewModel viewModel, IBookStore bookStore, IService service)
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            _bookStore = bookStore;
            _service = service;

            Book = new Book
            {
                id = viewModel.Id,
                title = viewModel.Title,
                description = viewModel.Description,
                author = viewModel.Author,
                year = viewModel.Year,
                isFavorite = viewModel.IsFavorite
            };
            SaveBookCommand = new Command(async () => await SaveBook());
            DeleteBookCommand = new Command(async b => await DeleteBook());
        }

        public ICommand DeleteBookCommand { get; }
        public Book Book { get; }
        public ICommand SaveBookCommand { get; }

        private async Task DeleteBook()
        {
            if (await _service.DisplayAlert("Warning", $"Are you sure you want to delete {Book.title}?", "Yes", "No"))
                _bookStore.DeleteBook(Book);

            await _service.PopAsync(); //leaves current page
        }

        private async Task SaveBook()
        {
            //some validation
            if (string.IsNullOrWhiteSpace(Book.title))
            {
                await _service.DisplayAlert("Error", "Please enter the Book Title.", "OK");
                return;
            }

            if (Book.id == 0) //default value of id
            {
                
                _bookStore.AddBook(Book);
                MessagingCenter.Send(this, EventType.BookAdded, Book);
            }
            else
            {
                _bookStore.UpdateBook(Book);
                MessagingCenter.Send(this, EventType.BookUpdated, Book);
            }

            await _service.PopAsync(); //leaves current page
        }
    }
}