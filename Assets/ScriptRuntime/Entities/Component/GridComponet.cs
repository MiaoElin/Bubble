using UnityEngine;
using System.Collections.Generic;

public class GridComponet {
    public int horizontalCount;
    public int verticalCount;
    List<GridEntity> allGrid;
    Vector2 gridSize;

    public GridComponet() {
        allGrid = new List<GridEntity>();
        gridSize = new Vector2(2, 2);
    }

    public void Ctor(int horzontalCount, int verticalCount) {

        this.horizontalCount = horzontalCount;
        this.verticalCount = verticalCount;

        int gridCount = horzontalCount * verticalCount;

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

    public int GetX(int index) {
        return index % horizontalCount;
    }

    public int GetY(int index) {
        return index / horizontalCount;
    }

    public bool TryGetNearlyGrid(Vector2 pos, out Vector2 gridPos) {
        float nearlyDistance = 16;
        gridPos = default;
        GridEntity nearlygrid = null;
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
                gridPos = grid.pos;
            }
        }
        if (gridPos == default) {
            return false;
        } else {
            nearlygrid.hasBubble = true;
            return true;
        }
    }


}