using UnityEngine;
using System;

public static class GameNormalDomain {

    public static void ShootBubble(GameContext ctx) {
        ref var shooting_Bubble = ref ctx.shooting_Bubble;
        var ready_Bubble1 = ctx.ready_Bubble1;
        var ready_Bubble2 = ctx.ready_Bubble2;

        // 检测发射
        if (ctx.input.isMouseLeftDown && ready_Bubble1 && ctx.input.isMouseInGrid) {
            if (!shooting_Bubble || shooting_Bubble.isArrived) {
                ctx.shootCount -= 1;
                if (ctx.shootCount == 0) {
                    ctx.shootCount = ctx.maxShootCount;
                    // 所有的格子要下移动，泡泡也下移
                    ctx.isGridMoveDown = true;
                }
                // shoot sfx
                ctx.soundCore.BubbleShootPlay(ctx.backScene.bubbleBreak);

                // 根据备用1生成发射的泡泡
                shooting_Bubble = BubbleDomain.Spawn(ctx, Vector2Const.ReadyBubble1, ready_Bubble1.typeId);
                shooting_Bubble.landingPoint = ready_Bubble1.landingPoint;
                // 销毁备用1
                FakeBubbleDomain.UnSpawn(ready_Bubble1);
                // 根据备用2生成新的备用1
                ctx.ready_Bubble1 = FakeBubbleDomain.Spawn(ctx, Vector2Const.ReadyBubble1, ready_Bubble2.typeId, new Vector3(1, 1, 1));
                // 销毁备用2
                FakeBubbleDomain.UnSpawn(ready_Bubble2);
                // 生成随机的备用2
                ctx.ready_Bubble2 = FakeBubbleDomain.Spawn(ctx, Vector2Const.ReadyBubble2, UnityEngine.Random.Range(1, 5), new Vector3(0.5f, 0.5f, 0.5f));

                // 发射泡泡
                shooting_Bubble.isShooted = true;
                Vector2 screenPos = Input.mousePosition;
                shooting_Bubble.faceDir = (Vector2)Camera.main.ScreenToWorldPoint(screenPos) - shooting_Bubble.GetPos();
            }

        }

        // // 发射的Bubble到了格子的区域（离开发射区）
        // if (shooting_Bubble && shooting_Bubble.isInGrid == true) {
        //     shooting_Bubble.isInGrid = false;
        //     // 根据FakeBubble生成新的ReadyBubble
        //     ready_Bubble = BubbleDomain.Spawn(ctx, new Vector2(0, -8), ctx.fake_Bubble.typeId);
        //     // 销毁fakeBubble
        //     FakeBubbleDomain.UnSpawn(ctx, ctx.fake_Bubble);
        // }
        // //有发射的Bubble，且发射的buble撞到了边缘，开始做反射移动
        // if (shooting_Bubble != null && shooting_Bubble.isSideCollision) {
        //     if (shooting_Bubble.reflectTimes > 0) {
        //         BubbleDomain.Move_ByReflect(shooting_Bubble);

        //     }
        // }
    }

    public static void ShootLine(GameContext ctx) {
        ref var ready_Bubble = ref ctx.ready_Bubble1;
        // 发射线
        if (ctx.input.isMouseInGrid) {
            Vector2 mouseScreenPos = Input.mousePosition;
            ready_Bubble.faceDir = (Vector2)Camera.main.ScreenToWorldPoint(mouseScreenPos) - ready_Bubble.GetPos();
            ready_Bubble.SetlineR1Enable(true);
            RaycastHit2D[] hits = Physics2D.RaycastAll(ready_Bubble.GetPos() + ready_Bubble.faceDir.normalized * 2, ready_Bubble.faceDir, 100f);
            if (hits != null) {
                for (int i = 0; i < hits.Length; i++) {
                    var hit = hits[i];
                    if (hit.collider.CompareTag("BubbleEntity")) {
                        ready_Bubble.SetLinePos(hit.point);
                        ready_Bubble.landingPoint = hit.point;
                        ready_Bubble.SetlineR2Enable(false);
                        break;
                    } else if (hit.collider.CompareTag("SideCollider")) {
                        ready_Bubble.SetLinePos(hit.point);
                        var facedir = ready_Bubble.faceDir;
                        float angleTan = Mathf.Atan(facedir.x / facedir.y);
                        Vector2 up = new Vector2(0, 1);
                        Vector2 dir = new Vector2(-MathF.Sin(angleTan), MathF.Cos(angleTan));
                        var hit2s = Physics2D.RaycastAll(hit.point, dir, 100);
                        for (int j = 0; j < hit2s.Length; j++) {
                            var hit2 = hit2s[j];
                            if (hit2.collider.CompareTag("BubbleEntity")) {
                                var Distance = Vector2.Distance(hit.point, hit2.point);
                                ready_Bubble.SetlineR2Enable(true);
                                ready_Bubble.SetLine2Pos(hit.point, hit2.point);
                                ready_Bubble.landingPoint = hit2.point;
                                break;
                            } else {
                                if (hit2.collider.CompareTag("TopCollider") || hit2.collider.CompareTag("SideCollider")) {
                                    var Distance = Vector2.Distance(hit.point, hit2.point);
                                    ready_Bubble.SetlineR2Enable(true);
                                    ready_Bubble.SetLine2Pos(hit.point, hit2.point);
                                    ready_Bubble.landingPoint = hit2.point;
                                }
                            }

                        }

                    } else if (hit.collider.CompareTag("TopCollider")) {
                        ready_Bubble.SetlineR2Enable(false);
                        ready_Bubble.SetLinePos(hit.point);
                        ready_Bubble.landingPoint = hit.point;
                    }
                }
            }
        }
    }

    public static void ChangeReadyBubble(GameContext ctx) {
        // FakeBubbleEntity bubble1 = FakeBubbleDomain.Spawn(ctx, ctx.ready_Bubble1.GetPos(), ctx.ready_Bubble2.typeId, new Vector3(1, 1, 1));
        // FakeBubbleEntity bubble2 = FakeBubbleDomain.Spawn(ctx, ctx.ready_Bubble2.GetPos(), ctx.ready_Bubble1.typeId, new Vector3(0.5f, 0.5f, 0.5f));
        // FakeBubbleDomain.UnSpawn(ctx.ready_Bubble1);
        // FakeBubbleDomain.UnSpawn(ctx.ready_Bubble2);
        // ctx.ready_Bubble1 = bubble1;
        // ctx.ready_Bubble2 = bubble2;

        FakeBubbleEntity bubble = ctx.ready_Bubble1;
        // 先设为空，才能不影响bubble
        ctx.ready_Bubble1 = null;
        ctx.ready_Bubble1 = ctx.ready_Bubble2;
        ctx.ready_Bubble2 = bubble;
        // 要关闭line Render 只有reday_Bubble1才能发射射线
        ctx.ready_Bubble2.SetlineR1Enable(false);
        ctx.ready_Bubble2.SetlineR2Enable(false);
        ctx.ready_Bubble1.isEasing = true;
        ctx.ready_Bubble2.isEasing = true;
    }

}