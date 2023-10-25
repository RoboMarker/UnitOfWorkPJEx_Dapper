using System.Data;
using UnitOfWorkPJEx_DapperRepository.Interface;
using UnitOfWorkPJEx_DapperRepository.Models.Data;

namespace UnitOfWorkPJEx_DapperRepository.Repository
{
    public class UserRepository2 : GenericRepository2<User>, IUserRepository
    {
        public UserRepository2(IDbConnection connection, IDbTransaction transaction) : base(connection, transaction)
        { 
        }
    }
}
