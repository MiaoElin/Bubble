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

        ctx.gridCom.Ctor(15, 8);

        LoginBusiness.Enter(ctx);
    }

    private void Load(GameContext ctx) {
        ctx.uiApp.LoadAll();
        ctx.assetCore.LoadAll();
    }

    private void Bind(GameContext ctx) {
        var uieventCenter = ctx.uiApp.uIEventCenter;
        uieventCenter.OnStartClickHandle += () => {
            LoginBusiness.Close(ctx);
            GameBusiness_Normal.EnterGame(ctx);
        };
    }

    // Update is called once per frame
    void Update() {
        float dt = Time.deltaTime;

        ctx.input.Process();
        var status = ctx.fsmCom.status;
        if (status == GameStatus.Login) {

        } else if (status == GameStatus.Normal) {
            GameBusiness_Normal.Tick(ctx, dt);
        } else if (status == GameStatus.LevelEnd) {

        } else if (status == GameStatus.Pause) {

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
        ctx.assetCore.UnLoad();
    }
}
