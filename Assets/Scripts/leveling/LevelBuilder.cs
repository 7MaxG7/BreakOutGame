using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : TEventInvoker<int> {
    GameObject paddlePref;
    Vector3 startBlockPosition = new Vector3(-7.5f, 2.5f);
    LevelsPatterns levels;
    bool pausePrefInstantiated = false;

    void Awake() {
        if (!Breakout.Initialized) {
            Breakout.Initialize();
        }
        levels = new LevelsPatterns();
    }
    void Start() {
        TEventManager<int>.AddListener(EventsEnum.UnpauseEvent, EnablePausePref);
        paddlePref = Resources.Load<GameObject>(@"Prefabs\PaddlePref");
        Instantiate(paddlePref, new Vector3(0.25f, -4.5f, 0), Quaternion.identity);
        PlaceBlocks();
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) && !pausePrefInstantiated) {
            pausePrefInstantiated = true;
            MenuManager.GoToMenu(MenuName.Pause);
        }
    }

    void PlaceBlocks() {
        GameObject block_tmp = ObjectsPool.GetStandartBlockFromPool(Vector3.zero);
        float blockSize = block_tmp.GetComponent<BoxCollider2D>().size.x;
        ObjectsPool.ReturnObjToPool(block_tmp);

        List<List<int>> currentLvl = levels.GetLevelNumber(LevelsCounter.CurrentLevel);
        if (currentLvl != null) {
            Vector3 currentBlockPosition = startBlockPosition;
            int blockChance;
            for (int row = 0; row < currentLvl.Count; ++row) {
                for (int colum = 0; colum < currentLvl[row].Count; ++colum) {
                    if (currentLvl[row][colum] == 1) {
                        blockChance = UnityEngine.Random.Range(0, 100);
                        if (blockChance < ConfigurationUtils.BlockFreezeChance + ConfigurationUtils.BlockSpeedChance) {
                            GameObject newBlock = ObjectsPool.GetSpecialBlockFromPool(currentBlockPosition);
                            BlockSpecial newBlock_specComp = newBlock.GetComponent<BlockSpecial>();
                            if (blockChance < ConfigurationUtils.BlockFreezeChance) {
                                newBlock_specComp.BlockPickupEffect = SpecialEffectEnum.Freezer;
                            } else {
                                newBlock_specComp.BlockPickupEffect = SpecialEffectEnum.Speedup;
                            }
                            newBlock_specComp.Activate();
                        } else if (blockChance < ConfigurationUtils.BlockFreezeChance + ConfigurationUtils.BlockSpeedChance + ConfigurationUtils.BlockBonusChance) {
                            ObjectsPool.GetDoubleScoreBlockFromPool(currentBlockPosition);
                        } else {
                            ObjectsPool.GetStandartBlockFromPool(currentBlockPosition);
                        }
                    }
                    currentBlockPosition.x += blockSize;
                }
                currentBlockPosition.x = startBlockPosition.x;
                currentBlockPosition.y -= blockSize;
            }
        } else {
            MenuManager.GoToMenu(MenuName.MainMenu);
        }
    }
    void EnablePausePref(int noValue) {
        pausePrefInstantiated = false;
    }
}
