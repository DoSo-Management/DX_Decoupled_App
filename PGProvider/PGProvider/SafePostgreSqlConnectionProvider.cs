using System;
using System.Data;
using System.Linq;
using DevExpress.Xpo.DB;
using DevExpress.Xpo.DB.Exceptions;
using DevExpress.Xpo.Helpers;

namespace PostgreSqlConnectionProviderEx
{
    public class SafePostgreSqlConnectionProvider : IDataStore, IDisposable, ICommandChannel
    {
        PostgreSqlConnectionProviderCI _innerDataStore;
        IDbConnection _connection;
        public static IDbConnection ConnectionStatic;
        public static string ConnectionStrinStatic;
        readonly string _connectionString;
        readonly AutoCreateOption _autoCreateOption;

        public static bool EnableProfiler;
        

        public SafePostgreSqlConnectionProvider(string connectionString, AutoCreateOption autoCreateOption)
        {
            ConnectionStrinStatic = connectionString;
            //XpoDefault.ConnectionString = connectionString;
            _connectionString = connectionString;
            _autoCreateOption = autoCreateOption;
            DoReconnect();
        }

        ~SafePostgreSqlConnectionProvider()
        {
            Dispose(false);
        }

        public static void Register()
        {
            //var store = CreateProviderFromString;
            DataStoreBase.RegisterDataStoreProvider(XpoProviderTypeString, CreateProviderFromString);
        }
        //
        public const string XpoProviderTypeString = "Postgres2";

        public static IDataStore CreateProviderFromString(string connectionString, AutoCreateOption autoCreateOption, out IDisposable[] objectsToDisposeOnDisconnect)
        {
            var rv = new SafePostgreSqlConnectionProvider(connectionString, autoCreateOption);
            objectsToDisposeOnDisconnect = new IDisposable[] { rv };
            return rv;
        }

        void DoReconnect()
        {
            DoDispose(false);
            _connection = PostgreSqlConnectionProvider.CreateConnection(_connectionString);
            ConnectionStatic = _connection;
            _innerDataStore = new PostgreSqlConnectionProviderCI(_connection, _autoCreateOption);
        }

        void DoDispose(bool closeConnection)
        {
            if (_connection == null) return;

            if (closeConnection)
            {
                _connection.Close();
                _connection.Dispose();
            }
            _connection = null;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                DoDispose(true);
        }

        void HandleNullReferenceException(Exception ex)
        {
            if (ex == null) return;
            if (ex is NullReferenceException && _innerDataStore.Connection.State == ConnectionState.Open)
            {
                DoReconnect();
                return;
            }
            //NpgsqlException npgex = ex as NpgsqlException;
            //if (npgex != null && npgex.Errors != null && _innerDataStore.Connection.State == ConnectionState.Open)
            //{
            //    foreach (NpgsqlError error in npgex.Errors)
            //    {
            //        if (error.Message.Contains("broken"))
            //        {
            //            DoReconnect();
            //            return;
            //        }
            //    }
            //}
            throw ex;
        }

        //void HandleNullReferenceException(Exception ex)
        //{
        //    if (ex == null) return;
        //    if (ex is NullReferenceException && _innerDataStore.Connection.State == ConnectionState.Open)
        //    {
        //        DoReconnect();
        //        return;
        //    }
        //    var npgex = ex as NpgsqlException;

        //    if (npgex?.Errors == null || _innerDataStore.Connection.State != ConnectionState.Open)
        //        throw ex;

        //    if (!npgex.Errors.Cast<NpgsqlError>().Any(error => error.Message.Contains("broken"))) throw ex;

        //    DoReconnect();
        //}

        AutoCreateOption IDataStore.AutoCreateOption => _innerDataStore.AutoCreateOption;

        ModificationResult IDataStore.ModifyData(params ModificationStatement[] dmlStatements)
        {
            try { return _innerDataStore.ModifyData(dmlStatements); }
            catch (SqlExecutionErrorException ex) { HandleNullReferenceException(ex.InnerException); }

            return _innerDataStore.ModifyData(dmlStatements);
        }

        //public void selecD()
        //{
        //    var a = SelectData(null);
        //}

        SelectedData IDataStore.SelectData(params SelectStatement[] selects)
        {
            try
            {
                //if (EnableProfiler)
                //{
                //    var startTime = DateTime.Now;
                //    var data = _innerDataStore.SelectData(selects);
                //    var span = DateTime.Now - startTime;
                //    var ms = (int)span.TotalMilliseconds;
                //    var tableNames = "";
                //    var queries = "";
                //    foreach (var selectStatement in selects)
                //    {
                //        tableNames += selectStatement.TableName + "; ";
                //        queries += selectStatement + Environment.NewLine + "----------------------------------------" + Environment.NewLine;
                //    }

                //    var _event = new SelectDataFinishedEventArgs() { Duration = ms, Query = queries, TableName = tableNames };
                //    SelectDataFinished?.Invoke(this, _event);
                //    return data;
                //}
                return _innerDataStore.SelectData(selects);
            }
            catch (NullReferenceException ex)
            {
                HandleNullReferenceException(ex.InnerException);
            }
            catch (SqlExecutionErrorException ex)
            {
                DoReconnect();
            }

            return _innerDataStore.SelectData(selects);
        }

        UpdateSchemaResult IDataStore.UpdateSchema(bool dontCreateIfFirstTableNotExist, params DBTable[] tables)
        {
            try { return _innerDataStore.UpdateSchema(dontCreateIfFirstTableNotExist, tables.Where(t => !t.Name.StartsWith("public")).ToArray()); }
            catch (SqlExecutionErrorException ex) { HandleNullReferenceException(ex.InnerException); }

            return _innerDataStore.UpdateSchema(dontCreateIfFirstTableNotExist, tables);
        }



        void IDisposable.Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        //T270757
        public object Do(string command, object args) => ((ICommandChannel)_innerDataStore).Do(command, args);
    }
}