using UnityEngine;

public static class GameNormalDomain {

    public static void ShootBubble(GameContext ctx) {
        ref var shooting_Bubble = ref ctx.shooting_Bubble;
        ref var ready_Bubble = ref ctx.ready_Bubble;

        // 检测发射
        if (ctx.input.isMouseLeftDown) {
            shooting_Bubble = ready_Bubble;
            ready_Bubble = null;
            BubbleDomain.Move_ByShoot(shooting_Bubble);

        }

        // 发射的Bubble到了格子的区域（离开发射区）
        if (shooting_Bubble.isInGrid == true) {
            shooting_Bubble.isInGrid = false;
            // 根据FakeBubble生成新的ReadyBubble
            ready_Bubble = BubbleDomain.Spawn(ctx, new Vector2(0, -8), ctx.fake_Bubble.typeId);
            // 销毁fakeBubble
            FakeBubbleDomain.UnSpawn(ctx, ctx.fake_Bubble);
        }

        //有发射的Bubble，且发射的buble撞到了边缘，开始做反射移动
        if (shooting_Bubble != null && shooting_Bubble.isSideCollision) {
            BubbleDomain.Move_ByReflect(shooting_Bubble);
        }
    }
}