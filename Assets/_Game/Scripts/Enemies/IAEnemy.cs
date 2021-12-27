using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAEnemy : MonoBehaviour
{
    [SerializeField] private float speedX;
    private float direction = 1;
    private Rigidbody2D rig;
    private SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        rig = GetComponent<Rigidbody2D>();
        StartCoroutine(IA());
    }

    IEnumerator IA()
    {
        while (true)
        {
            rig.velocity = new Vector2(direction * speedX, 0);
            sprite.flipX = false;
            yield return new WaitForSeconds(1f);
            rig.velocity = new Vector2(-direction * speedX, 0);
            sprite.flipX = true;
            yield return new WaitForSeconds(1f);
            rig.velocity = new Vector2(direction * speedX, 0);
            sprite.flipX = false;
            yield return new WaitForSeconds(1f);
            rig.velocity = new Vector2(-direction * speedX, 0);
            sprite.flipX = true;
            yield return new WaitForSeconds(1f);
        }
    }
}
