using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using RestSharp;

namespace RingoMedia.UI.Services
{
    public class EmailService
    {
        public async Task SendEmailAsync(string toEmail, string title)
        {
            var client = new RestClient("https://send.api.mailtrap.io/api/send");
            var request = new RestRequest();
            request.AddHeader("Authorization", "Bearer da149bb0201a48977ac35d52fd4083b8");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", "{\"from\":{\"email\":\"mailtrap@demomailtrap.com\",\"name\":\""+ title + "\"},\"to\":[{\"email\":\""+ toEmail + "\"}],\"subject\":\"You are awesome!\",\"text\":\"Congrats for sending a reminder to me!\",\"category\":\"Integration Test\"}", ParameterType.RequestBody);
            var response = client.Post(request);
        }
    }
}
