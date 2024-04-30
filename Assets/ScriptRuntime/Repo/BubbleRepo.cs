using System.Collections.Generic;
using UnityEngine;

public class BubbleRepo {
    Dictionary<int, BubbleEntity> allBubble;

    public BubbleRepo() {
        allBubble = new Dictionary<int, BubbleEntity>();
    }
}