using UnityEngine;
using System;

public static class BubbleDomain {

    public static BubbleEntity Spawn(GameContext ctx, Vector2 pos, int typeId) {
        BubbleEntity bubble = GameFactory.CreateBubble(ctx, pos, typeId);
        return bubble;
    }

    public static void Move_ByShoot(BubbleEntity bubble) {
        Vector2 screenPos = Input.mousePosition;
        Vector2 dir = (Vector2)Camera.main.ScreenToWorldPoint(screenPos) - bubble.GetPos();
        bubble.faceDir = dir;
        bubble.Move(dir);
    }

    public static void Move_ByReflect(BubbleEntity bubble) {
        var facedir = bubble.faceDir;
        float angleTan = Mathf.Atan(facedir.x / facedir.y);
        Vector2 up = new Vector2(0, 1);
        Vector2 dir = new Vector2(-MathF.Sin(angleTan), MathF.Cos(angleTan));
        bubble.Move(dir);
    }
}