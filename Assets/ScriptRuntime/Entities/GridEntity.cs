using UnityEngine;

public class GridEntity {
    public int index;
    public Vector2 pos;
    public bool enable;

    public bool hasBubble;
    public int bubbleId;
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

    public void SetPos(Vector2 pos) {
        this.pos = pos;
    }

    public void Reuse() {
        bubbleId = default;
        hasBubble = false;

        hasSearchColor = false;
        colorType = ColorType.None;

        isNeedFalling = false;
        hasSearchTraction = false;
    }

    public void SetHasBubble(int bubbleId, ColorType colorType) {
        this.hasBubble = true;
        this.bubbleId = bubbleId;
        this.colorType = colorType;
    }
}