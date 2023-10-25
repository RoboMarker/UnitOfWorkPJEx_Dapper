using UnitOfWorkPJEx_DapperRepository.Interface;

namespace UnitOfWorkPJEx_DapperRepository.Factory.Interface
{
    public interface IUserRepositoryFactory
    {
        IUserRepository Create();
    }
}
