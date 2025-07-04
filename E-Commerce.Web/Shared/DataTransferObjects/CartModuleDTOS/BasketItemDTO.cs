using System.ComponentModel.DataAnnotations;

public class BasketItemDTO
{
    public int Id { get; set; }

    [Range(1, 100)]
    public int Quantity { get; set; } = 1;

    [Range(1, double.MaxValue)]
    public decimal Price { get; set; }

    [Required]
    public string PictureUrl { get; set; } = null!;

    [Required]
    public string Name { get; set; } = null!;

   
}
