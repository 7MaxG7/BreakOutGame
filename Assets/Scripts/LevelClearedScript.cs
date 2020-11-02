using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelClearedScript : MonoBehaviour {
    [SerializeField] Text showScore;
    int score;

    void Start() {
        ObjectsPool.ReturnAllObjToPool();
    }

    public int SetScore {
        set { score = value;
            showScore.text = "Score: " + score;
        }
    }

    public void OnClickContinueButton() {
        Destroy(gameObject);
        LevelsCounter.SetToNextLevel();
        MenuManager.GoToMenu(MenuName.Play);
    }
    public void OnClickMainMenuButtonEvent() {
        Destroy(gameObject);
        MenuManager.GoToMenu(MenuName.MainMenu);
    }

}
