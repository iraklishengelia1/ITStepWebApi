namespace WebApi.Models.Books
{
    public class Book
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }


        public void RemoveFromStorage()
        {
            if (Count == 0)
                throw new ArgumentException("There is no book in storage");
            Count--;
        }
    }
}