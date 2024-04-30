using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.EventSystems;
public class UIApp {

    UIContext ctx;
    public UIEventCenter uIEventCenter => ctx.uIEventCenter;

    public UIApp() {
        ctx = new UIContext();
    }
    public void Inject(Canvas screenCanvas) {
        ctx.Inject(screenCanvas);
    }
    public void LoadAll() {
        var uiPtr = Addressables.LoadAssetsAsync<GameObject>("UI", null);
        var list = uiPtr.WaitForCompletion();
        foreach (var ui in list) {
            ctx.Add(ui.name, ui);
        }
        ctx.uiPtr = uiPtr;
    }


    public void UnLoad() {
        if (ctx.uiPtr.IsValid()) {
            Addressables.Release(ctx.uiPtr);
        }
    }

    public void Panel_Login_Open() {
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

    internal void Panel_Login_Close() {
        var panel = ctx.panel_Login;
        if (panel == null) {
            return;
        }
        GameObject.Destroy(panel.gameObject);
    }
}