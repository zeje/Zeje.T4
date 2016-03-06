using System;
using System.Collections;
namespace Zeje.T4DLL
{
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
