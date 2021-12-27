using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float TimeDestroy;
    [SerializeField] private float direction;
    private Rigidbody2D rig;
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        rig.velocity = new Vector2(direction, 0);
        Destroy(gameObject, TimeDestroy);
    }
}
