using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Text;
using UnitOfWorkPJEx_DapperRepository.Interface;
using static Dapper.SqlMapper;

namespace Generally
{
    public class MsDBConn :IMsDBConn, IDisposable
    {
        IConfiguration _config;
        IDbConnection _conn;
        IDbTransaction _taran;

        public IDbConnection Connection
        {
            get { return _conn; }
        }

        public IDbTransaction Transcation
        {
            get { return _taran; }
        }
        const char dbParamPrefix = '@';
        public MsDBConn(IConfiguration config)
        {
            _config = config;
            _conn = new SqlConnection(_config.GetConnectionString("DefConnectionStr"));
            if (_conn.State != ConnectionState.Open)
                _conn.Open();

            if (_taran == null)
                _taran = _conn.BeginTransaction();
        }

        public int Insert<T>(T data)
        {
            int num = _conn.Execute(this.CreateInsertSql<T>(), data, _taran);
            return num;
        }

        public IEnumerable<T> Query<T>(string sSqlCmd, IDynamicParameters param = null)
        {

            var result =  _conn.Query<T>(sSqlCmd, param: param, transaction: _taran);
            return result;
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string sSqlCmd, IDynamicParameters param = null)
        {

            var result = await _conn.QueryAsync<T>(sSqlCmd, param: param, transaction: _taran);
            return result;
        }

        public int Excute(string sql, SqlMapper.IDynamicParameters param)
        {
            return _conn.Execute(sql, param, transaction: _taran);
        }

        private string CreateInsertSql<T>()
        {
            var typeList = typeof(T).GetProperties();

            StringBuilder sbsql = new StringBuilder();
            sbsql.Append(string.Format(@$"Insert Into ${typeof(T).Name}"));

            List<string> colList = new List<string>();
            List<string> valList = new List<string>();

            foreach (var item in typeList)
            {
                colList.Add($@"""${item.Name}""");
                valList.Add(dbParamPrefix + item.Name);
            }
            sbsql.Append($@"(${String.Join(",", colList.ToArray())})");
            sbsql.Append($@" values(${String.Join(",", valList.ToArray())})");
            return sbsql.ToString();
        }

        public void Commit()
        {
            try
            {
                _taran?.Commit();
                _conn.Close();
            }
            catch
            {
                Rollback();
                throw;
            }
            finally
            {
                _taran.Dispose();
                _taran = null;
            }
        }

        public void Rollback()
        {
            try
            {
                _taran?.Rollback();
            }
            finally
            {
                _taran?.Dispose();
                _taran = null;
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
                if (_taran != null)
                {
                    _taran.Dispose();
                    _taran = null;
                }
                if (_conn != null)
                {
                    _conn.Dispose();
                    _conn = null;
                }

            }
        }

        //~UnitOfWork_Dapper()
        //{
        //    Dispose(false);
        //}

    }
}
