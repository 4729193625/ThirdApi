using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ThirdAPI.Dtos;
using ThirdAPI.Interfaces;
using ThirdAPI.Models;

namespace ThirdAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;

        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

         public BookController(IBookRepository bookRepository,IReviewRepository reviewRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            var books =_mapper.Map<List<BookDto>>(_bookRepository.GetAllBooks());
            return Ok(books);
        }

        [HttpGet("{bookId:int}")]

        public IActionResult GetBookById (int bookId)
        {
            if (!_bookRepository.BookExistById(bookId))
                return NotFound("Book not found.");

            var book = _mapper.Map<BookDto>(_bookRepository.GetBookById(bookId));
            return Ok(book);    
        }

        [HttpGet("{name}")]
        public IActionResult GetBookByName (string name)
        {
            if (!_bookRepository.BookExistByName(name))
                return NotFound("Book not found.");

            var book = _mapper.Map<BookDto>(_bookRepository.GetBookByName(name));
            return Ok(book);    
        }

        [HttpPost]
        public IActionResult PostBook ([FromQuery] int publisherId, [FromBody] BookDto bookDto)
        {
            if (bookDto == null) return BadRequest(ModelState);

            if (_bookRepository.BookExistByName(bookDto.Name)) return Conflict("A book with the same name already exists.");

            var bookMap = _mapper.Map<Book>(bookDto);

            if (!_bookRepository.CreateBook(publisherId, bookMap))
            return StatusCode(500, "Something went wrong while creating the book.");

            return Ok("Created successfuly");
        }

        [HttpPut("{bookId}")]
        public IActionResult PutBook (int bookId, [FromQuery] int publisherId, [FromBody] BookDto updatedBookDto)
        {
            updatedBookDto.SetId(bookId);
            if (updatedBookDto == null || bookId != updatedBookDto.Id)
            return BadRequest("Invalid book data or mismatched ID.");

            if (!_bookRepository.BookExistById(bookId))
                return NotFound("Book not found.");

            var bookMap = _mapper.Map<Book>(updatedBookDto);

            if (!_bookRepository.UpdateBook(publisherId ,bookMap))
                return StatusCode(500, "Something went wrong while updating the book.");

            return Ok("Updated successfuly");    
        }

        [HttpDelete("{bookId}")]
        public IActionResult DeleteBook (int bookId)
        {
            if (!_bookRepository.BookExistById(bookId))
                return NotFound("Book not found.");

            var reviewsToDelete = _reviewRepository.GetReviewsOfABook(bookId);   

            if (!_reviewRepository.DeleteReviews(reviewsToDelete.ToList()))
                return StatusCode(500, "Something went wrong while deleting reviews.");

            if (!_bookRepository.DeleteBookById(bookId))
                return StatusCode(500, "Something went wrong while deleting the book.");        

            return Ok("Deleted successfuly");
        }
    }
}