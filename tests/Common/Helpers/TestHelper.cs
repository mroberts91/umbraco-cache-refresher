// Copyright (c) Umbraco.
// See LICENSE for more details.

using System.IO;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Logging;

namespace Our.Umbraco.CacheRefresher.Tests.Common.Helpers
{
    /// <summary>
    /// Common helper properties and methods useful to testing
    /// </summary>
    public static class TestHelper
    {
        public static TypeLoader GetMockedTypeLoader() =>
            new(Mock.Of<ITypeFinder>(), new VaryingRuntimeHash(), Mock.Of<IAppPolicyCache>(), new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "CacheRefresher")), Mock.Of<ILogger<TypeLoader>>(), Mock.Of<IProfiler>());

        public static IServiceCollection GetServiceCollection() => new ServiceCollection();
    }
}
