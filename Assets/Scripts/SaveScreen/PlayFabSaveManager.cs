using System;
using System.IO;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

public class PlayFabSaveManager : MonoBehaviour
{
    private string saveFilePath;

    void Awake()
    {
        // Define the local save file path
        saveFilePath = Path.Combine(Application.persistentDataPath, "gameSaveData.json");

        // Log in to PlayFab
        LoginToPlayFab();
    }

    /// <summary>
    /// Logs the player into PlayFab using a unique device ID.
    /// </summary>
    private void LoginToPlayFab()
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
        Debug.Log("PlayFab login successful!");

        // Once logged in, sync the local save data with PlayFab
        SyncSaveData();
    }

    private void OnLoginFailure(PlayFabError error)
    {
        Debug.LogError("PlayFab login failed: " + error.GenerateErrorReport());
        Debug.Log("Using local save data due to login failure.");

        // If login fails, load the game data from local storage
        LoadFromLocal();
    }

    /// <summary>
    /// Syncs local save data with PlayFab cloud storage.
    /// If offline, continues using local data.
    /// </summary>
    private void SyncSaveData()
    {
        if (IsConnectedToInternet())
        {
            Debug.Log("Internet connection detected, syncing local save file with PlayFab...");

            // If the local save file exists, upload it to PlayFab
            if (File.Exists(saveFilePath))
            {
                string localData = File.ReadAllText(saveFilePath);
                SaveToPlayFab(localData);  // Save local file to PlayFab
            }
            else
            {
                Debug.LogWarning("No local save file found. Creating new save file.");
                SaveToLocal("{}"); // Create an empty save file if it doesn't exist
            }
        }
        else
        {
            Debug.Log("No internet connection, using local save data.");
            LoadFromLocal();
        }
    }

    /// <summary>
    /// Saves the game data both to PlayFab and local storage.
    /// </summary>
    public void SaveGameData(string jsonData)
    {
        // Save the game data to local storage first
        SaveToLocal(jsonData);

        // If connected to the internet, save the game data to PlayFab
        if (IsConnectedToInternet())
        {
            Debug.Log("Saving game data to PlayFab...");
            SaveToPlayFab(jsonData);
        }
        else
        {
            Debug.Log("No internet connection. Data only saved locally.");
        }
    }

    /// <summary>
    /// Saves the game data to the local storage as a JSON file.
    /// </summary>
    private void SaveToLocal(string jsonData)
    {
        File.WriteAllText(saveFilePath, jsonData);
        Debug.Log("Game data saved locally at: " + saveFilePath);
    }

    /// <summary>
    /// Loads the game data from the local save file.
    /// </summary>
    public void LoadFromLocal()
    {
        if (File.Exists(saveFilePath))
        {
            string jsonData = File.ReadAllText(saveFilePath);
            Debug.Log("Loaded local save data: " + jsonData);
            // Use this jsonData to restore your game state
        }
        else
        {
            Debug.LogWarning("No local save data found.");
        }
    }

    /// <summary>
    /// Saves the game data to PlayFab cloud storage.
    /// </summary>
    private void SaveToPlayFab(string jsonData)
    {
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                { "SaveData", jsonData }
            }
        };

        PlayFabClientAPI.UpdateUserData(request, OnDataSendSuccess, OnDataSendFailure);
    }

    private void OnDataSendSuccess(UpdateUserDataResult result)
    {
        Debug.Log("Game data successfully saved to PlayFab.");
    }

    private void OnDataSendFailure(PlayFabError error)
    {
        Debug.LogError("Failed to save data to PlayFab: " + error.GenerateErrorReport());
    }

    /// <summary>
    /// Checks if the device has an active internet connection.
    /// </summary>
    /// <returns>True if connected, false otherwise.</returns>
    private bool IsConnectedToInternet()
    {
        return Application.internetReachability != NetworkReachability.NotReachable;
    }
}
