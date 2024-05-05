using UnityEngine;

public class GameContext {

    public float restTime;
    public GameFSMComponent fsmCom;
    public BackSceneEntity backScene;
    public UIApp uiApp;

    // === Core ===
    public AssetCore assetCore;

    public SoundCore soundCore;

    public InputEntity input;
    public Canvas screenCanvas;

    // === Repo ===
    public BubbleRepo bubbleRepo;

    public IDService iDService;

    // === Entity ===
    public FakeBubbleEntity ready_Bubble1;
    public FakeBubbleEntity ready_Bubble2;
    public BubbleEntity shooting_Bubble;
    public LevelEntity level;

    public int shootCount;
    public int maxShootCount;
    public bool isGridMoveDown;

    // === Component ===
    public GridComponet gridCom;



    public GameContext() {
        fsmCom = new GameFSMComponent();
        uiApp = new UIApp();
        input = new InputEntity();

        assetCore = new AssetCore();
        soundCore = new SoundCore();

        bubbleRepo = new BubbleRepo();
        iDService = new IDService();
        gridCom = new GridComponet();

        maxShootCount = 3;
        shootCount = maxShootCount;

        isGridMoveDown = false;
    }

    public void Inject(Canvas screenCanvas) {
        this.screenCanvas = screenCanvas;
        uiApp.Inject(screenCanvas);
    }
}