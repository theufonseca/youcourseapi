using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.CourseAggregate
{
    public class Content
    {
        public int Id { get; set; }
        public int TrailId { get; set; }
        public int OrderNumber { get; set; }
        public string Title { get; set; }
        public ContentTypeEnum ContentType { get; set; }
        public string ContentUrl { get; set; }
        public bool IsVisible { get; set; }
    }
}
