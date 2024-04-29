using UnityEngine;

public static class BackSceneDomain {
    public static void Spawn(GameContext ctx) {
        ctx.assetCtx.TryGetEntityPrefab(typeof(BackSceneEntity).Name, out var prefab);
        ctx.backScene = GameObject.Instantiate<GameObject>(prefab).GetComponent<BackSceneEntity>();
        
    }
}