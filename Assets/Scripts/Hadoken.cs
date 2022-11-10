using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hadoken : MonoBehaviour
{
    [field: SerializeField] private Image CoolDownFill { get; set; }

    [SerializeField] private GameObject hadokenPrefab;

    [SerializeField] private float speed;

    [SerializeField] private Transform firePoint;
    
    private bool _canFire = false;
    private bool _canCast = true;
    
    [SerializeField] private float castTime;
    [SerializeField] private float currentCastTime;
    [SerializeField] private float coolDownDuration;

    // Start is called before the first frame update
    void Start()
    {
        CoolDownFill.fillAmount = 0;
    }

    private void Update()
    {
        HandleHaduken();
    }

    private void HandleHaduken()
    {
        if (!_canCast) return;
        
        if (Input.GetKey(KeyCode.H))
        {
            currentCastTime += Time.deltaTime;

            if(currentCastTime > castTime) FireHadoken();
        }

        if (Input.GetKeyUp(KeyCode.H))
        {
            currentCastTime = 0;
        }
    }

    private void FireHadoken()
    {
        _canCast = false;
        
        currentCastTime = 0;
        
        GameObject hadoken = Instantiate(hadokenPrefab, firePoint.position, Quaternion.identity);

        Vector2 dir = Vector2.right * transform.localScale.x;

        hadoken.GetComponent<Rigidbody2D>().velocity = dir * speed;

        hadoken.transform.localScale = new Vector3(transform.localScale.x, hadoken.transform.localScale.y,
            hadoken.transform.localScale.z);

        StartCoroutine(HadukenCoolDown());

    }

    private IEnumerator HadukenCoolDown()
    {
        Debug.Log("cooldown started: " + CoolDownFill.fillAmount);
        
        float duration = coolDownDuration;

        float timePassed = 0;
        
        while (timePassed < duration)
        {
            timePassed += Time.deltaTime;

            //timePassed += Mathf.Min(Time.deltaTime, duration - timePassed);
            
            CoolDownFill.fillAmount = 1 - timePassed / duration;

            Debug.Log("fill amount: " + CoolDownFill.fillAmount);
            
            yield return null;
        }

        _canCast = true;
    }
}
