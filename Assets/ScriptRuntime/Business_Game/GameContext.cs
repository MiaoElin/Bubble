using UnityEngine;

public class GameContext {

    public GameFSMComponent fsmCom;
    public BackSceneEntity backScene;
    public AssetContext assetCtx;
    public UIContext uiCtx;
    public GameContext() {
        fsmCom = new GameFSMComponent();
    }

    public void Inject(AssetContext assetCtx, UIContext uiCtx) {
        this.assetCtx = assetCtx;
        this.uiCtx = uiCtx;
    }
}