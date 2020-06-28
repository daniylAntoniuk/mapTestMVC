using Septik.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Septik.Web.Models
{
    public class CityItemVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<CityImage> Images { get; set; }
    }
}
