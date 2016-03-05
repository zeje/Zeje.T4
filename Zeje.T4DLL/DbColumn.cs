using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Zeje.T4DLL
{ 
    /// <summary>
    /// 表字段结构
    /// </summary>
    public sealed class DbColumn
    {
        /// <summary>
        /// 字段ID
        /// </summary>
        public int columnID { get; set; }
        /// <summary>
        /// 是否主键
        /// </summary>
        public bool isPrimaryKey { get; set; }
        /// <summary>
        /// 字段名称
        /// </summary>
        public string columnName { get; set; }
        /// <summary>
        /// 字段类型
        /// </summary>
        public string columnType { get; set; }
        /// <summary>
        /// 数据库类型对应的C#类型
        /// </summary>
        public string cSharpType
        {
            get
            {
                return SqlServerDbTypeMap.MapCsharpType(columnType);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public Type commonType
        {
            get
            {
                return SqlServerDbTypeMap.MapCommonType(columnType);
            }
        }
        /// <summary>
        /// 字节长度
        /// </summary>
        public int byteLength { get; set; }
        /// <summary>
        /// 字符长度
        /// </summary>
        public int charLength { get; set; }
        /// <summary>
        /// 小数位
        /// </summary>
        public int scale { get; set; }
        /// <summary>
        /// 是否自增列
        /// </summary>
        public bool isIdentity { get; set; }
        /// <summary>
        /// 是否允许空
        /// </summary>
        public bool isNullable { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string remark { get; set; }
    }
}
