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
        ref var ready_Bubble = ref ctx.ready_Bubble;
        // 发射线
        if (ready_Bubble) {
            Vector2 mouseScreenPos = Input.mousePosition;
            ready_Bubble.faceDir = (Vector2)Camera.main.ScreenToWorldPoint(mouseScreenPos) - ready_Bubble.GetPos();
            ready_Bubble.SetlineREnable(true);
            RaycastHit2D[] hits = Physics2D.RaycastAll(ready_Bubble.GetPos() + ready_Bubble.faceDir.normalized * 2, ready_Bubble.faceDir, 100f);
            if (hits != null) {
                // Debug.Log();
                for (int i = 0; i < hits.Length; i++) {
                    var hit = hits[i];
                    // Debug.Log(hit.collider.name + hits.Length);
                    if (hit.collider.CompareTag("BubbleEntity")) {
                        Debug.Log("Bubble");
                        // Debug.DrawLine(hit.point, (hit2.point), Color.yellow);
                        ready_Bubble.SetLinePos(hit.point);
                        ready_Bubble.landingPoint = hit.point;
                        ready_Bubble.SetlineR2Enable(false);
                        break;
                    } else if (hit.collider.CompareTag("SideCollider")) {
                        Debug.Log("SideCollider");
                        ready_Bubble.SetLinePos(hit.point);
                        var facedir = ready_Bubble.faceDir;
                        float angleTan = Mathf.Atan(facedir.x / facedir.y);
                        Vector2 up = new Vector2(0, 1);
                        Vector2 dir = new Vector2(-MathF.Sin(angleTan), MathF.Cos(angleTan));
                        var hit2s = Physics2D.RaycastAll(hit.point, dir, 100);
                        for (int j = 0; j < hit2s.Length; j++) {
                            var hit2 = hit2s[j];
                            // Debug.Log("Hit2:" + hit2.collider.name);
                            if (hit2.collider.CompareTag("BubbleEntity")) {
                                Debug.Log("Bubble");
                                var Distance = Vector2.Distance(hit.point, hit2.point);
                                // Debug.DrawLine(hit.point, (hit2.point), Color.yellow);
                                ready_Bubble.SetlineR2Enable(true);
                                ready_Bubble.SetLine2Pos(hit.point, hit2.point);
                                ready_Bubble.landingPoint = hit2.point;
                                break;
                            } else {
                                if (hit2.collider.CompareTag("TopCollider") || hit2.collider.CompareTag("SideCollider")) {
                                    // Debug.DrawRay(hit.point, dir * 15, Color.red);
                                    var Distance = Vector2.Distance(hit.point, hit2.point);
                                    // Debug.DrawLine(hit.point, (hit2.point), Color.yellow);
                                    ready_Bubble.SetlineR2Enable(true);
                                    ready_Bubble.SetLine2Pos(hit.point, hit2.point);
                                    ready_Bubble.landingPoint = hit2.point;
                                }
                            }

                        }

                    } else if (hit.collider.CompareTag("TopCollider")) {
                        Debug.Log("TopCollider");
                        ready_Bubble.SetlineR2Enable(false);
                        ready_Bubble.SetLinePos(hit.point);
                        ready_Bubble.landingPoint = hit.point;
                    }
                }
            }
            // Debug.DrawRay(ready_Bubble.GetPos() + ready_Bubble.faceDir.normalized * 2, ready_Bubble.faceDir * 1f, Color.green);
        }

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
            bool has = ctx.gridCom.TryGetNearlyGrid(bubble.landingPoint, out var gridPos);
            if (!has) {
                Debug.Log(bubble.colorType + " " + bubble.id + " " + bubble.landingPoint);
            }
            if (has) {
                bubble.SetPos(gridPos);
                bubble.hasSetGridPos = true;
                bubble.RemoveRigidboddy();
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