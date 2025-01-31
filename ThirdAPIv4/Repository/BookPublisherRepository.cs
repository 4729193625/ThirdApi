using ThirdAPI.Interfaces;
using ThirdAPI.Datas;
using ThirdAPI.Models;

namespace ThirdAPI.Repository
{
    public class BookPublisherRepository : IBookPublisherRepository
    {
        private readonly DataContext _context;

        public BookPublisherRepository(DataContext context)
        {
            _context = context;
        }

        public BookPublisher GetBookPublisherById(int bookId, int publisherId)
        {
            return _context.BookPublishers.FirstOrDefault(bp => bp.BookId == bookId && bp.PublisherId == publisherId);
        }

        public bool CreateBookPublisher(BookPublisher bookpublisher)
        {
             _context.Add(bookpublisher);
            return Save();
        }

        public bool DeleteBookPublisher(int bookId, int publisherId)
        {
            var bookpublisher = GetBookPublisherById(bookId, publisherId);
            if (bookpublisher == null) return false;

            _context.Remove(bookpublisher);
            return Save();
        }

        public bool BookPublisherExists(int bookId, int publisherId)
        {
            return _context.BookPublishers.Any(bp => bp.BookId == bookId && bp.PublisherId == publisherId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}