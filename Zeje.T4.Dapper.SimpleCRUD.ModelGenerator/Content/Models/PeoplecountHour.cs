
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Zeje.Models
{
    /// <summary>
	/// 
	/// abdoor_blackcatmonitor.t_monitor_peoplecount_hour
    /// </summary> 
	[Table("t_monitor_peoplecount_hour")]
    public partial class PeoplecountHour
    {
		/// <summary>
		///
		/// </summary>  
		[Key][Column("peopleCount_hour_id")]
		public int PeoplecountHourId { get; set; }
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
		[Column("uploadtime")]
		public DateTime? Uploadtime { get; set; }
		/// <summary>
		///
		/// </summary>  
		[Column("number")]
		public int? Number { get; set; }
		/// <summary>
		///
		/// </summary>  
		[Column("iccard_num")]
		public int? IccardNum { get; set; }
		/// <summary>
		///
		/// </summary>  
		[Column("face_num")]
		public int? FaceNum { get; set; }
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
    }
}
