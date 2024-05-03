using UnityEngine;
using System;

public static class GameNormalDomain {

    public static void ShootBubble(GameContext ctx) {
        ref var shooting_Bubble = ref ctx.shooting_Bubble;
        ref var ready_Bubble = ref ctx.ready_Bubble;

        // 检测发射
        if (ctx.input.isMouseLeftDown && ready_Bubble) {
            if (!shooting_Bubble || shooting_Bubble.isArrived) {
                // shoot sfx
                ctx.soundCore.BubbleShootPlay(ctx.backScene.bubbleBreak);

                ready_Bubble.SetlineREnable(false);
                ready_Bubble.SetlineR2Enable(false);
                shooting_Bubble = ready_Bubble;
                ready_Bubble = null;
                shooting_Bubble.isShooted = true;
                Vector2 screenPos = Input.mousePosition;
                shooting_Bubble.faceDir = (Vector2)Camera.main.ScreenToWorldPoint(screenPos) - shooting_Bubble.GetPos();
            }

        }

        // 发射的Bubble到了格子的区域（离开发射区）
        if (shooting_Bubble && shooting_Bubble.isInGrid == true) {
            shooting_Bubble.isInGrid = false;
            // 根据FakeBubble生成新的ReadyBubble
            ready_Bubble = BubbleDomain.Spawn(ctx, new Vector2(0, -8), ctx.fake_Bubble.typeId);
            // 销毁fakeBubble
            FakeBubbleDomain.UnSpawn(ctx, ctx.fake_Bubble);
        }
        // //有发射的Bubble，且发射的buble撞到了边缘，开始做反射移动
        // if (shooting_Bubble != null && shooting_Bubble.isSideCollision) {
        //     if (shooting_Bubble.reflectTimes > 0) {
        //         BubbleDomain.Move_ByReflect(shooting_Bubble);

        //     }
        // }
    }

    public static void ShootLine(GameContext ctx) {
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
                        // Debug.Log("Bubble");
                        // Debug.DrawLine(hit.point, (hit2.point), Color.yellow);
                        ready_Bubble.SetLinePos(hit.point);
                        ready_Bubble.landingPoint = hit.point;
                        ready_Bubble.SetlineR2Enable(false);
                        break;
                    } else if (hit.collider.CompareTag("SideCollider")) {
                        // Debug.Log("SideCollider");
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
                                // Debug.Log("Bubble");
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
                        // Debug.Log("TopCollider");
                        ready_Bubble.SetlineR2Enable(false);
                        ready_Bubble.SetLinePos(hit.point);
                        ready_Bubble.landingPoint = hit.point;
                    }
                }
            }
            // Debug.DrawRay(ready_Bubble.GetPos() + ready_Bubble.faceDir.normalized * 2, ready_Bubble.faceDir * 1f, Color.green);
        }
    }

}