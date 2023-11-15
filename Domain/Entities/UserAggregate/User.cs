﻿using Domain.Entities.CourseAggregate;
using Domain.Entities.MetricsAggregate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.UserAggregate
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? ProfileDescription { get; set; }
        public string? ProfilePicture { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual IEnumerable<Course> Courses { get; set; }
    }
}
