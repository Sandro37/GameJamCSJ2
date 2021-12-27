using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    [SerializeField] private Animator animInteract;
    [SerializeField] private SceneManagerControl sceneManager;

    private bool isInteract = false;
    // Update is called once per frame
    void Update()
    {
        if (isInteract && Input.GetKeyDown(KeyCode.E))
            sceneManager.NextLevel();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animInteract.SetBool("interact", true);
            isInteract = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animInteract.SetBool("interact", false);
            isInteract = false;
        }
    }
}
