using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zeje.T4DLL
{
    /// <summary>表结构
    /// </summary>
    public sealed class DbTable
    {
        /// <summary>
        /// 表名称
        /// </summary>
        public string tableName { get; set; }
        /// <summary>
        /// 表的架构
        /// </summary>
        public string schemaName { get; set; }
        /// <summary>
        /// 表的记录数
        /// </summary>
        public int rows { get; set; }
        /// <summary>
        /// 表的注释
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 是否含有主键
        /// </summary>
        public bool hasPrimaryKey { get; set; }
    }
}
