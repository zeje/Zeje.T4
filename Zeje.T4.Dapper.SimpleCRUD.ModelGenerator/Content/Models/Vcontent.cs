
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Zeje.Models
{
    /// <summary>
	/// 
	/// abdoor_blackcatmonitor.t_monitor_tvcontent
    /// </summary> 
	[Table("t_monitor_tvcontent")]
    public partial class Vcontent
    {
		/// <summary>
		///
		/// </summary>  
		[Key][Column("content_id")]
		public int ContentId { get; set; }
		/// <summary>
		///
		/// </summary>  
		[Column("cat_id")]
		public int? CatId { get; set; }
		/// <summary>
		///
		/// </summary>  
		[Column("title")]
		public string Title { get; set; }
		/// <summary>
		///
		/// </summary>  
		[Column("content")]
		public string Content { get; set; }
		/// <summary>
		///
		/// </summary>  
		[Column("status")]
		public int? Status { get; set; }
    }
}
