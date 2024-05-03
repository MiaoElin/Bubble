using UnityEngine;

public class InputEntity {

    public bool isMouseLeftDown;
    public float mouseHorizontalAxis;
    public bool isMouseInGrid;

    public void Process() {
        isMouseLeftDown = Input.GetMouseButtonDown(0);
        mouseHorizontalAxis = Input.GetAxis("Mouse X");
        Vector2 gridleftBottom = new(-15, -6);
        Vector2 gridRightTop = new Vector2(30, 16);
        Vector2 mousePosInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        isMouseInGrid = PureFuction.IsPosInRect(mousePosInWorld, gridleftBottom, gridRightTop);

    }
}
