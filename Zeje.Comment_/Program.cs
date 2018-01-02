using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zeje.T4_;
using System.IO;
using System.Xml.Linq;

namespace Zeje.Comment_
{
    class Program
    {
        static void Main(string[] args)
        {
            string rootPath = AppDomain.CurrentDomain.BaseDirectory;

            //Zeje.Esd.WeChat.WechatArticle.Id
            string dllNamespace = "Zeje.Esd.WeChat";
            string xmlName = dllNamespace + ".xml";
            string xml = Path.Combine(rootPath, xmlName);

            if (!File.Exists(xml))
            {
                Console.WriteLine("文件不存在！");
                Console.ReadLine();
                return;
            }

            XDocument xDoc = XDocument.Load(xml);

            var lstSummary = from item in xDoc.Element("doc").Element("members").Elements()
                             select new
                             {
                                 name = item.Attribute("name") == null ? "" : item.Attribute("name").Value,
                                 summary = item.Element("summary").Value.Trim('\r').Trim('\n').Trim()
                             };

            SchemaReaderFactory srf = new SchemaReaderFactory();
            srf.ConnectionString = "server=localhost;database=Dev.ZaEsd;uid=sa;pwd=P@ssw0rd";
            srf.ProviderName = "System.Data.SqlClient";
            var lstTable = srf.LoadTables();

            if (lstTable.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < lstTable.Count; i++)
                {
                    DBTable tbl = lstTable[i];
                    if (tbl.Name.StartsWith("Wechat", StringComparison.OrdinalIgnoreCase))
                    {
                        //查找xml获取并更新注释
                        var tbSummary = lstSummary.Where(it => it.name == ("T:" + dllNamespace + "." + tbl.Name)).FirstOrDefault();
                        if (tbSummary != null)
                        {
                            string funcName = "sp_addextendedproperty";
                            if (!string.IsNullOrEmpty(tbl.Comment))
                                funcName = "sp_updateextendedproperty";
                            string query = @"EXEC " + funcName + @" N'MS_Description', @desc, N'Schema', 'dbo', N'Table', @table";
                            sb.AppendLine(query
                                .Replace("@desc", "\'" + tbSummary.summary + "\'")
                                .Replace("@table", "\'" + tbl.Name + "\'"));
                        }

                        foreach (DBColumn col in tbl.Columns)
                        {
                            //查找xml获取并更新注释
                            var colSummary = lstSummary.Where(it => it.name == ("P:" + dllNamespace + "." + tbl.Name + "." + col.Name)).FirstOrDefault();
                            if (colSummary != null)
                            {
                                string funcName2 = "sp_addextendedproperty";
                                if (!string.IsNullOrEmpty(col.Comment))
                                    funcName2 = "sp_updateextendedproperty";

                                string query = @"EXEC " + funcName2 + @" N'MS_Description', @desc, N'Schema', 'dbo', N'Table', @table, N'Column', @column";
                                sb.AppendLine(query
                                    .Replace("@desc", "\'" + colSummary.summary + "\'")
                                    .Replace("@table", "\'" + tbl.Name + "\'")
                                    .Replace("@column", "\'" + col.Name + "\'"));
                            }
                        }
                    }
                }
                Console.WriteLine(sb);
                Console.ReadLine();
            }
        }
    }
}
