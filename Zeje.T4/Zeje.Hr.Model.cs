
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
    /// <summary>添加人
    /// </summary> 
    public partial class HrPosition
    {
        /// <summary>
        /// </summary>        
        public int id { get; set; }
        /// <summary>
        /// </summary>        
        public string levelCode { get; set; }
        /// <summary>
        /// </summary>        
        public string code { get; set; }
        /// <summary>
        /// </summary>        
        public string name { get; set; }
        /// <summary>
        /// </summary>        
        public int? sortId { get; set; }
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
    /// <summary>排序号
    /// </summary> 
    public partial class HrPositionLevel
    {
        /// <summary>
        /// </summary>        
        public int id { get; set; }
        /// <summary>
        /// </summary>        
        public string code { get; set; }
        /// <summary>
        /// </summary>        
        public string name { get; set; }
        /// <summary>
        /// </summary>        
        public int? parentId { get; set; }
        /// <summary>排序号
        /// </summary>        
        public int? sortId { get; set; }
        /// <summary>添加人
        /// </summary>        
        public string addPerson { get; set; }
        /// <summary>添加时间
        /// </summary>        
        public DateTime? addTime { get; set; }
        /// <summary>更新人
        /// </summary>        
        public string updatePerson { get; set; }
        /// <summary>更新时间
        /// </summary>        
        public DateTime? updateTime { get; set; }
        /// <summary>删除人
        /// </summary>        
        public string deletePerson { get; set; }
        /// <summary>删除时间
        /// </summary>        
        public DateTime? deleteTime { get; set; }
    }
    /// <summary>员工表
    /// </summary> 
    public partial class HrStaff
    {
        /// <summary>ID
        /// </summary>        
        public string id { get; set; }
        /// <summary>人才编号
        /// </summary>        
        public string staffCode { get; set; }
        /// <summary>人才名称
        /// </summary>        
        public string name { get; set; }
        /// <summary>
        /// </summary>        
        public string positionLevelCode { get; set; }
        /// <summary>
        /// </summary>        
        public string positionCode { get; set; }
        /// <summary>
        /// </summary>        
        public bool isVisual { get; set; }
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