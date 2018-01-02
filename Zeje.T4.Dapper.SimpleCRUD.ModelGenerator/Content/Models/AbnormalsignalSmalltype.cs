
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Zeje.Models
{
    /// <summary>
	/// 
	/// abdoor_blackcatmonitor.t_abnormalsignal_smalltype
	/// abnormalsignal_smalltype
    /// </summary> 
	[Table("t_abnormalsignal_smalltype")]
    public partial class AbnormalsignalSmalltype
    {
		/// <summary>
		///
		/// </summary>  
		[Column("abnormalsignal_samll_id")]
		public int? AbnormalsignalSamllId { get; set; }
		/// <summary>
		///
		/// </summary>  
		[Column("systempara_id")]
		public int? SystemparaId { get; set; }
		/// <summary>
		///
		/// </summary>  
		[Column("signal_small_name")]
		public string SignalSmallName { get; set; }
		/// <summary>
		///
		/// </summary>  
		[Column("signal_small_value")]
		public int? SignalSmallValue { get; set; }
    }
}
