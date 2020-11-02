using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : TEventInvoker<int> {
    Timer spawnBallTimer;
    float spawnPositionOy = -1;
    float spawnTimeMin, spawnTimeMax;
    float speedupRespawnMultiplier = 0.15f;

    void Start() {
        events.Add(EventsEnum.StartSpawnTimer, new IntEvent());
        TEventManager<int>.AddInvoker(EventsEnum.StartSpawnTimer, this);
        TEventManager<int>.AddListener(EventsEnum.BallRespawn, SpawnBall);
        TEventManager<int>.AddListener(EventsEnum.AllBallsLost, StopBallsSpawning);
        TEventManager<int>.AddListener(EventsEnum.AllBlocksDestroyed, StopBallsSpawning);
        TEventManager<int>.AddListener(EventsEnum.LevelReset, StopBallsSpawning);

        spawnTimeMin = ConfigurationUtils.SpawnTimeMin / (1 - speedupRespawnMultiplier);
        spawnTimeMax = ConfigurationUtils.SpawnTimeMax / (1 - speedupRespawnMultiplier);
        spawnBallTimer = gameObject.AddComponent<Timer>();
        spawnBallTimer.AddTimerFinishedListener(SpawnNewBall);
        SpawnNewBall();
    }

    void SpawnNewBall() {
        SpawnBall(0);
        spawnTimeMin *= (1f - speedupRespawnMultiplier);
        spawnTimeMax *= (1f - speedupRespawnMultiplier);
        spawnBallTimer.Stop();
        int newTimer = (int)Random.Range(spawnTimeMin, spawnTimeMax);
        spawnBallTimer.Duration = newTimer;
        events[EventsEnum.StartSpawnTimer].Invoke(newTimer);
        spawnBallTimer.Run();
    }

    void SpawnBall(int noValue) {
        float spawnPositionOx = Random.Range(ScreenUtils.ScreenLeft / 2, ScreenUtils.ScreenRight / 2);
        GameObject ball = ObjectsPool.GetBallFromPool(new Vector3(spawnPositionOx, spawnPositionOy, 0));
        ball.GetComponent<Ball>().Activate();
        AudioManager.Play(AudioClipEnum.BallSpawn);
        if (spawnBallTimer.TimeLeft < 1) {
            spawnBallTimer.AddDuration(1);
        }
    }
    void StopBallsSpawning(int noValue) {
        spawnBallTimer.Stop();
    }
}
