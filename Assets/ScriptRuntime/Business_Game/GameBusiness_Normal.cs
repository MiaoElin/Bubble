using System;
using UnityEngine;

public static class GameBusiness_Normal {
    public static void EnterGame(GameContext ctx) {

        // 生成背景场景
        BackSceneDomain.Spawn(ctx);
        // 打开GameStatus
        UIDomain.Panel_GameStatus_Open(ctx);

        // 生成关卡
        var level = LevelDomain.Spawn(ctx, 0);
        ctx.level = level;
        ctx.moveDownTimes = level.verticalCount - GridConst.ScreenMaxVerticalCount;
        int currentTopLine = ctx.moveDownTimes;
        int firstGridIndex = ctx.moveDownTimes * level.horizontalCount;
        ctx.gridCom.Ctor(level.horizontalCount, level.verticalCount, currentTopLine, firstGridIndex);
        var allGrid = ctx.gridCom.allGrid;

        // 设置Grid
        int gridCount = level.gridTypes.Length;
        for (int i = 0; i < gridCount; i++) {
            var typeId = level.gridTypes[i];
            allGrid[i].typeId = typeId;
        }

        // 根据Grid里的typeId生成静态泡泡
        for (int i = gridCount - 1; i >= 60; i--) {
            var grid = allGrid[i];
            if (grid.typeId == 0 || !grid.enable) {
                continue;
            }
            var bubble = BubbleDomain.SpawnStatic(ctx, grid.pos, grid.typeId);
            grid.SetHasBubble(bubble.id, bubble.colorType);
        }

        //播放背景音乐 
        ctx.soundCore.BgmPlay(ctx.backScene.bgm);

        // 生成发射器的泡泡
        ctx.ready_Bubble1 = FakeBubbleDomain.Spawn(ctx, Vector2Const.ReadyBubble1, UnityEngine.Random.Range(1, 5), new Vector3(1, 1, 1));
        ctx.ready_Bubble2 = FakeBubbleDomain.Spawn(ctx, Vector2Const.ReadyBubble2, UnityEngine.Random.Range(1, 5), new Vector3(0.5f, 0.5f, 0.5f));
        ctx.fsmCom.EnteringNormal();
        // ctx.shooting_Bubble = ctx.ready_Bubble;
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
            FixedTick(ctx, restTime);
            restTime = 0;
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

        // 射线检测
        GameNormalDomain.ShootLine(ctx);

        // 发射Bubble
        GameNormalDomain.ShootBubble(ctx);

        var shooting_Bubble = ctx.shooting_Bubble;
        if (shooting_Bubble && !shooting_Bubble.hasSetGridPos) {
            if (!shooting_Bubble.isArrived) {
                // 移动shoot_Bubble
                BubbleDomain.Move(shooting_Bubble);
            } else if (shooting_Bubble.isArrived) {
                // 到达，重置buble位置
                ctx.gridCom.TryGetNearlyGrid(shooting_Bubble.landingPoint, out var grid);
                shooting_Bubble.SetPos(grid.pos);
                shooting_Bubble.SetToStatic();

                // 设置格子为habubble
                ctx.gridCom.SetGridHasBubble(grid, shooting_Bubble.colorType, shooting_Bubble.id);
                // 获取与发射的泡泡相连的泡泡数(只在发射到达后检测一次)
                ctx.gridCom.UpdateCenterCount(grid.index);
            }

        }

        // 消除相连的泡泡
        ctx.gridCom.Foreah(grid => {
            if (!grid.hasBubble || !grid.hasSearchColor) {
                return;
            }

            bool has = ctx.bubbleRepo.Tryget(grid.bubbleId, out var bubble);
            if (has) {
                BubbleDomain.UnSpawn(ctx, bubble);
                grid.Reuse();
            } else {
                Debug.Log(grid.index + " " + grid.bubbleId);
            }

        });

        // 消除需要掉落的泡泡
        ctx.gridCom.UpdateTraction();
        ctx.gridCom.Foreah(grid => {
            if (!grid.hasBubble || !grid.isNeedFalling) {
                return;
            }
            ctx.bubbleRepo.Tryget(grid.bubbleId, out var bubble);
            bubble.fallingPos = grid.pos;
            bubble.FallingEasing(dt);
            if (bubble.fallingTimer <= 0) {
                BubbleDomain.UnSpawn(ctx, bubble);
                grid.Reuse();
            }
        });

        // 所有格子下移一个单位
        // 所有的Bubble也下移一个单位
        if (ctx.isGridMoveDown && ctx.moveDownTimes > 0 && ctx.shooting_Bubble.isArrived) {
            ctx.isGridMoveDown = false;
            ctx.moveDownTimes -= 1;
            ctx.gridCom.currentTopLine -= 1;
            ctx.gridCom.firstGridIndex -= ctx.gridCom.horizontalCount;
            var index = ctx.gridCom.firstGridIndex;
            Debug.Log(index);
            // 新生成一排
            for (int i = index; i < index + ctx.gridCom.horizontalCount; i++) {
                var grid = ctx.gridCom.allGrid[i];
                if (grid.typeId == 0 || !grid.enable) {
                    continue;
                }
                var bubble = BubbleDomain.SpawnStatic(ctx, grid.pos, grid.typeId);
                grid.SetHasBubble(bubble.id, bubble.colorType);
            }
            // 下移格子
            var yOffset = MathF.Sqrt(3) * GridConst.GridRadius;
            ctx.gridCom.Foreah(grid => {
                grid.pos.y -= yOffset;
            });
            int bubblelen = ctx.bubbleRepo.TakeAll(out var bubbles);
            for (int i = 0; i < bubblelen; i++) {
                var bubble = bubbles[i];
                bubble.enterDownEasing = true;
                bubble.downStarPos = bubble.GetPos();
                bubble.downEndPos = bubble.GetPos() + Vector2.down * yOffset;
            }
        }

        //bubble下移缓动
        {
            int bubblelen = ctx.bubbleRepo.TakeAll(out var bubbles);
            for (int i = 0; i < bubblelen; i++) {
                var bubble = bubbles[i];
                if (bubble.enterDownEasing) {
                    bubble.DownEasing(dt);
                }
            }
        }




        Physics2D.Simulate(dt);
    }

    public static void LateTick(GameContext ctx, float dt) {
        FakeBubbleDomain.MoveToByEasing(ctx, dt);
        ctx.gridCom.Foreah(grid => {
            Debug.DrawLine(grid.pos, new Vector3(grid.pos.x, grid.pos.y - 1), Color.red);
        });
    }
}