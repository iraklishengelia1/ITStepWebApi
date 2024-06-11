namespace WebApi.Models.Books
{
    public class BookDTO
    {
        public string Description { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public BookDTO(Book book)
        {
            Description = book.Description;
            Title = book.Title;
            Price = book.Price;
        }
    }
}
