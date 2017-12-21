
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Zeje.Models
{
    /// <summary>
	/// 
	/// abdoor_blackcatmonitor.t_monitor_operate_log
    /// </summary> 
	[Table("t_monitor_operate_log")]
    public partial class PerateLog
    {
		/// <summary>
		///
		/// </summary>  
		[Key][Column("operate_id")]
		public int OperateId { get; set; }
		/// <summary>
		///
		/// </summary>  
		[Column("uid")]
		public int? Uid { get; set; }
		/// <summary>
		///
		/// </summary>  
		[Column("cat_id")]
		public int? CatId { get; set; }
		/// <summary>
		///
		/// </summary>  
		[Column("operate_type")]
		public int? OperateType { get; set; }
		/// <summary>
		///
		/// </summary>  
		[Column("create_time")]
		public DateTime? CreateTime { get; set; }
    }
}
