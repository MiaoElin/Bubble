using System;
using UnityEngine;
using GameFunctions;

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
    public Vector2 landingPoint;

    public bool enterFallingEasing;
    public float fallingTimer;
    public float fallingMoutainDuration;
    public float fallingDuration;
    public Vector2 fallingPos;

    public bool enterDownEasing;
    public float downTimer;
    public float downDuration;
    public Vector2 downStarPos;
    public Vector2 downEndPos;
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
        faceDir = new Vector2(0, 1);

        enterFallingEasing = false;
        fallingTimer = 0;
        fallingMoutainDuration = 0.3f;
        fallingDuration = 0.35f;

        enterDownEasing = false;
        downTimer = 0;
        downDuration = 0.4f;

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

    public void SetToStatic() {
        hasSetGridPos = true;
        isArrived = true;
        RemoveRigidboddy();
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

    public void FallingEasing(float dt) {
        fallingTimer += dt;

        if (fallingTimer <= fallingMoutainDuration) {
            transform.position = GFEasing.Ease2D(GFEasingEnum.MountainInSine, fallingTimer, fallingMoutainDuration, fallingPos, new Vector2(fallingPos.x, fallingPos.y + 3));
        } else if (fallingTimer <= fallingDuration) {
            transform.position = GFEasing.Ease2D(GFEasingEnum.Linear, fallingTimer, fallingDuration, fallingPos, new Vector2(fallingPos.x, -9));
        } else if (fallingTimer > fallingDuration) {
            fallingTimer = 0;
            enterFallingEasing = false;
        }
    }

    public void DownEasing(float dt) {
        downTimer += dt;
        if (downTimer <= downDuration) {
            transform.position = GFEasing.Ease2D(GFEasingEnum.Linear, downTimer, downDuration, downStarPos, downEndPos);
        } else {
            downTimer = 0;
            enterDownEasing = false;
        }
    }
}
