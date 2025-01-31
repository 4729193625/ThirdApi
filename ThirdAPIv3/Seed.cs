using ThirdAPI.Datas;
using ThirdAPI.Models;

namespace ThirdAPIv3
{
    public class Seed (DataContext context)
    {
        private readonly DataContext dataContext = context;
    

        public void SeedDataContext()
        {
          if (!dataContext.Book.Any())
          {
            var Books = new List<Book>()
            {
               new ()
               {
                 Name = "Ekonomi 101",
                 Price = 10.99M
               },

               new ()
               {
                 Name = "Yatırım 101",
                 Price = 12.75M
               },

               new ()
               {
                 Name = "Para Yönetimi 101",
                 Price = 15.95M
               }
 
            };

            dataContext.Book.AddRange(Books);
            dataContext.SaveChanges();
          }
        }
    }
}