using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Hud : TEventInvoker<int> {
    [SerializeField] Text textScore, textBalls, textSpawnTimer;
    Timer spawnTimer;
    int score, ballsLeft;

    void Awake() {
        spawnTimer = gameObject.AddComponent<Timer>();
        TEventManager<int>.AddListener(EventsEnum.StartSpawnTimer, StartSpawnTimer);
    }
    void Start() {
        score = Breakout.CurrentScore;
        textScore.text = "Score: " + score.ToString();
        ballsLeft = ConfigurationUtils.NumberBallsToLoose;
        textBalls.text = "Balls left: " + ballsLeft.ToString();

        events.Add(EventsEnum.AllBlocksDestroyed, new IntEvent());
        TEventManager<int>.AddInvoker(EventsEnum.AllBlocksDestroyed, this);
        events.Add(EventsEnum.AllBallsLost, new IntEvent());
        TEventManager<int>.AddInvoker(EventsEnum.AllBallsLost, this);
        TEventManager<int>.AddListener(EventsEnum.BlockDestroyed, BlockDestroyed);
        TEventManager<int>.AddListener(EventsEnum.BallLost, MinusBall);
    }
    void Update() {
        textSpawnTimer.text = "New ball in " + spawnTimer.TimeLeft.ToString("f2") + " seconds";
    }

    public int Score {
        get { return score; }
    }
    void BlockDestroyed(int addScore) {
        AddScore(addScore);
        if (ObjectsPool.NoBlocksInUse()) {
            spawnTimer.Stop();
            events[EventsEnum.AllBlocksDestroyed].Invoke(score);
        }
    }
    void AddScore(int addScore) {
        score += addScore;
        textScore.text = "Score: " + score.ToString();
    }
    public void MinusBall(int noValue) {
        --ballsLeft;
        textBalls.text = "Balls left: " + ballsLeft.ToString();
        if (ballsLeft <= 0) {
            //lastBallLostEvent.Invoke(score);
            events[EventsEnum.AllBallsLost].Invoke(score);
        }
        AudioManager.Play(AudioClipEnum.BallLoose);
    }
    void StartSpawnTimer(int spawnTimerDuration) {
        spawnTimer.Stop();
        spawnTimer.Duration = spawnTimerDuration;
        spawnTimer.Run();
    }
}
