using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
namespace Zeje.T4DLL
{
    /// <summary>
    /// </summary>
    public class DbHelper
    {
        /// <summary>获得数据库中关于表的列表
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="lstTables"></param>
        /// <returns></returns>
        public static List<DbTable> GetDbTables(string connectionString, List<string> lstTables = null)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                string database = sqlConnection.Database;

                string strTables = string.Empty;
                if (lstTables != null)
                {
                    strTables = string.Format(" and obj.name in ('{0}')", string.Join(",", lstTables));
                }
                #region SQL
                string sql = string.Format(@"
SELECT obj.name       tablename,
       schem.name     schemname,
       idx.rows,
       (
           SELECT TOP 1 ep.[value]
           FROM   sys.extended_properties AS ep
           WHERE  ep.name = 'MS_Description'
                  AND ep.major_id = obj.[object_id]
       )              remark,
       CAST(
           CASE 
                WHEN (
                         SELECT COUNT(1)
                         FROM   sys.indexes
                         WHERE  OBJECT_ID = obj.OBJECT_ID
                                AND is_primary_key = 1
                     ) >= 1 THEN 1
                ELSE 0
           END 
           AS BIT
       )              hasPrimaryKey
FROM   [{0}].sys.objects obj
       INNER JOIN [{0}].dbo.sysindexes idx
            ON  obj.object_id = idx.id
            AND idx.indid <= 1
       INNER JOIN [{0}].sys.schemas schem
            ON  obj.schema_id = schem.schema_id
WHERE  TYPE = 'U' AND obj.name <> 'sysdiagrams'
ORDER BY
       obj.name
", database, strTables);
                #endregion
                DataTable dt = GetDataTable(sqlConnection, sql);
                return dt.Rows.Cast<DataRow>().Select(row => new DbTable
                {
                    tableName = row.Field<string>("tablename") ?? "",
                    schemaName = row.Field<string>("schemname") ?? "",
                    rows = row.Field<int>("rows"),
                    remark = row.Field<string>("remark") ?? "",
                    hasPrimaryKey = row.Field<bool>("hasPrimaryKey")
                }).ToList();
            }
        }
        /// <summary>获得某个表的列
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="tableName"></param>
        /// <param name="schema"></param>
        /// <returns></returns>
        public static List<DbColumn> GetDbColumns(string connectionString, string tableName, string schema = "dbo")
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                var lstColumn = GetDbColumns(sqlConnection, tableName);
                return lstColumn;
            }
        }
        /// <summary>获得某个表的列
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="tableName"></param>
        /// <param name="schema"></param>
        /// <returns></returns>
        public static List<DbColumn> GetDbColumns(SqlConnection sqlConnection, string tableName, string schema = "dbo")
        {
            #region SQL
            string sql = string.Format(@"                     
WITH indexCTE AS
     (
         SELECT ic.column_id,
                ic.index_column_id,
                ic.object_id
         FROM   [{0}].sys.indexes idx
                INNER JOIN [{0}].sys.index_columns ic
                     ON  idx.index_id = ic.index_id
                     AND idx.object_id = ic.object_id
         WHERE  idx.object_id = OBJECT_ID(@tableName)
                AND idx.is_primary_key = 1
     )

SELECT colm.column_id                   columnID,
       CAST(
           CASE 
                WHEN indexCTE.column_id IS NULL THEN 0
                ELSE 1
           END AS BIT
       )                                isPrimaryKey,
       colm.name                        columnName,
       systype.name                     columnType,
       colm.is_identity                 isIdentity,
       colm.is_nullable                 isNullable,
       CAST(colm.max_length AS INT)     byteLength,
       (
           CASE 
                WHEN systype.name = 'nvarchar'
           AND colm.max_length > 0 THEN colm.max_length / 2 
               WHEN systype.name = 'nchar'
           AND colm.max_length > 0 THEN colm.max_length / 2
               WHEN systype.name = 'ntext'
           AND colm.max_length > 0 THEN colm.max_length / 2 
               ELSE colm.max_length
               END
       )                                charLength,
       CAST(colm.precision AS INT)      precision,
       CAST(colm.scale AS INT)          scale,
       prop.value                       remark
FROM   [{0}].sys.columns colm
       INNER JOIN [{0}].sys.types systype
            ON  colm.system_type_id = systype.system_type_id
            AND colm.user_type_id = systype.user_type_id
       LEFT JOIN [{0}].sys.extended_properties prop
            ON  colm.object_id = prop.major_id
            AND colm.column_id = prop.minor_id
       LEFT JOIN indexCTE
            ON  colm.column_id = indexCTE.column_id
            AND colm.object_id = indexCTE.object_id
WHERE  colm.object_id = OBJECT_ID(@tableName)
ORDER BY
       colm.column_id", sqlConnection.Database);
            #endregion
            SqlParameter param = new SqlParameter("@tableName", SqlDbType.NVarChar, 100) { Value = string.Format("[{0}].[{1}].[{2}]", sqlConnection.Database, schema, tableName) };
            DataTable dt = GetDataTable(sqlConnection, sql, param);
            return dt.Rows.Cast<DataRow>().Select(row => new DbColumn()
            {
                columnID = row.Field<int>("columnID"),
                isPrimaryKey = row.Field<bool>("isPrimaryKey"),
                columnName = row.Field<string>("columnName") ?? "",
                columnType = row.Field<string>("columnType") ?? "",
                isIdentity = row.Field<bool>("isIdentity"),
                isNullable = row.Field<bool>("isNullable"),
                byteLength = row.Field<int>("byteLength"),
                charLength = row.Field<int>("charLength"),
                scale = row.Field<int>("scale"),
                remark = row.Field<string>("remark") ?? ""
            }).ToList();
        }
        /// <summary>执行命令获得数据
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="commandText"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static DataTable GetDataTable(SqlConnection sqlConnection, string commandText, params SqlParameter[] parms)
        {
            SqlCommand command = sqlConnection.CreateCommand();
            command.CommandText = commandText;
            command.Parameters.AddRange(parms);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }
    }
}
