using BlueCat.Contract;

namespace BlueCat.Api.Service.Interface
{
    public interface IStudentService
    {
        GetStudentsResponse GetStudents(GetStudentsRequest request);
    }
}
