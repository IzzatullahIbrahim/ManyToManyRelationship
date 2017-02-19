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
    public class MoviesController : Controller
    {
        private ApplicationDbContext _db;

        public MoviesController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public List<Movie> Get()
        {
            return _db.Movies.ToList();
        }

        [HttpGet("{id}")]
        public MovieWithActors Get(int id)
        {
            MovieWithActors mov = (from m in _db.Movies
                                   where m.Id == id
                                   select new MovieWithActors
                                   {
                                       Id = m.Id,
                                       Title = m.Title,
                                       Director = m.Director,
                                       Actors = (from ma in _db.MovieActors
                                                 where ma.MovieId == m.Id
                                                 select ma.Actor).ToList()
                                   }).FirstOrDefault();
            return mov;
        }

        [HttpPost]
        public IActionResult Post([FromBody] MovieWithActors mov)
        {
            if (mov == null)
            {
                return BadRequest();
            }
            else if (mov.Id == 0)
            {
                Movie newMov = new Movie
                {
                    Title = mov.Title,
                    Director = mov.Director
                };
                _db.Movies.Add(newMov);
                _db.SaveChanges();

                foreach (Actor actor in mov.Actors)
                {
                    _db.MovieActors.Add(new MovieActor
                    {
                        MovieId = newMov.Id,
                        ActorId = actor.Id
                    });
                    _db.SaveChanges();
                }

                return Ok();
            }
            else {
                //edit stuff
                return BadRequest();
            }
        }
    }
}
