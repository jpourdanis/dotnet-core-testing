using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AngleSharp.Dom.Html;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using RazorPagesProject.Services;
using RazorPagesProject.Tests.Helpers;
using System.Net.Http;
using System.Net.Http.Headers;
using System;

namespace RazorPagesProject.Tests.IntegrationTests
{
    public class BasicTests 
        : IClassFixture<WebApplicationFactory<RazorPagesProject.Startup>>
    {
        private readonly WebApplicationFactory<RazorPagesProject.Startup> _factory;

        public BasicTests(WebApplicationFactory<RazorPagesProject.Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("ypourdanis", "John Pourdanis")]
        [InlineData("pavkout", "Pavlos Koutoglou")]
        [InlineData("ziaziosk", "Ziazios Konstantinos")]
        public async Task CanGetUserAsyncFromGithubClientApi(string username, string fullName)
        {
            // Arrange
            var _client = new HttpClient();
            _client.BaseAddress = new Uri("https://api.github.com");
            _client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("Yolo", "0.1.0"));
            var _githubClient = new GithubClient(_client);

            // Act
            var response = await _githubClient.GetUserAsync(username);

            // Assert
            Assert.Equal(response.Name, fullName);
        }

        [Theory]
        [InlineData("/")]
        [InlineData("/Index")]
        [InlineData("/About")]
        [InlineData("/Privacy")]
        [InlineData("/Contact")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("text/html; charset=utf-8", 
                response.Content.Headers.ContentType.ToString());
        }



        [Fact]
        public async Task Get_SecurePageRequiresAnAuthenticatedUser()
        {
            // Arrange
            var client = _factory.CreateClient(
                new WebApplicationFactoryClientOptions
                {
                    AllowAutoRedirect = false
                });

            // Act
            var response = await client.GetAsync("/SecurePage");

            // Assert
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.StartsWith("http://localhost/Identity/Account/Login", 
                response.Headers.Location.OriginalString);
        }

        [Fact]
        public async Task CanGetAGithubUser()
        {
            // Arrange
            void ConfigureTestServices(IServiceCollection services) =>
                services.AddSingleton<IGithubClient>(new TestGithubClient());
            var client = _factory
                .WithWebHostBuilder(builder => 
                    builder.ConfigureTestServices(ConfigureTestServices))
                .CreateClient();

            // Act
            var profile = await client.GetAsync("/GithubProfile");
            Assert.Equal(HttpStatusCode.OK, profile.StatusCode);
            var profileHtml = await HtmlHelpers.GetDocumentAsync(profile);

            var profileWithUserName = await client.SendAsync(
                (IHtmlFormElement)profileHtml.QuerySelector("#user-profile"),
                new Dictionary<string, string> { ["Input_UserName"] = "user" });

            // Assert
            Assert.Equal(HttpStatusCode.OK, profileWithUserName.StatusCode);
            var profileWithUserHtml = 
                await HtmlHelpers.GetDocumentAsync(profileWithUserName);
            var userLogin = profileWithUserHtml.QuerySelector("#user-login");
            Assert.Equal("user", userLogin.TextContent);
        }

        public class TestGithubClient : IGithubClient
        {
            public Task<GithubUser> GetUserAsync(string userName)
            {
                if (userName == "user")
                {
                    return Task.FromResult(
                        new GithubUser
                        {
                            Login = "user",
                            Company = "Contoso Blockchain",
                            Name = "John Doe"
                        });
                }
                else
                {
                    return Task.FromResult<GithubUser>(null);
                }
            }
        }
    }
}
