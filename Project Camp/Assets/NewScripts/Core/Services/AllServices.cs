namespace BeaconProject.Core.Services
{
    public class AllServices
    {
        private static AllServices _instance;
        public static AllServices Container => _instance ??= new AllServices();

        public void RegisterSingle<TService>(TService service) where TService : IService =>
            SingleService<TService>.Instance = service;

        public TService Single<TService>() where TService : IService =>
            SingleService<TService>.Instance;

        private static class SingleService<TService> where TService : IService
        {
            public static TService Instance { get; set; }
        }
    }
}