using UnityEngine;

public class LoginContext {

    public UIContext uiCtx;

    public LoginContext() {

    }
    public void Inject(UIContext uiCtx) {
        this.uiCtx = uiCtx;
    }
}