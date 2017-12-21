
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Zeje.Models
{
    /// <summary>
	/// 
	/// abdoor_blackcatmonitor.t_monitor_user
    /// </summary> 
	[Table("t_monitor_user")]
    public partial class User
    {
		/// <summary>
		///
		/// </summary>  
		[Key][Column("uid")]
		public int Uid { get; set; }
		/// <summary>
		///
		/// </summary>  
		[Column("account")]
		public string Account { get; set; }
		/// <summary>
		///
		/// </summary>  
		[Column("username")]
		public string Username { get; set; }
		/// <summary>
		///
		/// </summary>  
		[Column("password")]
		public string Password { get; set; }
		/// <summary>
		///
		/// </summary>  
		[Column("email")]
		public string Email { get; set; }
    }
}
