using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Reflection;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using ModernHttpClient;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Phant
{
	public class MainPageViewModel : INotifyPropertyChanged	// Allows model to alert the viewmodel of property changes
	{
		private double humidity;

		public double Humidity {
			get { return humidity; }
			set {
				humidity = value;
				NotifyPropertyChanged ();
			}
		}

		private double tempc;

		public double Tempc {
			get { return tempc; }
			set {
				tempc = value;
				NotifyPropertyChanged ();
			}
		}

		private double tempf;

		public double Tempf {
			get { return tempf; }
			set {
				tempf = value;
				NotifyPropertyChanged ();
			}
		}

		private long light;

		public long Light {
			get { return light; }
			set {
				light = value;
				NotifyPropertyChanged ();
			}
		}


		private string station;

		public string Station {
			get { return station; }
			set {
				station = value;
				NotifyPropertyChanged ();
			}
		}

		private string timestamp;

		public string Timestamp {
			get { return timestamp; }
			set {
				timestamp = value;
				NotifyPropertyChanged ();
			}
		}

		public async Task GetClimateAsync (string url)
		{
			var client = new HttpClient (new NativeMessageHandler ());
			client.BaseAddress = new Uri (url);

			try {
				var response = await client.GetAsync (client.BaseAddress);
				response.EnsureSuccessStatusCode ();
				var JsonResult = response.Content.ReadAsStringAsync ().Result;
				var climate = JsonConvert.DeserializeObject<List<ClimateObservation>> (JsonResult);
				SetValues (climate [0]);
			} catch (HttpRequestException ex) {
				throw new Exception ("HTTP request failed to get the data from Phant", ex);
			} catch (Exception ex) {
				throw new Exception ("Failed to get the data from Phant", ex);
			}
		}

		private void SetValues (ClimateObservation climateObservation)
		{
			var timestamp = climateObservation.timestamp;
			var humidity = climateObservation.humidity;
			var tempc = climateObservation.tempc;
			var tempf = climateObservation.tempf;
			var light = climateObservation.light;

			// Parse the date and time and convert to local time for display to user
			String dt = "Invalid format";
			DateTime date;

			try {
				date = DateTime.Parse (timestamp, null, System.Globalization.DateTimeStyles.RoundtripKind);
				dt = date.ToLocalTime ().ToString ("hh:mm:ss tt");
			} catch (FormatException) {
				// Don't really care about the time, so do nothing...
			}

			Timestamp = dt;
			Humidity = humidity;
			Tempc = tempc;
			Tempf = tempf;
			Light = light;
		}


 
		public event PropertyChangedEventHandler PropertyChanged;

		private void NotifyPropertyChanged ([CallerMemberName] string propertyName = "")
		{
			if (PropertyChanged != null) {
				PropertyChanged (this, new PropertyChangedEventArgs (propertyName));
			}
		}

	}
}

