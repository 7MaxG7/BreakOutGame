using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {
    Rigidbody2D rb_comp;
    float paddleMovePS;
    float oxInput;
    float paddleWidthHalf;
    const float BounceAngleHalfRange = 60 * Mathf.Deg2Rad;
    Timer frozenTimer;

    float screenLeft;
    float screenRight;
    float screenTop;
    float screenBottom;

    void Start() {
        rb_comp = GetComponent<Rigidbody2D>();
        paddleMovePS = ConfigurationUtils.PaddleMoveSpeed;
        paddleWidthHalf = GetComponent<BoxCollider2D>().size.x / 2;
        frozenTimer = gameObject.AddComponent<Timer>();
        frozenTimer.AddTimerFinishedListener(frozenTimer.Stop);
        TEventManager<int>.AddListener(EventsEnum.Freeze, FreezePaddle);
        TEventManager<int>.AddListener(EventsEnum.AllBallsLost, DestroyPaddle);
        TEventManager<int>.AddListener(EventsEnum.AllBlocksDestroyed, DestroyPaddle);

        float screenZ = -Camera.main.transform.position.z;
        Vector3 lowerLeftCornerScreen = new Vector3(0, 0, screenZ);
        Vector3 upperRightCornerScreen = new Vector3(Screen.width, Screen.height, screenZ);
        Vector3 lowerLeftCornerWorld = Camera.main.ScreenToWorldPoint(lowerLeftCornerScreen);
        Vector3 upperRightCornerWorld = Camera.main.ScreenToWorldPoint(upperRightCornerScreen);
        screenLeft = lowerLeftCornerWorld.x;
        screenRight = upperRightCornerWorld.x;
        screenTop = upperRightCornerWorld.y;
        screenBottom = lowerLeftCornerWorld.y;
    }
    void FixedUpdate() {
        if (!frozenTimer.Running) {
            oxInput = Input.GetAxis("Horizontal");
            if (oxInput != 0) {
                rb_comp.MovePosition(new Vector2(CalculateClampedX(oxInput), rb_comp.position.y));
            }
        }
    }

    float CalculateClampedX(float input) {
        if (rb_comp.position.x + input * paddleMovePS * Time.fixedDeltaTime > screenRight - paddleWidthHalf) {
            return screenRight - paddleWidthHalf;
        } else if (rb_comp.position.x + input * paddleMovePS * Time.fixedDeltaTime < screenLeft + paddleWidthHalf) {
            return screenLeft + paddleWidthHalf;
        } else {
            return rb_comp.position.x + input * paddleMovePS * Time.fixedDeltaTime;
        }
    }

    /// <summary>
    /// Detects collision with a ball to aim the ball
    /// </summary>
    /// <param name="coll">collision info</param>
    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.TryGetComponent(out Ball ball_comp)/* CompareTag("Ball")*/ && IsPaddleTop(coll)) {
            // calculate new ball direction
            float ballOffsetFromPaddleCenter = transform.position.x - coll.transform.position.x;
            float normalizedBallOffset = ballOffsetFromPaddleCenter / paddleWidthHalf;
            float angleOffset = normalizedBallOffset * BounceAngleHalfRange;
            float angle = Mathf.PI / 2 + angleOffset;
            Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            //direction.Normalize();
            ball_comp.SetDirection(direction);
        }
    }
    bool IsPaddleTop(Collision2D coll) {
        float paddleTop = transform.position.y + GetComponent<BoxCollider2D>().size.y / 2.05f;
        float ballBot = coll.transform.position.y - coll.gameObject.GetComponent<CircleCollider2D>().radius;
        if (ballBot < paddleTop) {
            return false;
        }
        return true;
    }
    void FreezePaddle(int freezeDuration) {
        if (!frozenTimer.Running) {
            frozenTimer.Duration = freezeDuration;
            frozenTimer.Run();
        } else {
            frozenTimer.AddDuration(freezeDuration);
        }
    }
    void DestroyPaddle(int noValue) {
        Destroy(gameObject);
    }
}
