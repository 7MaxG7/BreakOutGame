using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ConfigurationUtils {
    static ConfigurationData config;

    #region Properties
    public static float PaddleMoveSpeed {
        get { return config.PaddleMoveSpeed; }
    }
    public static float BallImpulseForce {
        get { return config.BallImpulseForce; }
    }
    public static float BallLifetime {
        get { return config.BallLifetime; }
    }
    public static float SpawnTimeMin {
        get { return config.SpawnTimeMin; }
    }
    public static float SpawnTimeMax {
        get { return config.SpawnTimeMax; }
    }
    public static int ScoreForStandartBlock {
        get { return config.ScoreForStandartBlock; }
    }
    public static int ScoreForBonusBlock {
        get { return config.ScoreForBonusBlock; }
    }
    public static int ScoreForSpecialBlock {
        get { return config.ScoreForSpecialBlock; }
    }
    public static int BlockBonusChance {
        get { return config.BlockBonusChance; }
    }
    public static int BlockFreezeChance {
        get { return config.BlockFreezeChance; }
    }
    public static int BlockSpeedChance {
        get { return config.BlockSpeedChance; }
    }
    public static int NumberBallsToLoose {
        get { return config.NumberBallsToLoose; }
    }
    public static float FreezeTime {
        get { return config.FreezeTime; }
    }
    public static float SpeedTime {
        get { return config.SpeedTime; }
    }
    public static float SpeedFactor {
        get { return config.SpeedFactor; }
    }
    #endregion

    public static void Initialize() {
        config = new ConfigurationData();
    }
}
