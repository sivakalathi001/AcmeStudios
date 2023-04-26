﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace AcmeStudiosApi.Entities
{
    public class StudioItemType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudioItemTypeId { get; set; }
        [Required]
        public string Value { get; set; }
        [JsonIgnore]
        public ICollection<StudioItem> StudioItems { get; set; }
            = new List<StudioItem>();
    }
}
