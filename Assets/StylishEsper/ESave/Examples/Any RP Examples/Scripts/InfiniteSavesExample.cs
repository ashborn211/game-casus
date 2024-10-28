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
            saveSlotCreator.onClick.AddListener(CreateNewSave);
        }

        private void Update()
        {
            // Increment time only if the game is not paused
            timeElapsed += Time.deltaTime;
            timeElapsedText.text = $"Time Elapsed: {timeElapsed}";
        }

        public void CreateNewSaveSlot(SaveFile saveFile)
        {
            string dataPath = Application.persistentDataPath + "Saves";
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

        public void CreateNewSave()
        {
            // Create the save file data
            SaveFileSetupData saveFileSetupData = new()
            {
                fileName = $"GameSaveData{SaveStorage.instance.saveCount}",
                saveLocation = SaveLocation.DataPath,
                filePath = Application.persistentDataPath + "Saves",
                fileType = FileType.Json,
                encryptionMethod = EncryptionMethod.None,
                addToStorage = true
            };

            SaveFile saveFile = new SaveFile(saveFileSetupData);

            // Save only if there is data to save
            if (timeElapsed > 0)
            {
                OverwriteSave(saveFile);
                SaveInventoryData(saveFile);
                CreateNewSaveSlot(saveFile);
            }
            else
            {
                Debug.LogWarning("No data to save. Skipping save operation.");
            }
        }

        public void LoadSave(SaveFile saveFile)
        {
            // Load the time elapsed
            timeElapsed = saveFile.GetData<float>(timeElapsedKey);
            Debug.Log($"Loaded Time Elapsed: {timeElapsed}");

            // Load inventory data
            LoadInventoryData(saveFile);
        }

        public void OverwriteSave(SaveFile saveFile)
        {
            // Save the time elapsed
            saveFile.AddOrUpdateData(timeElapsedKey, timeElapsed);
            saveFile.Save();
            Debug.Log($"Saved Time Elapsed: {timeElapsed}");
        }

        private void SaveInventoryData(SaveFile saveFile)
        {
            InventoryData inventoryData = Inventory.Singleton.GetInventoryData(); // Method to retrieve inventory data
            if (inventoryData != null) // Check if inventory data exists
            {
                saveFile.AddOrUpdateData(inventoryDataKey, inventoryData);
                saveFile.Save();
                Debug.Log("Saved Inventory Data.");
            }
            else
            {
                Debug.LogWarning("No inventory data to save.");
            }
        }

        private void LoadInventoryData(SaveFile saveFile)
        {
            InventoryData inventoryData = saveFile.GetData<InventoryData>(inventoryDataKey);
            Inventory.Singleton.LoadInventoryData(inventoryData); // Method to load inventory data
        }

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
