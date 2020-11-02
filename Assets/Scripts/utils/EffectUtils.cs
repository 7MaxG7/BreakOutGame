using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EffectUtils {
    static SpeedupEffectMonitor speedEffectMon = Camera.main.GetComponent<SpeedupEffectMonitor>();
    public static bool SpeedEffectActive {
        get { return speedEffectMon.Active; }
    }
    public static float SpeedTimeLeft {
        get { return speedEffectMon.TimeLeft; }
    }
    public static float Factor {
        get { return speedEffectMon.Factor; }
    }
}
