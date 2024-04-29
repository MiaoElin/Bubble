using UnityEngine;

public class GameFSMComponent {
    public GameStatus status;
    public bool isNormalEntering;

    public bool isLevelEndEntering;


    public void NormalEnter() {
        isNormalEntering = true;
        status = GameStatus.Normal;
    }

    public void LevelEndEnter() {
        isLevelEndEntering = true;
        status = GameStatus.LevelEnd;
    }
}