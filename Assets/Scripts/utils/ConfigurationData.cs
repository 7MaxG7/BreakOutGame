using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Globalization;

public class ConfigurationData {
    Dictionary<ConfigurationEnum, float> configValues = new Dictionary<ConfigurationEnum, float>();
    const string ConfigurationDataFileName = "Config.csv";

    #region Properties
    /// <summary>
    /// Paddle move speed in units per seconds
    /// </summary>
    public float PaddleMoveSpeed {
        get { return configValues[ConfigurationEnum.PaddleMoveSpeed]; }
    }
    public float BallImpulseForce {
        get { return configValues[ConfigurationEnum.BallImpulseForce]; }
    }
    public float BallLifetime {
        get { return configValues[ConfigurationEnum.BallLifetime]; }
    }
    public float SpawnTimeMin {
        get { return configValues[ConfigurationEnum.SpawnTimeMin]; }
    }
    public float SpawnTimeMax {
        get { return configValues[ConfigurationEnum.SpawnTimeMax]; }
    }
    public int ScoreForStandartBlock {
        get { return (int)configValues[ConfigurationEnum.ScoreForStandartBlock]; }
    }
    public int ScoreForBonusBlock {
        get { return (int)configValues[ConfigurationEnum.ScoreForBonusBlock]; }
    }
    public int ScoreForSpecialBlock {
        get { return (int)configValues[ConfigurationEnum.ScoreForSpecialBlock]; }
    }
    public int BlockBonusChance {
        get { return (int)configValues[ConfigurationEnum.BlockBonusChance]; }
    }
    public int BlockFreezeChance {
        get { return (int)configValues[ConfigurationEnum.BlockFreezeChance]; }
    }
    public int BlockSpeedChance {
        get { return (int)configValues[ConfigurationEnum.BlockSpeedChance]; }
    }
    public int NumberBallsToLoose {
        get { return (int)configValues[ConfigurationEnum.NumberBallsToLoose]; }
    }
    public float FreezeTime {
        get { return configValues[ConfigurationEnum.FreezeTime]; }
    }
    public float SpeedTime {
        get { return configValues[ConfigurationEnum.SpeedTime]; }
    }
    public float SpeedFactor {
        get { return configValues[ConfigurationEnum.SpeedFactor]; }
    }
    #endregion

    public ConfigurationData() {
        StreamReader input = null;
        try {
            input = File.OpenText(Path.Combine(Application.streamingAssetsPath, ConfigurationDataFileName));
            string currentStr = input.ReadLine();
            while (currentStr != null) {
                string[] tokens = currentStr.Split(',');
                ConfigurationEnum dataName = (ConfigurationEnum)Enum.Parse(typeof(ConfigurationEnum), tokens[0]);
                configValues.Add(dataName, float.Parse(tokens[1], NumberFormatInfo.InvariantInfo));
                currentStr = input.ReadLine();
            }
        } catch (Exception) {
            SetDefaultValues();
        } finally {
            if (input != null) {
                input.Close();
            }
        }
    }
    void SetDefaultValues() {
        configValues.Clear();
        configValues.Add(ConfigurationEnum.BallImpulseForce, 10);
        configValues.Add(ConfigurationEnum.PaddleMoveSpeed, 10);
        configValues.Add(ConfigurationEnum.BallLifetime, 10);
        configValues.Add(ConfigurationEnum.SpawnTimeMin, 12);
        configValues.Add(ConfigurationEnum.SpawnTimeMax, 18);
        configValues.Add(ConfigurationEnum.ScoreForStandartBlock, 10);
        configValues.Add(ConfigurationEnum.ScoreForBonusBlock, 30);
        configValues.Add(ConfigurationEnum.ScoreForSpecialBlock, 15);
        configValues.Add(ConfigurationEnum.BlockBonusChance, 20);
        configValues.Add(ConfigurationEnum.BlockFreezeChance, 10);
        configValues.Add(ConfigurationEnum.BlockSpeedChance, 10);
        configValues.Add(ConfigurationEnum.NumberBallsToLoose, 8);
        configValues.Add(ConfigurationEnum.FreezeTime, 2);
        configValues.Add(ConfigurationEnum.SpeedTime, 2);
        configValues.Add(ConfigurationEnum.SpeedFactor, 2);
    }
}
