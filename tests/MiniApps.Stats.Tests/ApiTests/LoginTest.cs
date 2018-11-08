using MiniApps.Stats.Api;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xunit;

namespace MiniApps.Stats.Tests.ApiTests
{
    public class LoginTest
    {
        private const string TestDomain = "http://localhost:5000";
        private const string TestAppId = "AppId0123456789";

        [Fact]
        public async void NewUserLogin()
        {
            string url = $"{TestDomain}/api/apps/login";

            IList<string> mockUsers = CreateMockUsers(1000); // 模拟用户

            bool testResult = true;

            foreach (var mockUser in mockUsers)
            {
                HttpClient httpClient = new HttpClient();

                AppUserDto userDto = new AppUserDto
                {
                    AppId = TestAppId,
                    UserId = mockUser,
                    Channel = ""
                };

                var httpContent = new StringContent(JsonConvert.SerializeObject(userDto), Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(url, httpContent);

                testResult &= response.IsSuccessStatusCode;
            }
            Assert.True(testResult);
        }


        public IList<string> CreateMockUsers(int userCount)
        {
            if (userCount < 1)
                throw new ArgumentException("userCount cannot less than 1");

            IList<string> userList = new List<string>();
            for(int i=0; i < userCount; i++)
            {
                userList.Add(Guid.NewGuid().ToString("N"));
            }
            return userList;
        }
    }
}
