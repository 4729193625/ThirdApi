using ThirdAPI.Models;
using ThirdAPI.Datas;
using ThirdAPI.Interfaces;
using ThirdAPI.Dtos;

namespace ThirdAPI.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly DataContext _context;
        public BookRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Book> GetAllBooks()
        {
            return _context.Books.OrderBy(b => b.Id).ToList();
        }

        public Book GetBookById(int id)
        {
            return _context.Books.Where(b => b.Id == id).FirstOrDefault();
        }

        public Book GetBookByName(string name)
        {
            return _context.Books.Where(b => b.Name == name).FirstOrDefault();
        }

        public bool BookExistById(int bookId)
        {
            return _context.Books.Any(p => p.Id == bookId);
        }

        public bool BookExistByName(string bookName)
        {
            return _context.Books.Any(p => p.Name == bookName);
        }

        public bool CreateBook(int publisherId, Book book)
        {
            var BookPublisherEntity = _context.Publishers.Where(a => a.Id == publisherId).FirstOrDefault();

            var bookPublisher = new BookPublisher ()
            {
                Book = book,
                Publisher = BookPublisherEntity,
            };

            _context.Add(bookPublisher);
            _context.Add(book);
            return Save();
        }

        public bool UpdateBook(int publisherId, Book book)
        {
            _context.Update(book);
            return Save();
        }

        public bool DeleteBookById(int bookId)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == bookId);
            if (book == null) return false;

            _context.Remove(book);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}