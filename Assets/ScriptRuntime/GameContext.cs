using UnityEngine;

public class GameContext {

    public GameFSMComponent fsmCom;
    public BackSceneEntity backScene;
    public UIApp uiApp;
    public AssetCore assetCore;
    public InputEntity input;
    public Canvas screenCanvas;
    public IDService iDService;

    public GameContext() {
        fsmCom = new GameFSMComponent();
        uiApp = new UIApp();
        input = new InputEntity();
        assetCore = new AssetCore();
        iDService = new IDService();
    }

    public void Inject(Canvas screenCanvas) {
        this.screenCanvas = screenCanvas;
        uiApp.Inject(screenCanvas);
    }
}