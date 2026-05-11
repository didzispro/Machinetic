using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [Space(5)]
    public float bounceForce = 1f;

    void OnTriggerEnter2D(Collider2D other)
    {
        Rigidbody2D rb = other.attachedRigidbody;

        if (rb != null)
        {
            rb.velocity = new Vector2(rb.velocity.x, bounceForce);
        }
    }
}
