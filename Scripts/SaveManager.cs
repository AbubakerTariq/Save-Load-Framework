using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }
    private static readonly Dictionary<string, object> SaveData = new();

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
    }

    private void Update()
    {
        var keyboard = Keyboard.current;
        if (keyboard == null) return;

        if (keyboard.numpad0Key.wasPressedThisFrame)
        {
            Debug.Log($"Save Data: {SaveData.Count} objects saved.");
        }

        if (keyboard.numpad1Key.wasPressedThisFrame)
        {
            SceneManager.LoadScene("Main Scene");
        }

        if (keyboard.numpad2Key.wasPressedThisFrame)
        {
            SceneManager.LoadScene("2nd Scene");
        }
    }

    public static void CaptureObjectState(string id, object data)
    {
        // Capture and store the state of the object
        SaveData[id] = data;
    }

    public static object RestoreObjectState(string id)
    {
        if (SaveData.TryGetValue(id, out var state))
            return state;
        
        return null;
    }
}