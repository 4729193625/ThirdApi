using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ThirdAPI.Dtos;
using ThirdAPI.Interfaces;
using ThirdAPI.Models;

namespace ThirdAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookPublisherController : Controller
    {
        private readonly IBookPublisherRepository _bookpublisherRepository;
        private readonly IPublisherRepository _publisherRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

         public BookPublisherController(IBookPublisherRepository bookpublisherRepository, IPublisherRepository publisherRepository, IBookRepository bookRepository,IAuthorRepository authorRepository , IMapper mapper)
        {
            _bookpublisherRepository = bookpublisherRepository;
            _publisherRepository = publisherRepository;
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult PostBookPublisher([FromBody] BookPublisherDto bookpublisherDto)
        {
            if (bookpublisherDto == null)
                return BadRequest(ModelState);

            if (_bookpublisherRepository.GetBookPublisherById(bookpublisherDto.BookId, bookpublisherDto.PublisherId) != null)
                return Conflict("A book:publisher already exists."); 

            var bookpublisherMap = _mapper.Map<BookPublisher>(bookpublisherDto);

            if (!_bookpublisherRepository.CreateBookPublisher(bookpublisherMap))
            return StatusCode(500, "Something went wrong while creating the book:publisher.");

            return Ok("Created successfuly");       
        }

        [HttpDelete("{bookId:int}/{publisherId}")]
        public IActionResult DeleteBookPublisher (int bookId, int publisherId)
        {
            if (!_bookpublisherRepository.BookPublisherExists(bookId, publisherId))
            return NotFound("book:publisher not found.");

            var book = _bookRepository.GetBookById(bookId);
            if (book == null)
            return NotFound($"Book with ID {bookId} not found.");

            var publisher = _publisherRepository.GetPublisherById(publisherId);
            if (publisher == null)
            return NotFound($"Publisher with ID {publisherId} not found.");

            var associatedAuthor = _authorRepository.GetAuthorOfABook(bookId);

            if (!_authorRepository.HasOtherBooks(associatedAuthor.Id, bookId))
                if (!_authorRepository.DeleteAuthorById(associatedAuthor.Id))
                   return StatusCode(500, $"Failed to delete author with ID {associatedAuthor.Id}");

            var associatedPublishers = _publisherRepository.GetPublishersOfBook(bookId);

                if (!associatedPublishers.Any(b => b.Id != publisherId))
                    if (!_bookRepository.DeleteBookById(book.Id) && !_publisherRepository.DeletePublisherById(publisherId))
                        return StatusCode(500, $"Failed to delete book with ID {book.Id} or publisher with ID {publisherId}");

            if (!_bookpublisherRepository.DeleteBookPublisher(bookId, publisherId))
                return StatusCode(500, "Something went wrong while deleting the book:publisher.");               

             return Ok("Deleted successfuly");
        }
    }
}