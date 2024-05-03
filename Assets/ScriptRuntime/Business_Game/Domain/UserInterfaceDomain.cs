using UnityEngine;

public static class UserInterfaceController {

    public static void Tick(GameContext ctx) {
        if (ctx.ready_Bubble1) {
            ctx.ready_Bubble1.faceDir = (Vector2)Input.mousePosition - ctx.ready_Bubble1.GetPos();
        }
    }
}