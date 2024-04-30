using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientMain : MonoBehaviour {
    [SerializeField] Canvas screenCanvas;
    bool isTearDown;
    GameContext ctx = new GameContext();
    void Start() {
        // Inject
        ctx.Inject(screenCanvas);
        // Load
        Load(ctx);
        // Bind
        Bind(ctx);

        LoginBusiness.Enter(ctx);
    }

    private void Load(GameContext ctx) {
        ctx.uiApp.LoadAll();
        AssetCore.LoadAll(ctx.assetCtx);
    }

    private void Bind(GameContext ctx) {
        var uieventCenter = ctx.uiApp.uIEventCenter;
        uieventCenter.OnStartClickHandle += () => {
            LoginBusiness.Close(ctx);
            GameBusiness.EnterGame(ctx);
        };
    }

    // Update is called once per frame
    float resetTime = 0;
    const float IntervalTime = 0.01f;
    void Update() {
        float dt = Time.deltaTime;

        ctx.input.Process();

        resetTime += dt;
        if (resetTime < IntervalTime) {
            resetTime = 0;
            GameBusiness.FixedTick(ctx, dt);
        } else {
            while (resetTime >= IntervalTime) {
                resetTime -= IntervalTime;
                GameBusiness.FixedTick(ctx, IntervalTime);
            }
        }

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
        ctx.uiApp.UnLoad();
        AssetCore.UnLoad(ctx.assetCtx);
    }
}
