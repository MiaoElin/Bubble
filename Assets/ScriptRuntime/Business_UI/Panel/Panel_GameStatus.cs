using UnityEngine;
using UnityEngine.UI;
using System;

public class Panel_GameStatus : MonoBehaviour {

    [SerializeField] Button btn_Change;
    public Action OnclickBtnChangeHandle;
    public void Ctor() {
        btn_Change.onClick.AddListener(() => {
            OnclickBtnChangeHandle.Invoke();
        });
    }

}