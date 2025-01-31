using ThirdAPI.Models;

namespace ThirdAPI.Interfaces
{
    public interface IReviewRepository
    {
        ICollection<Review> GetAllReviews ();
        Review GetReviewById (int reviewId);
        ICollection<Review> GetReviewsOfABook (int bookId);
        bool ReviewExistsById (int reviewId);
        bool CreateReview (Review review);
        bool UpdateReview (Review review);
        bool DeleteReview (Review review);
        bool DeleteReviews (List<Review> reviews);
        bool Save();
    }
}