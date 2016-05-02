using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Runtime.CompilerServices;

namespace Phant {
	public partial class MainPage : ContentPage {

		MainPageViewModel vm;

		public MainPage() {
			vm = new MainPageViewModel();
			BindingContext = vm;
			InitializeComponent();
		}

		public async void OnClicked(object o, EventArgs e) {
			var url = "https://data.sparkfun.com/output/0lxM48GANlsyJrWXOmDd.json?page=1";
			try {
				await vm.GetClimateAsync(url);
			} catch (Exception ex) {
				await DisplayAlert ("Network Error", "Error connecting to Phant server.", "OK");
			}

		}
	}
}

