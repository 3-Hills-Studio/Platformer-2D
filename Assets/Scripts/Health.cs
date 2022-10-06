using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [field: SerializeField] public Slider HealthSlider { get; private set; }
    
    [field: SerializeField] public float MaxHealth { get; private set; }
    
    [field: SerializeField] public float ModifyHealthLerpDuration { get; private set; }
    
    [field: SerializeField] public float HealthRecoveryDuration { get; private set; }

    [field: SerializeField] public float CurrentHealth { get; private set; }

    private Coroutine modifyHealthCoroutine;
    
    private Coroutine recoverHealthCoroutine;

    private bool stopRecover = false;

    private void Start()
    {
        CurrentHealth = MaxHealth;

        HealthSlider.value = CurrentHealth / MaxHealth;
    }

    private void Update()
    {
        if (modifyHealthCoroutine == null && recoverHealthCoroutine == null)
        {
            RecoverHealth(HealthSlider.value, 1);
        }
    }

    public void ModifyHealth(float amount)
    {
        CurrentHealth += amount;

        ConfigCurrentHealth();

        //HealthSlider.value = _currentHealth / MaxHealth;

        ModifyHealth(HealthSlider.value, CurrentHealth / MaxHealth);
        
        if (CurrentHealth <= 0)
        {
            Destroy(gameObject,ModifyHealthLerpDuration);
            GetComponent<PlayerMovement>().enabled = false;
        }
    }
    
    private void ConfigCurrentHealth()
    {
        if (CurrentHealth >= MaxHealth)
        {
            CurrentHealth = MaxHealth;
            return;
        }

        if (CurrentHealth < 0)
        {
            CurrentHealth = 0;
        }
    }

    private void ModifyHealth(float from, float to)
    {

        if (recoverHealthCoroutine != null)
        {
            //stopRecover = true;

            StopCoroutine(recoverHealthCoroutine);
            recoverHealthCoroutine = null;
            Debug.Log("stopping recovery");
        }

        modifyHealthCoroutine = StartCoroutine(ModifyHealthInternal(from,to,ModifyHealthLerpDuration));

    }
    
    private void RecoverHealth(float from, float to)
    {
        recoverHealthCoroutine = StartCoroutine(RecoverHealthInternal(from,to,HealthRecoveryDuration));
    }
    
    private IEnumerator ModifyHealthInternal(float from, float to,float duration)
    {
        if (to <= 0)
        {
            to -= 0.1f;
        }

        float timeElapsed = 0;
            
        while (timeElapsed < duration)
        {
            HealthSlider.value = Mathf.Lerp(from, to, timeElapsed / duration);

            if (from < to)
            {
                CurrentHealth = HealthSlider.value * MaxHealth;
            }
                
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        modifyHealthCoroutine = null;

    } 
    
    private IEnumerator RecoverHealthInternal(float from, float to,float duration)
    {
        if (to <= 0)
        {
            to -= 0.1f;
        }

        float timeElapsed = 0;
            
        while (timeElapsed < duration)
        {
            HealthSlider.value = Mathf.Lerp(from, to, timeElapsed / duration);

            if (from < to)
            {
                CurrentHealth = HealthSlider.value * MaxHealth;
            }
                
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        recoverHealthCoroutine = null;
    }
}
