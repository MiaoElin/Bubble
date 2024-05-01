using UnityEngine;

public static class GameFactory {

    public static BubbleEntity CreateBubble(GameContext ctx, Vector2 pos, int typeId) {
        bool has = ctx.assetCore.BubbleTM_TryGet(typeId, out var tm);
        if (!has) {
            Debug.Log($"GameFactory.CreateBubble cant find{typeId}");
        }
        ctx.assetCore.TryGetEntityPrefab(typeof(BubbleEntity).Name, out var prefab);
        BubbleEntity bubble = GameObject.Instantiate(prefab).GetComponent<BubbleEntity>();
        bubble.colorType = tm.colorType;
        bubble.typeId = typeId;
        bubble.sr.sprite = tm.spr;
        bubble.id = ctx.iDService.bubbleRecord++;
        bubble.moveSpeed = tm.moveSpeed;
        bubble.SetPos(pos);
        return bubble;
    }

    public static FakeBubbleEntity CreateFakeBubble(GameContext ctx, Vector2 pos, int typeId) {
        bool has = ctx.assetCore.FakeBubbleTM_TryGet(typeId, out var tm);
        if (!has) {
            Debug.Log($"GameFactory.CreateFakeBubble cant find{typeId}");
        }
        ctx.assetCore.TryGetEntityPrefab(typeof(FakeBubbleEntity).Name, out var prefab);
        FakeBubbleEntity bubble = GameObject.Instantiate(prefab).GetComponent<FakeBubbleEntity>();
        bubble.typeId = typeId;
        bubble.sr.sprite = tm.spr;
        bubble.transform.position = pos;
        return bubble;
    }
}