using System;
using System.Net;
using System.Net.Http.Json;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTest.Extensions.Contracts;
using WatchDagServer.Model;

namespace WatchDagServer.Test
{
    [TestClass]
    public class DogControllerTest
    {
        [ContractTestCase]
        public void FeedDog()
        {
            "���Է���ι�����������Է��ʵ�����".Test(async () =>
            {
                var testClient = TestHostBuild.GetTestClient();
                var response = await testClient.GetAsync("Dog/FeedDog");
                Assert.AreEqual(true, response.StatusCode == HttpStatusCode.OK);
            });
        }

        [ContractTestCase]
        public void RegisterWatch()
        {
            "��û�г����涨ʱ����ι�������ᱻҧ".Test(async () =>
            {
                var testClient = TestHostBuild.GetTestClient();

                var registerRequest = new RegisterRequest()
                {
                    Token = Guid.NewGuid().ToString("N"),
                    DelaySecond = 2,
                    MaxDelayCount = 5,
                    NotifyEmailList = new[] { "lindexi@doubi.com" }
                };

                var response = await testClient.PostAsJsonAsync("Dog/RegisterWatch", registerRequest);

                Thread.Sleep(3000);

                await testClient.PostAsJsonAsync("Dog/RegisterWatch", registerRequest);

                Thread.Sleep(1000000);
            });

            "���Լ��涨ʱ����ι�������ᱻҧ".Test(async () =>
            {
                var testClient = TestHostBuild.GetTestClient();

                var registerRequest = new RegisterRequest()
                {
                    Token = Guid.NewGuid().ToString("N"),
                    DelaySecond = 2,
                    MaxDelayCount = 5,
                    NotifyEmailList = new[] { "lindexi@doubi.com" }
                };

                var response = await testClient.PostAsJsonAsync("Dog/RegisterWatch", registerRequest);

                Thread.Sleep(1000);

                await testClient.PostAsJsonAsync("Dog/RegisterWatch", registerRequest);
            });

            "���Դ������".Test(async () =>
            {
                var testClient = TestHostBuild.GetTestClient();

                var registerRequest = new RegisterRequest()
                {
                    Token = Guid.NewGuid().ToString("N"),
                    DelaySecond = 2,
                    MaxDelayCount = 5,
                    NotifyEmailList = new []{ "lindexi@doubi.com" }
                };

                var response = await testClient.PostAsJsonAsync("Dog/RegisterWatch", registerRequest);

                Thread.Sleep(1000000);
            });

            "����ע��".Test(async () =>
            {
                var testClient = TestHostBuild.GetTestClient();
                var response = await testClient.GetAsync("Dog/RegisterWatch?token=as");

              });
        }
    }
}
