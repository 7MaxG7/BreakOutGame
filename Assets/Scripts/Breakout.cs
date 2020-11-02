using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Breakout {
    public static bool Initialized { get; private set; } = false;
    public static int CurrentScore { get; private set; } = 0;

    public static void Initialize() {
        Initialized = true;
        ScreenUtils.Initialize();
        ConfigurationUtils.Initialize();
        TEventManager<int>.Initialize();
        ObjectsPool.Initialize();
        TEventManager<int>.AddListener(EventsEnum.AllBallsLost, ShowGameOverScreen);
        TEventManager<int>.AddListener(EventsEnum.AllBlocksDestroyed, ShowLevelClearedScreen);
    }
    static void ShowGameOverScreen(int score) {
        CurrentScore = 0;
        AudioManager.Play(AudioClipEnum.GameOver);
        GameObject gameOverPref = GameObject.Instantiate(Resources.Load<GameObject>(@"Prefabs\GameOverPref"));
        gameOverPref.GetComponent<GameOverScript>().SetScore = score;
    }
    static void ShowLevelClearedScreen(int score) {
        CurrentScore = score;
        AudioManager.Play(AudioClipEnum.Congrats);
        GameObject levelClearedPref = GameObject.Instantiate(Resources.Load<GameObject>(@"Prefabs\LevelClearedPref"));
        levelClearedPref.GetComponent<LevelClearedScript>().SetScore = score;
    }
}
