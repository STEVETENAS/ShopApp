using ShopApp.API.Models;
using System.ComponentModel.DataAnnotations;

namespace ShopApp.API.Dtos
{
    public class AuthorCreateDto
    {
        [Required,StringLength(50),MinLength(2)]
        public string FirstName { get; set; }
        [Required, StringLength(50), MinLength(2)]
        public string LastName { get; set; }
        [StringLength(250), MinLength(2)]
        public string Description { get; set; }
    }
    public class AuthorUpdateDto
    {
        public int Id { get; set; }
        [Required,StringLength(50),MinLength(2)]
        public string FirstName { get; set; }
        [Required, StringLength(50), MinLength(2)]
        public string LastName { get; set; }
        [StringLength(250), MinLength(2)]
        public string Description { get; set; }
    }
    public class AuthorGetDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [StringLength(250), MinLength(2)]
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual ICollection<Book> Books { get; set; }

    }
}
