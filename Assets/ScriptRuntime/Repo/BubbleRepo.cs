using System.Collections.Generic;
using UnityEngine;

public class BubbleRepo {
    Dictionary<int, BubbleEntity> all;
    BubbleEntity[] temp;
    public BubbleRepo() {
        all = new Dictionary<int, BubbleEntity>();
        temp = new BubbleEntity[300];
    }

    public void Add(BubbleEntity bubble) {
        all.Add(bubble.id, bubble);
    }

    public void Remove(BubbleEntity bubble) {
        all.Remove(bubble.id);
    }

    public void Remove(int bubbleId) {
        all.Remove(bubbleId);
    }

    public bool Tryget(int id, out BubbleEntity bubble) {
        return all.TryGetValue(id, out bubble);
    }

    public int TakeAll(out BubbleEntity[] allBubble) {
        if (all.Count >= temp.Length) {
            temp = new BubbleEntity[all.Count * 2];
        }
        all.Values.CopyTo(temp, 0);
        allBubble = temp;
        return all.Count;
    }
}