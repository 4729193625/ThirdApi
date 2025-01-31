using ThirdAPI.Models;

namespace ThirdAPI.Interfaces
{
    public interface IPublisherRepository
    {
        ICollection<Publisher> GetAllPublisher ();
        Publisher GetPublisherById (int publisherId);
        ICollection<Publisher> GetPublishersOfBook (int bookId);
        ICollection<Book> GetBooksFromAPublisher (int publisherId);
        bool PublisherExistsById (int publisherId);
        bool PublisherExistsByName (int publisherName);
        bool CreatePublisher (Publisher publisher);
        bool UpdatePublisher (Publisher publisher);
        bool DeletePublisherById (int publisherId);
        bool Save();
    }
}