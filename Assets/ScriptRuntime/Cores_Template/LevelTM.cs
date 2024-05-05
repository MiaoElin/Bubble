using UnityEngine;

[CreateAssetMenu(menuName = "TM/LevelTM", fileName = "LevelTM_")]
public class levelTM : ScriptableObject {

    public int level;
    public int typeId;
    public int horizontalCount;
    public int verticalCount;
    public int[] gridTypes;
}