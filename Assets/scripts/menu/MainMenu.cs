using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    private void Awake()
    {
        Screen.SetResolution(300, 400, false, 60);
    }

    public void HandlePlayButtonOnClick()
    {
        MenuManager.GoToMenu(MenuName.Gameplay);
    }
}
