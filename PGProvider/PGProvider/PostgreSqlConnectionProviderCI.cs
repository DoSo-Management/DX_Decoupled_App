using System;
using System.Data;
using System.Globalization;
using System.Linq;
using DevExpress.Data.Filtering;
using DevExpress.Xpo.DB;
using DevExpress.Xpo.DB.Exceptions;
using DevExpress.Xpo.DB.Helpers;
using Npgsql;

//using DevExpress.ExpressApp;

namespace PostgreSqlConnectionProviderEx
{
    //public class Test1 : MSSqlConnectionProvider
    //{
    //    public Test1(IDbConnection connection, AutoCreateOption autoCreateOption) : base(connection, autoCreateOption) { }

    //    public override string FormatColumn(string columnName)
    //    {
    //        return base.FormatColumn(columnName);
    //    }
    //}

    // ReSharper disable once InconsistentNaming
    public class PostgreSqlConnectionProviderCI : PostgreSqlConnectionProvider
    {

        public PostgreSqlConnectionProviderCI(IDbConnection connection, AutoCreateOption autoCreateOption) : base(connection, autoCreateOption) { }
        public static event EventHandler<SelectDataFinishedEventArgs> SelectDataFinished;

        public override string FormatTable(string schema, string tableName)
        {
            var a = string.IsNullOrEmpty(schema)
                   ? string.Format(CultureInfo.InvariantCulture, "{0}", tableName.ToLower())
                   : string.Format(CultureInfo.InvariantCulture, "{0}.{1}", schema.ToLower(), tableName.ToLower());
            return a;
        }

        public override string FormatTable(string schema, string tableName, string tableAlias)
        {
            var a =
            string.IsNullOrEmpty(schema)
                ? string.Format(CultureInfo.InvariantCulture, "{0} {1}", tableName.ToLower(), tableAlias.ToLower())
                : string.Format(CultureInfo.InvariantCulture, "{0}.{1} {2}", schema.ToLower(), tableName.ToLower(),
                    tableAlias.ToLower());

            return a;
        }

        public override string ComposeSafeTableName(string tableName) => base.ComposeSafeTableName(tableName).ToLower();

        public string[] CustomFieldNames = new[] { "from", "to", "user", "limit" };

        public string ConvetColumnName(string name)
        {
            if (CustomFieldNames.Any(x => x == name.ToLower()))
                return '"' + name.ToLower() + '"';
            return name.ToLower();
        }

        public override string FormatColumn(string columnName)
        {
            var a = string.Format(CultureInfo.InvariantCulture, "{0}", ConvetColumnName(columnName));
            return a;
        }

//        protected override SelectStatementResult SelectData(Query query, CriteriaOperatorCollection targets)
//        {
//            if (SafePostgreSqlConnectionProvider.EnableProfiler)
//            {

//                var queryString = query.Sql;
//                var parameters = query.ParametersNames;
//                for (int i = 0; i < parameters.Count; i++)
//                {
//                    var value = query.Parameters[i].ToString().Replace("}", "'").Replace("{", "'");
//                    queryString = queryString.Replace(parameters[i].ToString(), value);
//                }

//                var tableName = ExtractString(queryString);
//                var startTime = DateTime.Now;
//                var data = base.SelectData(query, targets);
//                var span = DateTime.Now - startTime;
//                var ms = (int)span.TotalMilliseconds;
//                var _event = new SelectDataFinishedEventArgs() { Duration = ms, Query = queryString, TableName = tableName };
//                SelectDataFinished?.Invoke(this, _event);
//                return data;
//            }
//            var a = base.SelectData(query, targets);

//#if Dev
//            if (System.Diagnostics.Debugger.IsAttached)
//                if (query?.Sql?.Contains("select c.relname, c.relkind, n.nspname from pg_class c ") ?? false)
//                    try
//                    {
//                        foreach (var item in a.Rows)
//                            item.Values[1] = item.Values[1].ToString();
//                    }
//                    catch (Exception) { }
//#endif


//            return a;
//        }

        string ExtractString(string s)
        {
            var startTag = "from ";
            int startIndex = s.IndexOf(startTag) + startTag.Length;
            int endIndex = s.IndexOf(" n0", startIndex);
            return s.Substring(startIndex, endIndex - startIndex).Replace("(", "");
        }

        protected override void CreateDataBase()
        {
            try
            {
                base.CreateDataBase();
            }
            catch (UnableToOpenDatabaseException)
            {
                Connection.Close();
                Connection.Open();
                throw new InvalidOperationException("Unable To Connect Database. Try Again");
            }
        }

        protected override IDbCommand CreateCommand(Query query)
        {
            var command = base.CreateCommand(query);
            return command;
        }

        public override string FormatColumn(string columnName, string tableAlias)
        {
            return string.Format(CultureInfo.InvariantCulture, "{1}.{0}", ConvetColumnName(columnName), tableAlias.ToLower());
        }


        protected override long GetIdentity(InsertStatement root, TaggedParametersHolder identitiesByTag)
        {
            root.IdentityColumn = root.IdentityColumn.ToLower();
            return base.GetIdentity(root, identitiesByTag);
        }

        public override void CreateColumn(DBTable table, DBColumn column)
        {
            table.Name = table.Name.ToLower();
            column.Name = column.Name.ToLower();

            base.CreateColumn(table, column);
        }

        //public override string FormatSelect(string selectedPropertiesSql, string fromSql, string whereSql, string orderBySql, string groupBySql, string havingSql, int topSelectedRecords)
        //{
        //    return base.FormatSelect(selectedPropertiesSql, fromSql, whereSql, orderBySql, groupBySql, havingSql, topSelectedRecords);
        //}

        //public override string FormatSelect(string selectedPropertiesSql, string fromSql, string whereSql, string orderBySql, string groupBySql, string havingSql, int skipSelectedRecords, int topSelectedRecords)
        //{
        //    return base.FormatSelect(selectedPropertiesSql, fromSql, whereSql, orderBySql, groupBySql, havingSql, skipSelectedRecords, topSelectedRecords);
        //}

        public override string FormatFunction(ProcessParameter processParameter, FunctionOperatorType operatorType, params object[] operands)
        {
            switch (operatorType)
            {
                case FunctionOperatorType.StartsWith:
                case FunctionOperatorType.Contains:
                    var secondOperand = operands[1];
                    if (((OperandValue)secondOperand)?.Value is string)
                    {
                        var operandString = (string)((OperandValue)secondOperand).Value;
                        var prefix = operatorType == FunctionOperatorType.Contains ? "%" : "";
                        return string.Format(CultureInfo.InvariantCulture, "({0} ilike {1})", processParameter(operands[0]), processParameter(new ConstantValue($"{prefix}{operandString}%"))); ;
                    }
                    break;
            }
            return base.FormatFunction(processParameter, operatorType, operands);
        }

        public override string FormatBinary(BinaryOperatorType operatorType, string leftOperand, string rightOperand)
        {
            return operatorType == BinaryOperatorType.Like ?
                string.Format(CultureInfo.InvariantCulture, "{0} ilike {1}", leftOperand, rightOperand) :
                base.FormatBinary(operatorType, leftOperand, rightOperand);
        }

        //public override string FormatConstraint(string constraintName)
        //{
        // return string.Format(CultureInfo.InvariantCulture, "{0}", new object[] { this.ComposeSafeConstraintName(constraintName) });
        //}

        public new static void Register()
        {
            try
            { RegisterDataStoreProvider(XpoProviderTypeString, CreateProviderFromString); }
            // ReSharper disable once UnusedVariable
            catch (ArgumentException e)
            {
                //Tracing.Tracer.LogText(e.Message);
                //Tracing.Tracer.LogText("A connection provider with the same name ( {0} ) has already been registered",
                //XpoProviderTypeString);
            }
        }
        public new static IDataStore CreateProviderFromString(string connectionString, AutoCreateOption autoCreateOption, out IDisposable[] objectsToDisposeOnDisconnect)
        {
            IDbConnection connection = new NpgsqlConnection(connectionString);
            objectsToDisposeOnDisconnect = new IDisposable[] { connection };
            return CreateProviderFromConnection(connection, autoCreateOption);
        }

        public new static IDataStore CreateProviderFromConnection(IDbConnection connection, AutoCreateOption autoCreateOption) => new PostgreSqlConnectionProviderCI(connection, autoCreateOption);
        public new const string XpoProviderTypeString = "PostgresCI";
    }
}
