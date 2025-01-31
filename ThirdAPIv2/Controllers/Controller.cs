using Microsoft.AspNetCore.Mvc;
using ThirdAPI.Models;
using ThirdAPI.Datas;

namespace ThirdAPI.Controllers
{
        [Route("api/[controller]")]
        [ApiController]

        public class BooksController : ControllerBase
        {
            [HttpGet]
            public ActionResult<List<Book>> Get() => Data_Repository.GetAll();

            [HttpGet("{id}")]
            public ActionResult<Book> Get(int id)
            {
                var book = Data_Repository.GetById(id);

                if (book == null)
                return NotFound("Geçersiz kitap bilgileri.");

                return book;
            }

            [HttpPost]
            public IActionResult Post (Book book)
            {
                if (book == null || string.IsNullOrEmpty(book.Name) || book.Price <= 0)
                return BadRequest("Geçersiz kitap bilgileri.");

                Data_Repository.Post(book);
                return CreatedAtAction(nameof(Get), new {id = book.Id}, book);
            }

            [HttpPut("{id}")]

            public IActionResult Put (int id, Book UpdatedBook)
            {
                var ExistingBook = Data_Repository.GetById(id);

                if (ExistingBook == null)
                return NotFound("Geçersiz kitap bilgileri.");

                ExistingBook.Name = UpdatedBook.Name;
                ExistingBook.Price = UpdatedBook.Price;
                return NoContent();
            }

            [HttpDelete("{id}")]

            public IActionResult Delete (int id)
            {
                var book = Data_Repository.GetById(id);

                if (book == null)
                return NotFound("Geçersiz kitap bilgileri.");

                Data_Repository.Delete(id);
                return NoContent();
            }
        }
    
}