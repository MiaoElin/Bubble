using UnityEngine;

public static class LoginBusiness {

    public static void Enter(LoginContext ctx) {
        UIApp.Panel_Login_Open(ctx.uiCtx);
    }

    public static void Close(LoginContext ctx) {
        UIApp.Panel_Login_Close(ctx.uiCtx);
    }
}