using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutePackage.Models
{

    public class SearchCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class SearchCategories
    {
        public List<SearchCategory> searchcategory { get; set; }
    }


}
