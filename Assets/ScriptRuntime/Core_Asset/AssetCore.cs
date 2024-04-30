using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AssetCore {
    public Dictionary<string, GameObject> allEntities;
    public AsyncOperationHandle entityPtr;

    public AssetCore() {
        allEntities = new Dictionary<string, GameObject>();
    }
    public void LoadAll() {
        var entityPtr = Addressables.LoadAssetsAsync<GameObject>("Entities", null);
        var list = entityPtr.WaitForCompletion();
        foreach (var prefab in list) {
            EntityAdd(prefab.name, prefab);
        }
        this.entityPtr = entityPtr;
    }
    public void UnLoad() {
        if (entityPtr.IsValid()) {
            Addressables.Release(entityPtr);
        }
    }

    public void EntityAdd(string name, GameObject value) {
        allEntities.Add(name, value);
    }

    public bool TryGetEntityPrefab(string name, out GameObject prefab) {
        return allEntities.TryGetValue(name, out prefab);
    }
}