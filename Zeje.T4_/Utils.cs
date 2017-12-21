
using System.Linq;
using System.Text.RegularExpressions;
using System;
using System.Text;
using System.Configuration;
/*
This code is part of the PetaPoco project (http://www.toptensoftware.com/petapoco).
It is based on the SubSonic T4 templates but has been considerably re-organized and reduced

-----------------------------------------------------------------------------------------
This template can read minimal schema information from the following databases:
* SQL Server
* SQL Server CE
* MySQL
* PostGreSQL
* Oracle
For connection and provider settings the template will look for the web.config or app.config file of the 
containing Visual Studio project.  It will not however read DbProvider settings from this file.
In order to work, the appropriate driver must be registered in the system machine.config file.  If you're
using Visual Studio 2010 the file you want is here:
C:\Windows\Microsoft.NET\Framework\v4.0.30319\Config\machine.config
After making changes to machine.config you will also need to restart Visual Studio.
Here's a typical set of entries that might help if you're stuck:
<system.data>
<DbProviderFactories>
<add name="Odbc Data Provider" invariant="System.Data.Odbc" description=".Net Framework Data Provider for Odbc" type="System.Data.Odbc.OdbcFactory, System.Data, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"/>
<add name="OleDb Data Provider" invariant="System.Data.OleDb" description=".Net Framework Data Provider for OleDb" type="System.Data.OleDb.OleDbFactory, System.Data, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"/>
<add name="OracleClient Data Provider" invariant="System.Data.OracleClient" description=".Net Framework Data Provider for Oracle" type="System.Data.OracleClient.OracleClientFactory, System.Data.OracleClient, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"/>
<add name="SqlClient Data Provider" invariant="System.Data.SqlClient" description=".Net Framework Data Provider for SqlServer" type="System.Data.SqlClient.SqlClientFactory, System.Data, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"/>
<add name="MySQL Data Provider" invariant="MySql.Data.MySqlClient" description=".Net Framework Data Provider for MySQL" type="MySql.Data.MySqlClient.MySqlClientFactory, MySql.Data, Version=6.3.4.0, Culture=neutral, PublicKeyToken=c5687fc88969c44d"/>
<add name="Microsoft SQL Server Compact Data Provider" invariant="System.Data.SqlServerCe.3.5" description=".NET Framework Data Provider for Microsoft SQL Server Compact" type="System.Data.SqlServerCe.SqlCeProviderFactory, System.Data.SqlServerCe, Version=3.5.1.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"/><add name="Microsoft SQL Server Compact Data Provider 4.0" invariant="System.Data.SqlServerCe.4.0" description=".NET Framework Data Provider for Microsoft SQL Server Compact" type="System.Data.SqlServerCe.SqlCeProviderFactory, System.Data.SqlServerCe, Version=4.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"/>
<add name="Npgsql Data Provider" invariant="Npgsql" support="FF" description=".Net Framework Data Provider for Postgresql Server" type="Npgsql.NpgsqlFactory, Npgsql, Version=2.0.11.91, Culture=neutral, PublicKeyToken=5d8b90d52f46fda7" />
</DbProviderFactories>
</system.data>
Also, the providers and their dependencies need to be installed to GAC.  
Eg; this is how I installed the drivers for PostgreSQL
gacutil /i Npgsql.dll
gacutil /i Mono.Security.dll
-----------------------------------------------------------------------------------------

SubSonic - http://subsonicproject.com

The contents of this file are subject to the New BSD
License (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of
the License at http://www.opensource.org/licenses/bsd-license.php

Software distributed under the License is distributed on an 
"AS IS" basis, WITHOUT WARRANTY OF ANY KIND, either express or
implied. See the License for the specific language governing
rights and limitations under the License.
*/
namespace Zeje.T4_
{
    public class Utils
    {
        #region 基本
        public static Func<string, string> CleanUp = (str) =>
        {
            str = Constants.rxCleanUp.Replace(str, "_");
            if (char.IsDigit(str[0]) || Constants.cs_keywords.Contains(str))
                str = "@" + str;
            return str;
        };
        public static string CheckNullable(DBColumn col)
        {
            string result = "";
            if (col.IsNullable &&
                col.PropertyType != "byte[]" &&
                col.PropertyType != "string" &&
                col.PropertyType != "Microsoft.SqlServer.Types.SqlGeography" &&
                col.PropertyType != "Microsoft.SqlServer.Types.SqlGeometry"
                )
                result = "?";
            return result;
        }

        public static string zap_password(string connectionString)
        {
            var rx = new Regex("password=.*;", RegexOptions.Singleline | RegexOptions.Multiline | RegexOptions.IgnoreCase);
            return rx.Replace(connectionString, "password=**zapped**;");
        }
        #endregion

        #region 命名规范 NamingConvention

        /// <summary>
        /// 驼峰法类型
        /// </summary>
        public enum UnderScoreType
        {
            /// <summary>
            /// 全部大写
            /// </summary>
            全部大写,
            /// <summary>
            /// 全部小写
            /// </summary>
            全部小写
        }

        /// <summary>
        /// 将驼峰式命名的字符串转换为下划线大写方式。如果转换前的驼峰式命名的字符串为空，则返回空字符串。
        /// 例如：HelloWorld->hello_World
        /// </summary>
        /// <param name="name">转换前的驼峰式命名的字符串</param>
        /// <param name="underScoreType">转换前的驼峰式命名的字符串</param>
        /// <returns>转换后下划线大写方式命名的字符串</returns>
        public static String UnderScoreName(String name, UnderScoreType underScoreType = UnderScoreType.全部小写)
        {
            StringBuilder result = new StringBuilder();
            if (name != null && name.Length > 0)
            {
                for (int i = 0; i < name.Length; i++)
                {
                    String s = name.Substring(i, 1);
                    // 在大写字母前添加下划线
                    int tmp;
                    if (s.Equals(s.ToUpper()) && !int.TryParse(s, out tmp) && s != "_" && i != 0)
                    {
                        result.Append("_");
                    }
                    switch (underScoreType)
                    {
                        case UnderScoreType.全部大写:
                            // 其他字符直接转成大写
                            result.Append(s.ToUpper());
                            break;
                        case UnderScoreType.全部小写:
                            // 其他字符直接转成小写
                            result.Append(s.ToLower());
                            break;
                        default:
                            result.Append(s);
                            break;
                    }
                }
            }
            result = result.Replace("__", "_");
            return result.ToString();
        }

        /// <summary>
        /// 驼峰法类型
        /// </summary>
        public enum CamelType
        {
            /// <summary>
            /// 变量一般用小驼峰法标识。驼峰法的意思是：除第一个单词之外，其他单词首字母大写
            /// </summary>
            小驼峰法,
            /// <summary>
            /// 相比小驼峰法，大驼峰法（即帕斯卡命名法）把第一个单词的首字母也大写了。常用于类名，命名空间等。
            /// </summary>
            大驼峰法
        }
        /// <summary>
        /// 将下划线大写方式命名的字符串转换为驼峰式。如果转换前命名的字符串为空，则返回空字符串。
        /// 例如：
        /// HELLO_WORLD->HelloWorld
        /// _HELLO_WORLD->HelloWorld
        /// hello_world->HelloWorld
        /// _hello_world->HelloWorld
        /// </summary>
        /// <param name="name">转换前下划线命名的字符串</param>
        /// <param name="camelType">驼峰类型</param>
        /// <returns>转换后的驼峰式命名的字符串</returns>
        public static String CamelName(String name, CamelType camelType = CamelType.大驼峰法)
        {
            StringBuilder result = new StringBuilder();
            // 快速检查
            if (string.IsNullOrWhiteSpace(name))
            {
                // 没必要转换
                return "";
            }
            else if (name == "_")
            {
                // 直接返回
                return name;
            }
            else if (!name.Contains("_"))
            {
                switch (camelType)
                {
                    case CamelType.小驼峰法:
                        // 不含下划线，仅将首字母小写，其他不变
                        return name.Substring(0, 1).ToLower() + (name.Length > 1 ? name.Substring(1) : "");
                    case CamelType.大驼峰法:
                        // 不含下划线，仅将首字母大写，其他不变
                        return name.Substring(0, 1).ToUpper() + (name.Length > 1 ? name.Substring(1) : "");
                    default:
                        return name;
                }
            }

            // 用下划线将原始字符串分割
            String[] camels = name.Split('_');
            foreach (String camel in camels)
            {
                // 跳过原始字符串中开头、结尾的下换线或双重下划线
                if (string.IsNullOrWhiteSpace(camel))
                {
                    continue;
                }
                // 处理真正的驼峰片段
                if (result.Length == 0 && camelType == CamelType.小驼峰法)
                {
                    // 第一个驼峰片段，全部字母都小写
                    result.Append(camel.ToLower());
                }
                else
                {
                    // 其他的驼峰片段，首字母大写
                    result.Append(camel.Substring(0, 1).ToUpper());
                    if (camel.Length > 1) result.Append(camel.Substring(1).ToLower());
                }
            }
            return result.ToString();
        }
        #endregion

        #region CodeFirst

        public static string RenderHasMaxLength(DBColumn col)
        {
            if (col.PropertyType == "string" && col.MaxLength != "-1")
            {
                return string.Format(".HasMaxLength({0})", col.MaxLength);
            }
            else
            {
                return string.Empty;
            }
        }

        public static string RenderIsRequired(DBColumn col)
        {
            if (!col.IsNullable)
            {
                return string.Format(".IsRequired()", col.MaxLength);
            }
            else
            {
                return string.Empty;
            }
        }



        public static string RenderHasMaxLengthAndIsRequired(DBColumn col)
        {
            string strHasMaxLength = RenderHasMaxLength(col);
            string strIsRequired = RenderIsRequired(col);
            return strHasMaxLength + strIsRequired + ";";
        }

        public static string FormatComment(string comment, bool clearName = true)
        {
            string temp = (comment ?? "").Replace("\r\n", "|-|").Replace("\r\n", "|-|").Replace("\r\n", "|-|")
                .Replace("\r", "|-|").Replace("\r", "|-|").Replace("\r", "|-|");

            if (clearName)
            {
                return temp.EndsWith("表") ? temp.Remove(temp.Length - 1) : temp;
            }
            else
            {
                return temp;
            }
        }

        public static string RenderStringLength(DBColumn col)
        {
            if (col.PropertyType == "string")
            {
                if (col.MaxLength != "-1")
                {
                    return string.Format("[StringLength({1}, ErrorMessage = \"{0}不能超过{1}字。\")]" + Environment.NewLine, col.Comment, col.MaxLength);
                }
            }
            return "";
        }

        public static string RenderRequired(DBColumn col)
        {
            if (col.PropertyType != "string" && !col.IsNullable)
            {
                return string.Format(" [Required(ErrorMessage = \"请输入{0}。\")]" + Environment.NewLine, col.Comment);
            }
            return "";
        }

        #endregion
    }
}