using System;
using UnityEngine;

public class BubbleEntity : MonoBehaviour {

    public ColorType colorType;
    public SpriteRenderer sr;
    public int id;
    public int typeId;
    public float moveSpeed;
    public Vector2 faceDir;
    [SerializeField] Rigidbody2D rb;
    public bool isSideCollision;
    public bool isShooted;
    public bool isInGrid;

    public BubbleEntity() {
        isSideCollision = false;
        isShooted = false;
        isInGrid = false;
    }

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
        isSideCollision = false;
        if (other.gameObject.tag == "BottomCollider") {
            isInGrid = true;
        }
    }

    internal void Move_To(Vector2 target, float dt) {
        Vector2 dir = target - GetPos();
        Debug.Log(transform.position);
        if (dir.sqrMagnitude <= moveSpeed * dt) {
            Debug.Log("arrive");
            rb.velocity = Vector3.zero;
            return;
        }
        var velocity = rb.velocity;
        velocity = dir.normalized * moveSpeed;
        rb.velocity = velocity;
    }

    public void RemoveRigidboddy() {
        Destroy(rb.gameObject);
    }
}
