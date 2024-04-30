using System;
using UnityEngine;

public static class GameBusiness {
    public static void EnterGame(GameContext ctx) {
        BackSceneDomain.Spawn(ctx);
        ctx.fsmCom.EnteringNormal();
    }

    public static void FixedTick(GameContext ctx, float dt) {
        var status = ctx.fsmCom.status;
        if (status == GameStatus.Normal) {
            NormalTick(ctx, dt);
        }
    }

    private static void NormalTick(GameContext ctx, float dt) {
        var fsmCom = ctx.fsmCom;
        if (fsmCom.isNormalEntering) {
            fsmCom.isNormalEntering = false;

        }
    }
}