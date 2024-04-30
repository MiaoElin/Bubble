using UnityEngine;

public class InputEntity {

    bool isMouseLeftDown;

    public void Process() {
        isMouseLeftDown = Input.GetMouseButton(0);
    }
}
