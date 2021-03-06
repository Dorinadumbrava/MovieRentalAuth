using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using MovieRental.Client.ViewModels;
using MovieRental.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MovieRental.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory ??
                throw new ArgumentNullException(nameof(httpClientFactory));
        }

        public async Task<IActionResult> Index()
        {
            var identityToken = await HttpContext
               .GetTokenAsync(OpenIdConnectParameterNames.IdToken);
            var claims = User.Claims;
            var httpClient = _httpClientFactory.CreateClient("APIClient");

            var request = new HttpRequestMessage(
                HttpMethod.Get,
                "/api/movies/");

            var response = await httpClient.SendAsync(
                request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var responseSting = await response.Content.ReadAsStringAsync();
                return View(new MoviesViewModel(JsonConvert.DeserializeObject<List<Movie>>(responseSting)));
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
                    response.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                return RedirectToAction("AccessDenied", "Authorization");
            }

            throw new Exception("Problem accessing the API");
        }

        public async Task Logout()
        {

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme); //logout from aplication
            await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme); //logout from identity provider
        }

        public async Task<IActionResult> Review(Guid id)
        {
            var httpClient = _httpClientFactory.CreateClient("APIClient");

            var request = new HttpRequestMessage(
                HttpMethod.Get,
                $"/api/movies/{id}");

            var response = await httpClient.SendAsync(
                request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            var responseSting = await response.Content.ReadAsStringAsync();
            var movie = JsonConvert.DeserializeObject<Movie>(responseSting);
            return View(new ReviewViewModel { Id = id, ReviewText = movie.Review, MovieName = movie.Title });
        }



        [Authorize(Roles = "Subscribed")]
        public async Task<IActionResult> RentMovie()
        {
            var idpClient = _httpClientFactory.CreateClient("IDPClient");

            var metaDataResponse = await idpClient.GetDiscoveryDocumentAsync();

            if (metaDataResponse.IsError)
            {
                throw new Exception( "Could not access the discovery endpoint.", metaDataResponse.Exception);
            }

            var accessToken = await HttpContext
              .GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

            //Call to receive the user Info
            var userInfoResponse = await idpClient.GetUserInfoAsync(
               new UserInfoRequest
               {
                   Address = metaDataResponse.UserInfoEndpoint,
                   Token = accessToken
               });

            if (userInfoResponse.IsError)
            {
                throw new Exception( "Could not access the discovery endpoint." , userInfoResponse.Exception);
            }

            var address = userInfoResponse.Claims
                .FirstOrDefault(c => c.Type == "address")?.Value;

            return View(new RentMovieViewModel(address));
        }

        [HttpPost]
        public async Task<IActionResult> Review(ReviewViewModel reviewViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // create an ImageForUpdate instance
            var movieReview = new MovieReview()
            {
                Text = reviewViewModel.ReviewText
            };

            // serialize it
            var serializedreviewForUpdate = JsonConvert.SerializeObject(movieReview);

            var httpClient = _httpClientFactory.CreateClient("APIClient");

            var request = new HttpRequestMessage(
                HttpMethod.Put,
                $"/api/movies/{reviewViewModel.Id}");

            request.Content = new StringContent(
                serializedreviewForUpdate,
                System.Text.Encoding.Unicode,
                "application/json");

            var response = await httpClient.SendAsync(
                request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            return RedirectToAction("Index");
        }

        [Authorize(Policy = "CanBuyMerch")]
        public async Task<IActionResult> BuyMerch()
        {
            var idpClient = _httpClientFactory.CreateClient("IDPClient");

            var metaDataResponse = await idpClient.GetDiscoveryDocumentAsync();

            if (metaDataResponse.IsError)
            {
                throw new Exception(
                    "Problem accessing the discovery endpoint."
                    , metaDataResponse.Exception);
            }

            var accessToken = await HttpContext
              .GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

            var userInfoResponse = await idpClient.GetUserInfoAsync(
               new UserInfoRequest
               {
                   Address = metaDataResponse.UserInfoEndpoint,
                   Token = accessToken
               });

            if (userInfoResponse.IsError)
            {
                throw new Exception(
                    "Problem accessing the UserInfo endpoint."
                    , userInfoResponse.Exception);
            }

            var country = userInfoResponse.Claims
                .FirstOrDefault(c => c.Type == "country")?.Value;

            return View(new BuyMerchViewModel(country));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}