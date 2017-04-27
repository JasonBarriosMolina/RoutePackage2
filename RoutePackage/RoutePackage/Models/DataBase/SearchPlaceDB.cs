using System;
using SQLite.Net.Attributes;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace RoutePackage.Models.Database
{
    [Table("SearchPlace")]
    public class SearchPlaceDB
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

}
