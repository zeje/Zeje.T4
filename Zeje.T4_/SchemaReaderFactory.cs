using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Zeje.T4_
{
    public class SchemaReaderFactory
    {
        public SchemaReaderFactory()
        {
        }
        //public SchemaReaderFactory(ITextTemplatingEngineHost p_Host, TextTransformation p_outer)
        //{
        //    //_host = p_Host;
        //    //_outer = p_outer;
        //}

        public string ClassPrefix = "";
        public string ClassSuffix = "";
        public string SchemaName = null;
        public bool IncludeViews = false;
        public string ConnectionString = "server=10.8.7.4;uid=sa;pwd=Aa83986588;database=WynWXQY;";
        public string ProviderName = "System.Data.SqlClient";

        //private ITextTemplatingEngineHost _host;
        //public TextTransformation _outer;

        public DBTables LoadTables()
        {
            //_outer.WriteLine("// 此文件由Zeje T4模板生产");
            DbProviderFactory _factory;
            try
            {
                _factory = DbProviderFactories.GetFactory(ProviderName);
            }
            catch (Exception x)
            {
                var error = x.Message.Replace("\r\n", "\n").Replace("\n", " ");
                //_outer.Warning(string.Format("数据访问接口加载失败 `{0}` - {1}", ProviderName, error));
                //_outer.WriteLine("// -----------------------------------------------------------------------------------------");
                //_outer.WriteLine("// 数据访问接口加载失败 `{0}` - {1}", ProviderName, error);
                //_outer.WriteLine("// -----------------------------------------------------------------------------------------");
                return new DBTables();
            }
            try
            {
                DBTables result;
                using (var conn = _factory.CreateConnection())
                {
                    conn.ConnectionString = ConnectionString;
                    conn.Open();
                    SchemaReader reader = null;
                    if (_factory.GetType().Name == "MySqlClientFactory")
                    {
                        // MySql
                        reader = new MySqlSchemaReader();
                    }
                    else if (_factory.GetType().Name == "SqlCeProviderFactory")
                    {
                        // SQL CE
                        reader = new SqlServerCeSchemaReader();
                    }
                    else if (_factory.GetType().Name == "NpgsqlFactory")
                    {
                        // PostgreSQL
                        reader = new PostGreSqlSchemaReader();
                    }
                    else if (_factory.GetType().Name == "OracleClientFactory")
                    {
                        // Oracle
                        reader = new OracleSchemaReader();
                    }
                    else
                    {
                        // Assume SQL Server
                        reader = new SqlServerSchemaReader();
                    }
                    result = reader.ReadSchema(conn, _factory);
                    // Remove unrequired tables/views
                    for (int i = result.Count - 1; i >= 0; i--)
                    {
                        if (SchemaName != null && string.Compare(result[i].Schema, SchemaName, true) != 0)
                        {
                            result.RemoveAt(i);
                            continue;
                        }
                        if (!IncludeViews && result[i].IsView)
                        {
                            result.RemoveAt(i);
                            continue;
                        }
                    }
                    conn.Close();
                    var rxClean = new Regex("^(Equals|GetHashCode|GetType|ToString|repo|Save|IsNew|Insert|Update|Delete|Exists|SingleOrDefault|Single|First|FirstOrDefault|Fetch|Page|Query)$");
                    foreach (var t in result)
                    {
                        t.ClassName = ClassPrefix + t.ClassName + ClassSuffix;
                        foreach (var c in t.Columns)
                        {
                            c.PropertyName = rxClean.Replace(c.PropertyName, "_$1");
                            // Make sure property name doesn't clash with class name
                            if (c.PropertyName == t.ClassName)
                                c.PropertyName = "_" + c.PropertyName;
                        }
                    }
                    return result;
                }
            }
            catch (Exception x)
            {
                var error = x.Message.Replace("\r\n", "\n").Replace("\n", " ");
                //_outer.Warning(string.Format("读取数据模型失败 - {0}", error));
                //_outer.WriteLine("// -----------------------------------------------------------------------------------------");
                //_outer.WriteLine("// 读取数据模型失败 - {0}", error);
                //_outer.WriteLine("// -----------------------------------------------------------------------------------------");
                return new DBTables();
            }
        }
    }
}
