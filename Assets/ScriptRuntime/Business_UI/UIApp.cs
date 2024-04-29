using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.EventSystems;
public static class UIApp {

    public static void LoadAll(UIContext ctx) {
        var uiPtr = Addressables.LoadAssetsAsync<GameObject>("UI", null);
        var list = uiPtr.WaitForCompletion();
        foreach (var ui in list) {
            ctx.Add(ui.name, ui);
        }
        ctx.uiPtr = uiPtr;
    }


    public static void UnLoad(UIContext ctx) {
        if (ctx.uiPtr.IsValid()) {
            Addressables.Release(ctx.uiPtr);
        }
    }

    public static void Panel_Login_Open(UIContext ctx) {
        var panel = ctx.panel_Login;
        if (panel == null) {
            ctx.TryGetUI_Prefab(typeof(Panel_Login).Name, out var prefab);
            panel = GameObject.Instantiate<GameObject>(prefab, ctx.screenCanvas.transform).GetComponent<Panel_Login>();
            panel.Ctor();
            panel.OnStartClickHandle = () => { ctx.uIEventCenter.Panel_Login_StartGame(); };
            // 要赋值给Ctx.Panel_Login
            ctx.panel_Login = panel;
        }
        EventSystem.current.SetSelectedGameObject(panel.btn_Start.gameObject);
    }

    internal static void Panel_Login_Close(UIContext uiCtx) {
        var panel = uiCtx.panel_Login;
        if (panel == null) {
            return;
        }
        GameObject.Destroy(panel.gameObject);
    }
}