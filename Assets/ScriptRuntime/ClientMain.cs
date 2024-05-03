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

        // int[] arr = new int[10];
        // arr[0] = 0;
        // arr[1] = 0;
        // foreach (var i in arr) {
        //     if (i == 0) {
        //         Debug.Log("arr1:" + i);
        //         continue;
        //     }
        // }
        // Array.Clear(arr, 0, arr.Length);
        // arr[0] = 0;
        // foreach (var i in arr) {
        //     if (i == 0) {
        //         Debug.Log("arr2:" +i);
        //         continue;
        //     }
        // }
    }

    private void Load(GameContext ctx) {
        ctx.uiApp.LoadAll();
        ctx.assetCore.LoadAll();
        ctx.soundCore.Load();
    }

    private void Bind(GameContext ctx) {
        var uieventCenter = ctx.uiApp.uIEventCenter;
        uieventCenter.OnStartClickHandle += () => {
            LoginBusiness.Close(ctx);
            GameBusiness_Normal.EnterGame(ctx);
        };

        uieventCenter.OnChangeClickHandle += () => {
            GameNormalDomain.ChangeReadyBubble(ctx);
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
