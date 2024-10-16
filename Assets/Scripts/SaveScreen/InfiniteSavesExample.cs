//***************************************************************************************
// Writer: Stylish Esper
// Last Updated: May 2024
// Description: ESave infinite saves example.
//***************************************************************************************

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Esper.ESave.SaveFileSetupData;

namespace Esper.ESave.Example
{
    public class InfiniteSavesExample : MonoBehaviour
    {
        private const string timeElapsedKey = "TimeElapsed";
        private const string inventoryDataKey = "InventoryData"; // Key for inventory data

        [SerializeField]
        private Button saveSlotPrefab;
        [SerializeField]
        private Button saveSlotCreator;
        [SerializeField]
        private Transform content;
        [SerializeField]
        private Text timeElapsedText;
        [SerializeField]
        private Toggle modeToggle;

        private List<Button> slots = new();
        private float timeElapsed;

        private bool loadMode => modeToggle.isOn;

        private void Start()
        {
            // Instantiate slots for existing saves
            foreach (var save in SaveStorage.instance.saves.Values)
            {
                CreateNewSaveSlot(save);
            }

            // Save slot creator on-click event
            saveSlotCreator.onClick.AddListener(CreateNewSaveWrapper); // Use the wrapper method
        }

        private void Update()
        {
            // Increment time only if the game is not paused
            timeElapsed += Time.deltaTime;
            timeElapsedText.text = $"Time Elapsed: {timeElapsed}";
        }

        /// <summary>
        /// Creates a save slot for a save file.
        /// </summary>
        /// <param name="saveFile">The save file.</param>
        public void CreateNewSaveSlot(SaveFile saveFile)
        {
            string dataPath = Application.persistentDataPath;
            Debug.Log($"Data Path: {dataPath}");

            // Instantiate the save slot
            var slot = Instantiate(saveSlotPrefab, content);
            var slotText = slot.transform.GetChild(0).GetComponent<Text>();
            slotText.text = $"Save Slot {slots.Count}";

            // Move save creator to the bottom
            saveSlotCreator.transform.SetAsLastSibling();

            // Add on-click event for loading
            slot.onClick.AddListener(() => LoadOrOverwriteSave(saveFile));

            slots.Add(slot);
        }
        private void CreateNewSaveWrapper()
        {
            // Create a new SaveFileSetupData instance (you might want to customize this based on your needs)
            SaveFileSetupData saveFileData = new SaveFileSetupData
            {
                // Set properties as needed, or pull from a user interface or other sources
                saveLocation = SaveFileSetupData.SaveLocation.PersistentDataPath // Example: just setting a default
                                                                                 // Add any other necessary initialization
            };

            CreateNewSave(saveFileData); // Call the original method with the created data
        }

        /// <summary>
        /// Creates a new save.
        /// </summary>
        public void CreateNewSave(SaveFileSetupData saveFileData)
        {
            // Determine the appropriate file path based on the save location
            string basePath;

            // Check the selected save location from the saveFileData instance
            if (saveFileData.saveLocation == SaveFileSetupData.SaveLocation.PersistentDataPath)
            {
                basePath = Application.persistentDataPath + "/Saves"; // Updated to include "Saves" folder
            }
            else
            {
                basePath = Application.dataPath + "/Saves"; // Use normal data path
            }

            // Ensure the directory exists
            if (!System.IO.Directory.Exists(basePath))
            {
                System.IO.Directory.CreateDirectory(basePath);
            }

            // Create the save file data
            SaveFileSetupData saveFileSetupData = new SaveFileSetupData
            {
                fileName = $"gameSaveData{SaveStorage.instance.saveCount}",
                saveLocation = saveFileData.saveLocation, // Use the dynamically determined save location
                filePath = basePath, // Set the determined file path
                fileType = SaveFileSetupData.FileType.Json,
                encryptionMethod = SaveFileSetupData.EncryptionMethod.None,
                addToStorage = true
            };

            SaveFile saveFile = new SaveFile(saveFileSetupData);

            // Get the full path to the save file
            string fullPath = System.IO.Path.Combine(saveFileSetupData.filePath, saveFileSetupData.fileName + ".json");

            // Validate the file path
            if (string.IsNullOrEmpty(fullPath) || !System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(fullPath)))
            {
                Debug.LogError($"Invalid file path: {fullPath}. Cannot save data.");
                return; // Exit the method if the path is invalid
            }

            // Save the time elapsed
            OverwriteSave(saveFile);

            // Save the inventory data
            SaveInventoryData(saveFile);

            // Create the save slot for this data
            CreateNewSaveSlot(saveFile);
        }


        /// <summary>
        /// Loads a save.
        /// </summary>
        /// <param name="saveFile">The save file.</param>
        public void LoadSave(SaveFile saveFile)
        {
            // Load the time elapsed
            timeElapsed = saveFile.GetData<float>(timeElapsedKey);
            Debug.Log($"Loaded Time Elapsed: {timeElapsed}");

            // Load inventory data
            LoadInventoryData(saveFile);
        }

        /// <summary>
        /// Overwrites a save.
        /// </summary>
        /// <param name="saveFile">The save file.</param>
        public void OverwriteSave(SaveFile saveFile)
        {
            // Save the time elapsed
            saveFile.AddOrUpdateData(timeElapsedKey, timeElapsed);
            saveFile.Save();
            Debug.Log($"Saved Time Elapsed: {timeElapsed}");
        }

        /// <summary>
        /// Saves the inventory data to the save file.
        /// </summary>
        private void SaveInventoryData(SaveFile saveFile)
        {
            InventoryData inventoryData = Inventory.Singleton.GetInventoryData(); // Method to retrieve inventory data
            saveFile.AddOrUpdateData(inventoryDataKey, inventoryData);
            saveFile.Save();
            Debug.Log("Saved Inventory Data.");
        }

        /// <summary>
        /// Loads the inventory data from the save file.
        /// </summary>
        private void LoadInventoryData(SaveFile saveFile)
        {
            InventoryData inventoryData = saveFile.GetData<InventoryData>(inventoryDataKey);
            Inventory.Singleton.LoadInventoryData(inventoryData); // Method to load inventory data
        }

        /// <summary>
        /// Loads or overwrites the save based on the active mode.
        /// </summary>
        /// <param name="saveFile">The save file.</param>
        public void LoadOrOverwriteSave(SaveFile saveFile)
        {
            if (loadMode)
            {
                LoadSave(saveFile);
            }
            else
            {
                OverwriteSave(saveFile);
            }
        }
    }
}
