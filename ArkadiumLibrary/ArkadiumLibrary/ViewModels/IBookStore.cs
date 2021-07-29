using System.Collections.Generic;
using ArkadiumLibrary.Models;

namespace ArkadiumLibrary.ViewModels
{
    //interface for CRUD Operations of book details

    public interface IBookStore
    {
        IEnumerable<Book> GetBooks();
        Book GetBookById(int id);
        void DeleteBook(Book book);
        void AddBook(Book book);
        void UpdateBook(Book book);
    }
}