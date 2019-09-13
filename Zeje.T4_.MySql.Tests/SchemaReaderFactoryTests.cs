using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zeje.T4_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zeje.T4_.Tests
{
    [TestClass()]
    public class SchemaReaderFactoryTests
    {
        [TestMethod()]
        public void SchemaReaderFactoryTest()
        {
            SchemaReaderFactory srf = new SchemaReaderFactory();

            srf.ConnectionString = "User ID=root;Password=admintest123456;Host=47.105.237.32;Port=5882;Database=QXDatabase;Pooling=true;Min Pool Size=0;Max Pool Size=100;CharSet=UTF8;";
            srf.ProviderName = "MySql.Data.MySqlClient";
            var data = srf.LoadTables();
        }
    }
}