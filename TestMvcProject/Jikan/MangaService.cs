namespace TestMvcProject.Jikan
{
    public class MangaService : IHostedService
    {
        private readonly IMangaLogicService _mangaLogicService;

        public MangaService(IServiceScopeFactory factory)
        {
            //_mangaLogicService = mangaLogicService;
            _mangaLogicService = factory.CreateScope().ServiceProvider.GetRequiredService<IMangaLogicService>();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // Не блокируем поток выполнения: StartAsync должен запустить выполнение фоновой задачи и завершить работу
            GetMangaAsync(cancellationToken);
            return Task.CompletedTask;
        }

        private async Task GetMangaAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    //await _mangaLogicService.GetTopManga();
                }
                catch (Exception ex)
                {
                    // обработка ошибки однократного неуспешного выполнения фоновой задачи
                }

                await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            // Если нужно дождаться завершения очистки, но контролировать время, то стоит предусмотреть в контракте использование CancellationToken
            //await someService.DoSomeCleanupAsync(cancellationToken);
            return Task.CompletedTask;
        }
    }
}
