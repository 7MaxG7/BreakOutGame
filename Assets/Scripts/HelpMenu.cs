using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpMenu : MonoBehaviour {
    public void OnClickBackButtonEvent() {
        MenuManager.GoToMenu(MenuName.MainMenu);
    }
}
