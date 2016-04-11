using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace Zeje.T4_
{
    public abstract class SchemaReader
    {
        public abstract DBTables ReadSchema(DbConnection connection, DbProviderFactory factory);
    }

}
