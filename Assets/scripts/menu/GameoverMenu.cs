using UnityEngine;

public class GameoverMenu : MonoBehaviour {

    public void HandlePlayAgainButtonOnClick()
    {
        MenuManager.GoToMenu(MenuName.Gameplay);
    }

    public void HandleMainMenuButtonOnClick()
    {
        MenuManager.GoToMenu(MenuName.Main);
    }
}
