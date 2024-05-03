using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.ResourceManagement.AsyncOperations;


public class UIContext {
    Dictionary<string, GameObject> allUI;
    public AsyncOperationHandle uiPtr;

    public UIEventCenter uIEventCenter;
    public Panel_Login panel_Login;
    public Panel_GameStatus panel_GameStatus;
    public Canvas screenCanvas;

    public UIContext() {
        allUI = new Dictionary<string, GameObject>();
        uIEventCenter = new UIEventCenter();

    }

    public void Inject(Canvas screenCanvas) {
        this.screenCanvas = screenCanvas;
    }

    public void Add(string name, GameObject prefab) {
        allUI.Add(name, prefab);
    }

    public bool TryGetUI_Prefab(string name, out GameObject prefab) {
        return allUI.TryGetValue(name, out prefab);
    }

}