using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestourantServiceApp.BLogicLayer.Exceptions
{
	public class MenuItemNotFound : Exception
	{
		public MenuItemNotFound(string message) : base(message) { }
	}
}
