using UnityEngine;

public class GameContext {

    public float restTime;
    public GameFSMComponent fsmCom;
    public BackSceneEntity backScene;
    public UIApp uiApp;
    public AssetCore assetCore;
    public InputEntity input;
    public Canvas screenCanvas;
    public IDService iDService;
    public BubbleEntity ready_Bubble;
    public FakeBubbleEntity fake_Bubble;
    public BubbleEntity shooting_Bubble;


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