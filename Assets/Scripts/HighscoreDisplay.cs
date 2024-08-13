using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreDisplay : MonoBehaviour
{
    private Text highscoreText;

    void Start()
    {
        highscoreText = gameObject.GetComponent<Text>();
        if (MainManager.Instance != null)
        {
            SaveData SaveData = MainManager.Instance.GetSaveData();
            highscoreText.text = string.Format("Best Score : {0}: {1}", SaveData.HighscoreName, SaveData.Highscore);
        }
    }
}
