using System.Data;
using UnitOfWorkPJEx_DapperRepository.Interface;

namespace UnitOfWorkPJEx_DapperRepository.Repository
{
    public class UnitOfWork_Dapper2 : IUnitOfWork_Dapper, IDisposable
    {
        private  IDbConnection _connection;
        private IDbTransaction _transaction;


        public IUserRepository Users
        { get; }
        public IDbConnection Connection => _connection;
        public IDbTransaction Transaction => _transaction;

        public UnitOfWork_Dapper2(IDbConnection connection, IDbTransaction transaction)
        {

            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }
            _transaction = transaction;
            Users = new UserRepository(this);
            //if (_trans == null)
            //{
            //    _trans = _connection.BeginTransaction();
            //}
        }
        public void Commit()
        {
            try
            {
                _transaction?.Commit();
            }
            catch
            {
                Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = null;
            }
        }


        public void Rollback()
        {
            try
            {
                _transaction?.Rollback();
            }
            finally
            {
                _transaction?.Dispose();
                _transaction = null;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_transaction != null)
                {
                    _transaction.Dispose();
                    _transaction = null;
                }
                if (_connection != null)
                {
                    _connection.Dispose();
                    _connection = null;
                }

            }
        }

        //~UnitOfWork_Dapper()
        //{
        //    Dispose(false);
        //}

    }
}
