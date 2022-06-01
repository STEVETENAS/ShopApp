using System.ComponentModel.DataAnnotations;

namespace ShopApp.API.Dtos
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int Year { get; set; }
        public string Isbn { get; set; } = null!;
        public string? Summary { get; set; }
        public string? Image { get; set; }
        public decimal Price { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
    public class BookCreateDto
    {
        [Required,MaxLength(50), MinLength(2)]
        public string Title { get; set; }
        [Required, Range(1800, int.MaxValue)]
        public int Year { get; set; }
        [Required]
        public string Isbn { get; set; }
        [Required, StringLength(250, MinimumLength = 5)]
        public string? Summary { get; set; }
        [MaxLength(int.MaxValue)]
        public string? Image { get; set; }
        [Required, Range(0, int.MaxValue)]
        public decimal Price { get; set; }
        [Required]
        public int AuthorId { get; set; }
    }
    public class BookUpdateDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int Year { get; set; }
        public string Isbn { get; set; } = null!;
        public string? Summary { get; set; }
        public string? Image { get; set; }
        public decimal Price { get; set; }
        public int AuthorId { get; set; }
    }

    public class BookWithAuthorDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string Isbn { get; set; }
        public string? Summary { get; set; }
        public string? Image { get; set; }
        public decimal Price { get; set; }
        public int AuthorId { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual AuthorGetDto Author { get; set; }
    }
}
