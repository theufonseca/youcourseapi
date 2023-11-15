using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.CourseAggregate
{
    public class Trail
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int OrderNumber { get; set; }
        public string Title { get; set; }
        public bool IsVisible { get; set; }

        public virtual IEnumerable<Content> Contents { get; set; }
    }
}
