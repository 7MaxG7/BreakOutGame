using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// A timer
/// </summary>
public class Timer : MonoBehaviour {
	float totalSeconds = 0;
	float elapsedSeconds = 0;
	bool running = false;
	bool started = false;
	TimerFinishedEvent timerFinishedEvent = null;

	public float Duration {
		set {
			if (!running) {
				totalSeconds = value;
			}
		}
	}
	public bool Finished {
		get { return started && !running; }
	}
	public bool Running {
		get { return running; }
	}
	public float TimeLeft {
		get { return totalSeconds - elapsedSeconds; }
	}

	void Update() {
		// update timer and check for finished
		if (running) {
			elapsedSeconds += Time.deltaTime;
			if (elapsedSeconds >= totalSeconds) {
				running = false;
				if (timerFinishedEvent != null) {
					timerFinishedEvent.Invoke();
				}
			}
		}
	}

	public void Run() {
		// only run with valid duration
		if (totalSeconds > 0) {
			started = true;
			running = true;
			elapsedSeconds = 0;
		}
	}
	public void Stop() {
		started = false;
		running = false;
	}
	public float AddDuration(float addSeconds) {
		totalSeconds += addSeconds;
		return totalSeconds;
	}
	public void AddTimerFinishedListener(UnityAction fooListener) {
		timerFinishedEvent = new TimerFinishedEvent();
		timerFinishedEvent.AddListener(fooListener);
	}
}
