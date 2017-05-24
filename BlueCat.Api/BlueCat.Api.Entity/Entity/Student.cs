using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlueCat.Api.Entity.Entity
{
     [Table("Student")]
    public class Student
    {
        public Guid StudentId { get; set; }

        [StringLength(20)]
        public string LastName { get; set; }

        [StringLength(20)]
        public string FirstMidName { get; set; }

        public DateTime? EnrollmentDate { get; set; }
    }
}
