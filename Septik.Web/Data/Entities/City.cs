using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Septik.Web.Data.Entities
{
    [Table("tblCities")]
    public class City
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(255)]
        public string Name { get; set; }
        [Required, StringLength(255)]
        public string Image { get; set; }
        [Required, StringLength(255)]
        public string Lat { get; set; }
        [Required, StringLength(255)]
        public string Lng { get; set; }

        public virtual ICollection<CityImage> CityImages { get; set; }
    }
}
