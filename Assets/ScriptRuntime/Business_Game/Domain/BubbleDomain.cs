using UnityEngine;

public static class BublleDomain {

    public static BubbleEntity Spawn(GameContext ctx, Vector2 pos, int typeId) {
        BubbleEntity bubble = GameFactory.CreateBubble(ctx, pos, typeId);
        return bubble;
    }
}