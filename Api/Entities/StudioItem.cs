using AcmeStudiosApi.Services;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace AcmeStudiosApi.Entities
{
    public class StudioItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudioItemId { get; set; }
        public DateTime Acquired { get; set; }
        public DateTime? Sold { get; set; } //= null;
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string SerialNumber { get; set; }
        public decimal Price { get; set; } //= 10.00M;
        public decimal? SoldFor { get; set; } //= 0M;
        public bool Eurorack { get; set; } //= false;

        [ForeignKey("StudioItemTypeId")]
        public int StudioItemTypeId { get; set; }       
        public StudioItemType StudioItemType { get; set; }           

    }
}
