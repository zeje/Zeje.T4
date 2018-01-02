
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Zeje.Models
{
    /// <summary>
	/// 
	/// abdoor_blackcatmonitor.t_monitor_abnormalsignal_log
	/// abnormalsignal_log
    /// </summary> 
	[Table("t_monitor_abnormalsignal_log")]
    public partial class AbnormalsignalLog
    {
		/// <summary>
		///
		/// </summary>  
		[Key][Column("singnal_id")]
		public int SingnalId { get; set; }
		/// <summary>
		///
		/// </summary>  
		[Column("cat_id")]
		public int? CatId { get; set; }
		/// <summary>
		///
		/// </summary>  
		[Column("singnal_b_type")]
		public int? SingnalBType { get; set; }
		/// <summary>
		///
		/// </summary>  
		[Column("singnal_s_type")]
		public int? SingnalSType { get; set; }
		/// <summary>
		///
		/// </summary>  
		[Column("create_time")]
		public DateTime? CreateTime { get; set; }
    }
}
