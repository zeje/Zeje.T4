using System;
using System.Collections;
namespace Zeje.T4DLL
{
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
}
