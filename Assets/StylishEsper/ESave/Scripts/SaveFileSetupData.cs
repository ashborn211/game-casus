using Esper.ESave.Encryption;
using UnityEngine;

namespace Esper.ESave
{
    [System.Serializable]
    public class SaveFileSetupData
    {
        public string fileName = "SaveFileName";
        public SaveLocation saveLocation;
        public string filePath = "Example/Path";
        public FileType fileType;
        public EncryptionMethod encryptionMethod;
        public string aesKey;
        public string aesIV;
        public bool addToStorage = true;
        public bool backgroundTask;

        public SaveFileSetupData() 
        {
            // Default constructor
        }

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

        public void SaveData(object data)
        {
            string fullPath = GetFullPath();

            if (!string.IsNullOrEmpty(fullPath))
            {
                // Check if file already exists to avoid duplicate saves
                if (!System.IO.File.Exists(fullPath))
                {
                    // Serialize your data and write to the file
                    string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                    System.IO.File.WriteAllText(fullPath, jsonData);
                    UnityEngine.Debug.Log($"Saved data to: {fullPath}");
                }
                else
                {
                    UnityEngine.Debug.LogWarning($"File already exists at: {fullPath}. Skipping save to avoid duplicates.");
                }
            }
            else
            {
                UnityEngine.Debug.LogError("File path is invalid. Cannot save data.");
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
