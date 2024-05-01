using UnityEngine;

public class GridEntity {
    public int index;
    public Vector2 pos;
    public bool hasBubble;
    public bool enable;
    public void Ctor(int index, Vector2 pos) {
        hasBubble = false;
        this.index = index;
        this.pos = pos;
    }
}