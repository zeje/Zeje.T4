using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zeje.T4_
{
    public class SqlServerSchemaReader : SchemaReader
    {
        // SchemaReader.ReadSchema
        public override DBTables ReadSchema(DbConnection connection, DbProviderFactory factory)
        {
            var result = new DBTables();
            _connection = connection;
            _factory = factory;
            var cmd = _factory.CreateCommand();
            cmd.Connection = connection;
            cmd.CommandText = TABLE_SQL;
            //pull the tables in a reader
            using (cmd)
            {
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        DBTable tbl = new DBTable();
                        tbl.Name = rdr["TABLE_NAME"].ToString();
                        tbl.Schema = rdr["TABLE_SCHEMA"].ToString();
                        tbl.IsView = string.Compare(rdr["TABLE_TYPE"].ToString(), "View", true) == 0;
                        tbl.CleanName = Utils.CleanUp(tbl.Name);
                        tbl.ClassName = Inflector.MakeSingular(tbl.CleanName);
                        if (rdr["Comment"] != null)
                        {
                            tbl.Comment = Utils.FormatComment(rdr["Comment"].ToString());
                        }
                        result.Add(tbl);
                    }
                }
            }
            foreach (var tbl in result)
            {
                tbl.Columns = LoadColumns(tbl);
                // Mark the primary key
                string PrimaryKey = GetPK(tbl.Name);
                var pkColumn = tbl.Columns.SingleOrDefault(x => x.Name.ToLower().Trim() == PrimaryKey.ToLower().Trim());
                if (pkColumn != null)
                {
                    pkColumn.IsPK = true;
                }
            }
            return result;
        }
        public override DataTable ReadTable(DbConnection connection, DbProviderFactory factory, string tableName)
        {
            DataTable dt;
            _factory = factory;
            var cmd = _factory.CreateCommand();
            cmd.CommandText = "select * from " + tableName;
            cmd.CommandType = CommandType.Text;
            DbDataAdapter adapter = factory.CreateDataAdapter();
            adapter.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            dt = ds.Tables[0];
            return dt;
        }

        DbConnection _connection;
        DbProviderFactory _factory;
        List<DBColumn> LoadColumns(DBTable tbl)
        {
            using (var cmd = _factory.CreateCommand())
            {
                cmd.Connection = _connection;
                cmd.CommandText = COLUMN_SQL;
                var p = cmd.CreateParameter();
                p.ParameterName = "@tableName";
                p.Value = tbl.Name;
                cmd.Parameters.Add(p);
                p = cmd.CreateParameter();
                p.ParameterName = "@schemaName";
                p.Value = tbl.Schema;
                cmd.Parameters.Add(p);
                var result = new List<DBColumn>();
                using (IDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        DBColumn col = new DBColumn();
                        col.Name = rdr["ColumnName"].ToString();
                        col.PropertyName = Utils.CleanUp(col.Name);
                        col.PropertyType = GetPropertyType(rdr["DataType"].ToString());
                        col.IsNullable = rdr["IsNullable"].ToString() == "YES";
                        col.IsAutoIncrement = ((int)rdr["IsIdentity"]) == 1;
                        if (rdr["Comment"] != null)
                        {
                            col.Comment = Utils.FormatComment(rdr["Comment"].ToString());
                        }
                        if (rdr["DataType"] != null)
                        {
                            col.DataType = rdr["DataType"].ToString();
                        }

                        if (rdr["MaxLength"] != null)
                        {
                            col.MaxLength = rdr["MaxLength"].ToString();
                        }
                        result.Add(col);
                    }
                }
                return result;
            }
        }
        string GetPK(string table)
        {
            string sql = @"SELECT c.name AS ColumnName
                FROM sys.indexes AS i 
                INNER JOIN sys.index_columns AS ic ON i.object_id = ic.object_id AND i.index_id = ic.index_id 
                INNER JOIN sys.objects AS o ON i.object_id = o.object_id 
                LEFT OUTER JOIN sys.columns AS c ON ic.object_id = c.object_id AND c.column_id = ic.column_id
                WHERE (i.is_primary_key = 1) AND (o.name = @tableName)";
            using (var cmd = _factory.CreateCommand())
            {
                cmd.Connection = _connection;
                cmd.CommandText = sql;
                var p = cmd.CreateParameter();
                p.ParameterName = "@tableName";
                p.Value = table;
                cmd.Parameters.Add(p);
                var result = cmd.ExecuteScalar();
                if (result != null)
                    return result.ToString();
            }
            return "";
        }
        string GetPropertyType(string sqlType)
        {
            string sysType = "string";
            switch (sqlType)
            {
                case "bigint":
                    sysType = "long";
                    break;
                case "smallint":
                    sysType = "short";
                    break;
                case "int":
                    sysType = "int";
                    break;
                case "uniqueidentifier":
                    sysType = "Guid";
                    break;
                case "smalldatetime":
                case "datetime":
                case "datetime2":
                case "date":
                case "time":
                    sysType = "DateTime";
                    break;
                case "datetimeoffset":
                    sysType = "DateTimeOffset";
                    break;
                case "float":
                    sysType = "double";
                    break;
                case "real":
                    sysType = "float";
                    break;
                case "numeric":
                case "smallmoney":
                case "decimal":
                case "money":
                    sysType = "decimal";
                    break;
                case "tinyint":
                    sysType = "byte";
                    break;
                case "bit":
                    sysType = "bool";
                    break;
                case "image":
                case "binary":
                case "varbinary":
                case "timestamp":
                    sysType = "byte[]";
                    break;
                case "geography":
                    sysType = "Microsoft.SqlServer.Types.SqlGeography";
                    break;
                case "geometry":
                    sysType = "Microsoft.SqlServer.Types.SqlGeometry";
                    break;
            }
            return sysType;
        }
        const string TABLE_SQL = @"
SELECT *,
       (
           SELECT TOP 1 ISNULL(VALUE, '')
           FROM   sys.extended_properties
           WHERE  NAME             = 'MS_Description'
                  AND major_id     = OBJECT_ID(TABLE_NAME)
                  AND minor_id     = 0
       ) Comment
FROM   INFORMATION_SCHEMA.TABLES
WHERE  TABLE_TYPE = 'BASE TABLE'
       OR  TABLE_TYPE = 'VIEW'
";
        const string COLUMN_SQL = @"
SELECT TABLE_CATALOG             AS [Database],
       TABLE_SCHEMA              AS [OWNER],
       TABLE_NAME                AS TableName,
       COLUMN_NAME               AS ColumnName,
       ORDINAL_POSITION          AS OrdinalPosition,
       COLUMN_DEFAULT            AS DefaultSetting,
       IS_NULLABLE               AS IsNullable,
       DATA_TYPE                 AS DataType,
       CHARACTER_MAXIMUM_LENGTH  AS MaxLength,
       DATETIME_PRECISION        AS DatePrecision,
       COLUMNPROPERTY(
           OBJECT_ID('[' + TABLE_SCHEMA + '].[' + TABLE_NAME + ']'),
           COLUMN_NAME,
           'IsIdentity'
       )                         AS IsIdentity,
       COLUMNPROPERTY(
           OBJECT_ID('[' + TABLE_SCHEMA + '].[' + TABLE_NAME + ']'),
           COLUMN_NAME,
           'IsComputed'
       )                         AS IsComputed,
       (
           SELECT TOP 1 ISNULL(VALUE, '')
           FROM   sys.extended_properties
           WHERE  NAME             = 'MS_Description'
                  AND major_id     = OBJECT_ID(TABLE_NAME)
                  AND minor_id     = ORDINAL_POSITION
       )                         AS Comment
FROM   INFORMATION_SCHEMA.COLUMNS
WHERE  TABLE_NAME = @tableName
       AND TABLE_SCHEMA = @schemaName
ORDER BY
       OrdinalPosition           ASC
";
    }
}
