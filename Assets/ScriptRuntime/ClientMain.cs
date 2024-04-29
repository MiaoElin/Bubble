using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientMain : MonoBehaviour {
    [SerializeField] Canvas screenCanvas;
    bool isTearDown;
    MainContext ctx = new MainContext();
    void Start() {
        // Inject
        ctx.Inject(screenCanvas);
        // Load
        Load(ctx);
        // Bind
        Bind(ctx);

        LoginBusiness.Enter(ctx.loginCtx);
    }

    private void Load(MainContext ctx) {
        UIApp.LoadAll(ctx.uiCtx);
        AssetCore.LoadAll(ctx.assetCtx);
    }

    private void Bind(MainContext ctx) {
        var uieventCenter = ctx.uiCtx.uIEventCenter;
        uieventCenter.OnStartClickHandle += () => {
            LoginBusiness.Close(ctx.loginCtx);
            GameBusiness.EnterGame(ctx.gameCtx);
        };
    }

    // Update is called once per frame
    void Update() {

    }

    void OnApplicationQuit() {
        TearDown();
    }

    void OnDestory() {
        TearDown();
    }

    void TearDown() {
        if (isTearDown) {
            return;
        }
        isTearDown = true;
        UnLoad();
    }

    private void UnLoad() {
        UIApp.UnLoad(ctx.uiCtx);
        AssetCore.UnLoad(ctx.assetCtx);
    }
}
