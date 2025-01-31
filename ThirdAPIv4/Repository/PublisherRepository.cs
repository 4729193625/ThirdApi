using ThirdAPI.Interfaces;
using ThirdAPI.Datas;
using ThirdAPI.Models;

namespace ThirdAPI.Repository
{
    public class PublisherRepository : IPublisherRepository
    {
        private readonly DataContext _context;
        public PublisherRepository(DataContext context)
        {
            _context = context;
        }
        public ICollection<Publisher> GetAllPublisher ()
        {
            return _context.Publishers.OrderBy(p => p.Id).ToList();
        }

        public Publisher GetPublisherById (int publisherId)
        {
            return _context.Publishers.Where(p => p.Id == publisherId).FirstOrDefault();
        }

        public ICollection<Publisher> GetPublishersOfBook (int bookId)
        {
            return _context.BookPublishers
            .Where(bp => bp.BookId == bookId)
            .Select(bp => bp.Publisher)
            .ToList();
        }

        public ICollection<Book> GetBooksFromAPublisher (int publisherId)
        {
             return _context.BookPublishers
            .Where(bp => bp.PublisherId == publisherId)
            .Select(bp => bp.Book)
            .ToList();
        }

        public bool PublisherExistsById(int publisherId)
        {
            return _context.Publishers.Any(p => p.Id == publisherId);
        }

        public bool PublisherExistsByName(int publisherName)
        {
            return _context.Publishers.Any(p => p.Name.Equals(publisherName));
        }

        public bool CreatePublisher(Publisher publisher)
        {
            _context.Add(publisher);
            return Save();
        }

        public bool UpdatePublisher(Publisher publisher)
        {
             _context.Update(publisher);
            return Save();
        }

        public bool DeletePublisherById (int publisherId)
        {
            var publisher = _context.Publishers.FirstOrDefault(b => b.Id == publisherId);
            if (publisher == null) return false;

            _context.Remove(publisher);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}