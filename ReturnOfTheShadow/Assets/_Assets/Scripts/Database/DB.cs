using System;
using System.Net.Http;
using System.Text;
using UnityEngine;
using System.Threading.Tasks;
using Lean.Gui;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine.UI;
using Newtonsoft.Json;
using System.Collections.Generic;
public class DB : MonoBehaviour
{
    [SerializeField] private VariableReference<int> coins;

    [SerializeField] private GameEvent onLogin;
    [SerializeField] private GameEvent onSignUp;
    [SerializeField] private GameEvent onSignUpFailed;
    [SerializeField] private GameEvent onLoginFailed;
    [SerializeField] private GameEvent onCoinGet;
    
    [SerializeField] private TMP_InputField loginEmailInputField;
    [SerializeField] private TMP_InputField loginPasswordInputField;
    [SerializeField] private TMP_InputField signUpEmailInputField;
    [SerializeField] private TMP_InputField signUpPasswordInputField;
    [SerializeField] private Button loginButton;
    [SerializeField] private Button signUpButton;
    
    [SerializeField] private GameObject loginWindow;
    [SerializeField] private GameObject signupWindow;  

    [SerializeField] private VariableReference<string> apiKey;
    [Title("SupabaseManager")]
    [SerializeField] private SupabaseManager supabaseManager;
    

    private void Start()
    {
        loginButton.onClick.AddListener(Login);
        signUpButton.onClick.AddListener(CreateUser);
    }

    private void OnDisable()
    {
        loginButton.onClick.RemoveListener(Login);
        signUpButton.onClick.RemoveListener(CreateUser);
    }

    public async void Login()
    {
        await SignIn(loginEmailInputField.text, loginPasswordInputField.text);
    }
    public async void CreateUser()
    {
        await SignUp(signUpEmailInputField.text, signUpPasswordInputField.text);
    }
    public async Task SignIn(string email, string password)
    {
        var client = supabaseManager.GetClient();
        try
        {
            var signedInPerson = await client.Auth.SignIn(email, password);
            Debug.Log("Signed in: " + signedInPerson.User.Id);
            onLogin?.Raise(true);
            try
            {
                await GetCoins();
                
            }
            catch (Exception e)
            {
                await SetCoins(coins.value + 1);
                Console.WriteLine(e);
                throw;
            }
        }
        catch (Exception e)
        {
            onLoginFailed?.Raise(false);
            Debug.Log(e.Message);
            throw;
        }
    }
    public async Task SignUp(string email, string password)
    {
        var client = supabaseManager.GetClient();
        
            var session = await client.Auth.SignUp(email, password);
            if (session.User.Email != email)
            {
                Debug.Log($"Signed up: {session.User.Id} - {session.User.Email}");
                onSignUp?.Raise(true);
            }
            else
            {
                onSignUpFailed?.Raise(false);
                Debug.LogError("Sign up failed");
            }
    }
    
    public async Task GetCoins()
    {
        var client = supabaseManager.GetClient();
        var userId = client.Auth.CurrentUser.Id;
        
        var result = await client.From<UserData>().Where(u => u.User_DataID == userId).Get();
        
        if (result.Content is null)
        {
            Debug.LogError("Failed to get coins");
        }
        var userData = result.Models[0];
        coins.value = userData.Coins;
        //raise event
        onCoinGet?.Raise(coins.value);
    }
    public async Task SetCoins(int newValue)
    {
        var client = supabaseManager.GetClient();
        try
        {
            var userId = client.Auth.CurrentUser.Id;

            UserData newData = new UserData
            {
                User_DataID = userId,
                Coins = newValue
            };

            await client.From<UserData>().Upsert(newData);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
