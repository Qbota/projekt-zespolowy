﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplication.Mongo.Models
{
    public class MealPreferenceDO
    {
        public IEnumerable<string> Cousines { get; set; }
        public IEnumerable<string> Allergens { get; set; }
    }
}
