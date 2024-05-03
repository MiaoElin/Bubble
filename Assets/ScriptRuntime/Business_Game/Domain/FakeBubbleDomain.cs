using UnityEngine;

public static class FakeBubbleDomain {

    public static FakeBubbleEntity Spawn(GameContext ctx, Vector2 pos, int typeId, Vector3 size) {
        FakeBubbleEntity bubble = GameFactory.CreateFakeBubble(ctx, pos, typeId);
        bubble.transform.localScale = size;
        return bubble;
    }

    public static void UnSpawn(FakeBubbleEntity fakeBubble) {
        GameObject.Destroy(fakeBubble.gameObject);
    }

    public static void MoveToByEasing(GameContext ctx, float dt) {
        if (ctx.ready_Bubble1.isEasing) {
            ctx.ready_Bubble1.MoveToByEasing(Vector2Const.ReadyBubble2, Vector2Const.ReadyBubble1, 0.5f, 1, dt);
            ctx.ready_Bubble2.MoveToByEasing(Vector2Const.ReadyBubble1, Vector2Const.ReadyBubble2, 1, 0.5f, dt);
        }
    }
}