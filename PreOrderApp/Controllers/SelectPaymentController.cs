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

			return View(menuItem);
		}

		//
		// POST: /Registru/Edit/5

		[HttpPost]
		public ActionResult SelectMenu(MenuItem menuItem)
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
