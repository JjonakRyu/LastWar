using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContols : MonoBehaviour
{
    // La vitesse des personnages individuels
    [SerializeField] private int m_playerAgentSpeed;
    // Le nombre de personnages, modifie cette valeur pour ajouter ou enlever des personnages, le code fait le reste !
    [SerializeField] private int m_playerNumber;
    // Le rayon du cercle dans lequel les personnages spawnent
    [SerializeField] private int m_playerRadius;
    // Le perfab des personnages, c'est eux qui vont tirrer et c'est eux qui suivent le joueur
    [SerializeField] private GameObject m_playerAgentPrefab;

    private List<Vector2> m_circlePoints = new List<Vector2>();
    private List<PlayerAgent> m_agents = new List<PlayerAgent>();
    private float m_phi = (1 + Mathf.Sqrt(5)) / 2;
    private int m_lastNumber = 0;
    public float speed = 5f;


    void Update()
{
    float move = Input.GetAxis("Horizontal");
    transform.Translate(Vector3.right * move * speed * Time.deltaTime);

    // Normalement tu devrais pas trop avoir a toucher au reste de update !

    // On r�cup�re une liste de points dans un cercle, qu'on utilise pour faire spawn les personnages
    m_circlePoints.Clear();
    m_circlePoints = GeneratePointsInSphere(m_playerNumber, 1);

    // Si m_lastNumber change, on d�truit les personnages et on les respawn
    if (m_lastNumber != m_playerNumber)
    {
        m_lastNumber = m_playerNumber;
        foreach (PlayerAgent agent in m_agents)
        {
            Destroy(agent.gameObject);
        }

        m_agents.Clear();
        for (int i = 0; i < m_playerNumber; i++)
        {
            Vector2 position = new Vector2((m_circlePoints[i + 1].x * m_playerRadius) + transform.position.x, (m_circlePoints[i + 1].y * m_playerRadius) + transform.position.z);

            m_agents.Add(Instantiate(m_playerAgentPrefab).GetComponent<PlayerAgent>());

            m_agents[i].transform.position = transform.position;
            m_agents[i].m_targetPos = position;
            m_agents[i].m_moveSpeed = m_playerAgentSpeed;
        }
    }

    // D�placent les personnages vers leur position cible autour du joueur (le point qui est g�n�r� avec GeneratePointsInSphere et qui leur est assign�)
    for (int i = 0; i < m_playerNumber; i++)
    {
        if (m_agents.Count >= i)
        {

            Vector2 position = new Vector2((m_circlePoints[i + 1].x * m_playerRadius) + transform.position.x, (m_circlePoints[i + 1].y * m_playerRadius) + transform.position.z);

            float radiusFactor = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), position) / m_playerRadius;
            radiusFactor = Mathf.Clamp(radiusFactor, 0, 1);
            float interpRadiusFactor = Mathf.Lerp(1, 0.3f, radiusFactor);
            m_agents[i].m_radiusFactor = interpRadiusFactor;


            m_agents[i].m_targetPos = position;
        }
    }
}

// Affiche la position de spawn des personnages, tu peux le supprimer si tu veux
private void OnDrawGizmos()
{

    foreach (Vector2 point in m_circlePoints)
    {
        Vector3 position = new Vector3((point.x * m_playerRadius) + transform.position.x, transform.position.y, (point.y * m_playerRadius) + transform.position.z);
        Gizmos.DrawWireSphere(position, 1);
    }
    Gizmos.DrawWireSphere(transform.position, m_playerRadius);
}

// G�n�re les points dans un cercle, ne t'emb�te pas avec, c'est des maths compliqu�es que je ne comprends pas non plus
public List<Vector2> GeneratePointsInSphere(int count, float radius)
{
    List<Vector2> points = new List<Vector2>(count);
    float angleStride = 360 * m_phi;
    int b = (int)Mathf.Round(radius * Mathf.Sqrt(count));
    for (int i = 0; i < count + 1; i++)
    {
        float r = Radius(i, count, b);
        float theta = i * angleStride;
        points.Add(new Vector2(r * Mathf.Cos(theta), r * Mathf.Sin(theta)));
    }
    return points;
}
// Aussi n�cessaire pour j�n�rer les points
public float Radius(float k, float n, float b)
{
    if (k > n - b)
    {
        return 1;
    }
    else
    {
        return Mathf.Sqrt(k - 0.5f) / Mathf.Sqrt(n - (b + 1) / 2);
    }
}
}