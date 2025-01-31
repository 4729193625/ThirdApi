using ThirdAPI.Models;

namespace ThirdAPI.Datas
{
    public class Data
    {
        private static List<Book> Books = new List<Book>
        {
            new Book {Id = 1, Name = "Ekonomi 101", Price = 10.99M},
            new Book {Id = 2, Name = "Yatırım 101", Price = 11.99M},
            new Book {Id = 3, Name = "Para Yönetimi 101", Price = 12.99M}
        };

        public static List<Book> GetAll () => Books;

        public static Book? GetById (int id) => Books.FirstOrDefault(b => b.Id == id);

        public static void Post (Book book) 
        {
            book.GetType().GetProperty("Id")?.SetValue(book, Books.Max(b => b.Id) + 1);
            Books.Add(book);
        }

        public static void Update(int id, Book updatedBook)
        {
            var existingBook = Books.FirstOrDefault(b => b.Id == id);

            if (existingBook != null)
            {
            existingBook.Name = updatedBook.Name;
            existingBook.Price = updatedBook.Price;
            }
        }

        public static void Delete (int id)
        {
            var book = Books.FirstOrDefault(b => b.Id == id);

            if (book != null)
            Books.Remove(book);
        }
    }
}