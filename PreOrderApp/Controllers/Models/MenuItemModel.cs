using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace PreOrderApp.Models
{
	
	public class MenuItemModel:MenuItem
	{
		[Required]
		[Display(Name = "Reservation Hour")]
		public string ReservationHour { get; set; }

		public IEnumerable<SelectListItem> RestaurantReservationHours { get; set; }
	}
}
