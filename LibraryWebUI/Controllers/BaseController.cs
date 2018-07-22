using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreLibrary.Inventory;
using CoreLibrary.Members;
using LibraryWebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebUI.Controllers
{
    public class BaseController : Controller {

		protected ViewModels.ViewModelFactory vmFactory = new ViewModels.ViewModelFactory();

		protected IAccount GetUserAccount() {
			return this.HttpContext.Session.GetJson<Account>("User");
		}

		protected void LoginUser(IAccount account) {
			this.HttpContext.Session.SetJson("User", account);
		}

		protected void LogoutUser() {
			this.HttpContext.Session.Remove("User");
		}

		protected Cart GetCart() {
			Cart cart = this.HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();
			return cart;
		}

		protected void AddToCart(IBook book) {
			Cart cart = this.GetCart();
			cart.Add((Book) book);
			this.SaveCart(cart);
		}

		protected void RemoveFromCart(IBook book) {
			Cart cart = this.GetCart();
			cart.Contents.Remove((Book) book);
			this.SaveCart(cart);
		}

		protected void SaveCart(Cart cart) {
			this.HttpContext.Session.SetJson("Cart", cart);
		}
	}
}
