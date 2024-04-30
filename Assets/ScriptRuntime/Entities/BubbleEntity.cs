using UnityEngine;

public class BubbleEntity : MonoBehaviour {

    public ColorType colorType;
    public SpriteRenderer sr;
    public int id;
    public int typeId;
    public float moveSpeed;
    public Vector2 faceDir;
    [SerializeField] public Rigidbody2D rb;
    public bool isSideCollision;
    public bool isShooted;

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
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "TopCollider") {
            rb.velocity = Vector3.zero;
        }
        if (other.gameObject.tag == "SideCollider") {
            isSideCollision = true;
            Debug.Log("side:" + isSideCollision);
        }
    }

    void OnCollisionStay2D(Collision2D other) {
        // if (other.gameObject.tag == "TopCollider") {
        //     Debug.Log("intop");
        // }
        // if (other.gameObject.tag == "SideCollider") {
        //     isSideCollision = true;
        //     Debug.Log("side:" + isSideCollision);
        // }
    }

    void OnCollisionExit2D(Collision2D other) {
        Debug.Log("exit");
        isSideCollision = false;
    }
}
