using UnityEngine;
using System;

public static class BubbleDomain {

    public static BubbleEntity Spawn(GameContext ctx, Vector2 pos, int typeId) {
        BubbleEntity bubble = GameFactory.CreateBubble(ctx, pos, typeId);
        bubble.Reset();
        ctx.bubbleRepo.Add(bubble);
        return bubble;
    }

    public static BubbleEntity SpawnStatic(GameContext ctx, Vector2 pos, int typeId) {
        var bubble = BubbleDomain.Spawn(ctx, pos, typeId);
        bubble.hasSetGridPos = true;
        bubble.isArrived = true;
        bubble.RemoveRigidboddy();
        return bubble;
    }

    public static void UnSpawn(GameContext ctx, BubbleEntity bubble) {
        ctx.bubbleRepo.Remove(bubble);
        GameObject.Destroy(bubble.gameObject);
    }

    public static void Move_ByShoot(BubbleEntity bubble) {
        bubble.Move(bubble.faceDir);
    }

    public static void Move_ByReflect(BubbleEntity bubble) {
        var facedir = bubble.faceDir;
        float angleTan = Mathf.Atan(facedir.x / facedir.y);
        Vector2 up = new Vector2(0, 1);
        Vector2 dir = new Vector2(-MathF.Sin(angleTan), MathF.Cos(angleTan));
        bubble.faceDir = dir; // 为了多次反射用
        bubble.Move(dir);
    }

    public static void Move(BubbleEntity bubble) {
        if (!bubble.isShooted) {
            return;
        }
        // 如果碰到左右边缘，做反射运动
        if (bubble.isSideCollision && bubble.reflectTimes > 0) {
            bubble.isSideCollision = false;// 这样才能一帧检测一次
            Move_ByReflect(bubble);
            bubble.reflectTimes -= 1;
        } else {
            Move_ByShoot(bubble);
        }
    }

}