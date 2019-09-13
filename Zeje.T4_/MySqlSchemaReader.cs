using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace Zeje.T4_
{
    public class MySqlSchemaReader : SchemaReader
    {
        // SchemaReader.ReadSchema
        public override DBTables ReadSchema(DbConnection connection, DbProviderFactory factory)
        {
            var result = new DBTables();
            var cmd = factory.CreateCommand();
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
                        tbl.Comment = rdr["TABLE_COMMENT"].ToString();
                        result.Add(tbl);
                    }
                }
            }
            //this will return everything for the DB
            var schema = connection.GetSchema("COLUMNS");
            //loop again - but this time pull by table name
            foreach (var item in result)
            {
                item.Columns = new List<DBColumn>();
                //pull the columns from the schema
                var columns = schema.Select("TABLE_NAME='" + item.Name + "'");
                foreach (var row in columns)
                {
                    DBColumn col = new DBColumn();
                    col.Name = row["COLUMN_NAME"].ToString();
                    col.PropertyName = Utils.CleanUp(col.Name);
                    col.PropertyType = GetPropertyType(row);
                    col.IsNullable = row["IS_NULLABLE"].ToString() == "YES";
                    col.IsPK = row["COLUMN_KEY"].ToString() == "PRI";
                    col.IsAutoIncrement = row["extra"].ToString().ToLower().IndexOf("auto_increment") >= 0;
                    col.Comment = row["COLUMN_COMMENT"].ToString();
                    item.Columns.Add(col);
                }
            }
            return result;
        }
        public override DataTable ReadTable(DbConnection connection, DbProviderFactory factory, string tableName)
        {
            DataTable dt;
            var cmd = factory.CreateCommand();
            cmd.CommandText = "select * from " + tableName;
            cmd.CommandType = CommandType.Text;
            DbDataAdapter adapter = factory.CreateDataAdapter();
            adapter.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            dt = ds.Tables[0];
            return dt;
        }
        static string GetPropertyType(DataRow row)
        {
            bool bUnsigned = row["COLUMN_TYPE"].ToString().IndexOf("unsigned") >= 0;
            string propType = "string";
            switch (row["DATA_TYPE"].ToString())
            {
                case "bigint":
                    propType = bUnsigned ? "ulong" : "long";
                    break;
                case "int":
                    propType = bUnsigned ? "uint" : "int";
                    break;
                case "smallint":
                    propType = bUnsigned ? "ushort" : "short";
                    break;
                case "guid":
                    propType = "Guid";
                    break;
                case "smalldatetime":
                case "date":
                case "datetime":
                case "timestamp":
                    propType = "DateTime";
                    break;
                case "float":
                    propType = "float";
                    break;
                case "double":
                    propType = "double";
                    break;
                case "numeric":
                case "smallmoney":
                case "decimal":
                case "money":
                    propType = "decimal";
                    break;
                case "bit":
                case "bool":
                case "boolean":
                    propType = "bool";
                    break;
                case "tinyint":
                    propType = bUnsigned ? "byte" : "sbyte";
                    break;
                case "image":
                case "binary":
                case "blob":
                case "mediumblob":
                case "longblob":
                case "varbinary":
                    propType = "byte[]";
                    break;
            }
            return propType;
        }
        const string TABLE_SQL = @"
			SELECT * 
			FROM information_schema.tables 
			WHERE (table_type='BASE TABLE' OR table_type='VIEW') AND TABLE_SCHEMA=DATABASE()
			";
    }
        
}
