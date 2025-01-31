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

        public ICollection<Book> GetAll ()
        {
            return _context.Book.OrderBy(b => b.Id).ToList();
        }

        public Book? GetById (int id)
        {
            return _context.Book.Where(b => b.Id == id).FirstOrDefault();
        }

        public Book? GetByName (string name)
        {
            return _context.Book.Where(b => b.Name == name).FirstOrDefault();
        }

        public bool BookExistById (int id)
        {
            return _context.Book.Any(b => b.Id == id);
        }

        public bool BookExistByName (string name)
        {
            return _context.Book.Any(b => b.Name.Equals(name));
        }

        public bool IsBookPriceSame(decimal price)
        {
            return _context.Book.Any(b => b.Price.Equals(price));
        }
        
        public bool Save ()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
        
        public bool Create (Book book)
        {
            _context.Add(book);
            return Save();
        }

        public bool Update (Book book)
        {
            _context.Update(book);
            return Save();
        }

        public bool DeleteById (int id)
        {
            if (_context.Book == null)
                throw new InvalidOperationException("Books table does not exist or is not configured properly.");

            var book = _context.Book.FirstOrDefault(b => b.Id == id);
            if (book == null) return false;

            _context.Remove(book);
            return Save();
        }
    }
}