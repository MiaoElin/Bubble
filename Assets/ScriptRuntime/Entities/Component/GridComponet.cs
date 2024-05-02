using UnityEngine;
using System.Collections.Generic;
using System;

public class GridComponet {
    public int horizontalCount;
    public int verticalCount;
    List<GridEntity> allGrid;
    Vector2 gridSize;
    int[] tempArray;

    public GridComponet() {
        allGrid = new List<GridEntity>();
        gridSize = new Vector2(2, 2);
    }

    public void Ctor(int horzontalCount, int verticalCount) {

        this.horizontalCount = horzontalCount;
        this.verticalCount = verticalCount;

        int gridCount = horzontalCount * verticalCount;
        tempArray = new int[gridCount];

        float firGridX = (-(float)horzontalCount / 2) * gridSize.x + gridSize.x / 2;
        float firGridY = ((float)verticalCount / 2) * gridSize.y + gridSize.y / 2;
        Vector2 firgrid1 = new Vector2(firGridX, firGridY);// 布局的中心点的（0,0），如果不是要加上中心点，作为的偏移量
        Vector2 firgrid2 = new Vector2(firGridX + gridSize.x / 2, firGridY);

        // 生成格子
        for (int i = 0; i < gridCount; i++) {
            var grid = new GridEntity();
            int x = GetX(i);
            int y = GetY(i);
            Vector2 pos;
            // 判断y是奇数还是偶数
            if (y % 2 == 1) {
                pos = firgrid2 + new Vector2(gridSize.x * x, -gridSize.y / 2 * Mathf.Sqrt(3) * y);
                if (x == horzontalCount - 1) {
                    grid.Ctor(i, pos);
                    grid.enable = false;
                    // 单数行的最后一个不可用
                    allGrid.Add(grid);
                    continue;
                }
            } else {
                pos = firgrid1 + new Vector2(gridSize.x * x, -gridSize.y / 2 * Mathf.Sqrt(3) * y);
            }

            grid.Ctor(i, pos);
            grid.enable = true;
            allGrid.Add(grid);
        }

    }

    // 上2颗 下两颗 左右各一颗 第一行是0行为双！！！！
    // o x x o 
    //  x x x    中心点在单数的行 上下要获取 x和x+1 
    // o x x o

    // o o o o
    //  x x o        
    // x x x o    中心点在双数的行 上下要获取 x-1和x 
    //  x x o

    public void UpdateCenterGrid(int index) {
        var centerGrid = allGrid[index];
        centerGrid.hasSearch = true;
        centerGrid.centerCount = 1;
        Array.Clear(tempArray, 0, tempArray.Length);
        tempArray[1] = centerGrid.index;
        TryGetArroundCount(index, centerGrid, tempArray);
        Debug.Log("centerCount is: " + centerGrid.centerCount);
        if (centerGrid.centerCount < 3) {
            for (int i = 0; i < tempArray.Length; i++) {
                if (tempArray[i] == default) {
                    continue;
                }
                var id = tempArray[i];
                var grid = allGrid[id];
                grid.hasSearch = false;
            }
        }
    }

    public void TryGetArroundCount(int index, GridEntity centerGrid, int[] tempArray) {
        int line = GetY(index);

        bool isSingular = false;
        // 在单行
        if (line % 2 == 1) {
            isSingular = true;
        }

        for (int j = -1; j <= 1; j++) {
            if (j == 0) {
                for (int i = -1; i <= 1; i++) {
                    if (i == 0) {
                        continue;
                    }
                    TryGetCenterCount(index, i, j, centerGrid, ref tempArray);
                }
            } else {
                if (isSingular) {
                    for (int i = 0; i <= 1; i++) {
                        TryGetCenterCount(index, i, j, centerGrid, ref tempArray);
                    }
                } else {
                    for (int i = -1; i < 1; i++) {
                        TryGetCenterCount(index, i, j, centerGrid, ref tempArray);
                    }
                }
            }
        }
    }

    public void TryGetCenterCount(int index, int xOffset, int yOffset, GridEntity centerGrid, ref int[] tempArray) {
        int x = GetX(index) + xOffset;
        int y = GetY(index) + yOffset;
        if (x < 0 || x >= horizontalCount || y < 0 || y >= verticalCount) {
            return;
        }
        int id = GetIndex(x, y);
        var grid = allGrid[id];
        if (grid.hasSearch) {
            return;
        }
        if (grid.colorType == centerGrid.colorType) {
            grid.hasSearch = true;
            centerGrid.centerCount += 1;
            Debug.Log(grid.index + " " + grid.colorType);
            tempArray[centerGrid.centerCount] = grid.index;
            TryGetArroundCount(grid.index, centerGrid, tempArray);
        }
    }

    public void ReuseGrid(int index, int xOffset, int yOffset, GridEntity centerGrid) {
        int x = GetX(index) + xOffset;
        int y = GetY(index) + yOffset;
        if (x < 0 || x >= horizontalCount || y < 0 || y >= verticalCount) {
            return;
        }
        int id = GetIndex(x, y);
        var grid = allGrid[id];
        if (grid.hasSearch) {
            grid.hasSearch = false;
        }
    }

    public int GetX(int index) {
        return index % horizontalCount;
    }

    public int GetY(int index) {
        return index / horizontalCount;
    }

    public int GetIndex(int x, int y) {
        return y * horizontalCount + x;
    }
    public bool TryGetNearlyGrid(Vector2 pos, out GridEntity nearlygrid) {
        float nearlyDistance = 16;
        nearlygrid = null;
        for (int i = 0; i < allGrid.Count; i++) {
            var grid = allGrid[i];
            if (!grid.enable) {
                continue;
            }
            if (grid.hasBubble) {
                continue;
            }
            float currentDistan = Vector2.SqrMagnitude(pos - grid.pos);
            if (currentDistan <= nearlyDistance) {
                nearlyDistance = currentDistan;
                nearlygrid = grid;
            }
        }
        if (nearlygrid == default) {
            return false;
        } else {
            return true;
        }
    }

    public void Foreah(Action<GridEntity> action) {
        allGrid.ForEach(action);
    }


    public void SetGridHasBubble(GridEntity grid, ColorType colorType, int bubbleId) {
        grid.hasBubble = true;
        grid.colorType = colorType;
        grid.bubbleId = bubbleId;
    }

}