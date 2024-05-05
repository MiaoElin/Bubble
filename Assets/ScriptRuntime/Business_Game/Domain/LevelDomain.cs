using UnityEngine;

public static class LevelDomain {

    public static LevelEntity Spawn(GameContext ctx, int typeId) {
        return GameFactory.CreateLevel(ctx, typeId);
    }
}