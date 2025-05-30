﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Core.Entities
{
    public class Trip : ModelBase
    {
        public string Name { get; set; }
        public string CityName { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public string Content { get; set; } // HTML Content
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
