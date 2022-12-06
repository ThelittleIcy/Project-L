using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class PopUpManager : MonoBehaviour
{
    public static PopUpManager Instance { get => m_instance; set => m_instance = value; }
    [SerializeField]
    private GameObject m_popUp;

    [SerializeField]
    private List<PopUp> m_currentPopUps;

    private static PopUpManager m_instance;

    private void Awake()
    {
        if (m_instance != null && m_instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            m_instance = this;
        }
    }
    public void GeneratePopUp(string _message)
    {
        GameObject obj = Instantiate(m_popUp, this.transform);
        PopUp pop = obj.GetComponent<PopUp>();

        m_currentPopUps.Add(pop);

        pop.Manager = this;
        pop.SetUpMessage(_message);

        pop.StartPopUp();
    }

    public void DeletePopUp(PopUp _toDelete)
    {
        for (int i = 0; i < m_currentPopUps.Count; i++)
        {
            if(m_currentPopUps[i] == _toDelete)
            {
                Destroy(m_currentPopUps[i].gameObject);
                m_currentPopUps.RemoveAt(i);
            }
        }
    }
}
