using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zeje.T4_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zeje.T4_Tests
{
    [TestClass()]
    public class SchemaReaderFactoryTests
    {
        [TestMethod()]
        public void MysqlTest()
        {
            SchemaReaderFactory srf = new SchemaReaderFactory();

            //srf.ConnectionString = "User ID=root;Password=123456;Host=127.0.0.1;Port=3306;Database=DB;Pooling=true;Min Pool Size=0;Max Pool Size=100;CharSet=UTF8;";
            srf.ConnectionString = "User ID=root;Password=admintest123456;Host=47.105.237.32;Port=5882;Database=QXDatabaseDev;Pooling=true;Min Pool Size=0;Max Pool Size=100;CharSet=UTF8;";
            srf.ProviderName = "MySql.Data.MySqlClient";

            var lstTable = srf.LoadTables();
            var strDBUtilityNamespace = "Zeje.DBUtility";
            var strModelNamespace = "Zeje.Model";
            var strDALNamespace = "Zeje.DAL";
            var strBLLNamespace = "Zeje.BLL";
        }
    }
}
