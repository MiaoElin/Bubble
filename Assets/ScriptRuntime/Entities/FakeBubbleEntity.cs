using UnityEngine;
using System;
using GameFunctions;

public class FakeBubbleEntity : MonoBehaviour {

    public int typeId;
    public Vector2 faceDir;
    [SerializeField] public SpriteRenderer sr;

    [SerializeField] LineRenderer lineR;
    [SerializeField] LineRenderer lineReflect;
    [NonSerialized] public Vector2 landingPoint;

    public bool isEasing;
    public float timer;
    public float maintain;

    public FakeBubbleEntity() {
        faceDir = new Vector2(0, 1);
        isEasing = false;
        timer = 0;
        maintain = 0.1f;
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

    public void MoveToByEasing(Vector2 startPos, Vector2 endPos, float startScale, float endScale, float dt) {
        timer += dt;
        if (timer <= maintain) {
            transform.position = GFEasing.Ease2D(GFEasingEnum.Linear, timer, maintain, startPos, endPos);
            var scale = GFEasing.Ease1D(GFEasingEnum.Linear, timer, maintain, startScale, endScale);
            transform.localScale = new Vector3(scale, scale, scale);
        } else {
            timer = 0;
            isEasing = false;
        }

    }

}