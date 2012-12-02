using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PreOrderApp.Models;
using WebMatrix.WebData;

namespace PreOrderApp.Controllers
{
	public class PaymentController : Controller
	{
		private PreOrderAppEntities _db = new PreOrderAppEntities();

		
		public ActionResult Index()
		{
			IOrderedQueryable<Payment> orderByDescending = _db.Payments.OrderByDescending(payment => payment.Date);
			IQueryable<Payment> queryable = orderByDescending.Include("MenuItem");

			if (Roles.IsUserInRole("Customer"))
			{
				return View(queryable.Where(payment => payment.UserId == WebSecurity.CurrentUserId));
			}
			else
			{
				return View(queryable.Where(payment => payment.MenuItem.UserId == WebSecurity.CurrentUserId));	
			}

		}

	}
}
