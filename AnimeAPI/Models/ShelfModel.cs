using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnimeAPI.Models
{
    public class ShelfModel
    {
        public ShelfModel(Shelf shelf)
        { 
            Id = shelf.Id;
            Anime = shelf.Anime;
            Name = shelf.Name;
            Status = shelf.Status;
            Image = shelf.Image;
        }
            public int Id { get; set; }
            public string Anime { get; set; }
            public string Name { get; set; }
            public string Status { get; set; }
            public string Image { get; set; }
    }
}