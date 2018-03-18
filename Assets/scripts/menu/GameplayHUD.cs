using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayHUD : MonoBehaviour {

    bool paused = false;

    public CanvasGroup trans;


    private void Start()
    {
//        gameObject.GetComponent<CanvasGroup>();
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
        
        if (paused)
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

}
