using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class PlayFabLogin : MonoBehaviour
{
    public static bool IsLoggedIn { get; private set; } = false;

    void Start()
    {
        // Example login procedure
        Login();
    }

    private void Login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };

        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
    }

    private void OnLoginSuccess(LoginResult result)
    {
        IsLoggedIn = true;
        Debug.Log("Login successful!");
    }

    private void OnLoginFailure(PlayFabError error)
    {
        IsLoggedIn = false;
        Debug.LogError("Login failed: " + error.GenerateErrorReport());
    }
}
