using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    [SerializeField] private Transform pos;
    [SerializeField] private float timeReposition;
    [SerializeField] private float fallingTime;
    private TargetJoint2D targetJoint2D;
    private BoxCollider2D boxCollider2D;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        targetJoint2D = GetComponent<TargetJoint2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Invoke(nameof(Falling), fallingTime);
            
        }
    }

    private void Falling()
    {
        targetJoint2D.enabled = false;
        boxCollider2D.isTrigger = true;
        Invoke(nameof(Reposition), timeReposition);
    }
    void Reposition()
    {
        targetJoint2D.enabled = true;
        boxCollider2D.isTrigger = false;
        transform.position = pos.position;
    }
}
