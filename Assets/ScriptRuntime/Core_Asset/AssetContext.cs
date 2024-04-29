using UnityEngine;
using System.Collections.Generic;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AssetContext {

    Dictionary<string, GameObject> allEntities;
    public AsyncOperationHandle entityPtr;

    public AssetContext() {
        allEntities = new Dictionary<string, GameObject>();
    }

    public void EntityAdd(string name, GameObject value) {
        allEntities.Add(name, value);
    }

    public bool TryGetEntityPrefab(string name, out GameObject prefab) {
        return allEntities.TryGetValue(name, out prefab);
    }
}