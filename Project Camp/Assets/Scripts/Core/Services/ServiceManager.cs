namespace Core.Services
{
    public class ServiceManager
    {
        private static ServiceManager _container;
        public static ServiceManager Container => _container ??= new ServiceManager();

        public void Register<TService>(TService service) where TService: ISingleService =>
            Service<TService>.Instance = service;

        public TService Single<TService>() where TService: ISingleService => 
            Service<TService>.Instance;

        private static class Service<TService> where TService: ISingleService
        {
            public static TService Instance { get; set; }
        }
    }
}