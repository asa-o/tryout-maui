using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace tryoiutmaui.pages;

public partial class OpenAiChat : ContentPage
{
    public OpenAiChat()
    {
        InitializeComponent();
    }

    private string _inputQuery = "";

    private void OnQueryChanged(object sender, TextChangedEventArgs e)
    {
        _inputQuery = ((Editor)sender).Text;
    }

    private async void OnSubmitClicked(object sender, EventArgs e)
    {
        var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        using (var client = new HttpClient())
        {
            // APIエンドポイントのURL
            var url = "https://api.openai.com/v1/chat/completions";

            // 送信するJSONデータ
            var data = new
            {
                messages = new[] {
                        new { role = "user", content = _inputQuery }
                    },
                model = "gpt-3.5-turbo"
            };

            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", configuration["OpenAiKey"]);

            var response = await client.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var responseObject = JsonConvert.DeserializeObject<dynamic>(responseString);
                AnswerTextBox.Text = responseObject?.choices?[0]?.message?.content?.ToString();
            }
            else
            {
                Console.WriteLine("Error: " + response.StatusCode);
            }
        }
    }
}