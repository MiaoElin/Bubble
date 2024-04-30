using System;
using UnityEngine;

public static class GameBusiness_Normal {
    public static void EnterGame(GameContext ctx) {
        BackSceneDomain.Spawn(ctx);
        ctx.currenBubble = BublleDomain.Spawn(ctx, new Vector2(0, -8), 0);
        ctx.fsmCom.EnteringNormal();
    }

    public static void Tick(GameContext ctx, float dt) {
        var fsmCom = ctx.fsmCom;
        if (fsmCom.isNormalEntering) {
            fsmCom.isNormalEntering = false;
        }
        // PreTick
        PreTick(ctx, dt);

        // FixedTick 物理相关
        ref var restTime = ref ctx.restTime;
        const float IntervalTime = 0.01f;
        restTime += dt;
        if (restTime < IntervalTime) {
            restTime = 0;
            FixedTick(ctx, dt);
        } else {
            while (restTime >= IntervalTime) {
                restTime -= IntervalTime;
                FixedTick(ctx, IntervalTime);
            }
        }

        // LateTick
        // 跟表现相关的逻辑，比如扫雷grid信息更新，赋值给Panel的element的部分

    }

    public static void PreTick(GameContext ctx, float dt) {
        UserInterfaceController.Tick(ctx);
    }

    public static void FixedTick(GameContext ctx, float dt) {
        var currenBubble = ctx.currenBubble;
        if (ctx.input.isMouseLeftDown) {
            ctx.input.isMouseLeftDown = false;
            Vector2 screenPos = Input.mousePosition;
            Vector2 dir = (Vector2)Camera.main.ScreenToWorldPoint(screenPos) - currenBubble.GetPos();
            BublleDomain.Move(dir, currenBubble);
        }
    }

    public static void LateTick(GameContext ctx, float dt) {

    }
}