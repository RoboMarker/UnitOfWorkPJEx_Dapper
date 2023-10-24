namespace UnitOfWorkPJEx_DapperRepository.Interface
{
    public interface IUnitOfWork_Dapper 
    {
        IUserRepository Users { get; }

        void Commit();
    }
}
