using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]private float VisionRange;
    void Start()
    {
        
    }
    void Update()
    {
        Follow();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, VisionRange);
    }
    private void Follow()
    {
        if (GameManager.instance.player = null) return;
        print("Siguiendo al jugador");
    }
}
