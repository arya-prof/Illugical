using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private GameObject healthCanvas;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private int maxHealth;
    private int _health;
    public int health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
            StartCoroutine(GetDamage());
            if (_health >= 0)
            {
                healthSlider.value = _health;
            }
            if (_health <= 0)
            {
                Die();
            }

        }
    }
    private bool damageCooldown;

    IEnumerator GetDamage()
    {
        yield return new WaitForSeconds(1f);
        damageCooldown = false;
    }

    void Die()
    {
    }

    private void Start()
    {
        healthCanvas.SetActive(false);
        
        health = maxHealth;
        healthSlider.maxValue = health;
        healthSlider.value = health;
    }
    
    public void Summon()
    {
        StartCoroutine(SummonAction());
    }
    IEnumerator SummonAction()
    {
        healthSlider.transform.localScale = new Vector3(0,1,1);
        healthCanvas.SetActive(true);
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime;
            healthSlider.transform.localScale = new Vector3(t,1,1);
            yield return null;
        }
        healthSlider.transform.localScale = new Vector3(1,1,1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(damageCooldown) return;
        if (other.CompareTag("BossProjectile"))
        {
            damageCooldown = true;
            health--;
        }
    }
}
