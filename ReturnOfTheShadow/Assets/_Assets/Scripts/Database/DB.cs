using System;
using System.Net.Http;
using System.Text;
using UnityEngine;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
public class DB : MonoBehaviour
{
    string supabaseURL = "https://edmkfjdowivfcoacemfs.supabase.co";

    [SerializeField, ReadOnly] private string tokenKey;
    [SerializeField, ReadOnly] private string userID;
    [SerializeField] private int coins;

    [SerializeField] private GameEvent onlogin;

    [SerializeField] private VariableReference<string> apiKey;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    async void Start()
    {
       await LoginUser("tobiasktm85sx@gmail.com", "Ktm1190r");
    }
    
    public async Task LoginUser(string email, string password)
    {
        string LoginURL = supabaseURL + "/auth/v1/token?grant_type=password";
        var request = new HttpRequestMessage(HttpMethod.Post, LoginURL);
        
        request.Headers.Add("apikey",apiKey);
        var json = $"{{\"email\":\"{email}\",\"password\":\"{password}\"}}";
        request.Content = new StringContent(json, Encoding.UTF8, "application/json");
        
        var response = await new HttpClient().SendAsync(request);
        var body = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            Debug.Log("Login successful");
            onlogin?.Raise(true);
            tokenKey = ExtractAccessToken(body);
            userID = ExtractUserID(body);
            await UpdateUserScore(tokenKey, userID,coins);
            await GetData(tokenKey, userID);
        }
        else
        {
            Debug.Log("Login Unsuccessful");
        }
    }
    public async Task UpdateUserScore(string accessToken, string user, int newScore)
    {
        string url = $"{supabaseURL}/rest/v1/user_Data?user_DataID=eq.{user}";
        var request = new HttpRequestMessage(HttpMethod.Patch, url);

        request.Headers.Add("apikey", apiKey);
        request.Headers.Add("Authorization", $"Bearer {accessToken}");

        // JSON body with the column(s) to update
        var json = $"{{\"Coins\": {newScore}}}";
        request.Content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await new HttpClient().SendAsync(request);
        string body = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            Debug.Log("User updated successfully: " + body);
        }
        else
        {
            Debug.LogError($"Error updating user: {response.StatusCode} - {body}");
        }
    }

    public async Task GetData(string accessToken, string user)
    {
        string url = $"{supabaseURL}/rest/v1/user_Data?user_DataID=eq.{user}";
        var request = new HttpRequestMessage(HttpMethod.Get, url);

        request.Headers.Add("apikey", apiKey);
        request.Headers.Add("Authorization", $"Bearer {accessToken}");

        var response = await new HttpClient().SendAsync(request);
        string body = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            Debug.Log("User updated successfully: " + body); 
            coins = ExtractCoins(body);
        }
        else
        {
            Debug.LogError($"Error updating user: {response.StatusCode} - {body}");
        }
    }
    [Serializable]
    public class LoginResponse
    {
        public string access_token;
    }

    [Serializable]
    public class UserDetails
    {
        public string id;
        public int Coins;
        // Add other fields you need here
    }

    
    private string ExtractAccessToken(string body)
    {
        LoginResponse token = JsonUtility.FromJson<LoginResponse>(body);
        return token.access_token;
    }
    private string ExtractUserID(string body)
    {
        UserDetails details = JsonUtility.FromJson<UserDetails>(body);
        return details.id;
    }
    private int ExtractCoins(string body)
    {
        UserDetails details = JsonUtility.FromJson<UserDetails>(body);
        return details.Coins;
    }
}
