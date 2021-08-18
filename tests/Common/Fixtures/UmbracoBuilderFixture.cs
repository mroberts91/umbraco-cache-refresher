using Our.Umbraco.CacheRefresher.Tests.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Our.Umbraco.CacheRefresher.Tests.Common.Fixtures
{

    public abstract class TestWithUmbracoBuilder : IClassFixture<UmbracoBuilderFixture> { }

    public abstract class UmbracoBuilderFixture : IDisposable
    {
        protected IServiceCollection CreateRegister() => TestHelper.GetServiceCollection();
        protected readonly IServiceCollection _services;
        protected readonly IUmbracoBuilder _umbracoBuilder;
        protected readonly IServiceProvider _serviceProvider;
    
        public UmbracoBuilderFixture()
        {
            _services = CreateRegister();
            _umbracoBuilder = new UmbracoBuilder(_services, Mock.Of<IConfiguration>(), TestHelper.GetMockedTypeLoader());
            ConfigureServices();
            _serviceProvider = _services.BuildServiceProvider();
        }

        protected abstract void ConfigureServices();

        public void Dispose()
        {
            // ...
        }
    }
}
