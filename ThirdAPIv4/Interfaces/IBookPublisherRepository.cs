using ThirdAPI.Models;

namespace ThirdAPI.Interfaces
{
    public interface IBookPublisherRepository
    {
        BookPublisher GetBookPublisherById (int bookId, int publisherId);
        bool BookPublisherExists (int bookId, int publisherId);
        bool CreateBookPublisher (BookPublisher bookpublisher);
        bool DeleteBookPublisher (int bookId, int publisherId);
        bool Save();
    }
}