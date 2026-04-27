using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Models
{
    public class Rating
    {
        public Guid Id { get; set; }
       
        public Guid MovieId { get; set; }
        public Movie Movie { get; set; } = null!;

        public int Score { get; set; }
        public string UserName { get; set; } = null!;


    }
}
