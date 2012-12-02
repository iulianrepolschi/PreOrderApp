using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PreOrderApp.Models;
using WebMatrix.WebData;

namespace PreOrderApp.Controllers
{
	public class HomeController : Controller
	{
		private readonly PreOrderAppEntities _database = new PreOrderAppEntities();

		
		public ActionResult Index()
		{
		//	ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

			return View();
		}

		public ActionResult About()
		{
			//ViewBag.Message = "Your app description page.";

			return View();
		}

		public ActionResult Contact()
		{
			//ViewBag.Message = "Your contact page.";

			return View();
		}

		public ActionResult Restaurant()
		{
			ViewBag.Message = "Restaurant page.";

			return View();
		}

		public ActionResult RegisteredRestaurants()
		{
			ViewBag.Message = "Registered Restaurants.";

			return View(new UsersModel(_database.UserProfiles.ToList()).Users.Where(profile => Roles.IsUserInRole(profile.UserName, "Restaurant")));
		}

		public ActionResult RegisteredCustomers()
		{
			ViewBag.Message = "Registered Customers.";

			return View(new UsersModel(_database.UserProfiles.ToList()).Users.Where(profile => Roles.IsUserInRole(profile.UserName,"Customer")));
		}

		public ActionResult Customer()
		{
			ViewBag.Message = "Customer page.";

			return View();
		}


		//
		// GET: /Registru/Edit/5

		public ActionResult Edit(int id)
		{
			UserProfile userProfile = _database.UserProfiles.Single(userPorofile => userPorofile.UserId == id);

			return View(userProfile);
		}

		//
		// POST: /Registru/Edit/5

		[HttpPost]
		public ActionResult Edit(UserProfile userProfile)
		{
			try
			{
				if (ModelState.IsValid)
				{
					_database.UserProfiles.Attach(userProfile);
					_database.ObjectStateManager.ChangeObjectState(userProfile, EntityState.Modified);
					_database.SaveChanges();
					return RedirectToAction("RegisteredRestaurants");
				}
				return View(userProfile);
			}
			catch (OptimisticConcurrencyException optimisticConcurrencyException)
			{
				_database.UserProfiles.Detach(userProfile);
				ModelState.AddModelError("", "Optimistic Concurrency Exception occurred. Save Again to override");

			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", "See exception and inner exception");
			}

			return View(userProfile);
		}

		//
		// GET: /Registru/Delete/5



		//
		// GET: /Account/Register

		[AllowAnonymous]
		public ActionResult RegisterRestaurant()
		{
			ViewBag.RoleId = new SelectList(_database.webpages_Roles.Where(roles => roles.RoleName == "Restaurant"), "RoleId", "RoleName");
			return View();
		}

		//
		// POST: /Account/Register

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public ActionResult RegisterRestaurant(RegisterModel model)
		{
			if (ModelState.IsValid)
			{
				// Attempt to register the user
				try
				{
					WebSecurity.CreateUserAndAccount(model.UserName, model.Password, new
					{
						Email = model.Email,
						FirstName = model.FirstName,
						LastName = model.LastName
					});
					int userId = WebSecurity.GetUserId(model.UserName);

					UserProfile userProfile = _database.UserProfiles.Single(user => user.UserId == userId);
					//userProfile.FirstName = model.FirstName;
					//userProfile.LastName = model.LastName;
					//userProfile.Email = model.Email;

					userProfile.webpages_Roles.Add(_database.webpages_Roles.Single(role => role.RoleId == model.RoleId));
					_database.ObjectStateManager.ChangeObjectState(userProfile, EntityState.Modified);
					_database.SaveChanges();

					return RedirectToAction("RegisteredRestaurants", "Home");
				}
				catch (MembershipCreateUserException e)
				{
					ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
				}
			}

			// If we got this far, something failed, redisplay form
			return View(model);
		}

		private static string ErrorCodeToString(MembershipCreateStatus createStatus)
		{
			// See http://go.microsoft.com/fwlink/?LinkID=177550 for
			// a full list of status codes.
			switch (createStatus)
			{
				case MembershipCreateStatus.DuplicateUserName:
					return "User name already exists. Please enter a different user name.";

				case MembershipCreateStatus.DuplicateEmail:
					return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

				case MembershipCreateStatus.InvalidPassword:
					return "The password provided is invalid. Please enter a valid password value.";

				case MembershipCreateStatus.InvalidEmail:
					return "The e-mail address provided is invalid. Please check the value and try again.";

				case MembershipCreateStatus.InvalidAnswer:
					return "The password retrieval answer provided is invalid. Please check the value and try again.";

				case MembershipCreateStatus.InvalidQuestion:
					return "The password retrieval question provided is invalid. Please check the value and try again.";

				case MembershipCreateStatus.InvalidUserName:
					return "The user name provided is invalid. Please check the value and try again.";

				case MembershipCreateStatus.ProviderError:
					return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

				case MembershipCreateStatus.UserRejected:
					return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

				default:
					return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
			}
		}

		//
		// GET: /Registru/Delete/5

		public ActionResult Delete(int id)
		{
			UserProfile userProfile = _database.UserProfiles.Single(user => user.UserId== id);
			webpages_Roles role = userProfile.webpages_Roles.FirstOrDefault();
			ViewBag.RoleName = role.RoleName;
			return View(userProfile);
		}

		//
		// POST: /Registru/Delete/5

		[HttpPost, ActionName("Delete")]
		public ActionResult DeleteConfirmed(int id)
		{
			UserProfile userProfile = _database.UserProfiles.Single(user => user.UserId== id);
			userProfile.Active = ! userProfile.Active;
			_database.ObjectStateManager.ChangeObjectState(userProfile, EntityState.Modified);
			_database.SaveChanges();
			webpages_Roles role = userProfile.webpages_Roles.FirstOrDefault();
			if (role.RoleName == "Customer")
			{
				return RedirectToAction("RegisteredCustomers");
			}
			else
			{
				return RedirectToAction("RegisteredRestaurants");	
			}
			
		}

		
	}
}
