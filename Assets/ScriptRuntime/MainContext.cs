using UnityEngine;

public class MainContext {

    public UIContext uiCtx;
    public GameContext gameCtx;
    public AssetContext assetCtx;
    public LoginContext loginCtx;
    public Canvas screenCanvas;

    public MainContext() {
        uiCtx = new UIContext();
        gameCtx = new GameContext();
        assetCtx = new AssetContext();
        loginCtx = new LoginContext();
    }

    public void Inject(Canvas screenCanvas) {
        this.screenCanvas = screenCanvas;
        uiCtx.Inject(screenCanvas);
        loginCtx.Inject(uiCtx);
        gameCtx.Inject(assetCtx, uiCtx);
    }
}