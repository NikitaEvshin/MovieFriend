using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieFriend.Models
{
    interface IMovie
    {
        public int Id { get; set; }
        public string Images { get; set; }
        public string Trailer { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public string Reviews { get; set; }
        public string NameAuthor { get; set; }
        public string Called { get; set; }
    }
}
