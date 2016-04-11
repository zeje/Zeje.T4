using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zeje.T4_
{
    public class DBTable
    {
        public List<DBColumn> Columns;
        public string Name;
        public string Schema;
        public bool IsView;
        public string CleanName;
        public string ClassName;
        public string SequenceName;
        public bool Ignore;
        public DBColumn PK
        {
            get
            {
                return this.Columns.SingleOrDefault(x => x.IsPK);
            }
        }
        public DBColumn GetColumn(string columnName)
        {
            return Columns.Single(x => string.Compare(x.Name, columnName, true) == 0);
        }
        public DBColumn this[string columnName]
        {
            get
            {
                return GetColumn(columnName);
            }
        }
        /// <summary>备注
        /// </summary>
        public string Comment;
    }
}
