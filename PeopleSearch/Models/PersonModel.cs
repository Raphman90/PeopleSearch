﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeopleSearch.Models
{
    public class PersonModel
    {
        public int Id { get; set; }
        public int Age { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Interests { get; set; }
        public string Address { get; set; }

        public string ImageLocation { get; set; }
    }
}
