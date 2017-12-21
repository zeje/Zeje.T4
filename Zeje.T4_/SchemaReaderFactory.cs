using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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

        public string ClassPrefix = "";
        public string ClassSuffix = "";
        public string SchemaName = null;
        /// <summary>
        /// 是否包含视图
        /// </summary>
        public bool IncludeViews = false;
        /// <summary>
        /// 排除的表的前缀
        /// </summary>
        public string[] ExcludeTablePrefixes = new string[] { "aspnet_", "webpages_", "sysdiagrams" };

        public string[] RemoveTablePrefixes = new string[] { "tbl_", "tb_", "t_" };
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString = "server=localhost;uid=sa;pwd=123456;database=Zeje;";
        /// <summary>
        /// 
        /// </summary>
        public string ProviderName = "System.Data.SqlClient";

        /// <summary>加载表结构
        /// </summary>
        /// <returns></returns>
        public DBTables LoadTables()
        {
            return DoAction<DBTables>(new Func<DbConnection, DbProviderFactory, SchemaReader, DBTables>((conn, _factory, reader) =>
             {
                 DBTables result = reader.ReadSchema(conn, _factory);
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
                     if (ExcludeTablePrefixes != null && ExcludeTablePrefixes.Any(it => result[i].Name.StartsWith(it)))
                     {
                         result.RemoveAt(i);
                         continue;
                     }
                 }
                 var rxClean = new Regex("^(Equals|GetHashCode|GetType|ToString|repo|Save|IsNew|Insert|Update|Delete|Exists|SingleOrDefault|Single|First|FirstOrDefault|Fetch|Page|Query)$");
                 foreach (var t in result)
                 {
                     foreach (var item in RemoveTablePrefixes)
                     {
                         if (t.ClassName.StartsWith(item)) t.ClassName = t.ClassName.TrimStart(item.ToCharArray());
                     }

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

             }));
        }


        /// <summary>加载表数据
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataTable LoadTableData(string tableName)
        {
            return DoAction<DataTable>(new Func<DbConnection, DbProviderFactory, SchemaReader, DataTable>((conn, _factory, reader) =>
            {
                DataTable dt = reader.ReadTable(conn, _factory, tableName);
                return dt;
            }));
        }

        private T DoAction<T>(Func<DbConnection, DbProviderFactory, SchemaReader, T> func) where T : class, new()
        {
            DbProviderFactory _factory;
            try
            {
                _factory = DbProviderFactories.GetFactory(ProviderName);
            }
            catch (Exception x)
            {
                var error = x.Message.Replace("\r\n", "\n").Replace("\n", " ");
                return new T();
            }
            try
            {
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
                    //执行委托方法
                    T result = func(conn, _factory, reader);
                    //关闭
                    conn.Close();
                    //返回
                    return result;
                }
            }
            catch
            {
                return new T();
            }
        }
    }
}
