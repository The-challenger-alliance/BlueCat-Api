using BlueCat.Api.Entity.Entity;
using BlueCat.Api.Repository.Interface;
using BlueCat.Api.Service.Interface;
using BlueCat.Contract;
using System.Collections.Generic;

namespace BlueCat.Api.Service.Impl
{
    public class StudentService : IStudentService
    {
        private IStudentRepository StudentRepository { get; set; }

        public StudentService(IStudentRepository studentRepository)
        {
            this.StudentRepository = studentRepository;
        }

        public GetStudentsResponse GetStudents(GetStudentsRequest request)
        {
            List<Student> students = this.StudentRepository.GetAll();

            GetStudentsResponse response = new GetStudentsResponse();
            response.ResponseResult = true;
            response.Students = students;

            //do more here...

            return response;
        }

    }
}
