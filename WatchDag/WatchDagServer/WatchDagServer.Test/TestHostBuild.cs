using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WatchDagServer.Test
{
    [TestClass]
    public static class TestHostBuild
    {
        public static HttpClient GetTestClient() => _host.GetTestClient();

        [AssemblyInitialize]
        public static async Task GlobalInitialize(TestContext testContext)
        {
            IHost host = await CreateAndRun();
            _host = host;
        }

        private static IHost _host;

        [AssemblyCleanup]
        public static void GlobalCleanup()
        {
            _host.Dispose();
        }

        private static Task<IHost> CreateAndRun() => CreateHostBuilder().StartAsync();

        public static IHostBuilder CreateHostBuilder() =>
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseTestServer(); //�ؼ��Ƕ�����һ�н���TestServer
                })
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    // ���в��Ե�����
                    var appConfigurator = config.ToAppConfigurator();
                    // ����ʹ���� https://github.com/dotnet-campus/dotnetCampus.Configurations ������

                })
        //// ʹ�� auto fac ����Ĭ�ϵ� IOC ���� 
        //.UseServiceProviderFactory(new AutofacServiceProviderFactory())
        ;

    }
}