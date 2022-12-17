using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    public UnityEvent OnPlayerDied;
    public UnityEvent OnPlayerStartSleep;
    public UnityEvent OnPlayerAsleep;
    public UnityEvent OnPlayerAwake;

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

    [SerializeField]
    private int m_maxEnergy;
    [SerializeField]
    private int m_currentEnergy;
    [SerializeField]
    private int m_reduceEnergyValue = 1;
    [SerializeField]
    private float m_energyWaitForSecondsForReducing = 5f;
    [SerializeField]
    private SurvivalUI m_energyUI;
    [SerializeField]
    private KeyCode m_sleepKey = KeyCode.C;

    private Coroutine m_looseHealthCoroutine;
    private Coroutine m_looseHungerCoroutine;
    private Coroutine m_looseThirstCoroutine;
    private Coroutine m_looseEnergyCoroutine;

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
    public int CurrentEnergy
    {
        get => m_currentEnergy;
        set
        {
            m_currentEnergy = value;
            m_energyUI.SetValue(m_currentEnergy);
        }
    }

    public static PlayerHealth Instance { get => m_instance; set => m_instance = value; }
    public int MaxEnergy { get => m_maxEnergy; set => m_maxEnergy = value; }
    public int MaxThirst { get => m_maxThirst; set => m_maxThirst = value; }
    public int MaxHunger { get => m_maxHunger; set => m_maxHunger = value; }
    public int MaxHealth { get => m_maxHealth; set => m_maxHealth = value; }

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
        OnPlayerStartSleep.AddListener(Sleep);
        CurrentHealth = MaxHealth;
        CurrentHunger = MaxHunger;
        CurrentThirst = MaxThirst;
        CurrentEnergy = MaxEnergy;

        m_healthUI.SetValue(CurrentHealth);
        m_hungerUI.SetValue(CurrentHunger);
        m_thirstUI.SetValue(CurrentThirst);
        m_energyUI.SetValue(CurrentEnergy);

        m_looseHungerCoroutine = StartCoroutine(ReduceHungerOverTime());
        m_looseThirstCoroutine = StartCoroutine(ReduceThirstOverTime());
        m_looseEnergyCoroutine = StartCoroutine(ReduceEnergyOverTime());
    }

    private void Update()
    {
        if (Input.GetKeyDown(m_sleepKey))
        {
            OnPlayerStartSleep?.Invoke();
        }
    }
    /// <summary>
    /// Increases the Hunger value.
    /// </summary>
    /// <param name="_amount">the amount to increase</param>
    public void IncreaseHunger(int _amount)
    {
        CurrentHunger += _amount;
        if (CurrentHunger >= MaxHunger)
        {
            CurrentHunger = MaxHunger;
        }
    }
    /// <summary>
    /// Increases the Thirst value.
    /// </summary>
    /// <param name="_amount">the amount to increase</param>
    public void IncreaseThirst(int _amount)
    {
        CurrentThirst += _amount;
        if (CurrentThirst >= MaxThirst)
        {
            CurrentThirst = MaxThirst;
        }
    }
    /// <summary>
    /// Increases the Health value.
    /// </summary>
    /// <param name="_amount">the amount to increase</param>
    public void IncreaseHealth(int _amount)
    {
        CurrentHealth += _amount;
        if (CurrentHealth >= MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
    }
    /// <summary>
    /// Increases the Energy value.
    /// </summary>
    /// <param name="_amount">the amount to increase</param>
    public void IncreaseEnergy(int _amount)
    {
        CurrentEnergy += _amount;
        if (CurrentEnergy >= MaxEnergy)
        {
            CurrentEnergy = MaxEnergy;
        }
    }
    /// <summary>
    /// Increases the Health over Time.
    /// </summary>
    /// <param name="_amountToIncrease">the amount to increase</param>
    /// <returns></returns>
    public IEnumerator IncreaseHealthOverTime(int _amountToIncrease)
    {
        while (CurrentHealth <= CurrentHealth + _amountToIncrease)
        {
            if (CurrentHealth >= MaxHealth)
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
        if (CurrentHunger <= 0)
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
        if (CurrentThirst <= 0)
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
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            OnPlayerDied?.Invoke();
            return true;
        }
        return false;
    }
    /// <summary>
    /// Decreases the Energy Value and checks for Forced Sleep;
    /// </summary>
    /// <param name="_amount">the amount to Decrease the energy</param>
    /// <returns>true if the Player died</returns>
    public bool DecreaseEnergy(int _amount)
    {
        CurrentEnergy -= _amount;
        if (CurrentEnergy <= 0)
        {
            CurrentEnergy = 0;
            OnPlayerStartSleep?.Invoke();
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
    /// <summary>
    /// Reduces the Energy value over time.
    /// </summary>
    /// <returns></returns>
    private IEnumerator ReduceEnergyOverTime()
    {
        while (true)
        {
            if (DecreaseEnergy(m_reduceEnergyValue))
            {
                yield break;
            }
            yield return new WaitForSecondsRealtime(m_energyWaitForSecondsForReducing);
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
    private void Sleep()
    {
        BlackScreenManager.Instance.OnBlackScreenFinishedEvent.AddListener(WakeUp);
        BlackScreenManager.Instance.OnBlackEvent.AddListener(Asleep);
        BlackScreenManager.Instance.StartFadeToBlack();
    }
    private void WakeUp()
    {
        OnPlayerAwake?.Invoke();
        BlackScreenManager.Instance.OnBlackScreenFinishedEvent.RemoveListener(WakeUp);
    }
    private void Asleep()
    {
        OnPlayerAsleep?.Invoke();
        BlackScreenManager.Instance.OnBlackEvent.RemoveListener(Asleep);
    }
}
