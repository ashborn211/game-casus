using Esper.ESave.Encryption;
using UnityEngine;

namespace Esper.ESave
{
    [System.Serializable]
    public class SaveFileSetupData
    {
        public string fileName = "GameSaveData";
        public SaveLocation saveLocation;
        public string filePath = "Saves"; // Ensure this is set correctly in your instance.
        public FileType fileType;
        public EncryptionMethod encryptionMethod;
        public string aesKey;
        public string aesIV;
        public bool addToStorage = true;
        public bool backgroundTask;

        public SaveFileSetupData() { }

        public SaveFileSetupData(string fileName, SaveLocation saveLocation, string filePath, FileType fileType,
            EncryptionMethod encryptionMethod, string aesKey, string aesIV, bool addToStorage, bool backgroundTask)
        {
            this.fileName = fileName;
            this.saveLocation = saveLocation;
            this.filePath = filePath;
            this.fileType = fileType;
            this.encryptionMethod = encryptionMethod;
            this.aesKey = aesKey;
            this.aesIV = aesIV;
            this.addToStorage = addToStorage;
            this.backgroundTask = backgroundTask;
        }

        public void GenerateAESTokens()
        {
            aesKey = ESaveEncryption.GenerateRandomToken(16);
            aesIV = ESaveEncryption.GenerateRandomToken(16);
        }

        /// <summary>
        /// Saves the provided data to a file, overwriting if necessary.
        /// </summary>
        public void SaveData(object data)
        {
            string fullPath = GetFullPath();

            if (!string.IsNullOrEmpty(fullPath))
            {
                // Serialize your data and write to the file, overwriting if it exists
                string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                System.IO.File.WriteAllText(fullPath, jsonData); // Overwrite if exists
                UnityEngine.Debug.Log($"Saved data to: {fullPath}");
            }
            else
            {
                UnityEngine.Debug.LogError("File path is invalid. Cannot save data.");
            }
        }

        /// <summary>
        /// Loads the data from the save file if it exists.
        /// </summary>
        public T LoadData<T>()
        {
            string fullPath = GetFullPath();

            if (!string.IsNullOrEmpty(fullPath))
            {
                UnityEngine.Debug.Log($"Attempting to load from: {fullPath}");
                if (System.IO.File.Exists(fullPath))
                {
                    // Read the data from the file and deserialize it
                    string jsonData = System.IO.File.ReadAllText(fullPath);
                    T data = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonData);
                    UnityEngine.Debug.Log($"Loaded data from: {fullPath}");
                    return data;
                }
                else
                {
                    UnityEngine.Debug.LogWarning($"No save file found at: {fullPath}. Returning default data.");
                    return default;
                }
            }
            else
            {
                UnityEngine.Debug.LogError("File path is invalid. Cannot load data.");
                return default;
            }
        }

        private string GetFullPath()
        {
            switch (saveLocation)
            {
                case SaveLocation.PersistentDataPath:
                    return System.IO.Path.Combine(Application.persistentDataPath, filePath, fileName + ".json");
                case SaveLocation.DataPath:
                    return System.IO.Path.Combine(Application.dataPath, filePath, fileName + ".json");
                default:
                    return null;
            }
        }

        public enum SaveLocation
        {
            PersistentDataPath,
            DataPath
        }

        public enum FileType
        {
            Json
        }

        public enum EncryptionMethod
        {
            None,
            AES
        }
    }
}
