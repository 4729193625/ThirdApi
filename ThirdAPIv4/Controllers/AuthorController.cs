using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ThirdAPI.Dtos;
using ThirdAPI.Interfaces;
using ThirdAPI.Models;

namespace ThirdAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : Controller
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IPublisherRepository _publisherRepository;
        private readonly IMapper _mapper;

         public AuthorController(IAuthorRepository authorRepository, IBookRepository bookRepository, IPublisherRepository publisherRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _bookRepository = bookRepository;
            _publisherRepository = publisherRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllAuthors()
        {
            var authors =_mapper.Map<List<AuthorDto>>(_authorRepository.GetAllAuthor());
            return Ok(authors);
        }

        [HttpGet("{authorId:int}")]

        public IActionResult GetAuthorById (int authorId)
        {
            if (!_authorRepository.AuthorExistsById(authorId))
                return NotFound("author not found.");

            var author = _mapper.Map<AuthorDto>(_authorRepository.GetAuthorById(authorId));
            return Ok(author);    
        }

        [HttpGet("{bookId:int}/author")]

        public IActionResult GetAuthorOfABook (int bookId)
        {
            var book = _bookRepository.GetBookById(bookId);
            if (book == null) return NotFound("book not found.");

            var author = _mapper.Map<AuthorDto>(_authorRepository.GetAuthorOfABook(bookId));

            return Ok(author);
        }

        [HttpGet("{authorId:int}/books")]
        public IActionResult GetBooksByAuthor (int authorId)
        {
            var books = _authorRepository.GetBooksByAuthor(authorId);

            if (books == null || !books.Any()) return NotFound("No books found by this author.");

            return Ok(books);
        }

        [HttpPost]
        public IActionResult PostAuthor([FromBody] AuthorDto authorDto)
        {
            if (authorDto == null)
                return BadRequest(ModelState);

            if (_authorRepository.GetAuthorById(authorDto.Id) != null)
                return Conflict("An author already exists."); 

            var authorMap = _mapper.Map<Author>(authorDto);

            if (!_authorRepository.CreateAuthor(authorMap))
            return StatusCode(500, "Something went wrong while creating the author.");

            return Ok("Created successfuly");       
        }

        [HttpPut("{authorId}")]
        public IActionResult PutAuthor (int authorId, [FromBody] AuthorDto updatedAuthorDto)
        {
            updatedAuthorDto.SetId(authorId);
            if (updatedAuthorDto == null || authorId != updatedAuthorDto.Id)
            return BadRequest("Invalid Author data or mismatched ID.");

            if (!_authorRepository.AuthorExistsById(authorId))
                return NotFound("Author not found.");

            var authorMap = _mapper.Map<Author>(updatedAuthorDto);

            if (!_authorRepository.UpdateAuthor(authorMap))
                return StatusCode(500, "Something went wrong while updating the author.");

             return Ok("Updated successfuly");   
        }

        [HttpDelete("{authorId}")]
        public IActionResult Deleteauthor (int authorId)
        {
            if (!_authorRepository.AuthorExistsById(authorId))
                return NotFound("author not found.");

            var books = _authorRepository.GetBooksByAuthor(authorId);

        foreach (var book in books)
        {
            var associatedPublishers = _publisherRepository.GetPublishersOfBook(book.Id);

            if (associatedPublishers.Count() == 1)
            {
                var publisher = associatedPublishers.First();

                if (!_bookRepository.DeleteBookById(book.Id) || !_publisherRepository.DeletePublisherById(publisher.Id))
                    return StatusCode(500, $"Failed to delete book with ID {book.Id} or publisher with ID {publisher.Id}");
            }

            else
            {
                if (!_bookRepository.DeleteBookById(book.Id))
                    return StatusCode(500, $"Failed to delete book with ID {book.Id}");
            }
        }

            if (!_authorRepository.DeleteAuthorById(authorId))
                return StatusCode(500, "Something went wrong while deleting the author.");        

             return Ok("Deleted successfuly");
        }
    }
}