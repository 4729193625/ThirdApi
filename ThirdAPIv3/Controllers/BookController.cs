using Microsoft.AspNetCore.Mvc;
using ThirdAPI.Models;
using ThirdAPI.Dtos;
using ThirdAPI.Interfaces;

namespace ThirdAPIv3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet]
        public ActionResult<Book> GetAll()
        {
            var books = _bookRepository.GetAll();
            return Ok(books);  
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            if (!_bookRepository.BookExistById(id))
                return NotFound("Book not found.");

            var book = _bookRepository.GetById(id);
            return Ok(book);        
        }

        [HttpGet("{name}")]
        public IActionResult GetByName(string name)
        {
            if (!_bookRepository.BookExistByName(name))
                return NotFound("Book not found.");

            var book = _bookRepository.GetByName(name);
            return Ok(book);        
        }

        [HttpPost]
         public IActionResult Post ([FromBody] Book book)
        {
            if (book == null)
                return BadRequest("Book object is null.");

            if (_bookRepository.BookExistByName(book.Name))
                return Conflict("A book with the same name already exists.");

            if (!_bookRepository.Create(book))
                return StatusCode(500, "Something went wrong while creating the book.");

            return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
        }

        [HttpPut("{id:int}")]
        public IActionResult Put(int id, [FromBody] BookDto bookdto)
        {
            if (bookdto == null || id != bookdto.Id)
                return BadRequest("Invalid book data or mismatched ID.");

            if (!_bookRepository.BookExistById(id))
                return NotFound("Book not found.");

            var updatedBook = new Book
            {
                Id = bookdto.Id,
                Name = bookdto.Name,
                Price = bookdto.Price
            };

            if (_bookRepository.BookExistByName(updatedBook.Name) && 
                _bookRepository.IsBookPriceSame(updatedBook.Price))
                return Conflict("A book with the same name and price already exists.");

            if (!_bookRepository.Update(updatedBook))
                return StatusCode(500, "Something went wrong while updating the book.");

            return Ok("Updated Successfully.");
        }    

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            if (!_bookRepository.BookExistById(id))
                return NotFound("Book not found.");

            if (!_bookRepository.DeleteById(id))
                return StatusCode(500, "Something went wrong while deleting the book.");

            return Ok("Deleted Successfuly");
        }

    }
}