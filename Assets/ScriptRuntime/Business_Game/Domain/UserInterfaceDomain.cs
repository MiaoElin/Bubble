using UnityEngine;

public static class UserInterfaceController {

    public static void Tick(GameContext ctx) {
        if (ctx.ready_Bubble) {
            ctx.ready_Bubble.faceDir = (Vector2)Input.mousePosition - ctx.ready_Bubble.GetPos();
        }
    }
}