using System;

namespace Phant
{
	public class ClimateObservation
	{
		[Newtonsoft.Json.JsonProperty(PropertyName = "humidity")]
		public double humidity { get; set; }

		[Newtonsoft.Json.JsonProperty(PropertyName = "tempc")]
		public double tempc { get; set; }

		[Newtonsoft.Json.JsonProperty(PropertyName = "tempf")]
		public double tempf { get; set; }

		[Newtonsoft.Json.JsonProperty(PropertyName = "light")]
		public long light { get; set; }

		[Newtonsoft.Json.JsonProperty(PropertyName = "station")]
		public string station { get; set; }

		[Newtonsoft.Json.JsonProperty(PropertyName = "timestamp")]
		public string timestamp { get; set; }
	}
}

