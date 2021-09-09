using System.ComponentModel.DataAnnotations;

namespace Collection.Dtos {
    public record UpdateItem {
        [Required]
        public string Name { get; init; }

        [Required]
        [Range(1, 1000)]
        public decimal Price { get; set; }
    }
}