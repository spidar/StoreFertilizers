using System.ComponentModel.DataAnnotations;

namespace StoreFertilizers.Models
{
    public class Promotion
    {
        public int PromotionID { get; set; }

        [Required]
        public string Name { get; set; }

        public string Descr { get; set; }
    }
}