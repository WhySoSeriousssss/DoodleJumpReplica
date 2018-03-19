using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanishPlatform : Platform {

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("Doodler"))
        {
            if (collision.relativeVelocity.y < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
