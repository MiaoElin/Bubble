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

    // public static void Move(GameContext ctx){

    // }
}