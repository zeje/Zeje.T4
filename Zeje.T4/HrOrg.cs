using System;
using System.Collections;
namespace Zeje.T4DLL
{
    /// <summary>组织架构表
    /// </summary> 
    public partial class HrOrg
    {
        /// <summary>
        /// </summary>        
        public int Id { get; set; }
        /// <summary>组织编号
        /// </summary>        
        public string orgCode { get; set; }
        /// <summary>组织名称
        /// </summary>        
        public string orgName { get; set; }
        /// <summary>
        /// </summary>        
        public string parentOrgCode { get; set; }
        /// <summary>
        /// </summary>        
        public string remark { get; set; }
        /// <summary>添加人
        /// </summary>        
        public string addPerson { get; set; }
        /// <summary>添加时间
        /// </summary>        
        public DateTime addTime { get; set; }
        /// <summary>更新人
        /// </summary>        
        public string updatePerson { get; set; }
        /// <summary>更新时间
        /// </summary>        
        public DateTime updateTime { get; set; }
        /// <summary>删除人
        /// </summary>        
        public string deletePerson { get; set; }
        /// <summary>删除时间
        /// </summary>        
        public DateTime? deleteTime { get; set; }
    }
}
