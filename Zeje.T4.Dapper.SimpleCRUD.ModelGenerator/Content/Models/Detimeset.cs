
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Zeje.Models
{
    /// <summary>
	/// 
	/// abdoor_blackcatmonitor.t_monitor_modetimeset
	/// detimeset
    /// </summary> 
	[Table("t_monitor_modetimeset")]
    public partial class Detimeset
    {
		/// <summary>
		///
		/// </summary>  
		[Key][Column("modeset_id")]
		public int ModesetId { get; set; }
		/// <summary>
		///
		/// </summary>  
		[Column("cat_id")]
		public int? CatId { get; set; }
		/// <summary>
		///
		/// </summary>  
		[Column("mode_type")]
		public int? ModeType { get; set; }
		/// <summary>
		///
		/// </summary>  
		[Column("high_morning_from")]
		public string HighMorningFrom { get; set; }
		/// <summary>
		///
		/// </summary>  
		[Column("high_morning_to")]
		public string HighMorningTo { get; set; }
		/// <summary>
		///
		/// </summary>  
		[Column("high_after_from")]
		public string HighAfterFrom { get; set; }
		/// <summary>
		///
		/// </summary>  
		[Column("high_after_to")]
		public string HighAfterTo { get; set; }
		/// <summary>
		///
		/// </summary>  
		[Column("h_week1")]
		public int? HWeek1 { get; set; }
		/// <summary>
		///
		/// </summary>  
		[Column("h_week2")]
		public int? HWeek2 { get; set; }
		/// <summary>
		///
		/// </summary>  
		[Column("h_week3")]
		public int? HWeek3 { get; set; }
		/// <summary>
		///
		/// </summary>  
		[Column("h_week4")]
		public int? HWeek4 { get; set; }
		/// <summary>
		///
		/// </summary>  
		[Column("h_week5")]
		public int? HWeek5 { get; set; }
		/// <summary>
		///
		/// </summary>  
		[Column("h_week6")]
		public int? HWeek6 { get; set; }
		/// <summary>
		///
		/// </summary>  
		[Column("h_week7")]
		public int? HWeek7 { get; set; }
		/// <summary>
		///
		/// </summary>  
		[Column("night_after_from")]
		public string NightAfterFrom { get; set; }
		/// <summary>
		///
		/// </summary>  
		[Column("night_after_to")]
		public string NightAfterTo { get; set; }
		/// <summary>
		///
		/// </summary>  
		[Column("n_week1")]
		public int? NWeek1 { get; set; }
		/// <summary>
		///
		/// </summary>  
		[Column("n_week2")]
		public int? NWeek2 { get; set; }
		/// <summary>
		///
		/// </summary>  
		[Column("n_week3")]
		public int? NWeek3 { get; set; }
		/// <summary>
		///
		/// </summary>  
		[Column("n_week4")]
		public int? NWeek4 { get; set; }
		/// <summary>
		///
		/// </summary>  
		[Column("n_week5")]
		public int? NWeek5 { get; set; }
		/// <summary>
		///
		/// </summary>  
		[Column("n_week6")]
		public int? NWeek6 { get; set; }
		/// <summary>
		///
		/// </summary>  
		[Column("n_week7")]
		public int? NWeek7 { get; set; }
		/// <summary>
		///
		/// </summary>  
		[Column("normal_date")]
		public string NormalDate { get; set; }
    }
}
