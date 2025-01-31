using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ThirdAPI.Dtos;
using ThirdAPI.Interfaces;
using ThirdAPI.Models;

namespace ThirdAPIv4.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

         public ReviewController(IReviewRepository reviewRepository, IBookRepository bookRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllReviews()
        {
            var reviews =_mapper.Map<List<ReviewDto>>(_reviewRepository.GetAllReviews());
            return Ok(reviews);
        }

        [HttpGet("{reviewId:int}")]

        public IActionResult GetReviewById (int reviewId)
        {
            if (!_reviewRepository.ReviewExistsById(reviewId))
                return NotFound("review not found.");

            var review = _mapper.Map<ReviewDto>(_reviewRepository.GetReviewById(reviewId));
            return Ok(review);    
        }

        [HttpGet("{bookId:int}/reviews")]

        public IActionResult GetReviewsOfABook (int bookId)
        {
            var book = _bookRepository.GetBookById(bookId);
            if (book == null) return NotFound("book not found.");

            var reviews = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetReviewsOfABook(bookId));

            return Ok(reviews);
        }


        [HttpPost]
        public IActionResult Postreview([FromBody] ReviewDto reviewDto)
        {
            if (reviewDto == null)
                return BadRequest(ModelState);

            if (_reviewRepository.GetReviewById(reviewDto.Id) != null)
                return Conflict("An review already exists."); 

            var reviewMap = _mapper.Map<Review>(reviewDto);

            if (!_reviewRepository.CreateReview(reviewMap))
            return StatusCode(500, "Something went wrong while creating the review.");

            return Ok("Created successfuly");       
        }

        [HttpPut("{reviewId}")]
        public IActionResult Putreview (int reviewId, [FromBody] ReviewDto updatedReviewDto)
        {
            updatedReviewDto.SetId(reviewId);
            if (updatedReviewDto == null || reviewId != updatedReviewDto.Id)
            return BadRequest("Invalid review data or mismatched ID.");

            if (!_reviewRepository.ReviewExistsById(reviewId))
                return NotFound("review not found.");

            var reviewMap = _mapper.Map<Review>(updatedReviewDto);

            if (!_reviewRepository.UpdateReview(reviewMap))
                return StatusCode(500, "Something went wrong while updating the review.");

            return Ok("Updated successfuly");   
        }

        [HttpDelete("{reviewId}")]
        public IActionResult Deletereview (int reviewId)
        {
            if (!_reviewRepository.ReviewExistsById(reviewId))
                return NotFound("review not found.");

            var reviewToDelete = _reviewRepository.GetReviewById(reviewId);    

            if (!_reviewRepository.DeleteReview(reviewToDelete))
                return StatusCode(500, "Something went wrong while deleting the review.");        

            return Ok("Deleted successfuly");
        }
    }
}