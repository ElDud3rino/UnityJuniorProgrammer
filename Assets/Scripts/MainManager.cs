using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using TMPro;


#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class SaveData
{
    public string SelectedName = "NONAME";
    public string HighscoreName = "No highscore available";
    public int Highscore = 0;
}

public class MainManager : MonoBehaviour
{
    public static MainManager Instance { get; private set; }

    public string PlayerName = "NONAME";


    private void Awake()
    {
        if (Instance != null)
        {
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);


        GameObject nameTextObject = GameObject.Find("NameText");
        if (nameTextObject != null)
        {
            TMP_InputField nameTextField = nameTextObject.GetComponent<TMP_InputField>();
            if (nameTextField != null)
            {
                nameTextField.text = GetSaveData().SelectedName;
            }
        }
    }


    public void StartGame()
    {
        GameObject nameTextObject = GameObject.Find("NameText");
        if (nameTextObject != null)
        {
            TMP_InputField nameTextField = nameTextObject.GetComponent<TMP_InputField>();
            if (nameTextField != null)
            {
                PlayerName = nameTextField.text;
                SaveData saveData = GetSaveData();
                if(saveData.SelectedName != PlayerName)
                {
                    saveData.SelectedName = PlayerName;
                    SaveData(saveData);
                }
            }
        }

        SceneManager.LoadScene(1);
    }
    public void SaveData(SaveData saveData)
    {
        string filePath = Application.persistentDataPath + "/SaveData.json";
        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(filePath, json);
    }

    public SaveData GetSaveData()
    {
        string filePath = Application.persistentDataPath + "/SaveData.json";
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            return JsonUtility.FromJson<SaveData>(json);
        }

        return new SaveData();
    }

    public void ExitGame()
    {
        SaveData saveData = GetSaveData();
        saveData.SelectedName = PlayerName;
        SaveData(saveData);

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}