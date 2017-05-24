using BlueCat.Api.Entity.Entity;
using System.Collections.Generic;

namespace BlueCat.Api.Repository.Interface
{
    public interface IStudentRepository 
    {
        List<Student> GetAll();
    }
}
