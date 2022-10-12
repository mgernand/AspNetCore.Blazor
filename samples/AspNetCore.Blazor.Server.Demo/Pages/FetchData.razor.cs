namespace AspNetCore.Blazor.Server.Demo.Pages
{
	using System;
	using System.Threading.Tasks;
	using AspNetCore.Blazor.Server.Demo.Data;

	public partial class FetchData
	{
		private readonly WeatherForecastService weatherForecastService;
		private WeatherForecast[] forecasts;

		public FetchData(WeatherForecastService weatherForecastService)
		{
			this.weatherForecastService = weatherForecastService;
		}

		protected override async Task OnInitializedAsync()
		{
			this.forecasts = await this.weatherForecastService.GetForecastAsync(DateTime.Now);
		}
	}
}
