﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace Zeje.T4_
{
    public class PostGreSqlSchemaReader : SchemaReader
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
                        tbl.Name = rdr["table_name"].ToString();
                        tbl.Schema = rdr["table_schema"].ToString();
                        tbl.IsView = string.Compare(rdr["table_type"].ToString(), "View", true) == 0;
                        tbl.CleanName = Utils.CleanUp(tbl.Name);
                        tbl.ClassName = Inflector.MakeSingular(tbl.CleanName);
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
            return null;
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
                        col.Name = rdr["column_name"].ToString();
                        col.PropertyName = Utils.CleanUp(col.Name);
                        col.PropertyType = GetPropertyType(rdr["udt_name"].ToString());
                        col.IsNullable = rdr["is_nullable"].ToString() == "YES";
                        col.IsAutoIncrement = rdr["column_default"].ToString().StartsWith("nextval(");
                        result.Add(col);
                    }
                }
                return result;
            }
        }
        string GetPK(string table)
        {
            string sql = @"SELECT kcu.column_name 
			FROM information_schema.key_column_usage kcu
			JOIN information_schema.table_constraints tc
			ON kcu.constraint_name=tc.constraint_name
			WHERE lower(tc.constraint_type)='primary key'
			AND kcu.table_name=@tablename";
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
            switch (sqlType)
            {
                case "int8":
                case "serial8":
                    return "long";
                case "bool":
                    return "bool";
                case "bytea	":
                    return "byte[]";
                case "float8":
                    return "double";
                case "int4":
                case "serial4":
                    return "int";
                case "money	":
                    return "decimal";
                case "numeric":
                    return "decimal";
                case "float4":
                    return "float";
                case "int2":
                    return "short";
                case "time":
                case "timetz":
                case "timestamp":
                case "timestamptz":
                case "date":
                    return "DateTime";
                default:
                    return "string";
            }
        }
        const string TABLE_SQL = @"
			SELECT table_name, table_schema, table_type
			FROM information_schema.tables 
			WHERE (table_type='BASE TABLE' OR table_type='VIEW')
				AND table_schema NOT IN ('pg_catalog', 'information_schema');
			";
        const string COLUMN_SQL = @"
			SELECT column_name, is_nullable, udt_name, column_default
			FROM information_schema.columns 
			WHERE table_name=@tableName;
			";
    }
}
