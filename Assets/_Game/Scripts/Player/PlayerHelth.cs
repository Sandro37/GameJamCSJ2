using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHelth : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private float LimitY;
    [SerializeField] private int helth = 1;
    private int initHelth;
    private void Start()
    {
        initHelth = helth;
    }
    private void Update()
    {
        if (transform.position.y < LimitY)
            Respawn();
    }
    public void TakeDamage(int damage)
    {
        AudioController.instace.PlaySoundSFX(1);
        helth -= damage;
        if (helth <= 0)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<PlayerMovement>().CanMove = false;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            gameObject.tag = "Untagged";
            GameObject explosion = Instantiate(explosionPrefab, transform);
            Destroy(explosion, 1.208f);
            Invoke(nameof(Respawn), 0.700f);
        }
    }

    private void Respawn()
    {
        GameController.instance.Respawn();
        transform.position = GameController.instance.lastCheckPoint;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<PlayerMovement>().CanMove = true;
        gameObject.tag = "Player";
        helth = initHelth;
    }
}
