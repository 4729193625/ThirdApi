using ThirdAPI.Models;
using ThirdAPI.Datas;
using ThirdAPI.Interfaces;

namespace ThirdAPI.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly DataContext _context;
        public AuthorRepository(DataContext context)
        {
            _context = context;
        }
        public ICollection<Author> GetAllAuthor()
        {
            return _context.Authors.OrderBy(a => a.Id).ToList();
        }

        public Author GetAuthorById(int authorId)
        {
            return _context.Authors.Where(b => b.Id == authorId).FirstOrDefault();
        }

        public bool AuthorExistsById(int authorId)
        {
            return _context.Authors.Any(a => a.Id == authorId);
        }

        public bool AuthorExistsByName(string authorName)
        {
            return _context.Authors.Any(a => a.Name == authorName);
        }

        public bool HasOtherBooks(int authorId, int excludedBookId)
        {
            return _context.Books.Any(b => b.AuthorId == authorId && b.Id !=  excludedBookId);
        }

        public Author GetAuthorOfABook(int bookId)
        {
            return _context.Books
            .Where(b => b.Id == bookId)
            .Select(b => b.Author)
            .FirstOrDefault();
        }

        public ICollection<Book> GetBooksByAuthor(int authorId)
        {
            return _context.Books
            .Where(b => b.AuthorId == authorId)
            .ToList();
        }

        public bool CreateAuthor(Author author)
        {
            _context.Add(author);
            return Save();
        }

        public bool UpdateAuthor(Author author)
        {
            _context.Update(author);
            return Save();
        }

        public bool DeleteAuthorById(int authorId)
        {
            var author = _context.Authors.FirstOrDefault(a => a.Id == authorId);
            if (author == null) return false;

            _context.Remove(author);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}