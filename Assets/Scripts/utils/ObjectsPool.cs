using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ObjectsPool {
    static Dictionary<BlocksEnum, List<GameObject>> blocks = new Dictionary<BlocksEnum, List<GameObject>>(Enum.GetValues(typeof(BlocksEnum)).Length);
    static List<GameObject> balls = new List<GameObject>(8);
    static List<GameObject> objInUse = new List<GameObject>(200);
    static GameObject standartBlockPref, doubleScoreBlockPref, specialBlockPref, ballPref;
    static int blocksInUse = 0;

    public static bool NoBlocksInUse() {
        bool gameOver = !Convert.ToBoolean(blocksInUse);
        if (gameOver) {
            return true;
        }
        return false;
    }
    public static void Initialize() {
        standartBlockPref = Resources.Load<GameObject>(@"Prefabs\BlockStandartPref");
        doubleScoreBlockPref = Resources.Load<GameObject>(@"Prefabs\BlockDoubleScorePref");
        specialBlockPref = Resources.Load<GameObject>(@"Prefabs\BlockSpecialPref");
        ballPref = Resources.Load<GameObject>(@"Prefabs\BallPref");
        foreach (BlocksEnum blockType in Enum.GetValues(typeof(BlocksEnum))) {
            if (!blocks.ContainsKey(blockType) && blockType == BlocksEnum.Standart) {
                blocks.Add(blockType, new List<GameObject>(100));
            } else if (!blocks.ContainsKey(blockType)) {
                blocks.Add(blockType, new List<GameObject>(20));
            }
        }
        FillPool(blocks[BlocksEnum.Standart], standartBlockPref);
        FillPool(blocks[BlocksEnum.DoubleScore], doubleScoreBlockPref);
        FillPool(blocks[BlocksEnum.Special], specialBlockPref);
        FillPool(balls, ballPref);
    }

    static void FillPool(List<GameObject> pool, GameObject pref) {
        for (int i = pool.Count; i < pool.Capacity; ++i) {
            pool.Add(CreateNewObj(pref));
        }
    }
    static GameObject CreateNewObj(GameObject pref) {
        GameObject newObj = GameObject.Instantiate(pref);
        if (newObj.TryGetComponent(out Block block_comp)) {
            block_comp.Initialize();
        } else if (newObj.TryGetComponent(out Ball ball_comp)) {
            ball_comp.Initialize();
        }
        newObj.SetActive(false);
        GameObject.DontDestroyOnLoad(newObj);
        return newObj;
    }
    public static GameObject GetBallFromPool(Vector2 position) {
        return GetObjectFromPool(balls, ballPref, position);
    }
    public static GameObject GetStandartBlockFromPool(Vector2 position) {
        blocksInUse++;
        return GetObjectFromPool(blocks[BlocksEnum.Standart], standartBlockPref, position);
    }
    public static GameObject GetDoubleScoreBlockFromPool(Vector2 position) {
        blocksInUse++;
        return GetObjectFromPool(blocks[BlocksEnum.DoubleScore], doubleScoreBlockPref, position);
    }
    public static GameObject GetSpecialBlockFromPool(Vector2 position) {
        blocksInUse++;
        return GetObjectFromPool(blocks[BlocksEnum.Special], specialBlockPref, position);
    }
    static GameObject GetObjectFromPool(List<GameObject> pool, GameObject pref, Vector2 position) {
        GameObject obj;
        if (pool.Count > 0) {
            obj = pool[pool.Count - 1];
            pool.RemoveAt(pool.Count - 1);
        } else {
            pool.Capacity++;
            obj = CreateNewObj(pref);
        }
        obj.SetActive(true);
        obj.transform.position = position;
        objInUse.Add(obj);
        return obj;
    }
    public static void ReturnObjToPool(GameObject obj) {
        bool repeatedReturn = !objInUse.Remove(obj);
        if (!repeatedReturn) {
            if (obj.GetComponent<Ball>() != null) {
                obj.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                balls.Add(obj);
            } else if (obj.GetComponent<Block>() != null) {
                blocksInUse--;
                if (obj.GetComponent<BlockStandart>() != null) {
                    blocks[BlocksEnum.Standart].Add(obj);
                } else if (obj.GetComponent<BlockDoubleScore>() != null) {
                    blocks[BlocksEnum.DoubleScore].Add(obj);
                } else if (obj.GetComponent<BlockSpecial>() != null) {
                    blocks[BlocksEnum.Special].Add(obj);
                }
            }
            if (obj.activeSelf) {
                obj.SetActive(false);
            }
        }
    }
    public static void ReturnAllObjToPool() {
        for (int i = objInUse.Count - 1; i >= 0; --i) {
            ReturnObjToPool(objInUse[i]);
        }
    }
}
