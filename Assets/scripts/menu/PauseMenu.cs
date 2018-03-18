using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {
    
    private void Start()
    {
        Time.timeScale = 0;
    }

    public void HandleResumeButtonOnClick()
    {
        DestroyPauseMenu();
        GameplayHUD gphud = FindObjectOfType<GameplayHUD>();
        if (gphud != null)
        {
            gphud.FlipPause();
        }
        else
        {
            Debug.Log("GameplayHUD not found");
        }
    }

    public void HandleMenuButtonOnClick()
    {
        Time.timeScale = 1;
        MenuManager.GoToMenu(MenuName.Main);
    }

    public void DestroyPauseMenu()
    {
        Time.timeScale = 1;
        Destroy(gameObject);
    }
}
