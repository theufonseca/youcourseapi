using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.UserAggregate
{
    public class CourseFollowed
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public bool IsFollow { get; set; }
        public decimal Progress { get; set; }
        public bool IsFinished { get; set; }
        public DateTime StartFollowAt { get; set; }
    }
}
