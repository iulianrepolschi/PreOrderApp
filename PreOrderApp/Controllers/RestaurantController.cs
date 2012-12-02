using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using PreOrderApp.Models;
using WebMatrix.WebData;

namespace PreOrderApp.Controllers
{
	public class RestaurantController : Controller
	{
		private readonly PreOrderAppEntities _database = new PreOrderAppEntities();


		public ActionResult Index()
		{
			ObjectQuery<MenuItem> objectQuery = _database.MenuItems;
			return View(objectQuery.Where(menuItem => menuItem.UserId == WebSecurity.CurrentUserId));

		}

		public ActionResult About()
		{
			//ViewBag.Message = "Your app description page.";

			return View();
		}

		//
		// GET: /Registru/Edit/5

		public ActionResult EditMenu(int id)
		{
			MenuItem menuItem = _database.MenuItems.Single(menu => menu.Id == id);

			return View(menuItem);
		}

		//
		// POST: /Registru/Edit/5

		[HttpPost]
		public ActionResult EditMenu(MenuItem menuItem)
		{
			try
			{
				if (ModelState.IsValid)
				{
					_database.MenuItems.Attach(menuItem);
					menuItem.UserId = WebSecurity.CurrentUserId;
					_database.ObjectStateManager.ChangeObjectState(menuItem, EntityState.Modified);
					_database.SaveChanges();
					return RedirectToAction("Index");
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

		//
		// GET: /Registru/Delete/5



		public ActionResult DeleteMenu(int id)
		{
			MenuItem menuItem = _database.MenuItems.Single(menu => menu.Id== id);
			return View(menuItem);
		}

		//
		// POST: /Registru/Delete/5

		[HttpPost, ActionName("DeleteMenu")]
		public ActionResult DeleteConfirmed(int id)
		{
			MenuItem menuItem = _database.MenuItems.Single(menu=> menu.Id== id);
			_database.ObjectStateManager.ChangeObjectState(menuItem, EntityState.Deleted);
			_database.SaveChanges();
			return RedirectToAction("Index");
			
		}

		public ActionResult CreateMenu()
		{
			return View();
		}

		//
		// POST: /Registru/Create

		[HttpPost]
		public ActionResult CreateMenu(MenuItem menuItem)
		{
			if (ModelState.IsValid)
			{
				menuItem.UserId = WebSecurity.CurrentUserId;
				menuItem.Active = true;
				_database.MenuItems.AddObject(menuItem);
				_database.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(menuItem);
		}

		[HttpPost, ActionName("Upload")]
		public ActionResult Upload(MenuItem model)
		{
			var image = WebImage.GetImageFromRequest();

			if (image != null)
			{
				if (image.Width > 500)
				{
					image.Resize(500, ((500 * image.Height) / image.Width));
				}

				var filename = Path.GetFileName(image.FileName);
				image.Save(Path.Combine("../Uploads/Images", filename));
				filename = Path.Combine("~/Uploads/Images", filename);

				model.ImageUrl = Url.Content(filename);
				//model.ImageAltText = image.FileName.Substring(0, image.FileName.Length - 4);

			}

			return View(model);
		}

	}
}
