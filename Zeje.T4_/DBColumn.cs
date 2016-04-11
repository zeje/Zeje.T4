using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zeje.T4_
{
    public class DBColumn
    {
        public string Name;
        public string PropertyName;
        public string PropertyType;
        public bool IsPK;
        public bool IsNullable;
        public bool IsAutoIncrement;
        public bool Ignore;
        /// <summary>备注
        /// </summary>
        public string Comment;
    }
}
