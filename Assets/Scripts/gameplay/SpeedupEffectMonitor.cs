using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedupEffectMonitor : MonoBehaviour {
    Timer speedTimer;
    float speedFactor;

    void Awake() {
        speedFactor = ConfigurationUtils.SpeedFactor;
    }
    void Start() {
        speedTimer = gameObject.AddComponent<Timer>();
        //EventManager.AddSpeedListener(AddSpeedDuration);
        TEventManager<int>.AddListener(EventsEnum.SpeedUp, AddSpeedDuration);
        TEventManager<int>.AddListener(EventsEnum.LevelReset, ResetSpeed);
        speedTimer.AddTimerFinishedListener(speedTimer.Stop);
    }

    public bool Active {
        get { return speedTimer.Running; }
    }
    public float TimeLeft {
        get { return speedTimer.TimeLeft; }
    }
    public float Factor {
        get { return speedFactor; }
    }

    void ResetSpeed(int noValue) {
        speedTimer.Stop();
        speedFactor = ConfigurationUtils.SpeedFactor;
    }
    void AddSpeedDuration(int duration) {
        if (!speedTimer.Running) {
            speedTimer.Duration = duration;
            speedTimer.Run();
        } else {
            speedTimer.AddDuration(duration);
        }
    }
}
