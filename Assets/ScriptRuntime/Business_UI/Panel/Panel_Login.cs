using System;
using UnityEngine;
using UnityEngine.UI;

public class Panel_Login : MonoBehaviour {

    [SerializeField] public Button btn_Start;
    public Action OnStartClickHandle;

    public void Ctor() {
        btn_Start.onClick.AddListener(() => {
            OnStartClickHandle.Invoke();
        });
    }

    internal void Close() {
        GameObject.Destroy(gameObject);
    }
}