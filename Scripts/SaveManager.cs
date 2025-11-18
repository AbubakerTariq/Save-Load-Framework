using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.IO;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }
    private readonly Dictionary<string, object> GameData = new();
    private string SavePath => Path.Combine(Application.persistentDataPath, "savegame.json");

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        LoadGame();
    }

    private void Update()
    {
        var keyboard = Keyboard.current;
        if (keyboard == null) return;

        if (keyboard.numpad0Key.wasPressedThisFrame)
        {
            Debug.Log($"Save Data: {GameData.Count} objects saved.");
        }

        if (keyboard.numpad1Key.wasPressedThisFrame)
        {
            SceneManager.LoadScene("Main Scene");
        }

        if (keyboard.numpad2Key.wasPressedThisFrame)
        {
            SceneManager.LoadScene("2nd Scene");
        }

        if (keyboard.numpad3Key.wasPressedThisFrame)
        {
            SaveGame();
        }
    }

    public void CaptureObjectState(string id, object data)
    {
        // Capture and store the state of the object
        GameData[id] = data;
    }

    public object RestoreObjectState(string id)
    {
        if (GameData.TryGetValue(id, out var state))
            return state;
        
        return null;
    }

    private void SaveGame()
    {
        var saveableObjects = FindObjectsByType<SaveableObject>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (var obj in saveableObjects)
            obj.CaptureObjectState();

        // Convert dictionary into a serializable structure
        var json = JsonUtility.ToJson(new SerializationWrapper(Instance.GameData), true);
        File.WriteAllText(Instance.SavePath, json);
        Debug.Log($"Game saved to {Instance.SavePath}");
    }

    private void LoadGame()
    {
        if (!File.Exists(Instance.SavePath))
        {
            Debug.Log("No save file found.");
            return;
        }

        string json = File.ReadAllText(Instance.SavePath);
        var wrapper = JsonUtility.FromJson<SerializationWrapper>(json);

        Instance.GameData.Clear();
        wrapper.ToDictionary(Instance.GameData);

        Debug.Log("Game loaded.");
    }

    [System.Serializable]
    private class SerializationWrapper
    {
        public List<string> keys = new();
        public List<string> typeNames = new();
        public List<string> jsonValues = new();

        public SerializationWrapper(Dictionary<string, object> dict)
        {
            foreach (var kvp in dict)
            {
                keys.Add(kvp.Key);

                var type = kvp.Value.GetType();
                typeNames.Add(type.AssemblyQualifiedName);

                jsonValues.Add(JsonUtility.ToJson(kvp.Value));
            }
        }

        public void ToDictionary(Dictionary<string, object> dict)
        {
            dict.Clear();

            for (int i = 0; i < keys.Count; i++)
            {
                var type = System.Type.GetType(typeNames[i]);
                var obj = JsonUtility.FromJson(jsonValues[i], type);

                dict[keys[i]] = obj;
            }
        }
    }
}