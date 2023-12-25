using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BossManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Slider slider;
    [SerializeField] public NavMeshAgent agent;
    [SerializeField] GameObject player;

    [Header("Data")]
    [SerializeField] BossData BossData;
    [SerializeField] SkillsData SkillObjects;

    [Header("Variables")]
    [HideInInspector] public float Health;
    [HideInInspector] public float Damage;

    [HideInInspector] public bool InCombat = false;

    Vector3 locat;
    Vector3 Target;

    int skillTime;

    List<IEnumerator> allSkills = new List<IEnumerator>();

    void Start()
    {
        BossDataTake();
        SkillDataTake();
        slider.maxValue = Health;
        StartCoroutine(Skills());
    }

    void Update()
    {
        slider.value = Health;
        locat = new Vector3(player.transform.position.x + GetRandomValue(), 0, player.transform.position.z + GetRandomValue());
    }

    private void FixedUpdate()
    {
        if(InCombat == false)
        {
            agent.SetDestination(locat);
        }
    }

    private void LateUpdate()
    {
        if (SkillObjects.UpToBoss == true)
        {
            MoveUp();
        }

        if (SkillObjects.DownToBoss == true)
        {
            MoveDown();
        }

        rotationBoss();
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Magic")
        {
            Health -= 10;
            Debug.Log("HÝT!");
        }
    }

    IEnumerator Skills()
    {
        yield return new WaitForSeconds(Random.Range(6,10));

        InCombat = true;

        int SkillChoose = Random.Range(1, 3);

        switch(SkillChoose)
        {
            case 1:
                agent.SetDestination(gameObject.transform.position);
                Debug.Log("Case 1");
                StartCoroutine(allSkills[0]);
                yield return new WaitForSeconds(skillTime);
                InCombat = false;
                break;
            case 2:
                agent.SetDestination(gameObject.transform.position);
                Debug.Log("Case 2");
                StartCoroutine(allSkills[1]);
                yield return new WaitForSeconds(skillTime);
                InCombat = false;
                break;
            case 3:
                agent.SetDestination(gameObject.transform.position);
                Debug.Log("Case 3");
                StartCoroutine(allSkills[2]);
                yield return new WaitForSeconds(skillTime);
                InCombat = false;
                break;
        }

        allSkills.Clear();
        SkillDataTake();
        StartCoroutine(Skills());
    }

    float GetRandomValue()
    {
        float randomResult = 0f;

        do
        {
            randomResult = Random.Range(-30f, 30f);
        } while (randomResult >= -15f && randomResult <= 15f);

        return randomResult;
    }

    void rotationBoss()
    {
        Vector3 Rotation = new Vector3(player.transform.position.x, gameObject.transform.position.y, player.transform.position.z);


        gameObject.transform.LookAt(Rotation);
    }

    void SkillDataTake()
    {
        SkillObjects._BossManager = this;

        if (BossData.RainOfAbundance == true)
        {
            allSkills.Add(SkillObjects.RainOfAbundanceSkill(player));
        }

        if(BossData.JumpHigh == true)
        {
            allSkills.Add(SkillObjects.JumpHighSkill(player));
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
}
