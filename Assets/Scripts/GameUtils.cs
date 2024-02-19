using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameUtils {

    public static Vector3 GetRandomDirection2D() {
        return new Vector3(
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f)
        ).normalized;
    }

    public static float GetAngleFromVector(Vector3 vector) {
        return Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
    }
}
