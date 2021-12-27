using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyHead : MonoBehaviour
{
    [SerializeField] float forceImpulse;
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject effectUndoing;
    [SerializeField] private BoxCollider2D[] boxCollider2Ds;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * forceImpulse, ForceMode2D.Impulse);
            enemy.GetComponent<IAEnemy>().StopAllCoroutines();
            enemy.GetComponent<SpriteRenderer>().enabled = false;

            GameObject effect = Instantiate(effectUndoing, transform);
            foreach (BoxCollider2D boxCollider2D in boxCollider2Ds)
                boxCollider2D.enabled = false;

            AudioController.instace.PlaySoundSFX(1);
            Destroy(effect, 1.208f);
            Destroy(enemy, 1.210f);
            
        }
    }
}
