using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScript : TEventInvoker<int> {
    [SerializeField] Text showScore;
    int score;

    void Start() {
        //Time.timeScale = 0;
        ObjectsPool.ReturnAllObjToPool();
        events.Add(EventsEnum.LevelReset, new IntEvent());
        TEventManager<int>.AddInvoker(EventsEnum.LevelReset, this);
    }

    public int SetScore {
        set { score = value;
            showScore.text = "Score: " + score;
        }
    }

    public void OnClickRestartButton() {
        events[EventsEnum.LevelReset].Invoke(0);
        MenuManager.GoToMenu(MenuName.Restart);
    }
    public void OnClickQuitButtonEvent() {
        //Time.timeScale = 1;
        Destroy(gameObject);
        MenuManager.GoToMenu(MenuName.MainMenu);
    }

}
