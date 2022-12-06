using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    public UnityEvent OnPlayerDied;

    [SerializeField]
    private int m_maxHealth;
    [SerializeField]
    private int m_currentHealth;
    [SerializeField]
    private float m_healthWaitForSecondsForIncreasing = 5f;
    [SerializeField]
    private SurvivalUI m_healthUI;

    [SerializeField]
    private int m_maxHunger;
    [SerializeField]
    private int m_currentHunger;
    [SerializeField]
    private int m_reduceHungerValue = 1;
    [SerializeField]
    private float m_hungerWaitForSecondsForReducing = 5f;
    [SerializeField]
    private SurvivalUI m_hungerUI;

    [SerializeField]
    private int m_maxThirst;
    [SerializeField]
    private int m_currentThirst;
    [SerializeField]
    private int m_reduceThirstValue = 1;
    [SerializeField]
    private float m_thirstWaitForSecondsForReducing = 5f;
    [SerializeField]
    private SurvivalUI m_thirstUI;

    private Coroutine m_looseHealthCoroutine;
    private Coroutine m_looseHungerCoroutine;
    private Coroutine m_looseThirstCoroutine;

    private static PlayerHealth m_instance;

    public int CurrentHealth
    {
        get => m_currentHealth; set
        {
            m_currentHealth = value;
            m_healthUI.SetValue(m_currentHealth);
        }
    }
    public int CurrentHunger
    {
        get => m_currentHunger; set
        {
            m_currentHunger = value;
            m_hungerUI.SetValue(m_currentHunger);
        }
    }
    public int CurrentThirst
    {
        get => m_currentThirst; set
        {
            m_currentThirst = value;
            m_thirstUI.SetValue(m_currentThirst);
        }
    }

    public static PlayerHealth Instance { get => m_instance; set => m_instance = value; }

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

    private void Start()
    {
        OnPlayerDied.AddListener(Die);
        CurrentHealth = m_maxHealth;
        CurrentHunger = m_maxHunger;
        CurrentThirst = m_maxThirst;

        m_healthUI.SetValue(CurrentHealth);
        m_hungerUI.SetValue(CurrentHunger);
        m_thirstUI.SetValue(CurrentThirst);

        m_looseHungerCoroutine = StartCoroutine(ReduceHungerOverTime());
        m_looseThirstCoroutine = StartCoroutine(ReduceThirstOverTime());
    }
    /// <summary>
    /// Increases the Hunger value.
    /// </summary>
    /// <param name="_amount">the amount to increase</param>
    public void IncreaseHunger(int _amount)
    {
        CurrentHunger += _amount;
        if (CurrentHunger >= m_maxHunger)
        {
            CurrentHunger = m_maxHunger;
        }
    }
    /// <summary>
    /// Increases the Thirst value.
    /// </summary>
    /// <param name="_amount">the amount to increase</param>
    public void IncreaseThirst(int _amount)
    {
        CurrentThirst += _amount;
        if (CurrentThirst >= m_maxThirst)
        {
            CurrentThirst = m_maxThirst;
        }
    }
    /// <summary>
    /// Increases the Health value.
    /// </summary>
    /// <param name="_amount">the amount to increase</param>
    public void IncreaseHealth(int _amount)
    {
        CurrentHealth += _amount;
        if (CurrentHealth >= m_maxHealth)
        {
            CurrentHealth = m_maxHealth;
        }
    }
    /// <summary>
    /// Increases the Health over Time.
    /// </summary>
    /// <param name="_amountToIncrease">the amount to increase</param>
    /// <returns></returns>
    public IEnumerator IncreaseHealthOverTime(int _amountToIncrease)
    {
        while(CurrentHealth <= CurrentHealth + _amountToIncrease)
        {
            if(CurrentHealth >= m_maxHealth)
            {
                yield break;
            }
            CurrentHealth++;
            yield return new WaitForSecondsRealtime(m_healthWaitForSecondsForIncreasing);
        }
    }

    /// <summary>
    /// Decreases the Hunger Value and checks for Death;
    /// </summary>
    /// <param name="_amount">the amount to Decrease the hunger</param>
    /// <returns>true if the Player died</returns>
    public bool DecreaseHunger(int _amount)
    {
        CurrentHunger -= _amount;
        if(CurrentHunger <= 0)
        {
            CurrentHunger = 0;
            OnPlayerDied?.Invoke();
            return true;
        }
        return false;
    }
    /// <summary>
    /// Decreases the thirst Value and checks for Death;
    /// </summary>
    /// <param name="_amount">the amount to Decrease the thirst</param>
    /// <returns>true if the Player died</returns>
    public bool DecreaseThirst(int _amount)
    {
        CurrentThirst -= _amount;
        if(CurrentThirst <= 0)
        {
            CurrentThirst = 0;
            OnPlayerDied?.Invoke();
            return true;
        }
        return false;
    }
    /// <summary>
    /// Decreases the health Value and checks for Death;
    /// </summary>
    /// <param name="_amount">the amount to Decrease the health</param>
    /// <returns>true if the Player died</returns>
    public bool DecreaseHealth(int _amount)
    {
        CurrentHealth -= _amount;
        if(CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            OnPlayerDied?.Invoke();
            return true;
        }
        return false;
    }
    /// <summary>
    /// Reduces the Hunger value over time.
    /// </summary>
    /// <returns></returns>
    private IEnumerator ReduceHungerOverTime()
    {
        while (true)
        {
            if (DecreaseHunger(m_reduceHungerValue))
            {
                yield break;
            }
            yield return new WaitForSecondsRealtime(m_hungerWaitForSecondsForReducing);
        }
    }
    /// <summary>
    /// Reduces the Thirst value over time.
    /// </summary>
    /// <returns></returns>
    private IEnumerator ReduceThirstOverTime()
    {
        while (true)
        {
            if (DecreaseThirst(m_reduceThirstValue))
            {
                yield break;
            }
            yield return new WaitForSecondsRealtime(m_thirstWaitForSecondsForReducing);
        }
    }


    //private IEnumerator ReduceHealthOverTime()
    //{
    //    while (true)
    //    {
    //        CurrentHealth--;
    //        if (CurrentHealth <= 0)
    //        {
    //            OnPlayerDied?.Invoke();
    //            m_looseHealthCoroutine = null;
    //            yield break;
    //        }
    //        yield return new WaitForSecondsRealtime(5);
    //    }
    //}

    private void Die()
    {
        Debug.Log("Deeeeead");
    }
}