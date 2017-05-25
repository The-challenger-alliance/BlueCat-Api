using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlueCat.Api.Repository.Interface;
using BlueCat.Api.Service.Impl;
using BlueCat.Api.Repository.Impl;
using BlueCat.Contract;

namespace BlueCat.Service.Test
{
    [TestClass]
    public class StudentServiceTest
    {
        [TestMethod]
        public void GetStudentsTest()
        {
            IStudentRepository studentRepository = new StudentRepository();
            StudentService studentService = new StudentService(studentRepository);
            GetStudentsRequest resquest = new GetStudentsRequest();
            resquest.Name = "test";
            GetStudentsResponse response = studentService.GetStudents(resquest);

            if (response.ResponseResult)
            {
                Assert.IsTrue(response.Students.Count > 0);
            }
            else
            {
                Assert.IsFalse(true);
            }
        }
    }
}
