using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ManyToMany2.Data;
using ManyToMany2.ViewModels;
using ManyToMany2.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ManyToMany2.API
{
    [Route("api/[controller]")]
    public class ActorsController : Controller
    {
        private ApplicationDbContext _db;

        public ActorsController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public List<Actor> Get()
        {
            List<Actor> actors = (from a in _db.Actors
                                  select a).ToList();
            return actors;
        }

        [HttpGet("{id}")]
        public ActorWithMovies Get(int id)
        {
            ActorWithMovies act = (from a in _db.Actors
                                   where a.Id == id
                                   select new ActorWithMovies
                                   {
                                       Id = a.Id,
                                       FirstName = a.FirstName,
                                       LastName = a.LastName,
                                       Movies = (from am in _db.MovieActors
                                                 where am.ActorId == a.Id
                                                 select am.Movie).ToList()
                                   }).FirstOrDefault();
            return act;
        }
    }
}
