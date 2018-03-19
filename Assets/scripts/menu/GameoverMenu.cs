using UnityEngine;
using UnityEngine.UI;

public class GameoverMenu : MonoBehaviour {

    public Text scoreValueText;
    public Text highScoreValueText;
    public Text nameValueText;

    private void Start()
    {
        Time.timeScale = 0;
        int score = FindObjectOfType<ScoreTrack>().Score;
        SetFinalScore(score);
        FindObjectOfType<GameplayHUD>().FlipOverlay(true);
    }

    public void HandlePlayAgainButtonOnClick()
    {
        Destroy(gameObject);
        MenuManager.GoToMenu(MenuName.Gameplay);
    }

    public void HandleMainMenuButtonOnClick()
    {
        Destroy(gameObject);
        MenuManager.GoToMenu(MenuName.Main);
    }

    private void OnDestroy()
    {
        FindObjectOfType<GameplayHUD>().FlipOverlay(false);
    }

    private void SetFinalScore(int score)
    {
        if ((PlayerPrefs.HasKey("HighScore") && (PlayerPrefs.GetInt("HighScore") < score))
            || (!PlayerPrefs.HasKey("HighScore")))
        {
            PlayerPrefs.SetInt("HighScore", score);
        }

        scoreValueText.text = score.ToString();
        highScoreValueText.text = PlayerPrefs.GetInt("HighScore").ToString();
    }

}
