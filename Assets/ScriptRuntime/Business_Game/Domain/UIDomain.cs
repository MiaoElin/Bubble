using UnityEngine;

public static class UIDomain {

    // public static void Panel_Login_Open(GameContext ctx) {
    //     UIApp.Panel_Login_Open(ctx.uiCtx);
    // }
    // public static void Panel_Login_Close(GameContext ctx) {
    //     UIApp.Panel_Login_Close(ctx.uiCtx);
    // }

    public static void Panel_GameStatus_Open(GameContext ctx) {
        ctx.uiApp.Panel_GameStatus_Open();
    }
}