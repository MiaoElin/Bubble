using UnityEngine;

public class BubbleEntity : MonoBehaviour {

    public ColorType colorType;
    public SpriteRenderer sr;
    public int id;
    public int typeId;
    public float moveSpeed;
    public Vector2 faceDir;
    [SerializeField] Rigidbody2D rb;

    public void SetPos(Vector2 pos) {
        transform.position = pos;
    }

    public Vector2 GetPos() {
        return transform.position;
    }
    public void Move(Vector2 dir) {
        var velocity = rb.velocity;
        velocity = dir.normalized * moveSpeed;
        rb.velocity = velocity;
        Debug.Log(velocity);
    }
}
