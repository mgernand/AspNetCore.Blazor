namespace AspNetCore.Blazor.Wasm.Demo.Pages
{
	using System;
	using System.Net.Http;
	using System.Net.Http.Json;
	using System.Threading.Tasks;

	public partial class FetchData
	{
		private readonly HttpClient httpClient;
		private WeatherForecast[] forecasts;

		public FetchData(HttpClient httpClient)
		{
			this.httpClient = httpClient;
		}

		protected override async Task OnInitializedAsync()
		{
			this.forecasts = await this.httpClient.GetFromJsonAsync<WeatherForecast[]>("sample-data/weather.json");
		}

		public class WeatherForecast
		{
			public DateTime Date { get; set; }

			public int TemperatureC { get; set; }

			public string Summary { get; set; }

			public int TemperatureF => 32 + (int)(this.TemperatureC / 0.5556);
		}
	}
}
