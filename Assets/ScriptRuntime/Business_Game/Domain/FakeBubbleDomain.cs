using UnityEngine;

public static class FakeBubbleDomain {

    public static FakeBubbleEntity Spawn(GameContext ctx) {
        int typeId = UnityEngine.Random.Range(0, 3);
        Vector2 pos = new Vector2(-3, -8);
        FakeBubbleEntity bubble = GameFactory.CreateFakeBubble(ctx, pos, typeId);
        return bubble;
    }

    public static void UnSpawn(GameContext ctx, FakeBubbleEntity fakeBubble) {
        ctx.fake_Bubble = null;
        GameObject.Destroy(fakeBubble.gameObject);
    }
}