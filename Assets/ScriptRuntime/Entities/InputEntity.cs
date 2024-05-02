using UnityEngine;

public class InputEntity {

    public bool isMouseLeftDown;
    public float mouseHorizontalAxis;

    public void Process() {
        isMouseLeftDown = Input.GetMouseButtonDown(0);
        mouseHorizontalAxis = Input.GetAxis("Mouse X");
    }
}
