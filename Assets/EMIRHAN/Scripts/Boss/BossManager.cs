using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BossManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Slider slider;
    [SerializeField] public NavMeshAgent agent;
    [SerializeField] PlayerManager player;
    [SerializeField] public CharacterController characterController;
    [SerializeField] public BoxCollider Box;

    [Header("Scripts")]
    [SerializeField] GameManager _gameManager;
    [SerializeField] BossAnimation _bossAnimation;

    [Header("Data")]
    [SerializeField] BossData BossData;
    [SerializeField] SkillsData SkillObjects;

    [Header("Variables")]
    [SerializeField] public float Health;
    [HideInInspector] public float Damage;

    [Header("VFX Objects")]
    [SerializeField] private GameObject beforSlashVFX;
    [SerializeField] private GameObject HitVFX;
    [SerializeField] private GameObject slashVFX;
    [SerializeField] private GameObject deathVFX;
    [SerializeField] private GameObject burnVFX;

    [SerializeField] AudioClip deathExplosion;

    [HideInInspector] public bool InCombat = false;
    private bool Wait = false;
    bool death;
    
    private bool stun;
    private bool burning;

    Vector3 locat;
    Vector3 Target;
    Vector3 TargetHit;

    int skillTime;
    int lastRandomValue = 0;

    List<IEnumerator> allSkills = new List<IEnumerator>();

    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        BossDataTake();
        SkillDataTake();
        slider.maxValue = Health;
        StartCoroutine(Skills());
        StartCoroutine(HitSequence());
    }

    void Update()
    {
        slider.value = Health;

        if(Health <= 0 && death == false && InCombat == false)
        {
            death = true;
            StopCoroutine(Skills());
            StartCoroutine(Death());
        }

        if (player != null)
        {
            locat = new Vector3(player.transform.position.x + GetRandomValue(), 0, player.transform.position.z + GetRandomValue());
        }

        if (_gameManager.InHitSequence == true)
        {
            agent.ResetPath();
        }

        if (InCombat == false && Health > 0 && _gameManager.InHitSequence == false && Wait == false && stun == false)
        {
            agent.SetDestination(locat);
        }

        if (burning == true)
        {
            Health -= 10 * Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (player != null && Health > 0 && _gameManager.InHitSequence == false && Wait == false && _gameManager.DashBoss == false && stun == false)
        {
            rotationBoss();
        }

        if (_gameManager.UpToBoss == true && player != null)
        {
            MoveUp();
        }

        if (_gameManager.DownToBoss == true && player != null)
        {
            MoveDown();
        }

        float speedBoss = Mathf.Abs(agent.velocity.x) + Mathf.Abs(agent.velocity.z) + Mathf.Abs(agent.velocity.y);
        _bossAnimation.floatParameter("Speed", speedBoss);
    }

    private void LateUpdate()
    {
        if(_gameManager.DashBoss == true && player != null)
        {
            MoveDash();
        }

        if(InCombat == true)
        {
            _gameManager.InHitSequence = false;
        }

        if(_gameManager.InHitSequence == true)
        {
            RegularHit();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Magic")
        {
            Health -= (other.transform.localScale.x * 50);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.collider.CompareTag("FrozenAttack"))
        {
            other.gameObject.tag = "Untagged";
            if (InCombat == false && Health > 0 && Wait == false)
            {
                StartCoroutine(Stun());
            }
        }
        
        if(other.collider.CompareTag("FireAttack"))
        {
            other.gameObject.tag = "Untagged";
            if (InCombat == false && Health > 0 && Wait == false)
            {
                StartCoroutine(Burn());
            }
        }
    }

    private IEnumerator Burn()
    {
        burning = true;
        burnVFX.SetActive(true);
        yield return new WaitForSeconds(6f);
        burning = false;
        burnVFX.SetActive(false);
    }

    private IEnumerator Stun()
    {
        stun = true;
        agent.ResetPath();
        yield return new WaitForSeconds(3f);
        yield return new WaitForSeconds(3f);
        stun = false;
        InCombat = false;
    }

    IEnumerator Skills()
    {
        yield return new WaitForSeconds(Random.Range(6,10));

        InCombat = true;
        
        if(Health > 0)
        {
            if (stun == false)
            {
                switch (GetRandomSkillValue())
                {
                    case 1:
                        agent.ResetPath();
                        Debug.Log("Case 1");
                        StartCoroutine(allSkills[0]);
                        yield return new WaitForSeconds(skillTime);
                        InCombat = false;
                        break;
                    case 2:
                        agent.ResetPath();
                        Debug.Log("Case 2");
                        StartCoroutine(allSkills[1]);
                        yield return new WaitForSeconds(skillTime);
                        InCombat = false;
                        break;
                    case 3:
                        agent.ResetPath();
                        Debug.Log("Case 3");
                        StartCoroutine(allSkills[2]);
                        yield return new WaitForSeconds(skillTime);
                        InCombat = false;
                        break;
                }   
            }

            allSkills.Clear();
            SkillDataTake();
            StartCoroutine(Skills());
            StartCoroutine(HitSequence());
        }
    }

    float GetRandomValue()
    {
        float randomResult = 0f;

        do
        {
            randomResult = Random.Range(-50f, 50f);
        } while (randomResult >= -20f && randomResult <= 20f);

        return randomResult;
    }

    int GetRandomSkillValue()
    {
        int randomResult = 1;

        do
        {
            randomResult = Random.Range(1, 4);
        } while (randomResult == lastRandomValue);

        lastRandomValue = randomResult;
        
        return randomResult;

    }

    void rotationBoss()
    {
        Vector3 directionToPlayer = player.transform.position - transform.position;

        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 10 * Time.deltaTime);
    }

    void SkillDataTake()
    {
        SkillObjects._BossManager = this;

        if (BossData.RainOfAbundance == true)
        {
            allSkills.Add(SkillObjects.RainOfAbundanceSkill(player.gameObject));
        }

        if(BossData.JumpHigh == true)
        {
            allSkills.Add(SkillObjects.JumpHighSkill());
        }

        if(BossData.HungerDash == true)
        {
            allSkills.Add(SkillObjects.HungerDashing(player.gameObject));
        }
    }

    private void BossDataTake()
    {
        Health = BossData.Health;
        Damage = BossData.Damage;
        skillTime = BossData.SkillTime;
        agent.acceleration = BossData.Speed;
        agent.speed = BossData.Acceleration;
    }

    private void MoveUp()
    {
        Target = new Vector3(player.transform.position.x, transform.position.y + 70f, player.transform.position.z);
        transform.position = Vector3.Lerp(transform.position, Target, 3f * Time.deltaTime);
    }

    private void MoveDown()
    {
        if(transform.position.y >= 4)
        {
            Target = new Vector3(player.transform.position.x, transform.position.y - 70f, player.transform.position.z);
            transform.position = Vector3.Lerp(transform.position, Target, 3f * Time.deltaTime);
        }
    }

    private void MoveDash()
    {
        transform.Translate(transform.forward * 100f * Time.deltaTime, Space.World);
    }

    private void RegularHit()
    {
        if (stun == false)
        {
            transform.position = Vector3.Lerp(transform.position, TargetHit, 8 * Time.deltaTime);

            float distanceToTarget = Vector3.Distance(transform.position, TargetHit);

            _bossAnimation.boolParameter("Dash", true);   
            
            if (distanceToTarget < 4f || stun == true)
            {
                _gameManager.InHitSequence = false;
                StartCoroutine(waitAfterDash());
            }
        }
    }

    private IEnumerator HitSequence()
    {
        yield return new WaitForSeconds(Random.Range(3, 6));

        if (Health > 0 && InCombat == false && stun == false)
        {
            // GameObject newVFX = Instantiate(beforSlashVFX, transform.position, Quaternion.identity);
            //
            // newVFX.transform.parent = gameObject.transform;
            
            beforSlashVFX.SetActive(true);

            yield return new WaitForSeconds(1);

            // Destroy(newVFX);
            beforSlashVFX.SetActive(false);
            
            TargetHit = player.transform.position + (transform.forward * 10);
            Box.isTrigger = true;   

            if (InCombat == false && stun == false)
            {
                Instantiate(slashVFX, new Vector3(transform.position.x, 9.14f, transform.position.z), Quaternion.identity);
                Instantiate(HitVFX, transform.position, Quaternion.identity);
            }

            if (stun == false)
            {
                _gameManager.InHitSequence = true;   
            }
        }
    }

    private IEnumerator waitAfterDash()
    {
        _bossAnimation.boolParameter("Dash", false);
        Wait = true;
        yield return new WaitForSeconds(0.3f);
        Wait = false;
        Box.isTrigger = false;
        StartCoroutine(HitSequence());
    }

    IEnumerator Death()
    {
        _bossAnimation.boolParameter("Dead", death);
        agent.ResetPath();
        yield return new WaitForSeconds(2);
        Instantiate(deathVFX, new Vector3(transform.position.x, 4, transform.position.z), Quaternion.identity);
        _gameManager.audioSource.PlayOneShot(deathExplosion);
        yield return new WaitForSeconds(1.7f);
        transform.localScale = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(3f);
        _gameManager.loadScene(0);
    }
}
