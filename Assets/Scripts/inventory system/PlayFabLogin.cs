using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class PlayFabLogin : MonoBehaviour
{
    public string playFabId;

    void Start()
    {
        LoginWithPlayFab();
    }

    void LoginWithPlayFab()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };

        PlayFabClientAPI.LoginWithCustomID(request,
            result => {
                playFabId = result.PlayFabId;
                Debug.Log("Login successful: " + playFabId);
            },
            error => {
                Debug.LogError("Error during login: " + error.GenerateErrorReport());
                // Additional debugging information
                Debug.LogError("Error Details: " + error.ErrorDetails);
            }
        );
    }
}
