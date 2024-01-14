using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Skills", menuName = "AllSkills")]
public class SkillsData : ScriptableObject
{
    [Header("VFX Objects")]
    [SerializeField] GameObject rainObject;
    [SerializeField] GameObject JumpImpact;
    [SerializeField] GameObject DownImpact;
    [SerializeField] GameObject DownImpactText;
    [SerializeField] GameObject AppearShadow;
    [SerializeField] GameObject DashImpact;
    [SerializeField] GameObject DashCenterImpact;
    [SerializeField] GameObject HealVFX;

    [Header("Sounds")]
    [SerializeField] AudioClip[] RainOfAbundanceSound;
    [SerializeField] AudioClip[] JumpHighSound;
    [SerializeField] AudioClip[] HungerDashSound;

    [HideInInspector] public BossManager _BossManager;
    [HideInInspector] public GameManager _gameManager;
    [HideInInspector] public BossAnimation _bossAnimation;

    public IEnumerator RainOfAbundanceSkill(GameObject player)
    {
        GameObject VFX = Instantiate(HealVFX, _BossManager.transform.position, Quaternion.identity);
        while (_BossManager.InCombat && _BossManager.Health > 0)
        {
            _bossAnimation.boolParameter("RainOfAbundance", true);
            Vector3 rainTransform = new Vector3(player.transform.position.x + Random.Range(-6, 6), 30, player.transform.position.z + Random.Range(-6, 6));
            GameObject.Instantiate(rainObject, rainTransform, Quaternion.Euler(0, 0, 0));
            _BossManager.Health += 0.4f;
            yield return new WaitForSeconds(0.2f);
        }

        Destroy(VFX);
        _bossAnimation.boolParameter("RainOfAbundance", false);
    }

    public IEnumerator JumpHighSkill()
    {
        _bossAnimation.boolParameter("JumpImpact", true);
        yield return new WaitForSeconds(1f);
        JumpImpact.transform.localScale = new Vector3(6, 6, 6);
        Vector3 JumpTransform = new Vector3(_BossManager.transform.position.x, 1, _BossManager.transform.position.z);
        GameObject.Instantiate(JumpImpact, JumpTransform, Quaternion.Euler(0, 0, 0));
        _gameManager.audioSource.PlayOneShot(JumpHighSound[0]);

        AppearShadow.transform.localScale = new Vector3(3, 3, 3);
        GameObject shadow = GameObject.Instantiate(AppearShadow, _BossManager.transform.position, Quaternion.Euler(-90, 0, 0));

        while (true)
        {
           
            if (_BossManager.gameObject.transform.position.y <= 500)
            {
                _BossManager.agent.enabled = false;
                _gameManager.UpToBoss = true;
            }
            else
            {
                _gameManager.UpToBoss = false;
                break;
            }
            yield return new WaitForSeconds(0.1f);
        }

        _bossAnimation.boolParameter("JumpImpact", false);
        while (true)
        {
            _gameManager.DownDamage = true;
            _BossManager.Box.isTrigger = true;
            if (_BossManager.gameObject.transform.position.y >= 4)
            {
                _BossManager.agent.enabled = false;
                _gameManager.DownToBoss = true;
            }
            else
            {
                _bossAnimation.boolParameter("DownImpact", true);
                _gameManager.DownToBoss = false;
                _BossManager.agent.enabled = true;
                break;
            }
            yield return new WaitForSeconds(0.01f);
        }
        Destroy(shadow);

        _gameManager.audioSource.PlayOneShot(JumpHighSound[1]);

        DownImpact.transform.localScale = new Vector3(3, 3, 3);
        DownImpactText.transform.localScale = new Vector3(3, 3, 3);

        Vector3 DownTransform = new Vector3(_BossManager.transform.position.x, 1, _BossManager.transform.position.z);
        Vector3 DownTextTransform = new Vector3(_BossManager.transform.position.x + 4, 8, _BossManager.transform.position.z + 4);

        GameObject.Instantiate(DownImpact, DownTransform, Quaternion.Euler(0, 0, 0));
        GameObject.Instantiate(DownImpactText, DownTextTransform, Quaternion.Euler(0, 0, 0));

        yield return new WaitForSeconds(1f);
        _bossAnimation.boolParameter("DownImpact", false);
        _gameManager.DownDamage = false;
        _BossManager.Box.isTrigger = false;
    }

     public IEnumerator HungerDashing(GameObject player)
    {
        float timer = 4f;
        _gameManager.audioSource.PlayOneShot(HungerDashSound[0]);

        _gameManager.DashStart = true;

        yield return new WaitForSeconds(2f);

        while(true)
        {
            DashImpact.transform.localScale = new Vector3(4, 4, 4);
            Vector3 DashTransform = new Vector3(_BossManager.transform.position.x, 1, _BossManager.transform.position.z);
            GameObject.Instantiate(DashImpact, DashTransform, Quaternion.Euler(0, 0, 0));

            timer -= 1f;
            _gameManager.DashDamage = true;
            _gameManager.DashBoss = true;
            _BossManager.Box.isTrigger = true;
            _BossManager.Box.size = new Vector3(2, 2, 1.3f);

            if (timer < 0f || _BossManager.Health <= 0)
            {
                break;
            }

            yield return new WaitForSeconds(0.1f);
        }

        _gameManager.audioSource.PlayOneShot(HungerDashSound[1]);

        _gameManager.DashBoss = false;
        DashCenterImpact.transform.localScale = new Vector3(5, 5, 5);
        Vector3 DashCenterTransform = new Vector3(_BossManager.transform.position.x, 1, _BossManager.transform.position.z);
        GameObject.Instantiate(DashCenterImpact, DashCenterTransform, Quaternion.Euler(-90,0,0));

        yield return new WaitForSeconds(0.4f);
        _BossManager.Box.size = new Vector3(1.4f, 2, 1.3f);
        _BossManager.Box.isTrigger = false;
        _gameManager.DashDamage = false;
    }
}
