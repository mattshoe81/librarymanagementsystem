using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using CoreLibrary.Inventory;
using CoreLibrary.Members;
using LibraryWebUI.Controllers;
using LibraryWebUI.Models;
using LibraryWebUI.ViewModels.Admin;
using LibraryWebUI.ViewModels.Member;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebUI.ViewModels
{
    public sealed class ViewModelFactory : BaseController {

		private T GetViewModel<T>() where T : BaseViewModel, new() {
			T vm = new T();
			vm.UserAccount = this.GetUserAccount();
			vm.UserCart = this.GetCart();
			return vm;
		}

		public BrowseViewModel GetBrowseViewModel() {
			BrowseViewModel vm = this.GetViewModel<BrowseViewModel>();

			return vm;
		}

		public CartViewModel GetCartViewModel() {
			CartViewModel vm = this.GetViewModel<CartViewModel>();

			return vm;
		}

		public MemberHomeViewModel GetMemberHomeViewModel() {
			MemberHomeViewModel vm = this.GetViewModel<MemberHomeViewModel>();

			return vm;
		}

		public ReserveBookViewModel GetReserveBookViewModel(IBook book) {
			ReserveBookViewModel vm = this.GetViewModel<ReserveBookViewModel>();
			vm.Book = book;

			return vm;
		}

		public BookDetailsViewModel GetBookDetailsViewModel(IBook book) {
			BookDetailsViewModel vm = this.GetViewModel<BookDetailsViewModel>();
			vm.Book = book;

			return vm;
		}

		public CreateAccountViewModel CreateAccountViewModel(string errorMessage) {
			CreateAccountViewModel vm = this.GetViewModel<CreateAccountViewModel>();
			vm.ErrorMessage = errorMessage;

			return vm;
		}

		public CreateAccountViewModel CreateAccountViewModel() {
			CreateAccountViewModel vm = this.GetViewModel<CreateAccountViewModel>();

			return vm;
		}

		public LoginViewModel GetLoginViewModel() {
			LoginViewModel vm = this.GetViewModel<LoginViewModel>();

			return vm;
		}

		public LoginViewModel GetLoginViewModel(string[] errorMessages) {
			LoginViewModel vm = this.GetViewModel<LoginViewModel>();
			vm.LoginErrorInfo = errorMessages;

			return vm;
		}

		public CreateAccountViewModel GetCreateAccountViewModel() {
			CreateAccountViewModel vm = this.GetViewModel<CreateAccountViewModel>();

			return vm;
		}

		public CreateAccountViewModel GetCreateAccountViewModel(string errorMessage) {
			CreateAccountViewModel vm = this.GetViewModel<CreateAccountViewModel>();
			vm.ErrorMessage = errorMessage;

			return vm;
		}

	}
}
