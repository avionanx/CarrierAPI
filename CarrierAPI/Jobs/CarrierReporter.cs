namespace CarrierAPI.Jobs
{
    public class CarrierReporter
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
    
        public CarrierReporter(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task Report()
        {

        }
    }
}
