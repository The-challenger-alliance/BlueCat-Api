using BlueCat.Api.Entity.Entity;
using BlueCat.Api.Entity.Operations;
using BlueCat.Api.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlueCat.Api.Repository.Impl
{
    public class StudentRepository : IStudentRepository, IDisposable
    {
        private StudentEntityOperation studentEntityOperation;

        public StudentRepository()
        {
            this.studentEntityOperation = new StudentEntityOperation();
        }
        public List<Student> GetAll()
        {
            List<Student> students = studentEntityOperation.StudentRepository.Get().ToList();

            return students;
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.studentEntityOperation.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        //protected override void Dispose(bool disposing)
        //{
        //    this.studentEntityOperation.Dispose();
        //    Dispose(true);
        //}
    }
}
