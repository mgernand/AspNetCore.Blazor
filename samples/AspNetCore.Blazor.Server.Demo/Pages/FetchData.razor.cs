namespace MadEyeMatt.AspNetCore.Blazor.Server.Demo.Pages
{
    using System;
    using System.Threading.Tasks;

    public partial class FetchData
	{
		private readonly MadEyeMatt.AspNetCore.Blazor.Server.Demo.Data.WeatherForecastService weatherForecastService;
		private MadEyeMatt.AspNetCore.Blazor.Server.Demo.Data.WeatherForecast[] forecasts;

		public FetchData(MadEyeMatt.AspNetCore.Blazor.Server.Demo.Data.WeatherForecastService weatherForecastService)
		{
			this.weatherForecastService = weatherForecastService;
		}

		protected override async Task OnInitializedAsync()
		{
			this.forecasts = await this.weatherForecastService.GetForecastAsync(DateTime.Now);
		}
	}
}
