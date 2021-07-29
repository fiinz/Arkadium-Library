using System.Threading.Tasks;
using System.Windows.Input;
using ArkadiumLibrary.Models;

namespace ArkadiumLibrary.ViewModels
{
    public class BookViewModel : BaseViewModel
    {
        private string _author;
        private string _description;
        private string _title;
        private int _year;
        private bool _isFavorite;
        

        public BookViewModel()
        {
            
        }
        public BookViewModel(Book book)
        {
            Id = book.id;
            Title = book.title;
            Author = book.author;
            Description = book.description;
            IsFavorite = book.isFavorite;
            Year = book.year;
        }

        public int Id { get; set; }

        public string Title
        {
            get => _title;
            set => SetValue(ref _title, value);
        }

        public string Description
        {
            get => _description;
            set => SetValue(ref _description, value);
        }

        public string Author
        {
            get => _author;
            set => SetValue(ref _author, value);
        }

        public int Year
        {
            get => _year;
            set => SetValue(ref _year, value);
        }
        
        public bool IsFavorite
        {
            get => _isFavorite;
            set => SetValue(ref _isFavorite, value);
        }
  
    }
}