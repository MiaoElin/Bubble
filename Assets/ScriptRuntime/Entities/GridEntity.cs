using UnityEngine;

public class GridEntity {
    public int index;
    public int bubbleId;
    public Vector2 pos;
    public bool hasBubble;
    public bool enable;
    public ColorType colorType;
    public int centerCount;
    public bool hasSearch;
    public void Ctor(int index, Vector2 pos) {
        hasBubble = false;
        this.index = index;
        this.pos = pos;
    }

    public void Reset() {
        bubbleId = default;
        hasBubble = false;
        hasSearch = false;
    }
}