
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Zeje.Models
{
    /// <summary>
	/// 
	/// abdoor_blackcatmonitor.t_monitor_systemparamters
	/// systemparamter
    /// </summary> 
	[Table("t_monitor_systemparamters")]
    public partial class Systemparamter
    {
		/// <summary>
		///
		/// </summary>  
		[Column("systempara_id")]
		public int? SystemparaId { get; set; }
		/// <summary>
		///
		/// </summary>  
		[Column("category_code")]
		public string CategoryCode { get; set; }
		/// <summary>
		///
		/// </summary>  
		[Column("category_name")]
		public string CategoryName { get; set; }
		/// <summary>
		///
		/// </summary>  
		[Column("category_value")]
		public int? CategoryValue { get; set; }
    }
}
