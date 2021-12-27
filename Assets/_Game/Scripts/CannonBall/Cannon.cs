using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Transform positionAttack;
    public void Attack()
    {
        Instantiate(ballPrefab, positionAttack);
    }
}
