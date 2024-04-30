using UnityEngine;

public static class BublleDomain {

    public static BubbleEntity Spawn(GameContext ctx, Vector2 pos, int typeId) {
        BubbleEntity bubble = GameFactory.CreateBubble(ctx, pos, typeId);
        return bubble;
    }

    public static void Move(Vector2 dir, BubbleEntity bubble) {
        bubble.Move(dir);

    }
}