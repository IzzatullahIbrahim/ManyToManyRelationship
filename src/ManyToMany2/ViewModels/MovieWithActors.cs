﻿using ManyToMany2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManyToMany2.ViewModels
{
    public class MovieWithActors
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public List<Actor> Actors { get; set; }
    }
}
