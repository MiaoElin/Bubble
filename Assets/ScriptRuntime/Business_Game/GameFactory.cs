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
        bubble.SetPos(pos);
        return bubble;
    }
}