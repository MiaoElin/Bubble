using UnityEngine;
using System;

public class FakeBubbleEntity : MonoBehaviour {

    public int typeId;
    public Vector2 faceDir;
    [SerializeField] public SpriteRenderer sr;

    [SerializeField] LineRenderer lineR;
    [SerializeField] LineRenderer lineReflect;
    [NonSerialized] public Vector2 landingPoint;

    public float timer;
    public float maintain;

    public FakeBubbleEntity() {
        faceDir = new Vector2(0, 1);
    }

    public void SetlineR1Enable(bool b) {
        lineR.enabled = b;
    }

    public void SetPos(Vector2 pos) {
        transform.position = pos;
    }

    public Vector2 GetPos() {
        return transform.position;
    }

    public void SetLinePos(Vector2 endPos) {
        lineR.SetPosition(0, GetPos() + faceDir.normalized * 1.5f);
        lineR.SetPosition(1, endPos);
    }

    public void SetlineR2Enable(bool b) {
        lineReflect.enabled = b;
    }

    public void SetLine2Pos(Vector2 start, Vector2 endPos) {
        lineReflect.SetPosition(0, start);
        lineReflect.SetPosition(1, endPos);
    }

    public void MoveToByEasing(Vector2 target, float dt) {
        timer += dt;
        if (timer <= maintain) {
            // transform.position=
        }

    }

}