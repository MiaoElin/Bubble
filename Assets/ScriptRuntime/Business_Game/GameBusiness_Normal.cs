using System;
using UnityEngine;

public static class GameBusiness_Normal {
    public static void EnterGame(GameContext ctx) {
        BackSceneDomain.Spawn(ctx);
        ctx.ready_Bubble = BubbleDomain.Spawn(ctx, new Vector2(0, -8), 0);
        ctx.fake_Bubble = FakeBubbleDomain.Spawn(ctx);
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
        // 发射Bubble
        GameNormalDomain.ShootBubble(ctx);

        // 遍历修改位置
        int bubbleLen = ctx.bubbleRepo.TakeAll(out var allBubble);
        for (int i = 0; i < bubbleLen; i++) {
            var bubble = allBubble[i];
            if (bubble.hasSetGridPos) {
                continue;
            }
            if (!bubble.isArrived) {
                BubbleDomain.Move(bubble);
                continue;
            }
            bool has = ctx.gridCom.TryGetNearlyGrid(bubble.GetPos(), out var gridPos);
            if (has) {
                Debug.Log(gridPos);
                bubble.SetPos(gridPos);
                bubble.hasSetGridPos = true;
            }
        }
    }

    public static void LateTick(GameContext ctx, float dt) {
        // 生成新的fakeBubble
        if (ctx.fake_Bubble == null) {
            ctx.fake_Bubble = FakeBubbleDomain.Spawn(ctx);
        }
    }
}