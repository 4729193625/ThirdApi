using ThirdAPI.Dtos;
using ThirdAPI.Models;

namespace ThirdAPI.Interfaces
{
    public interface IBookRepository
    {
        ICollection<Book> GetAllBooks ();
        Book GetBookById (int bookId);
        Book GetBookByName (string bookName);
        bool BookExistById(int bookId);
        bool BookExistByName (string bookName);
        bool CreateBook (int publisherId, Book book);
        bool UpdateBook (int publisherId, Book book);
        bool DeleteBookById (int bookId);
        bool Save ();
    }
}