using UnityEngine;

public class InputEntity {

    public bool isMouseLeftDown;

    public void Process() {
        isMouseLeftDown = Input.GetMouseButtonDown(0);
    }
}
