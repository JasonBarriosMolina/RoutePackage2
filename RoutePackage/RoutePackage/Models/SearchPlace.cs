﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutePackage.Models
{
    public class SearchPlace
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public class SearchPlaces 
    {
        public List<SearchPlace> searchplace { get; set; }
    }
}
