using UnityEngine;
using UnityEngine.UI;

public class GameplayHUD : MonoBehaviour {

    bool paused = false;

    public CanvasGroup trans;

    Text scoreText;

    private void Start()
    {
        //        gameObject.GetComponent<CanvasGroup>();
        scoreText = GetComponentInChildren<Text>();
    }

    public void HandlePauseButtonOnClick()
    {
        FlipPause();

        if (paused)
            MenuManager.GoToMenu(MenuName.Pause);
        else
        {
            PauseMenu pm = FindObjectOfType<PauseMenu>();
            if (pm != null)
                pm.DestroyPauseMenu();
            else
                Debug.Log("PauseMenu not found!");
        }
    }


    public void FlipPause()
    {
        paused = !paused;
        FlipOverlay(paused);
    }

    public void FlipOverlay(bool value)
    {
        if (value)
        {
            gameObject.GetComponent<Canvas>().planeDistance = 9;
            trans.alpha = 0.95f;
        }
        else
        {
            gameObject.GetComponent<Canvas>().planeDistance = 100;
            trans.alpha = 1f;
        }
    }

    public void SetScore(int score)
    {
        scoreText.text = score.ToString();
    }
}
