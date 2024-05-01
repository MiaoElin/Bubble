using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AssetCore {
    public Dictionary<string, GameObject> allEntities;
    public AsyncOperationHandle entityPtr;

    public Dictionary<int, BubbleTM> bubbleTMs;
    public AsyncOperationHandle bubblePtr;

    public Dictionary<int, FakeBubbleTM> fakeBubbleTMs;
    public AsyncOperationHandle fakeBubblePtr;

    public AssetCore() {
        allEntities = new Dictionary<string, GameObject>();
        bubbleTMs = new Dictionary<int, BubbleTM>();
        fakeBubbleTMs = new Dictionary<int, FakeBubbleTM>();
    }
    public void LoadAll() {
        {
            var ptr = Addressables.LoadAssetsAsync<GameObject>("Entities", null);
            var list = ptr.WaitForCompletion();
            foreach (var prefab in list) {
                EntityAdd(prefab.name, prefab);
            }
            this.entityPtr = ptr;
        }

        {
            var ptr = Addressables.LoadAssetsAsync<BubbleTM>("BubbleTM", null);
            var list = ptr.WaitForCompletion();
            foreach (var tm in list) {
                bubbleTMs.Add(tm.typeId, tm);
            }
            bubblePtr = ptr;
        }

        {
            var ptr = Addressables.LoadAssetsAsync<FakeBubbleTM>("FakeBubbleTM", null);
            var list = ptr.WaitForCompletion();
            foreach (var tm in list) {
                fakeBubbleTMs.Add(tm.typeId, tm);
            }
            fakeBubblePtr = ptr;
        }
    }
    public void UnLoad() {
        if (entityPtr.IsValid()) {
            Addressables.Release(entityPtr);
        }
        if (bubblePtr.IsValid()) {
            Addressables.Release(bubblePtr);
        }
        if (fakeBubblePtr.IsValid()) {
            Addressables.Release(fakeBubblePtr);
        }

    }

    public void EntityAdd(string name, GameObject value) {
        allEntities.Add(name, value);
    }

    public bool TryGetEntityPrefab(string name, out GameObject prefab) {
        return allEntities.TryGetValue(name, out prefab);
    }

    public bool BubbleTM_TryGet(int typeId, out BubbleTM tm) {
        return bubbleTMs.TryGetValue(typeId, out tm);
    }

    public bool FakeBubbleTM_TryGet(int typeId, out FakeBubbleTM tm) {
        return fakeBubbleTMs.TryGetValue(typeId, out tm);
    }
}