using UnityEngine;

public class GameContext {

    public GameFSMComponent fsmCom;
    public BackSceneEntity backScene;
    public AssetContext assetCtx;
    public UIApp uiApp;
    public InputEntity input;
    public Canvas screenCanvas;
    public UIContext uiCtx;

    public GameContext() {
        fsmCom = new GameFSMComponent();
        uiApp = new UIApp();
        assetCtx = new AssetContext();
        input = new InputEntity();
        uiCtx = new UIContext();
    }

    public void Inject(Canvas screenCanvas) {
        this.screenCanvas = screenCanvas;
        uiApp.Inject(uiCtx, screenCanvas);
    }
}