using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAgent : MonoBehaviour
{
    // La position de la cible qu'ils vont suivre, elle est donnée par PlayerController
    [HideInInspector] public Vector2 m_targetPos = Vector2.zero;
    // La vitesse de déplacement, elle est donnée par PlayerController
    [HideInInspector] public float m_moveSpeed = 10f;
    // Une valeur qui dit la distance au centre du joueur, c'est juste pour rendre le déplacement plus satisfaisant
    [HideInInspector] public float m_radiusFactor;
    private Rigidbody m_rb;


    private void Awake()
    {
        m_rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // C'est ça qui le fait suivre la cible, tu devrais pas avoir a le modifier
        Vector3 targetPos = new Vector3(m_targetPos.x, transform.position.y, m_targetPos.y);
        Vector3 targetDir = targetPos - transform.position;

        m_rb.velocity = targetDir * (m_moveSpeed * m_radiusFactor);
    }

    public void BegunShooting()
    {
        
    }
}

