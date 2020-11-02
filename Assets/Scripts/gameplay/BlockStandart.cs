using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockStandart : Block {
    [SerializeField] Sprite spriteStandBlock_1, spriteStandBlock_2;
    public override void Initialize() {
        scoreCost = ConfigurationUtils.ScoreForStandartBlock;
        int spriteChooser = Random.Range(0, 2);
        switch (spriteChooser) {
            case 0:
                GetComponent<SpriteRenderer>().sprite = spriteStandBlock_1;
                break;
            case 1:
                GetComponent<SpriteRenderer>().sprite = spriteStandBlock_2;
                break;
        }
        base.Initialize();
    }
}
