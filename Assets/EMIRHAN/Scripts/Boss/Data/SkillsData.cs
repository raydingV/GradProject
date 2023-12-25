using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "Skills", menuName = "AllSkills")]
public class SkillsData : ScriptableObject
{
    [SerializeField] GameObject rainObject;
    [SerializeField] GameObject JumpImpact;
    [SerializeField] GameObject DownImpact;
    [SerializeField] GameObject AppearShadow;

    [HideInInspector] public BossManager _BossManager;

    public bool UpToBoss = false;
    public bool DownToBoss = false;



    public IEnumerator RainOfAbundanceSkill(GameObject player)
    {
        while (_BossManager.InCombat)
        {
            Vector3 rainTransform = new Vector3(player.transform.position.x + Random.Range(-6, 6), 30, player.transform.position.z + Random.Range(-6, 6));
            GameObject.Instantiate(rainObject, rainTransform, Quaternion.Euler(0, 0, 0));
            _BossManager.Health += 0.4f;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public IEnumerator JumpHighSkill(GameObject player)
    {
        JumpImpact.transform.localScale = new Vector3(6, 6, 6);
        Vector3 JumpTransform = new Vector3(_BossManager.transform.position.x, 1, _BossManager.transform.position.z);
        GameObject.Instantiate(JumpImpact, JumpTransform, Quaternion.Euler(0, 0, 0));

        AppearShadow.transform.localScale = new Vector3(3, 3, 3);
        GameObject shadow = GameObject.Instantiate(AppearShadow, _BossManager.transform.position, Quaternion.Euler(-90, 0, 0));

        while (true)
        {
           
            if (_BossManager.gameObject.transform.position.y <= 500)
            {
                _BossManager.agent.enabled = false;
                UpToBoss = true;
            }
            else
            {
                UpToBoss = false;
                break;
            }
            yield return new WaitForSeconds(0.1f);
        }


        while (true)
        {
            
            if (_BossManager.gameObject.transform.position.y >= 4)
            {
                _BossManager.agent.enabled = false;
                DownToBoss = true;
            }
            else
            {
                DownToBoss = false;
                _BossManager.agent.enabled = true;
                break;
            }
            yield return new WaitForSeconds(0.1f);
        }

        Destroy(shadow);

        DownImpact.transform.localScale = new Vector3(3, 3, 3);
        Vector3 DownTransform = new Vector3(_BossManager.transform.position.x, 1, _BossManager.transform.position.z);
        GameObject.Instantiate(DownImpact, DownTransform, Quaternion.Euler(0, 0, 0));
    }
}
