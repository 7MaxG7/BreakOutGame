using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ball : TEventInvoker<int> {
    Rigidbody2D ballRB;
    Timer timerBallStartFreeze;
    Timer timerBallDeath;
    Timer timerBallSpeedup;
    float speedFactor;

    public void Initialize() {
        ballRB = GetComponent<Rigidbody2D>();

        timerBallDeath = gameObject.AddComponent<Timer>();
        timerBallDeath.AddTimerFinishedListener(RebirthBall);

        timerBallStartFreeze = gameObject.AddComponent<Timer>();
        timerBallStartFreeze.AddTimerFinishedListener(PushBall);

        timerBallSpeedup = gameObject.AddComponent<Timer>();
        timerBallSpeedup.AddTimerFinishedListener(BallSpeedDown);
        TEventManager<int>.AddListener(EventsEnum.SpeedUp, BallSpeedUp);

        events.Add(EventsEnum.BallRespawn, new IntEvent());
        TEventManager<int>.AddInvoker(EventsEnum.BallRespawn, this);
        events.Add(EventsEnum.BallLost, new IntEvent());
        TEventManager<int>.AddInvoker(EventsEnum.BallLost, this);
    }
    public void Activate() {
        timerBallSpeedup.Stop();
        speedFactor = EffectUtils.Factor;
        timerBallDeath.Duration = ConfigurationUtils.BallLifetime;
        timerBallDeath.Run();
        timerBallStartFreeze.Duration = 1;
        timerBallStartFreeze.Run();
    }

    public void SetDirection(Vector2 direction) {
        ballRB.velocity = direction * ballRB.velocity.magnitude;
    }
    public void PushBall() {
        float pushAngle = 90 * Mathf.Deg2Rad;
        Vector2 direction = new Vector2(Mathf.Cos(pushAngle), Mathf.Sin(pushAngle)) * ConfigurationUtils.BallImpulseForce;
        if (EffectUtils.SpeedEffectActive) {
            timerBallSpeedup.Duration = EffectUtils.SpeedTimeLeft;
            timerBallSpeedup.Run();
            direction *= speedFactor;
        }
        ballRB.velocity = Vector2.zero;
        ballRB.AddForce(direction, ForceMode2D.Impulse);
    }
    private void OnBecameInvisible() {
        if (transform.position.y < ScreenUtils.ScreenBottom) {
            events[EventsEnum.BallLost].Invoke(0);
        }
        if (transform.position.y < ScreenUtils.ScreenBottom || transform.position.y > ScreenUtils.ScreenTop
                || transform.position.x < ScreenUtils.ScreenLeft || transform.position.x > ScreenUtils.ScreenRight) {
            RebirthBall();
        }
    }
    void RebirthBall() {
        events[EventsEnum.BallRespawn].Invoke(0);
        timerBallSpeedup.Stop();
        ObjectsPool.ReturnObjToPool(gameObject);
    }
    void BallSpeedUp(int duration) {
        if (gameObject.activeSelf && !timerBallSpeedup.Running) {
            ballRB.velocity *= speedFactor;
            timerBallSpeedup.Duration = duration;
            timerBallSpeedup.Run();
        } else if (gameObject.activeSelf) {
            timerBallSpeedup.AddDuration(duration);
        }
    }
    void BallSpeedDown() {
        if (gameObject.activeSelf) {
            timerBallSpeedup.Stop();
            ballRB.velocity /= speedFactor;
        }
    }
}
