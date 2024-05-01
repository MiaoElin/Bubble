using UnityEngine;
using System.Collections.Generic;

public class GridComponet {
    public int horzontalCount;
    public int verticalCount;
    List<GridEntity> allGrid;
    Vector2 gridSize;

    public GridComponet() {
        allGrid = new List<GridEntity>();
        gridSize = new Vector2(2, 2);
    }

    public void Ctor(int horzontalCount, int verticalCount) {

        this.horzontalCount = horzontalCount;
        this.verticalCount = verticalCount;

        int gridCount = horzontalCount * verticalCount;

        float firGridX = (-(float)horzontalCount / 2) * gridSize.x + gridSize.x / 2;
        float firGridY = (-(float)verticalCount / 2) * gridSize.y + gridSize.y / 2;
        Vector2 firgrid = new Vector2(firGridX, firGridY); // 布局的中心点的（0,0），如果不是要加上中心点，作为的偏移量
        
        // 生成格子
        for (int i = 0; i < gridCount; i++) {
            var grid = new GridEntity();
            int x = GetX(i);
            int y = GetY(i);
            Vector2 pos = firgrid + new Vector2(gridSize.x * x, gridSize.y * y);
            grid.Ctor(i, pos);
            allGrid.Add(grid);
        }

    }

    public int GetX(int index) {
        return index % horzontalCount;
    }

    public int GetY(int index) {
        return index / horzontalCount;
    }

    public bool TryGetNearlyGrid(Vector2 pos, out Vector2 gridPos) {
        float nearlyDistance = (gridSize.x / 2) * (gridSize.x / 2);
        gridPos = default;
        GridEntity nearlygrid = null;
        for (int i = 0; i < allGrid.Count; i++) {
            var grid = allGrid[i];
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