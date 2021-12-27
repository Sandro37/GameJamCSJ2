using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public Sprite[] perfil;
    public string[] speechTxt;
    public string[] actorName;

    [SerializeField] private LayerMask layer;
    [SerializeField] private float radius;


    private DialogueControl dialogoControle;
    private bool isRadius;
    public static bool podeInteragir = true;
    [SerializeField] private Animator animInteract;

    private void Start()
    {
        dialogoControle = FindObjectOfType<DialogueControl>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isRadius && podeInteragir)
        {
            podeInteragir = false;
            dialogoControle.speech(perfil, speechTxt, actorName);
        }
    }
    private void FixedUpdate()
    {
        interagindo();
    }

    public void interagindo()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, radius, layer);

        if (hit != null)
        {
            isRadius = true;
        }
        else
        {
            isRadius = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animInteract.SetBool("interact", true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animInteract.SetBool("interact", false);
        }
    }
}
