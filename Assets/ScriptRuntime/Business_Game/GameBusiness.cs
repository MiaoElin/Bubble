using System;
using UnityEngine;

public static class GameBusiness {
    public static void EnterGame(GameContext ctx) {
        BackSceneDomain.Spawn(ctx);
        ctx.currenBubble = BublleDomain.Spawn(ctx, new Vector2(0, -8), 0);
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
        if (ctx.input.isMouseLeftDown) {
            ctx.input.isMouseLeftDown = false;
            Vector2 mouseScreenPos = Input.mousePosition;
            Vector2 dir = (Vector2)Camera.main.ScreenToWorldPoint(mouseScreenPos) - ctx.currenBubble.GetPos();
            ctx.currenBubble.Move(dir);
        }
    }
}