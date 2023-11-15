using Domain.Entities.CourseAggregate;
using Domain.Entities.UserAggregate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.MetricsAggregate
{
    public class Viewed
    {

        public int Id { get; set; }
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public DateTime ViewedAt { get; set; }
    }
}
