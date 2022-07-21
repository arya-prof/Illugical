using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BossMaster : MonoBehaviour
{
    public static BossMaster boss;
    [SerializeField] private Transform player;

    private bool _action;
    private float _time;
    private bool _attack;
    private int _attackNum;
    private int _shootCounter;

    [SerializeField] private GameObject bossMesh;
    [SerializeField] private Animator bossAnim;
    private BossAttackDetail _currentPhase;
    private BossAttackDetail _activePhase;

    [Header("Health")] 
    [SerializeField] private Slider healthSlider;
    private bool damageDelay;
    [SerializeField] private int maxHealth;
    private int _health;

    [SerializeField] private GameObject natureSound;
    [SerializeField] private GameObject battleSound;
    public int health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
            if (_health > -1)
            {
                healthSlider.value = _health;
            }
            if (_health < 5)
            {
                _currentPhase = phaseII;
                if (_health < 3)
                {
                    _currentPhase = phaseIII;
                    if (_health < 1)
                    {
                        StartCoroutine(Death());
                    }
                }
            }
            damageDelay = false;
        }
    }
    [SerializeField] private UnityEvent deathEvent;
    [SerializeField] private Image fadeImage;

    [Header("Projectile")] 
    [SerializeField] private GameObject projectileCannonObj;
    [SerializeField] private GameObject projectileObj;
    [SerializeField] private GameObject projectileAmmoObj;
    private GameObject getProjectile(bool reward,int currentIndex ,int rewardIndex)
    {
        if (reward)
        {
            if (currentIndex == rewardIndex)
            {
                return getProjectileAmmo();
            }
            else
            {
                return getProjectilePool();
            }
        }
        else
        {
            return getProjectilePool();
        }
    }
    private GameObject getProjectileAmmo()
    {
        if (ammoCurrent != null)
        {
            return ammoCurrent;
        }
        else
        {
            GameObject newObj = Instantiate(projectileAmmoObj,projectileTra);
            ammoCurrent = newObj;
            return newObj;
        }
    }
    private GameObject getProjectilePool()
    {
        for (int i = 0; i < projectilePool.Count; i++)
        {
            if (!projectilePool[i].activeInHierarchy)
            {
                GameObject oldObj = projectilePool[i];
                oldObj.SetActive(true);
                return oldObj;
            }
        }
        GameObject newObj = Instantiate(projectileObj, projectileTra);
        projectilePool.Add(newObj);
        return newObj;
    }
    private List<GameObject> projectilePool = new List<GameObject>();
    private GameObject ammoCurrent;
    private List<GameObject> projectileActive = new List<GameObject>();

    [SerializeField] private Transform projectileTra;
    [SerializeField] private float minXpos;
    [SerializeField] private float maxYPos;

    [Header("AttackZone")]
    [SerializeField] private GameObject attackZoneObj;
    private GameObject getAttackZonePool()
    {
        for (int i = 0; i < attackZonePool.Count; i++)
        {
            if (!attackZonePool[i].activeInHierarchy)
            {
                GameObject oldObj = attackZonePool[i];
                return oldObj;
            }
        }
        GameObject newObj = Instantiate(attackZoneObj, projectileTra);
        newObj.SetActive(false);
        attackZonePool.Add(newObj);
        return newObj;
    }
    private List<GameObject> attackZonePool = new List<GameObject>();
    private List<GameObject> attackZoneActive = new List<GameObject>();
    public LayerMask groundLayer;
    private Vector3 attackSpot(Transform mother)
    {
        RaycastHit hit;
        Vector3 direction = Vector3.down;
        Ray ray = new Ray(mother.position, direction);
        if (Physics.Raycast(ray, out hit, groundLayer))
        {
            return hit.point;
        }
        else
        {
            return mother.position;
        }
    }
    
    [Header("Summon")] 
    [SerializeField] private GameObject bossCanvas;
    [SerializeField] private Transform bossHpSlider;
    [SerializeField] private float summonTime;
    
    [Header("PhaseZ")] 
    [SerializeField] private BossAttackDetail phaseZ;
    [SerializeField] private Transform cannonPlace;

    [Header("PhaseI")] 
    [SerializeField] private BossAttackDetail phaseI;
    
    [Header("PhaseII")] 
    [SerializeField] private BossAttackDetail phaseII;
    
    [Header("PhaseIII")] 
    [SerializeField] private BossAttackDetail phaseIII;
    
    private void Awake()
    {
        boss = this;
    }

    private void Start()
    {
        health = maxHealth;
        healthSlider.maxValue = health;
        healthSlider.value = health;

        bossCanvas.SetActive(false);
        
        bossMesh.SetActive(false);
    }

    public void Summon()
    {
        StartCoroutine(SummonAction());
    }
    IEnumerator SummonAction()
    {
        bossCanvas.SetActive(true);
        bossHpSlider.localScale = new Vector3(0,1,1);
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime;
            bossHpSlider.localScale = new Vector3(t,1,1);
            yield return null;
        }
        bossHpSlider.localScale = new Vector3(1,1,1);
        bossMesh.SetActive(true);
        bossAnim.SetTrigger("Summon");
        EZCameraShake.CameraShaker.Instance.ShakeOnce(3f, 3f, 0.1f, 15f);
        yield return new WaitForSeconds(summonTime);
        _currentPhase = phaseZ;
        _action = true;
    }

    private void Update()
    {
        if (!_action) return;
        if (!_attack)
        {
            if (_time < _currentPhase.attackStartTime)
            {
                _time += Time.deltaTime;
            }
            else
            {
                if (projectileActive.Count != 0) return;
                _attack = true;
                _time = 0;
                if (_attackNum < _currentPhase.rewardProjectile)
                {
                    StartCoroutine(Reload(false));
                }
                else
                {
                    _attackNum = 0;
                    if (_currentPhase.phaseName == "PhaseZ")
                    {
                        StartCoroutine(CannonSpawn());
                    }
                    else
                    {
                        if (References.Instance.playerBackpack.Count == 0)
                        {
                            StartCoroutine(Reload(true));
                        }
                        else
                        {
                            StartCoroutine(Reload(false));
                        }
                    }
                }
            }
        }
    }

    private IEnumerator Reload(bool reward)
    {
        Debug.Log(reward);
        _activePhase = _currentPhase;
        int rewardIndex = 0;
        if (reward)
            rewardIndex = Random.Range(0, _activePhase.totalProjectile);
        float distance = (minXpos * -2) / _currentPhase.totalProjectile;
        float eachReload = _activePhase.totalReloadTime / _activePhase.totalProjectile;
        for (int i = 0; i < _activePhase.totalProjectile; i++)
        {
            yield return new WaitForSeconds(eachReload);
            GameObject projectile = getProjectile(reward, i, rewardIndex);
            projectileActive.Add(projectile);
            projectile.transform.position = new Vector3(minXpos+(distance * (i+1)),0,0) + projectileTra.position;
        }
        StartCoroutine(Attack(reward));
    }
    private IEnumerator Attack(bool reward)
    {
        float fireDelay = _activePhase.totalFireTime / projectileActive.Count;
        for (int i = 0; i < projectileActive.Count; i++)
        {
            yield return new WaitForSeconds(fireDelay);
            
            Vector3 playerPos = attackSpot(player);
            
            GameObject projectile = projectileActive[i];
            IProjectile bossProjectile = projectile.GetComponent<IProjectile>();
            
            GameObject attackZone = getAttackZonePool();
            attackZoneActive.Add(attackZone);
            BossAttack bossAttack = attackZone.GetComponent<BossAttack>();
            attackZone.transform.position = playerPos;
            
            bossProjectile.StartAttack(_activePhase.projectileSpeed, _activePhase.projectileRadius ,playerPos,bossAttack);
        }
        if(!reward)
            _attackNum++;
        _attack = false;
    }

    private IEnumerator CannonSpawn()
    {
        GameObject cannonObj = Instantiate(projectileCannonObj, projectileTra);
        cannonObj.transform.position = projectileTra.position;
        yield return new WaitForSeconds(1f);
        IProjectile projectile = cannonObj.GetComponent<IProjectile>();
        
        GameObject attackZone = getAttackZonePool();
        attackZone.transform.position = attackSpot(cannonPlace);
        BossAttack bossAttack = attackZone.GetComponent<BossAttack>();

        projectile.StartAttack(_currentPhase.projectileSpeed, _currentPhase.projectileRadius ,cannonPlace.position, bossAttack);
        
        yield return new WaitForSeconds(2f);
        _currentPhase = phaseI;
        _attack = false;
    }

    private IEnumerator Death()
    {
        battleSound.GetComponent<AudioSource>().Stop();
        natureSound.GetComponent<AudioSource>().Play();
        projectileTra.gameObject.SetActive(false);
        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            fadeImage.color = new Color(0, 0, 0, i);
            yield return null;
        }
        deathEvent?.Invoke();
        bossMesh.SetActive(false);
        for (float i = 1; i >= 0; i -= Time.deltaTime)
        {
            fadeImage.color = new Color(0, 0, 0, i);
            yield return null;
        } 
    }

    public void RemoveProjectile(GameObject projectile)
    {
        _shootCounter++;
        EZCameraShake.CameraShaker.Instance.ShakeOnce(3f, 3f, 0.1f, 2f);
        if (_shootCounter == _activePhase.totalProjectile)
        {
            projectileActive.Clear();
            attackZoneActive.Clear();
            _shootCounter = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(damageDelay) return;
        if (other.CompareTag("CannonBall"))
        {
            damageDelay = true;
            other.gameObject.SetActive(false);
            EZCameraShake.CameraShaker.Instance.ShakeOnce(3f, 3f, 0.1f, 2f);
            health--;
        }
    }
}
