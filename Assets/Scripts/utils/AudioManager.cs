using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioManager {
    static bool initialized = false;
    static AudioSource audioSource;
    static Dictionary<AudioClipEnum, AudioClip> audioClips = new Dictionary<AudioClipEnum, AudioClip>();
    public static bool Initialized {
        get { return initialized; }
    }
    public static void Initialize(AudioSource audioSource_foo) {
        audioSource = audioSource_foo;
        initialized = true;
        audioClips.Add(AudioClipEnum.MenuClick, Resources.Load<AudioClip>(@"Sounds\MenuClick"));
        audioClips.Add(AudioClipEnum.GameStart, Resources.Load<AudioClip>(@"Sounds\GameStart"));
        audioClips.Add(AudioClipEnum.GameOver, Resources.Load<AudioClip>(@"Sounds\GameOver"));
        audioClips.Add(AudioClipEnum.GamePause, Resources.Load<AudioClip>(@"Sounds\GamePause"));
        audioClips.Add(AudioClipEnum.BallSpawn, Resources.Load<AudioClip>(@"Sounds\BallSpawn"));
        audioClips.Add(AudioClipEnum.BallLoose, Resources.Load<AudioClip>(@"Sounds\BallLoose"));
        audioClips.Add(AudioClipEnum.BallHit, Resources.Load<AudioClip>(@"Sounds\BallHit"));
        audioClips.Add(AudioClipEnum.BlockBreak, Resources.Load<AudioClip>(@"Sounds\BlockBreak"));
        audioClips.Add(AudioClipEnum.Congrats, Resources.Load<AudioClip>(@"Sounds\Congrats"));
    }
    public static void Play(AudioClipEnum clipName) {
        audioSource.PlayOneShot(audioClips[clipName]);
    }
}
