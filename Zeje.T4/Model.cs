
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
        public string OrgCode { get; set; }
        /// <summary>组织名称
        /// </summary>        
        public string OrgName { get; set; }
        /// <summary>
        /// </summary>        
        public string ParentOrgCode { get; set; }
        /// <summary>组织树（表示层级）
        /// </summary>        
        public string OrgTree { get; set; }
        /// <summary>
        /// </summary>        
        public string Remark { get; set; }
        /// <summary>添加人
        /// </summary>        
        public string AddPerson { get; set; }
        /// <summary>添加时间
        /// </summary>        
        public DateTime AddTime { get; set; }
        /// <summary>更新人
        /// </summary>        
        public string UpdatePerson { get; set; }
        /// <summary>更新时间
        /// </summary>        
        public DateTime UpdateTime { get; set; }
        /// <summary>删除人
        /// </summary>        
        public string DeletePerson { get; set; }
        /// <summary>删除时间
        /// </summary>        
        public DateTime? DeleteTime { get; set; }
    }
    /// <summary>角色具有人员关系表
    /// </summary> 
    public partial class HrOrgHStaff
    {
        /// <summary>
        /// </summary>        
        public int Id { get; set; }
        /// <summary>
        /// </summary>        
        public string HrOrgCode { get; set; }
        /// <summary>
        /// </summary>        
        public string HrStaffCode { get; set; }
        /// <summary>添加人
        /// </summary>        
        public string AddPerson { get; set; }
        /// <summary>添加时间
        /// </summary>        
        public DateTime AddTime { get; set; }
        /// <summary>更新人
        /// </summary>        
        public string UpdatePerson { get; set; }
        /// <summary>更新时间
        /// </summary>        
        public DateTime UpdateTime { get; set; }
        /// <summary>删除人
        /// </summary>        
        public string DeletePerson { get; set; }
        /// <summary>删除时间
        /// </summary>        
        public DateTime? DeleteTime { get; set; }
    }
    /// <summary>添加人
    /// </summary> 
    public partial class HrPosition
    {
        /// <summary>
        /// </summary>        
        public int Id { get; set; }
        /// <summary>
        /// </summary>        
        public string LevelCode { get; set; }
        /// <summary>
        /// </summary>        
        public string Code { get; set; }
        /// <summary>
        /// </summary>        
        public string Name { get; set; }
        /// <summary>
        /// </summary>        
        public int? SortId { get; set; }
        /// <summary>添加人
        /// </summary>        
        public string AddPerson { get; set; }
        /// <summary>添加时间
        /// </summary>        
        public DateTime AddTime { get; set; }
        /// <summary>更新人
        /// </summary>        
        public string UpdatePerson { get; set; }
        /// <summary>更新时间
        /// </summary>        
        public DateTime UpdateTime { get; set; }
        /// <summary>删除人
        /// </summary>        
        public string DeletePerson { get; set; }
        /// <summary>删除时间
        /// </summary>        
        public DateTime? DeleteTime { get; set; }
    }
    /// <summary>排序号
    /// </summary> 
    public partial class HrPositionLevel
    {
        /// <summary>
        /// </summary>        
        public int Id { get; set; }
        /// <summary>
        /// </summary>        
        public string Code { get; set; }
        /// <summary>
        /// </summary>        
        public string Name { get; set; }
        /// <summary>
        /// </summary>        
        public int? ParentId { get; set; }
        /// <summary>排序号
        /// </summary>        
        public int? SortId { get; set; }
        /// <summary>添加人
        /// </summary>        
        public string AddPerson { get; set; }
        /// <summary>添加时间
        /// </summary>        
        public DateTime? AddTime { get; set; }
        /// <summary>更新人
        /// </summary>        
        public string UpdatePerson { get; set; }
        /// <summary>更新时间
        /// </summary>        
        public DateTime? UpdateTime { get; set; }
        /// <summary>删除人
        /// </summary>        
        public string DeletePerson { get; set; }
        /// <summary>删除时间
        /// </summary>        
        public DateTime? DeleteTime { get; set; }
    }
    /// <summary>员工表
    /// </summary> 
    public partial class HrStaff
    {
        /// <summary>ID
        /// </summary>        
        public string Id { get; set; }
        /// <summary>人才编号
        /// </summary>        
        public string StaffCode { get; set; }
        /// <summary>人才名称
        /// </summary>        
        public string Name { get; set; }
        /// <summary>
        /// </summary>        
        public string PositionLevelCode { get; set; }
        /// <summary>
        /// </summary>        
        public string PositionCode { get; set; }
        /// <summary>
        /// </summary>        
        public bool IsVisual { get; set; }
        /// <summary>添加人
        /// </summary>        
        public string AddPerson { get; set; }
        /// <summary>添加时间
        /// </summary>        
        public DateTime AddTime { get; set; }
        /// <summary>更新人
        /// </summary>        
        public string UpdatePerson { get; set; }
        /// <summary>更新时间
        /// </summary>        
        public DateTime UpdateTime { get; set; }
        /// <summary>删除人
        /// </summary>        
        public string DeletePerson { get; set; }
        /// <summary>删除时间
        /// </summary>        
        public DateTime? DeleteTime { get; set; }
    }
}