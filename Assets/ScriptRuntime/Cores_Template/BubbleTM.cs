using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "TM/BubbleTM", fileName = "BubbleTM_")]
public class BubbleTM : ScriptableObject {
    public int typeId;
    public ColorType colorType;
    public Sprite spr;
    public float moveSpeed;
}