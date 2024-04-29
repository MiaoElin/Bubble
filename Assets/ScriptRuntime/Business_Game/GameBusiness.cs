using UnityEngine;

public static class GameBusiness {
    public static void EnterGame(GameContext ctx) {
        BackSceneDomain.Spawn(ctx);
    }

}