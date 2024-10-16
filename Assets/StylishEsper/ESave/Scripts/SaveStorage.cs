using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Esper.ESave
{
    [RequireComponent(typeof(SaveFileSetup)), DefaultExecutionOrder(-1)]
    public class SaveStorage : MonoBehaviour
    {
        public Dictionary<string, SaveFile> saves = new();
        private SaveFile savePathsFile;

        public static SaveStorage instance { get; private set; }

        public int saveCount => saves.Count;

        private void Awake()
        {
            // Singleton pattern
            if (!instance)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
                var saveFileSetup = GetComponent<SaveFileSetup>();
                savePathsFile = saveFileSetup.GetSaveFile();

                // Load existing save files
                LoadExistingSaves();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void LoadExistingSaves()
        {
            var allData = savePathsFile.GetAllDataOfType<SaveFileSetupData>();
            foreach (var data in allData)
            {
                // Only add if the save file doesn't already exist
                if (!saves.ContainsKey(data.fileName))
                {
                    new SaveFile(data, true);
                }
            }
        }

        public void AddSave(SaveFile saveFile)
        {
            if (saves.ContainsKey(saveFile.fileName))
            {
                Debug.LogWarning($"Save Storage: a save file with the name {saveFile.fileName} already exists.");
                return;
            }

            saves.Add(saveFile.fileName, saveFile);
            AddToSavedPaths(saveFile);
        }

        public void RemoveSave(SaveFile saveFile)
        {
            if (!saves.Remove(saveFile.fileName))
            {
                Debug.LogWarning($"Save Storage: a save file with the name {saveFile.fileName} does not exist.");
            }
            else
            {
                RemoveFromSavedPaths(saveFile);
            }
        }

        public void AddToSavedPaths(SaveFile saveFile)
        {
            if (!savePathsFile.HasData(saveFile.fileName))
            {
                savePathsFile.AddOrUpdateData(saveFile.fileName, saveFile.GetSetupData());
                savePathsFile.Save();
            }
        }

        public void RemoveFromSavedPaths(SaveFile saveFile)
        {
            if (savePathsFile.HasData(saveFile.fileName))
            {
                savePathsFile.DeleteData(saveFile.fileName);
                savePathsFile.Save();
            }
        }

        public bool ContainsKey(string key) => saves.ContainsKey(key);

        public bool ContainsFile(SaveFile saveFile) => saves.ContainsValue(saveFile);

        public SaveFile GetSaveByFileName(string fileName)
        {
            if (!ContainsKey(fileName))
            {
                Debug.LogWarning($"Save Storage: a save file with the name {fileName} does not exist.");
                return null;
            }
            return saves[fileName];
        }

        public SaveFile GetSaveAtIndex(int index)
        {
            if (index < 0 || index >= saves.Count)
            {
                Debug.LogWarning($"Save Storage: index out of range.");
                return null;
            }
            return saves.Values.ElementAt(index);
        }
    }
}
