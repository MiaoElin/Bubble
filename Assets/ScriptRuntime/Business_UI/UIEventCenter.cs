using UnityEngine;
using System;

public class UIEventCenter {

    public Action OnStartClickHandle;
    public void Panel_Login_StartGame() { OnStartClickHandle.Invoke(); }

    public Action OnChangeClickHandle;
    public void Panel_GameStatus_ChangeBubble() { OnChangeClickHandle.Invoke(); }
}