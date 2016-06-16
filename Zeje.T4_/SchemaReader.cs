using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Data;

namespace Zeje.T4_
{
    public abstract class SchemaReader
    {
        public abstract DBTables ReadSchema(DbConnection connection, DbProviderFactory factory);

        public abstract DataTable ReadTable(DbConnection connection, DbProviderFactory factory, string tableName);
    }

}
