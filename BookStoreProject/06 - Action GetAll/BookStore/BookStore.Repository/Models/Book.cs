using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Repository.Models
{
    [PrimaryKey(nameof(Id))]
    public class Book
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public  required string Title { get; init; }

        public required string Description { get; init; }

        public int AuthorId { get; set; }
        public virtual Author? Author { get; set; }
    }
}
