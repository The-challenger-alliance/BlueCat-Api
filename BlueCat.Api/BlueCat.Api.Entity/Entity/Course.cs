using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlueCat.Api.Entity.Entity
{
    [Table("Course")]
    public partial class Course
    {
        public Guid CourseID { get; set; }

        [StringLength(20)]
        public string Title { get; set; }

        [StringLength(20)]
        public string Credits { get; set; }
    }
}
