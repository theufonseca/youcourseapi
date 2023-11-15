using Domain.Entities.CourseAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class CourseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string TrailsTitles { get; set; }
        public string ContentTitles { get; set; }
        public string ThumbImage { get; set; }
        public string Tags { get; set; }
        public DateTime CreatedDate { get; set; }

        public static CourseModel FromCourse(Course course)
        {
            return new CourseModel
            {
                Id = course.Id,
                Name = course.Name,
                Description = course.Description,
                TrailsTitles = string.Join(",", course.Trails.Select(t => t.Title)),
                ContentTitles = string.Join(",", course.Trails.SelectMany(t => t.Contents).Select(c => c.Title)),
                ThumbImage = course.ThumbImage,
                Tags = course.Tags,
                CreatedDate = course.CreatedDate
            };
        }
    }
}
