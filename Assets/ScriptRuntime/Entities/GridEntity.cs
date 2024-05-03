using UnityEngine;

public class GridEntity {
    public int index;
    public int bubbleId;
    public Vector2 pos;
    public bool hasBubble;
    public bool enable;
    public ColorType colorType;
    public int centerCount;
    public bool hasSearchColor;

    public bool isNeedFalling;
    public bool hasSearchTraction; // 牵引

    public void Ctor(int index, Vector2 pos) {
        hasBubble = false;
        this.index = index;
        this.pos = pos;
        hasSearchTraction = false;
        isNeedFalling = false;
    }

    public void Reset() {
        bubbleId = default;
        hasBubble = false;
        
        hasSearchColor = false;
        colorType = ColorType.None;

        isNeedFalling = false;
        hasSearchTraction = false;
    }
}