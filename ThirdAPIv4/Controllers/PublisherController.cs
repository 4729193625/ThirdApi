using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ThirdAPI.Dtos;
using ThirdAPI.Interfaces;
using ThirdAPI.Models;

namespace ThirdAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PublisherController : ControllerBase
    {
        
    private readonly IPublisherRepository _publisherRepository;
    private readonly IBookRepository _bookRepository;
    private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

         public PublisherController(IPublisherRepository publisherRepository, IBookRepository bookRepository, IAuthorRepository authorRepository, IMapper mapper)
        {
            _publisherRepository = publisherRepository;
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllPublishers()
        {
            var publishers =_mapper.Map<List<PublisherDto>>(_publisherRepository.GetAllPublisher());
            return Ok(publishers);
        }

        [HttpGet("{publisherId:int}")]

        public IActionResult GetPublisherById (int publisherId)
        {
            if (!_publisherRepository.PublisherExistsById(publisherId))
                return NotFound("Publisher not found.");

            var publisher = _mapper.Map<PublisherDto>(_publisherRepository.GetPublisherById(publisherId));
            return Ok(publisher);    
        }

        [HttpGet("{bookId:int}/publishers")]

        public IActionResult GetPublishersOfBook (int bookId)
        {
            var book = _bookRepository.GetBookById(bookId);
            if (book == null) return NotFound("book not found.");

            var publishers = _mapper.Map<List<PublisherDto>>(_publisherRepository.GetPublishersOfBook(bookId));

            return Ok(publishers);
        }

        [HttpGet("{publisherId:int}/books")]
        public IActionResult GetBooksFromPublisher (int publisherId)
        {
            var books = _publisherRepository.GetBooksFromAPublisher(publisherId);

            if (books == null || !books.Any()) return NotFound("No books found from this publisher.");

            return Ok(_mapper.Map<List<BookDto>>(books));
        }

        [HttpPost]
        public IActionResult PostPublisher([FromBody] PublisherDto publisherDto)
        {
            if (publisherDto == null)
                return BadRequest(ModelState);

            if (_publisherRepository.GetPublisherById(publisherDto.Id) != null)
                return Conflict("A publisher already exists."); 

            var publisherMap = _mapper.Map<Publisher>(publisherDto);

            if (!_publisherRepository.CreatePublisher(publisherMap))
            return StatusCode(500, "Something went wrong while creating the publisher.");

            return Ok("Created successfuly");       
        }

        [HttpPut("{publisherId}")]
        public IActionResult Putpublisher (int publisherId, [FromBody] PublisherDto updatedPublisherDto)
        {
            updatedPublisherDto.SetId(publisherId);
            if (updatedPublisherDto == null || publisherId != updatedPublisherDto.Id)
            return BadRequest("Invalid publisher data or mismatched ID.");

            if (!_publisherRepository.PublisherExistsById(publisherId))
                return NotFound("publisher not found.");

            var publisherMap = _mapper.Map<Publisher>(updatedPublisherDto);

            if (!_publisherRepository.UpdatePublisher(publisherMap))
                return StatusCode(500, "Something went wrong while updating the publisher.");

            return Ok("Updated successfuly");    
        }

        [HttpDelete("{publisherId}")]
        public IActionResult Deletepublisher (int publisherId)
        {
            if (!_publisherRepository.PublisherExistsById(publisherId))
                return NotFound("publisher not found.");

            var books = _publisherRepository.GetBooksFromAPublisher(publisherId);

        foreach (var book in books)
        {
            var associatedAuthor = _authorRepository.GetAuthorOfABook(book.Id);

            if (!_authorRepository.HasOtherBooks(associatedAuthor.Id, book.Id))
                if (!_authorRepository.DeleteAuthorById(book.AuthorId))
                   return StatusCode(500, $"Failed to delete author with ID {book.AuthorId}");

            var associatedPublishers = _publisherRepository.GetPublishersOfBook(book.Id);
        
            if (!associatedPublishers.Any(p => p.Id != publisherId))
                if (!_bookRepository.DeleteBookById(book.Id))
                return StatusCode(500, $"Failed to delete book with ID {book.Id}");
            
        }

            if (!_publisherRepository.DeletePublisherById(publisherId))
                return StatusCode(500, "Something went wrong while deleting the publisher.");        

            return Ok("Deleted successfuly");
        }
    }
}