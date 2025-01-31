using ThirdAPI.Models;

namespace ThirdAPI.Interfaces
{
    public interface IAuthorRepository
    {
        ICollection<Author> GetAllAuthor ();
        Author GetAuthorById (int authorId);
        Author GetAuthorOfABook (int bookId);
        ICollection<Book> GetBooksByAuthor (int authorId);
        bool AuthorExistsById (int ownerId);
        bool AuthorExistsByName (string authorName);
        bool HasOtherBooks(int authorId, int excludedBookId);
        bool CreateAuthor (Author author);
        bool UpdateAuthor (Author author);
        bool DeleteAuthorById (int authorId);
        bool Save ();
    }
}