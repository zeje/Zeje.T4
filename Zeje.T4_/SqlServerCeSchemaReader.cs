﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace Zeje.T4_
{
    public class SqlServerCeSchemaReader : SchemaReader
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
                        tbl.CleanName = Utils.CleanUp(tbl.Name);
                        tbl.ClassName = Inflector.MakeSingular(tbl.CleanName);
                        tbl.Schema = null;
                        tbl.IsView = false;
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
                    pkColumn.IsPK = true;
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
                        col.IsAutoIncrement = rdr["AUTOINC_INCREMENT"] != DBNull.Value;
                        result.Add(col);
                    }
                }
                return result;
            }
        }
        string GetPK(string table)
        {
            string sql = @"SELECT KCU.COLUMN_NAME 
			FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE KCU
			JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS TC
			ON KCU.CONSTRAINT_NAME=TC.CONSTRAINT_NAME
			WHERE TC.CONSTRAINT_TYPE='PRIMARY KEY'
			AND KCU.TABLE_NAME=@tableName";
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
                case "date":
                case "time":
                    sysType = "DateTime";
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
            }
            return sysType;
        }
        const string TABLE_SQL = @"SELECT *
		FROM  INFORMATION_SCHEMA.TABLES
		WHERE TABLE_TYPE='TABLE'";
        const string COLUMN_SQL = @"SELECT 
			TABLE_CATALOG AS [Database],
			TABLE_SCHEMA AS Owner, 
			TABLE_NAME AS TableName, 
			COLUMN_NAME AS ColumnName, 
			ORDINAL_POSITION AS OrdinalPosition, 
			COLUMN_DEFAULT AS DefaultSetting, 
			IS_NULLABLE AS IsNullable, DATA_TYPE AS DataType, 
			AUTOINC_INCREMENT,
			CHARACTER_MAXIMUM_LENGTH AS MaxLength, 
			DATETIME_PRECISION AS DatePrecision
		FROM  INFORMATION_SCHEMA.COLUMNS
		WHERE TABLE_NAME=@tableName
		ORDER BY OrdinalPosition ASC";
    }
}
