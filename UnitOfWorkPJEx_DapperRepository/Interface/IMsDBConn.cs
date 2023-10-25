using Dapper;
using System.Data;
using static Dapper.SqlMapper;

namespace UnitOfWorkPJEx_DapperRepository.Interface
{
    public interface IMsDBConn
    {
        IDbConnection Connection { get; }

        IDbTransaction Transcation { get; }

        int Insert<T>(T data);


        IEnumerable<T> Query<T>(string sSqlCmd, IDynamicParameters param=null);
        Task<IEnumerable<T>> QueryAsync<T>(string sSqlCmd, IDynamicParameters param = null);


        int Excute(string sql, SqlMapper.IDynamicParameters param);
        void Commit();
        void Rollback();
        void Dispose();
    }
}
