using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Repository.Models
{
    public class Book
    {
        public int Id { get; set; }
        public  required string Title { get; init; }
        public required string Description { get; init; }
    }
}
