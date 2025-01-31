using ThirdAPI.Interfaces;
using ThirdAPI.Datas;
using ThirdAPI.Models;

namespace ThirdAPI.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DataContext _context;
        public ReviewRepository(DataContext context)
        {
            _context = context;
        }
        public ICollection<Review> GetAllReviews()
        {
            return _context.Reviews.OrderBy(r => r.Id).ToList();
        }

        public Review GetReviewById(int reviewId)
        {
             return _context.Reviews.Where(r => r.Id == reviewId).FirstOrDefault();
        }

        public ICollection<Review> GetReviewsOfABook(int bookId)
        {
             return _context.Reviews
            .Where(r => r.BookId == bookId)
            .ToList();
        }

        public bool ReviewExistsById(int reviewId)
        {
            return _context.Reviews.Any(r => r.Id == reviewId);
        }

        public bool CreateReview(Review review)
        {
            _context.Add(review);
            return Save();
        }

        public bool UpdateReview(Review review)
        {
            _context.Update(review);
            return Save();
        }

        public bool DeleteReview(Review review)
        {
            if (review == null) return false;

        _context.Reviews.Remove(review);
        return Save();
        }

        public bool DeleteReviews(List<Review> reviews)
        {
            if (reviews == null || reviews.Count == 0) return false;

        _context.Reviews.RemoveRange(reviews);
        return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}