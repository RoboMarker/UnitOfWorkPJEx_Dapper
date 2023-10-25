using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWorkPJEx_DapperRepository.Factory.Interface;
using UnitOfWorkPJEx_DapperRepository.Interface;
using UnitOfWorkPJEx_DapperRepository.Repository;

namespace UnitOfWorkPJEx_DapperRepository.Factory
{
    public class UserRepositoryFactory: IUserRepositoryFactory
    {
        private IUnitOfWork_Dapper _unitOfWork;
        public UserRepositoryFactory(IUnitOfWork_Dapper unitOfWork)
        {
            _unitOfWork=unitOfWork;
        }
        public IUserRepository Create()
        {
            //  return new UserRepository(_unitOfWork);
            return null;
        }
    }
}
