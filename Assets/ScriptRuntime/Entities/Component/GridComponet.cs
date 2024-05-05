using UnityEngine;
using System.Collections.Generic;
using System;

public class GridComponet {
    public int horizontalCount;
    public int verticalCount;
    public GridEntity[] allGrid;
    Vector2 bottom;
    float radius;
    // List<int> searchColorTemp;
    GridEntity[] searchColorTemp;
    List<int> searchTractionTemp;
    // GridEntity[] searchTractionTemp;
    public int currentTopLine;
    public int firstGridIndex;

    public GridComponet() {
        // allGrid = new List<GridEntity>();
        bottom = Vector2Const.GridBottom;
        radius = GridConst.GridRadius;
    }

    public void Ctor(int horzontalCount, int verticalCount, int currentTopLine, int firstGridIndex) {

        this.horizontalCount = horzontalCount;
        this.verticalCount = verticalCount;
        this.currentTopLine = currentTopLine;
        this.firstGridIndex = firstGridIndex;

        int gridCount = horzontalCount * verticalCount;
        allGrid = new GridEntity[gridCount];
        searchColorTemp = new GridEntity[gridCount];
        searchTractionTemp = new List<int>();


        float firGridX = (-(float)horzontalCount / 2) * radius * 2 + radius;
        float firGridY = ((verticalCount - 1) * radius * (Mathf.Sqrt(3))) + radius + bottom.y + 0.5f;
        Vector2 firgrid1 = new Vector2(firGridX, firGridY);// 布局的中心点的（0,0），如果不是要加上中心点，作为的偏移量
        Vector2 firgrid2 = new Vector2(firGridX + radius, firGridY);

        // 生成格子
        for (int i = 0; i < gridCount; i++) {
            var grid = new GridEntity();
            int x = GetX(i);
            int y = GetY(i);
            Vector2 pos;
            // 判断y是奇数还是偶数
            if (y % 2 == 1) {
                pos = firgrid2 + new Vector2(2 * radius * x, -radius * Mathf.Sqrt(3) * y);
                if (x == horzontalCount - 1) {
                    grid.Ctor(i, pos);
                    grid.enable = false;
                    // 单数行的最后一个不可用
                    allGrid[i] = grid;
                    continue;
                }
            } else {
                pos = firgrid1 + new Vector2(2 * radius * x, -radius * Mathf.Sqrt(3) * y);
            }

            if (i < horizontalCount) {
                grid.isNeedFalling = false;
            }
            grid.Ctor(i, pos);
            grid.enable = true;
            allGrid[i] = grid;
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

    #region UpdateCenterGrid
    public void UpdateCenterCount(int index) {
        var centerGrid = allGrid[index];
        centerGrid.hasSearchColor = true;
        centerGrid.centerCount = 1;
        // searchColorTemp.Clear();
        Array.Clear(searchColorTemp, 0, searchColorTemp.Length);
        searchColorTemp[centerGrid.centerCount] = centerGrid;
        TryGetArroundCount(index, centerGrid, searchColorTemp);
        if (centerGrid.centerCount < 3) {
            for (int i = 0; i < searchColorTemp.Length; i++) {
                if (searchColorTemp[i] == null) {
                    continue;
                }
                // bug :CenterCount2个的时候，0处的grid没有被重置
                var temGrid = searchColorTemp[i];
                // Debug.Log(i + ":" + id);
                var grid = allGrid[temGrid.index];
                grid.hasSearchColor = false;
            }
        }
    }

    public void TryGetArroundCount(int index, GridEntity centerGrid, GridEntity[] temp) {
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
                    TryGetCenterCount(index, i, j, centerGrid, temp);
                }
            } else {
                if (isSingular) {
                    for (int i = 0; i <= 1; i++) {
                        TryGetCenterCount(index, i, j, centerGrid, temp);
                    }
                } else {
                    for (int i = -1; i < 1; i++) {
                        TryGetCenterCount(index, i, j, centerGrid, temp);
                    }
                }
            }
        }
    }

    public void TryGetCenterCount(int index, int xOffset, int yOffset, GridEntity centerGrid, GridEntity[] tempArray) {
        int x = GetX(index) + xOffset;
        int y = GetY(index) + yOffset;
        if (x < 0 || x >= horizontalCount || y < 0 || y >= verticalCount) {
            return;
        }
        int id = GetIndex(x, y);
        var grid = allGrid[id];
        if (!grid.hasBubble || grid.hasSearchColor) {
            return;
        }
        if (grid.colorType == centerGrid.colorType) {
            grid.hasSearchColor = true;
            centerGrid.centerCount += 1;
            // Debug.Log(grid.index + " " + grid.colorType);
            tempArray[centerGrid.centerCount] = grid;
            TryGetArroundCount(grid.index, centerGrid, tempArray);
        }
    }

    #endregion

    #region UpdateTraction

    public void UpdateTraction() {
        // 检测每个grid，有bubble的，检测它的周围连接的所有grid。有位于顶部的算是有牵引的，否则移除
        for (int i = 0; i < allGrid.Length; i++) {
            if (i < horizontalCount) {
                continue;
            }
            var grid = allGrid[i];
            if (!grid.hasBubble) {
                continue;
            }
            grid.hasSearchTraction = true;
            searchTractionTemp.Clear();
            searchTractionTemp.Add(grid.index);

            // 先设为true
            grid.isNeedFalling = true;
            GetArroundTraction(i, grid, ref searchTractionTemp);

            // 不需要掉落
            if (!grid.isNeedFalling) {
                // Debug.Log("noneed" + grid.index);
                // 还原搜索
                foreach (var index in searchTractionTemp) {
                    // Debug.Log("out" + grid.index);
                    var gri = allGrid[index];
                    gri.hasSearchTraction = false;
                }
            } else {
                // Debug.Log("IN" + grid.index);
                // foreach (var index in searchTractionTemp) {
                //     var gri = allGrid[index];
                //     gri.isNeedFalling = true;
                // }
            }

        }
    }
    public void GetArroundTraction(int i, GridEntity grid, ref List<int> searchTraction) {
        // 判断单双行
        int line = GetY(i);
        bool isSingular = false;

        if (line % 2 == 1) {
            isSingular = true;
        }
        // 搜索周围有bubble的格子，如果有一个是在顶部，就不用掉落
        for (int j = -1; j <= 1; j++) {
            if (j == 0) {
                for (int k = -1; k <= 1; k++) {
                    if (k == 0) {
                        continue;
                    }
                    int x = GetX(i) + k;
                    int y = GetY(i) + j;
                    if (x < 0 || x >= horizontalCount || y < 0 || y >= verticalCount) {
                        continue;
                    }
                    int index = GetIndex(x, y);
                    var newGrid = allGrid[index];
                    if (!newGrid.hasBubble || newGrid.hasSearchTraction) {
                        continue;
                    }

                    if (y == currentTopLine) {
                        grid.isNeedFalling = false;
                        return;
                    } else {
                        newGrid.hasSearchTraction = true;
                        searchTraction.Add(newGrid.index);
                        GetArroundTraction(newGrid.index, grid, ref searchTraction);
                    }
                }
            } else {
                if (isSingular) {
                    for (int k = 0; k <= 1; k++) {
                        int x = GetX(i) + k;
                        int y = GetY(i) + j;
                        if (x < 0 || x >= horizontalCount || y < 0 || y >= verticalCount) {
                            continue;
                        }
                        int index = GetIndex(x, y);
                        var newGrid = allGrid[index];
                        if (!newGrid.hasBubble || newGrid.hasSearchTraction) {
                            continue;
                        }

                        if (y == currentTopLine) {
                            grid.isNeedFalling = false;
                            return;
                        } else {
                            newGrid.hasSearchTraction = true;
                            searchTraction.Add(newGrid.index);
                            GetArroundTraction(newGrid.index, grid, ref searchTraction);
                        }
                    }
                } else {
                    for (int k = -1; k <= 0; k++) {
                        int x = GetX(i) + k;
                        int y = GetY(i) + j;
                        if (x < 0 || x >= horizontalCount || y < 0 || y >= verticalCount) {
                            continue;
                        }
                        int index = GetIndex(x, y);
                        var newGrid = allGrid[index];

                        if (!newGrid.hasBubble || newGrid.hasSearchTraction) {
                            continue;
                        }

                        // Debug.Log("Index" + index);

                        if (y == currentTopLine) {
                            grid.isNeedFalling = false;
                            return;
                        } else {
                            newGrid.hasSearchTraction = true;
                            searchTraction.Add(newGrid.index);
                            GetArroundTraction(newGrid.index, grid, ref searchTraction);
                        }
                    }
                }
            }
        }
    }

    #endregion

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
        for (int i = 0; i < allGrid.Length; i++) {
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
        for (int i = 0; i < allGrid.Length; i++) {
            var grid = allGrid[i];
            if (grid.enable) {
                action(grid);
            }
        }
    }


    public void SetGridHasBubble(GridEntity grid, ColorType colorType, int bubbleId) {
        grid.SetHasBubble(bubbleId, colorType);
    }

}