using System;
using UnityEngine;

public class BubbleEntity : MonoBehaviour {

    public ColorType colorType;
    public SpriteRenderer sr;
    public int id;
    // public int index;
    public int typeId;
    public float moveSpeed;
    public Vector2 faceDir;
    [SerializeField] Rigidbody2D rb;
    public bool isSideCollision;
    public bool isShooted;
    public bool isInGrid;
    public bool isArrived;
    public bool hasSetGridPos;
    [NonSerialized] public int reflectTimes;
    [SerializeField] LineRenderer lineR;
    [SerializeField] LineRenderer lineRReflet;
    public Vector2 landingPoint;

    public BubbleEntity() {
        // isSideCollision = false;
        // isShooted = false;
        // isInGrid = false;
        // isArrived = false;
        // hasSetGridPos = false;
        // // reflectTimes = 2;
    }

    public void Reset() {
        isSideCollision = false;
        isShooted = false;
        isInGrid = false;
        isArrived = false;
        hasSetGridPos = false;
        reflectTimes = 2;
        SetlineREnable(false);
        SetlineR2Enable(false);
        faceDir = new Vector2(0, 1);
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
            isArrived = true;
        }
        if (other.gameObject.tag == "SideCollider") {
            isSideCollision = true;
        }
        if (other.gameObject.tag == "BubbleEntity") {
            isArrived = true;
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
        // rb.bodyType = RigidbodyType2D.Static;
        DestroyImmediate(rb);
    }

    // === Line Render ===
    public void SetlineREnable(bool b) {
        lineR.enabled = b;
    }

    public void SetLinePos(Vector2 endPos) {
        lineR.SetPosition(0, GetPos() + faceDir.normalized * 1.5f);
        lineR.SetPosition(1, endPos);
    }

    public void SetlineR2Enable(bool b) {
        lineRReflet.enabled = b;
    }

    public void SetLine2Pos(Vector2 start, Vector2 endPos) {
        lineRReflet.SetPosition(0, start);
        lineRReflet.SetPosition(1, endPos);
    }
}
