using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PreOrderApp.Models;
using WebMatrix.WebData;

namespace PreOrderApp.Controllers
{
	public class SelectPaymentController : Controller
	{
		private PreOrderAppEntities _database = new PreOrderAppEntities();

		
		public ActionResult Index()
		{
			ObjectQuery<MenuItem> objectQuery = _database.MenuItems.Include("UserProfile");
			return View(objectQuery.Where(menuItem => menuItem.Active == true));

		}

		public ActionResult SelectMenu(int id)
		{
			MenuItem menuItem = _database.MenuItems.Single(menu => menu.Id == id);
			UserProfile restaurant = _database.UserProfiles.Where(user => user.UserId == menuItem.UserId).FirstOrDefault();
			List<string> reservations=new List<string>();
			if(restaurant.ReservationStart.HasValue)
			{
				bool isIn = true;
				string time = string.Format("{0}:{1}", restaurant.ReservationStart.Value.ToString("hh"), restaurant.ReservationStart.Value.ToString("mm"));
				TimeSpan start = restaurant.ReservationStart.Value;
				while (isIn)
				{
					time = string.Format("{0}:{1}", start.ToString("hh"), start.ToString("mm"));
					reservations.Add(time);
					start=start.Add(TimeSpan.FromMinutes(restaurant.ReservationDuration.Value));
					isIn = (start <= restaurant.ReservationEnd.Value);
				}
			}
			else
			{
				reservations.AddRange(new string[]{"11:30", "12:00", "12:30", "13:00", "13:30", "14:00"});
			}
			//ViewBag.ReservationHour = new SelectList(reservations);
			List<SelectListItem> selectListItems = new List<SelectListItem>();
			foreach (string reservation in reservations)
			{
				selectListItems.Add(new SelectListItem(){Text=reservation, Value = reservation});
			}
			MenuItemModel menuItemModel = new MenuItemModel();
			menuItemModel.Id = menuItem.Id;
			menuItemModel.Price = menuItem.Price;
			menuItemModel.Name = menuItem.Name;
			menuItemModel.ReservationHour = "12:00";
			menuItemModel.RestaurantReservationHours = selectListItems;
			return View(menuItemModel);
		}

		//
		// POST: /Registru/Edit/5

		[HttpPost]
		public ActionResult SelectMenu(MenuItemModel menuItem)
		{
			try
			{
				if (ModelState.IsValid)
				{
					MenuItem item = _database.MenuItems.Single(menu => menu.Id == menuItem.Id);

					
					Payment payment = _database.Payments.CreateObject();
					payment.MenuItemId = item.Id;
					payment.UserId = WebSecurity.CurrentUserId;
					payment.Date = DateTime.Now;
					payment.RestaurantID = item.UserId;
					payment.ReservationHour = menuItem.ReservationHour;
					_database.Payments.Attach(payment);
					_database.ObjectStateManager.ChangeObjectState(payment, EntityState.Added);
					_database.SaveChanges();
					return RedirectToAction("Index","Payment");
				}
				return View(menuItem);
			}
			catch (OptimisticConcurrencyException optimisticConcurrencyException)
			{
				_database.MenuItems.Detach(menuItem);
				ModelState.AddModelError("", "Optimistic Concurrency Exception occurred. Save Again to override");

			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", "See exception and inner exception");
			}

			return View(menuItem);
		}


	}
}
