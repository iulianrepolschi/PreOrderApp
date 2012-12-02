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
	public class UsersModel
	{
		private readonly IEnumerable<UserProfile> _users;

		public UsersModel(IEnumerable<UserProfile> users)
		{
			_users = users;
		}

		public int Count
		{
			get { return _users.Count(); }
		}

		public IEnumerable<UserProfile> Users
		{
			get { return _users; }
		}
	}
}
