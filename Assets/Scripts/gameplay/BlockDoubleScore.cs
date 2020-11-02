using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDoubleScore : Block {
    public override void Initialize() {
        scoreCost = ConfigurationUtils.ScoreForBonusBlock;
        base.Initialize();
    }
}
