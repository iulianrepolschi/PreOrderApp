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
	
	public class PaymentModel
	{
		private readonly IEnumerable<Payment> _payments;

		public PaymentModel(IEnumerable<Payment> payments)
		{
			_payments = payments;
		}

		public int Count
		{
			get { return _payments.Count(); }
		}

		[Required]
		public IEnumerable<Payment> Payments
		{
			get { return _payments; }
		}
	}
}
