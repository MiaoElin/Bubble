using UnityEngine;
using UnityEngine.AddressableAssets;

public static class AssetCore {

    public static void LoadAll(AssetContext ctx) {
        var entityPtr = Addressables.LoadAssetsAsync<GameObject>("Entities", null);
        var list = entityPtr.WaitForCompletion();
        foreach (var prefab in list) {
            ctx.EntityAdd(prefab.name, prefab);
        }
        ctx.entityPtr = entityPtr;
    }

    public static void UnLoad(AssetContext ctx) {
        if (ctx.entityPtr.IsValid()) {
            Addressables.Release(ctx.entityPtr);
        }
    }
}