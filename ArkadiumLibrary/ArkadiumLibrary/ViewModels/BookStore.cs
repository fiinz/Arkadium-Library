using System.Collections.Generic;
using ArkadiumLibrary.Models;
using Realms;

namespace ArkadiumLibrary.ViewModels
{
    public class BookStore : IBookStore
    {
        private readonly Realm _realmInstance = Realm.GetInstance();


        public IEnumerable<Book> GetBooks()
        {
            return _realmInstance.All<Book>();
        }

        public Book GetBookById(int id)
        {
            return _realmInstance.Find<Book>(id);
        }

        public void DeleteBook(Book book)
        {
            var bookInRealm = GetBookById(book.id);
            using (var transaction = _realmInstance.BeginWrite())
            {
                _realmInstance.Remove(bookInRealm);
                transaction.Commit();
            }
        }

        public void AddBook(Book book)
        {
            _realmInstance.Write(() => { _realmInstance.Add(book); });
        }

        public void UpdateBook(Book book)
        {
            _realmInstance.Write(() => { _realmInstance.Add(book, true); });
        }
    }
}