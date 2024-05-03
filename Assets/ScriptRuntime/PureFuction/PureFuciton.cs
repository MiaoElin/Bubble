using UnityEngine;

public static class PureFuction {

    public static bool IsPosInRect(Vector2 pos, Vector2 rect, Vector2 size) {
        Vector2 rightTop = rect + size;
        if (pos.x >= rect.x && pos.x <= rightTop.x && pos.y >= rect.y && rect.y <= rightTop.y) {
            return true;
        }
        return false;
    }
}

