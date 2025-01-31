using ThirdAPI.Dtos;
using ThirdAPI.Models;

namespace ThirdAPI.Interfaces
{
    public interface IBookRepository
    {
        ICollection<Book> GetAll ();

        Book? GetById (int id);
        bool BookExistById(int id);

        Book? GetByName (string name);
        bool BookExistByName (string name);

        bool IsBookPriceSame (decimal price);

        bool Create (Book book);
        bool Save ();

        bool Update (Book book);
        bool DeleteById (int id);
    }
}