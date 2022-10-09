using Microsoft.Extensions.Hosting;
using System.Net.Http.Headers;
using System.Reflection;

namespace WebsiteStatusCheckerWorker
{
    //https://www.youtube.com/watch?v=PzrTiz_NRKA
    //host ile worker larý birleþtirip istemci yaz 
    public class Worker : BackgroundService
    {
        //default
        private readonly ILogger<Worker> _logger;
        //added
        private HttpClient httpClient;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
            httpClient = new HttpClient();
            ProductHeaderValue header = new ProductHeaderValue(("MyAwesomeLibrary"), Assembly.GetExecutingAssembly().GetName().Version?.ToString());
            ProductInfoHeaderValue userAgent = new ProductInfoHeaderValue(header);
            httpClient.DefaultRequestHeaders.UserAgent.Add(userAgent);
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {

            _logger.LogInformation("{time}: Nebula Qr Hizmeti Baþlatýldý.", DateTimeOffset.Now);
            return base.StartAsync(cancellationToken);
        }
        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogWarning("{time}: Worker service stopping.", DateTimeOffset.Now);
            _logger.LogInformation("{time}: Nebula Qr Hizmeti Sonlandýrýlýyor... ", DateTimeOffset.Now);
            httpClient.Dispose();
            _logger.LogWarning("{time}: Worker service stopped.", DateTimeOffset.Now);
            _logger.LogInformation("{time}: Nebula Qr Hizmeti Durduruldu.", DateTimeOffset.Now);
            return base.StopAsync(cancellationToken);
        }
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var result = await httpClient.GetAsync("http://anitr.net");
                var result2 = await httpClient.GetStringAsync("http://aerospace.com.tr");
                var a = result.Content.ReadAsStringAsync();
                var b = httpClient.DefaultRequestHeaders.UserAgent.ToString();


                if (result.IsSuccessStatusCode)
                {
                    _logger.LogInformation("{time}: Website is Up! Status code: {StatusCode}", DateTimeOffset.Now, result.StatusCode);
                }
                else
                {
                    _logger.LogError("{time}: Website is down! Status code: {StatusCode}", DateTimeOffset.Now, result.StatusCode);
                }
                _logger.LogWarning("{time}: Nebula Qr Hizmeti Veri Topluyor...", DateTimeOffset.Now);
                _logger.LogInformation("{time}: Nebula Qr Hizmeti Veri Topluyor...", DateTimeOffset.Now);
                //default
                _logger.LogInformation("{time}: Worker running...", DateTimeOffset.Now);
                await Task.Delay(5000, cancellationToken);
            }
        }
    }
}