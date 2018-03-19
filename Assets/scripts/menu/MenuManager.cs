using UnityEngine;
using UnityEngine.SceneManagement;

public static class MenuManager
{

    public static void GoToMenu(MenuName menu)
    {
        switch (menu)
        {
            case MenuName.Main:
                SceneManager.LoadScene("mainmenu");
                break;

            case MenuName.Gameplay:
                SceneManager.LoadScene("gameplay");
                break;

            case MenuName.Pause:
                Object.Instantiate(Resources.Load(@"prefabs\overlays\PauseMenu"));
                break;

            case MenuName.Gameover:
                Object.Instantiate(Resources.Load(@"prefabs\overlays\GameoverCanvas"));
                break;
        }
    }
}
