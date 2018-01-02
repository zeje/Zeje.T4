
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Zeje.Models
{
    /// <summary>
	/// 
	/// abdoor_blackcatmonitor.t_monitor_blackcat_info
	/// blackcat_info
    /// </summary> 
	[Table("t_monitor_blackcat_info")]
    public partial class BlackcatInfo
    {
		/// <summary>
		///
		/// </summary>  
		[Key][Column("cat_id")]
		public int CatId { get; set; }
		/// <summary>
		///
		/// </summary>  
		[Column("cat_code")]
		public string CatCode { get; set; }
		/// <summary>
		///
		/// </summary>  
		[Column("cat_name")]
		public string CatName { get; set; }
		/// <summary>
		///
		/// </summary>  
		[Column("cat_position")]
		public string CatPosition { get; set; }
		/// <summary>
		///
		/// </summary>  
		[Column("cat_mode_cur")]
		public int? CatModeCur { get; set; }
		/// <summary>
		///
		/// </summary>  
		[Column("cat_equipment_state")]
		public string CatEquipmentState { get; set; }
		/// <summary>
		///
		/// </summary>  
		[Column("cat_type")]
		public int? CatType { get; set; }
		/// <summary>
		///
		/// </summary>  
		[Column("cat_direction_cur")]
		public int? CatDirectionCur { get; set; }
    }
}
