using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelsCounter {
    public static int CurrentLevel { get; private set; } = 1;
    public static void SetToNextLevel() {
        CurrentLevel++;
    }
}
