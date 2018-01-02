
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Zeje.Models
{
    /// <summary>
	/// 
	/// abdoor_blackcatmonitor.t_monitor_peoplecount_day
	/// peoplecount_day
    /// </summary> 
	[Table("t_monitor_peoplecount_day")]
    public partial class PeoplecountDay
    {
		/// <summary>
		///
		/// </summary>  
		[Key][Column("peopleCount_day_id")]
		public int PeoplecountDayId { get; set; }
		/// <summary>
		///
		/// </summary>  
		[Column("cat_id")]
		public int? CatId { get; set; }
		/// <summary>
		///
		/// </summary>  
		[Column("direction_type")]
		public int? DirectionType { get; set; }
		/// <summary>
		///
		/// </summary>  
		[Column("curdate")]
		public string Curdate { get; set; }
		/// <summary>
		///
		/// </summary>  
		[Column("number")]
		public int? Number { get; set; }
		/// <summary>
		///
		/// </summary>  
		[Column("face_num")]
		public int? FaceNum { get; set; }
		/// <summary>
		///
		/// </summary>  
		[Column("iccard_num")]
		public int? IccardNum { get; set; }
		/// <summary>
		///
		/// </summary>  
		[Column("mobile_num")]
		public int? MobileNum { get; set; }
		/// <summary>
		///
		/// </summary>  
		[Column("idcard_num")]
		public int? IdcardNum { get; set; }
		/// <summary>
		///
		/// </summary>  
		[Column("talk_num")]
		public int? TalkNum { get; set; }
		/// <summary>
		///
		/// </summary>  
		[Column("total")]
		public int? Total { get; set; }
    }
}
