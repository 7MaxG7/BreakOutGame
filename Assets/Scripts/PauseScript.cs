using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : TEventInvoker<int> {
    void Start() {
        events.Add(EventsEnum.LevelReset, new IntEvent());
        events.Add(EventsEnum.UnpauseEvent, new IntEvent());
        TEventManager<int>.AddInvoker(EventsEnum.LevelReset, this);
        TEventManager<int>.AddInvoker(EventsEnum.UnpauseEvent, this);
        Time.timeScale = 0;
    }
    public void OnClickResumeButton() {
        Time.timeScale = 1;
        events[EventsEnum.UnpauseEvent].Invoke(0);
        Destroy(gameObject);
    }
    public void OnClickRestartButton() {
        OnClickResumeButton();
        ObjectsPool.ReturnAllObjToPool();
        events[EventsEnum.LevelReset].Invoke(0);
        MenuManager.GoToMenu(MenuName.Restart);
    }
    public void OnClickQuitButton() {
        OnClickResumeButton();
        ObjectsPool.ReturnAllObjToPool();
        MenuManager.GoToMenu(MenuName.MainMenu);
    }
}
