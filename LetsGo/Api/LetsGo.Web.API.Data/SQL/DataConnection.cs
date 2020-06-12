using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace LetsGo.Web.API.Data.SQL
{
    public class DataConnection
    {
        const int _commandTimeoutDefault = 30;
        readonly IDbConnection _conn;

        public DataConnection(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("LetsGo");
            _conn = new SqlConnection(connectionString);
        }

        private void OpenConnection()
        {
            if (_conn.State != ConnectionState.Open)
                _conn.Open();
        }

        private void CloseConnection()
        {
            if (_conn.State != ConnectionState.Closed)
                _conn.Close();
        }

        public T QueryScalar<T>(string sqlQuery, object parameters = null)
        {
            OpenConnection();

            var ret = _conn.Query<T>(sqlQuery, parameters).FirstOrDefault();

            CloseConnection();

            return ret;
        }

        public IEnumerable<T> Query<T>(string sqlQuery, object parameters = null, int? commandTimeout = null)
        {
            OpenConnection();

            var ret = _conn.Query<T>(sqlQuery, parameters, commandTimeout: commandTimeout ?? _commandTimeoutDefault);

            CloseConnection();

            return ret;
        }

        public void Execute(string sql, object parameters, int? commandTimeout = null)
        {
            OpenConnection();

            _conn.Execute(sql, parameters, commandTimeout: commandTimeout ?? _commandTimeoutDefault);

            CloseConnection();
        }

        ~DataConnection()
        {
            _conn.Dispose();
        }
    }
}
