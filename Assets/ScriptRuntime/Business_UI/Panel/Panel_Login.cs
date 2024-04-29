using System;
using UnityEngine;
using UnityEngine.UI;

public class Panel_Login : MonoBehaviour {

    [SerializeField] Button start;
    Action OnStartClickHandle;

    public void Ctor() {
        start.onClick.AddListener(() => {
            OnStartClickHandle.Invoke();
        });
    }
    
}