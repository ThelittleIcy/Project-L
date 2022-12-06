using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject m_itemToSpawn;
    [SerializeField]
    private int m_amountToSpawn;
    [SerializeField]
    private Transform[] m_points;
    [ContextMenu("Test")]
    private void Create()
    {
        for (int i = 0; i < m_amountToSpawn; i++)
        {
            if (m_points.Length == 0)
            {
                Instantiate(m_itemToSpawn, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(m_itemToSpawn, m_points[i].transform.position, Quaternion.identity);
            }
        }
    }
}
