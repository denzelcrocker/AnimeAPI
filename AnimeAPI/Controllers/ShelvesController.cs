using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AnimeAPI.Models;

namespace AnimeAPI.Controllers
{
    public class ShelvesController : ApiController
    {
        private MyAnimeShelf_KuznetsovaEntities1 db = new MyAnimeShelf_KuznetsovaEntities1();

        // GET: api/Shelves
        [ResponseType(typeof(List<ShelfModel>))]
        public IHttpActionResult GetShelf()
        {
            return Ok(db.Shelf.ToList().ConvertAll(x => new ShelfModel(x)));
        }
        [Route("api/Shelves/SortedList")]

        [HttpGet]

        public async Task<IHttpActionResult> SortedList(bool f)
        {

            if (f)
            {
                return Ok(db.Shelf.ToList().ConvertAll(x => new ShelfModel(x)).OrderBy(x => x.Status));
            }
            else
            {
                return Ok(db.Shelf.ToList().ConvertAll(x => new ShelfModel(x)).OrderBy(x => x.Status).Reverse());
            }
        }
        // GET: api/Shelves/5
        [ResponseType(typeof(Shelf))]
        public IHttpActionResult GetShelf(int id)
        {
            Shelf shelf = db.Shelf.Find(id);
            if (shelf == null)
            {
                return NotFound();
            }

            return Ok(shelf);
        }

        // PUT: api/Shelves/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutShelf(int id, Shelf shelf)
        {

            var dbnameofproduct = db.Shelf.FirstOrDefault(x => x.Id.Equals(id));

            dbnameofproduct.Anime = shelf.Anime;
            dbnameofproduct.Name = shelf.Name;
            dbnameofproduct.Status = shelf.Status;
            dbnameofproduct.Image = shelf.Image;
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShelfExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }
        // Search
        [Route("api/Shelves/search")]

        [HttpGet]
        public async Task<IHttpActionResult> ShelvesSearch(string nameofproductsSearchText, int field)
        {
            if (String.IsNullOrEmpty(nameofproductsSearchText))
            {

                switch (field)
                {

                    case 0:
                        return Ok(db.Shelf.ToList().ConvertAll(x => new ShelfModel(x)));
                        break;

                    case 1:
                        return Ok(db.Shelf.ToList().ConvertAll(x => new ShelfModel(x)).OrderBy(x => x.Name));
                        break;

                    case 2:

                        return Ok(db.Shelf.ToList().ConvertAll(x => new ShelfModel(x)).OrderByDescending(x => x.Name));
                        break;

                    case 3:
                        return Ok(db.Shelf.ToList().ConvertAll(x => new ShelfModel(x)).OrderBy(x => x.Status));
                        break;

                    case 4:
                        return Ok(db.Shelf.ToList().ConvertAll(x => new ShelfModel(x)).OrderByDescending(x => x.Status));
                        break;
                    default:
                        return Ok(db.Shelf.ToList().ConvertAll(x => new ShelfModel(x)));
                        break;
                }

            }
            else
            {
                switch (field)
                {

                    case 0:
                        return Ok(db.Shelf.ToList().ConvertAll(x => new ShelfModel(x)).Where(x => x.Anime.ToLower().Contains(nameofproductsSearchText.ToLower())));
                        break;

                    case 1:
                        return Ok(db.Shelf.ToList().ConvertAll(x => new ShelfModel(x)).Where(x => x.Anime.ToLower().Contains(nameofproductsSearchText.ToLower())).OrderBy(x => x.Name));
                        break;

                    case 2:

                        return Ok(db.Shelf.ToList().ConvertAll(x => new ShelfModel(x)).Where(x => x.Anime.ToLower().Contains(nameofproductsSearchText.ToLower())).OrderByDescending(x => x.Name));
                        break;

                    case 3:
                        return Ok(db.Shelf.ToList().ConvertAll(x => new ShelfModel(x)).Where(x => x.Anime.ToLower().Contains(nameofproductsSearchText.ToLower())).OrderBy(x => x.Status));
                        break;

                    case 4:
                        return Ok(db.Shelf.ToList().ConvertAll(x => new ShelfModel(x)).Where(x => x.Anime.ToLower().Contains(nameofproductsSearchText.ToLower())).OrderByDescending(x => x.Status));
                        break;
                    default:
                        return Ok(db.Shelf.ToList().ConvertAll(x => new ShelfModel(x)).Where(x => x.Anime.ToLower().Contains(nameofproductsSearchText.ToLower())));
                        break;
                }
            }
        }

        // POST: api/Shelves
        [ResponseType(typeof(Shelf))]
        public IHttpActionResult PostShelf(Shelf shelf)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Shelf.Add(shelf);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = shelf.Id }, shelf);
        }

        // DELETE: api/Shelves/5
        [ResponseType(typeof(Shelf))]
        public IHttpActionResult DeleteShelf(int id)
        {
            Shelf shelf = db.Shelf.Find(id);
            if (shelf == null)
            {
                return NotFound();
            }

            db.Shelf.Remove(shelf);
            db.SaveChanges();

            return Ok(shelf);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ShelfExists(int id)
        {
            return db.Shelf.Count(e => e.Id == id) > 0;
        }
    }
}