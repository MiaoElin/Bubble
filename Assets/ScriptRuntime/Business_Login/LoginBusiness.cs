using UnityEngine;

public static class LoginBusiness {

    public static void Enter(GameContext ctx) {
        ctx.uiApp.Panel_Login_Open();
    }

    public static void Close(GameContext ctx) {
        ctx.uiApp.Panel_Login_Close();
    }
}