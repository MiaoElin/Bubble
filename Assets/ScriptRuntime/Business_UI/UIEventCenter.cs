using UnityEngine;
using System;

public class UIEventCenter {

    public Action OnStartClickHandle;
    public void Panel_Login_StartGame() { OnStartClickHandle.Invoke(); }
}