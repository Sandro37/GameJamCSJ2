using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    [SerializeField] private GameObject point1;
    [SerializeField] private GameObject point2;
    [SerializeField] private float speedMove;
    private Vector2 nextPos;
    // Update is called once per frame
    private void Start()
    {
        nextPos = point2.transform.position;
    }
    void Update()
    {
        Move();
    } 

    private void Move()
    {
        if (transform.position == point1.transform.position)
            nextPos = point2.transform.position;

        if (transform.position == point2.transform.position)
            nextPos = point1.transform.position;

        transform.position = Vector2.MoveTowards(transform.position, nextPos, speedMove * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.parent = transform;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.parent = null;
    }
}
