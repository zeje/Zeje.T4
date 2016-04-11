using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zeje.T4_
{
    public class DBTables : List<DBTable>
    {
        public DBTables()
        {
        }
        public DBTable GetTable(string tableName)
        {
            return this.Single(x => string.Compare(x.Name, tableName, true) == 0);
        }
        public DBTable this[string tableName]
        {
            get
            {
                return GetTable(tableName);
            }
        }
    }
}
