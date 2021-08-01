using UnityEngine;

public static class Rigidbody2DExt
{
    // https://stackoverflow.com/questions/34250868/unity-addexplosionforce-to-2d
    public static void AddExplosionForce(this Rigidbody2D rb,
                                         float force,
                                         Vector2 explosionPosition,
                                         float radius)
    {
        var direction = rb.position - explosionPosition;
        var distance = direction.magnitude;
        direction /= distance;
        float lerp = Mathf.Lerp(0, force, radius - distance);
        rb.AddForce(lerp * direction, ForceMode2D.Impulse);
    }
}