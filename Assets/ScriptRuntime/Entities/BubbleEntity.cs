using UnityEngine;

public class BubbleEntity : MonoBehaviour {

    public ColorType colorType;
    public SpriteRenderer sr;
    public int id;
    public int typeId;

    public void SetPos(Vector2 pos) {
        gameObject.transform.position = pos;
    }
}
