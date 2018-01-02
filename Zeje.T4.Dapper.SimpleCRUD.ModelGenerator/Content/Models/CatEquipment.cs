
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Zeje.Models
{
    /// <summary>
	/// 
	/// abdoor_blackcatmonitor.t_monitor_cat_equipment
	/// cat_equipment
    /// </summary> 
	[Table("t_monitor_cat_equipment")]
    public partial class CatEquipment
    {
		/// <summary>
		///
		/// </summary>  
		[Key][Column("equipment_id")]
		public int EquipmentId { get; set; }
		/// <summary>
		///
		/// </summary>  
		[Column("cat_id")]
		public int? CatId { get; set; }
		/// <summary>
		///
		/// </summary>  
		[Column("equipment_code")]
		public string EquipmentCode { get; set; }
		/// <summary>
		///
		/// </summary>  
		[Column("equipment_name")]
		public string EquipmentName { get; set; }
		/// <summary>
		///
		/// </summary>  
		[Column("equipment_ip")]
		public string EquipmentIp { get; set; }
		/// <summary>
		///
		/// </summary>  
		[Column("equipment_port")]
		public string EquipmentPort { get; set; }
		/// <summary>
		///
		/// </summary>  
		[Column("equipment_username")]
		public string EquipmentUsername { get; set; }
		/// <summary>
		///
		/// </summary>  
		[Column("equipment_password")]
		public string EquipmentPassword { get; set; }
		/// <summary>
		///
		/// </summary>  
		[Column("equipment_category_code")]
		public string EquipmentCategoryCode { get; set; }
		/// <summary>
		///
		/// </summary>  
		[Column("equipment_category_value")]
		public int? EquipmentCategoryValue { get; set; }
    }
}
