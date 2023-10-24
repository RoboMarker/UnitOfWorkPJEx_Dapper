using UnitOfWorkPJEx_DapperRepository.Models.Data;

namespace UnitOfWorkPJEx_DapperService.Interface
{
    public interface IUserService
    {
        Task<User> GetById(int UserId);

        Task<IEnumerable<User>> GetUserAll();

        Task<bool> CreateUser(User user);
        Task<bool> UpdateUser(User user);

        Task<bool> DeleteUser(int UserId);
    }
}
