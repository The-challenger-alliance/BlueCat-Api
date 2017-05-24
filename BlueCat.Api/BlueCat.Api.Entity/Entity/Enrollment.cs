using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlueCat.Api.Entity.Entity
{
    [Table("Enrollment")]
    public class Enrollment
    {
        public Guid EnrollmentID { get; set; }

        public Guid CourseID { get; set; }

        public Guid StudentID { get; set; }

        public DateTime? Grade { get; set; }
    }
}
