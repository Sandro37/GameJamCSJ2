using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float leght;
    private float startPos;
    [SerializeField] private float parallaxEffect;
    private Transform cam;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position.x;
        leght = GetComponent<SpriteRenderer>().bounds.size.x;
        cam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        float rePos = cam.transform.position.x * (1 - parallaxEffect);  

        float distance = cam.transform.position.x * parallaxEffect;
        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);

        if(rePos > startPos + leght)
        {
            startPos += leght;
        }else if(rePos < startPos - leght)
        {
            startPos -= leght;
        }
    }
}
