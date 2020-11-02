using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class MenuManager {
    public static void GoToMenu(MenuName menuName) {
        switch (menuName) {
            case MenuName.MainMenu:
                SceneManager.LoadScene("01_MainMenu");
                AudioManager.Play(AudioClipEnum.MenuClick);
                break;
            case MenuName.Play:
                SceneManager.LoadScene("11_Level01");
                AudioManager.Play(AudioClipEnum.GameStart);
                break;
            case MenuName.Help:
                SceneManager.LoadScene("02_HelpMenu");
                AudioManager.Play(AudioClipEnum.MenuClick);
                break;
            case MenuName.Restart:
                Application.LoadLevel(Application.loadedLevel);
                break;
            case MenuName.Quit:
                Application.Quit();
                break;
            case MenuName.Pause:
                AudioManager.Play(AudioClipEnum.GamePause);
                Object.Instantiate(Resources.Load(@"Prefabs\PauseMenuPref"));
                break;
        }
    }
}
