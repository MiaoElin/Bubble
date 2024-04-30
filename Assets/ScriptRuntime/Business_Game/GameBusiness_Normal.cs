using System;
using UnityEngine;

public static class GameBusiness_Normal {
    public static void EnterGame(GameContext ctx) {
        BackSceneDomain.Spawn(ctx);
        ctx.ready_Bubble = BublleDomain.Spawn(ctx, new Vector2(0, -8), 0);
        int typeId = UnityEngine.Random.Range(0, 3);
        ctx.ready_Bubble2 = BublleDomain.Spawn(ctx, new Vector2(-3, -8), typeId);
        ctx.fsmCom.EnteringNormal();
        ctx.shooting_Bubble = ctx.ready_Bubble;
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
        LateTick(ctx, dt);
    }

    public static void PreTick(GameContext ctx, float dt) {
        UserInterfaceController.Tick(ctx);
    }

    public static void FixedTick(GameContext ctx, float dt) {
        ref var shooting_Bubble = ref ctx.shooting_Bubble;
        // 检测发射
        if (ctx.input.isMouseLeftDown) {
            ctx.input.isMouseLeftDown = false;
            shooting_Bubble = ctx.ready_Bubble;
            ctx.ready_Bubble = ctx.ready_Bubble2;
            ctx.ready_Bubble2 = null;
            BublleDomain.Move_ByShoot(shooting_Bubble);

        }
        ctx.ready_Bubble.Move_To(new Vector2(0, -8), dt);
        // if (shooting_Bubble != null && ctx.ready_Bubble.isShooted) {
        //     BublleDomain.Move_ByShoot(shooting_Bubble);
        //     ctx.ready_Bubble = null;
        // }
        if (shooting_Bubble != null && shooting_Bubble.isSideCollision) {
            BublleDomain.Move_ByReflect(shooting_Bubble);
        }


    }

    public static void LateTick(GameContext ctx, float dt) {
        // 重新生成CurrentBubble
        if (ctx.ready_Bubble2 == null && ctx.ready_Bubble.GetPos() == new Vector2(0, -8)) {
            int typeId = UnityEngine.Random.Range(0, 3);
            ctx.ready_Bubble2 = BublleDomain.Spawn(ctx, new Vector2(-3, -8), typeId);
        }
    }
}