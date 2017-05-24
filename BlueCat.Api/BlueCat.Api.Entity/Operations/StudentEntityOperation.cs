using BlueCat.Api.Entity.Entity;
using System;

namespace BlueCat.Api.Entity.Operations
{
    public class StudentEntityOperation
    {
        private SchoolDbContext context = new SchoolDbContext();

        private BaseEntityOperation<Student> studentRepository;

        private BaseEntityOperation<Course> courseRepository;

        public BaseEntityOperation<Student> StudentRepository
        {
            get
            {
                if (this.studentRepository == null)
                {
                    this.studentRepository = new BaseEntityOperation<Student>(context);
                }
                return studentRepository;
            }
        }

        public BaseEntityOperation<Course> CourseRepository
        {
            get
            {
                if (this.courseRepository == null)
                {
                    this.courseRepository = new BaseEntityOperation<Course>(context);
                }
                return courseRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }

            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
