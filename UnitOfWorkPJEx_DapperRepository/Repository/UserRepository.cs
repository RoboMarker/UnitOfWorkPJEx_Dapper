using System.Data;
using UnitOfWorkPJEx_DapperRepository.Interface;
using UnitOfWorkPJEx_DapperRepository.Models.Data;

namespace UnitOfWorkPJEx_DapperRepository.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(IDbConnection connection, IDbTransaction transaction) : base(connection, transaction)
        { 
        }
    }
}
