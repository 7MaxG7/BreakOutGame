using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {
    public void OnClickPlayButtonEvent() {
        MenuManager.GoToMenu(MenuName.Play);
    }
    public void OnClickHelpButtonEvent() {
        MenuManager.GoToMenu(MenuName.Help);
    }
    public void OnClickQuitButtonEvent() {
        MenuManager.GoToMenu(MenuName.Quit);
    }
}
