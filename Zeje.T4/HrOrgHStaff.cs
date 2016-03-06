using System;
using System.Collections;
namespace Zeje.T4DLL
{
    /// <summary>角色具有人员关系表
    /// </summary> 
    public partial class HrOrgHStaff
    {
        /// <summary>
        /// </summary>        
        public int id { get; set; }
        /// <summary>
        /// </summary>        
        public string hrOrgCode { get; set; }
        /// <summary>
        /// </summary>        
        public string hrStaffCode { get; set; }
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
