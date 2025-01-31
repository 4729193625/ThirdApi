using ThirdAPI.Models;

namespace ThirdAPI.Datas
{
    public class Seed
    {
        private readonly DataContext dataContext;

        public Seed(DataContext context)
        {
            this.dataContext = context;
        }

        public void SeedDataContext()
        {
            if (!dataContext.Books.Any())
            {
                var authors = new List<Author>()
                {
                    new Author() { Name = "Sabahattin Ali", BirthDate = new DateTime(1905, 2, 25), Nationality = "Türk" },
                    new Author() { Name = "Leo Tolstoy", BirthDate = new DateTime(1828, 9, 9), Nationality = "Rus" },
                    new Author() { Name = "Fyodor Dostoevsky", BirthDate = new DateTime(1821, 11, 11), Nationality = "Rus" }
                };

                var publishers = new List<Publisher>()
                {
                    new Publisher() { Name = "İletişim Yayinlari" },
                    new Publisher() { Name = "Can Yayinlari" },
                    new Publisher() { Name = "Kirmizi Kedi" }
                };

                var books = new List<Book>()
                {
                    new Book() { Name = "Kürk Mantolu Madonna", PublishingYear = 1943, Author = authors[0], Price = 10.50M},
                    new Book() { Name = "Aylak Adam", PublishingYear = 1959, Author = authors[0], Price = 15.75M},
                    new Book() { Name = "Savaş ve Bariş", PublishingYear = 1869, Author = authors[1], Price = 20.45M},
                    new Book() { Name = "Anna Karenina", PublishingYear = 1877, Author = authors[1], Price = 17.85M},
                    new Book() { Name = "Suç ve Ceza", PublishingYear = 1866, Author = authors[2], Price = 27.99M},
                    new Book() { Name = "Karamazov Kardeşler", PublishingYear = 1880, Author = authors[2], Price = 30.50M}
                };

                var bookPublishers = new List<BookPublisher>()
                {
                    new BookPublisher() { Book = books[0], Publisher = publishers[0] },
                    new BookPublisher() { Book = books[0], Publisher = publishers[1] },
                    new BookPublisher() { Book = books[1], Publisher = publishers[0] },
                    new BookPublisher() { Book = books[2], Publisher = publishers[1] },
                    new BookPublisher() { Book = books[2], Publisher = publishers[2] },
                    new BookPublisher() { Book = books[3], Publisher = publishers[1] },
                    new BookPublisher() { Book = books[4], Publisher = publishers[0] },
                    new BookPublisher() { Book = books[4], Publisher = publishers[2] },
                    new BookPublisher() { Book = books[5], Publisher = publishers[1] }
                };

                var reviews = new List<Review>()
                {
                    new Review() { Reviewer = "Ahmet Yilmaz", Comment = "Gerçekten etkileyici bir anlatim.", Rating = 5, Book = books[0] },
                    new Review() { Reviewer = "Mehmet Kaya", Comment = "Beklediğim kadar iyi değildi.", Rating = 2, Book = books[0] },
                    new Review() { Reviewer = "Zeynep Demir", Comment = "Kesinlikle herkesin okumasi gereken bir eser.", Rating = 5, Book = books[2] },
                    new Review() { Reviewer = "Elif Çelik", Comment = "Karakterler çok iyi işlenmiş.", Rating = 4, Book = books[3] },
                    new Review() { Reviewer = "Burak Şahin", Comment = "Felsefi ve derinlikli bir roman.", Rating = 5, Book = books[4] },
                    new Review() { Reviewer = "Ayşe koç", Comment = "Okumasi biraz zor ama değer.", Rating = 4, Book = books[5] }
                };

                dataContext.Authors.AddRange(authors);
                dataContext.Publishers.AddRange(publishers);
                dataContext.Books.AddRange(books);
                dataContext.BookPublishers.AddRange(bookPublishers);
                dataContext.Reviews.AddRange(reviews);

                dataContext.SaveChanges();
            }
        }
    }
}