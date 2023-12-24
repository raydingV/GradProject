using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BossManager : MonoBehaviour
{

    [SerializeField] Slider slider;
    float Health = 100;

    float Damage;

    [SerializeField] NavMeshAgent agent;

    [SerializeField] GameObject player;
    [SerializeField] GameObject rainObject;

    Vector3 locat;

    bool InCombat = false;

    void Start()
    {
        StartCoroutine(Skills());
        agent.acceleration = 40;
        agent.speed = 40;
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

        int SkillChoose = 1;

        switch(SkillChoose)
        {
            case 1:
                agent.SetDestination(gameObject.transform.position);
                Debug.Log("Case 1");
                StartCoroutine(RainSkill());
                yield return new WaitForSeconds(8);
                InCombat = false;
                break;
            case 2:
                break;
            case 3:
                break;
        }
        agent.SetDestination(player.transform.position);
        agent.speed = 80;

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

    IEnumerator RainSkill()
    {
        while(InCombat)
        {
            Vector3 rainTransform = new Vector3(player.transform.position.x + Random.Range(-6, 6), 30, player.transform.position.z + Random.Range(-6, 6));
            GameObject.Instantiate(rainObject, rainTransform, Quaternion.Euler(0, 0, 0));
            Health += 0.4f;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
