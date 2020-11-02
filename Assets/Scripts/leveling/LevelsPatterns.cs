using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsPatterns {
    List<List<List<int>>> levels;
    const int levelsQty = 3;
    const int numberOfBlocksRows = 3;
    const int numberOfBlocksColums = 16;

    public LevelsPatterns() {
        levels = new List<List<List<int>>>(levelsQty);

        List<List<int>> level01 = new List<List<int>> {
            new List<int> { 1, 0, 1, 1, 0, 1, 1, 0, 0, 1, 1, 0, 1, 1, 0, 1 }
            , new List<int> { 1, 1, 0, 1, 1, 0, 1, 1, 0, 0, 1, 1, 0, 1, 1, 0 }
            , new List<int> { 0, 1, 1, 0, 1, 1, 0, 1, 1, 0, 0, 1, 1, 0, 1, 1 }
        };
        levels.Add(level01);

        List<List<int>> level02 = new List<List<int>>(numberOfBlocksRows);
        for (int row = 0; row < level02.Capacity; ++row) {
            level02.Add(new List<int>(numberOfBlocksColums));
            for (int colum = 0; colum < level02[row].Capacity; ++colum) {
                if ((colum + row) % 2 == 0) {
                    level02[row].Add(1);
                } else {
                    level02[row].Add(0);
                }
            }
        }
        levels.Add(level02);

        List<List<int>> level03 = new List<List<int>>(numberOfBlocksRows);
        for (int row = 0; row < level03.Capacity; ++row) {
            level03.Add(new List<int>(numberOfBlocksColums));
            for (int colum = 0; colum < level03[row].Capacity; ++colum) {
                level03[row].Add(1);
            }
        }
        levels.Add(level03);
    }
    public List<List<int>> GetLevelNumber(int lvlNumber) {
        if (lvlNumber <= levelsQty) {
            return levels[lvlNumber - 1];
        } else {
            return null;
        }
    }
}
