using UnityEngine;

public class LevelEntity {
    public int level;
    public int typeId;
    public int horizontalCount;
    public int verticalCount;
    public int[] gridTypes;

    public LevelEntity() {
        gridTypes = new int[horizontalCount * verticalCount];
    }
}
