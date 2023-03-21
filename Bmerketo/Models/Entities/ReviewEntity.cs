using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bmerketo.Models.Entities
{
    public class ReviewEntity
    {
        [Key]
        public int Id { get; set; }      
        public string? Title { get; set; }
        public string? Body { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public int ProductEntityId { get; set; }       
    }
}
