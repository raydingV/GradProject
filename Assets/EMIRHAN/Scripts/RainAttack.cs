using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainAttack : MonoBehaviour
{
    [SerializeField] GameObject Explosion;
    [SerializeField] GameObject Target;

    [SerializeField] int DownSpeed;

    [SerializeField] PlayerManager playerManager;

    [SerializeField] AudioClip HitSound;

    [SerializeField] AudioSource audioSource;

    public bool needDestroy = false;

    private void FixedUpdate()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, (gameObject.transform.position.y - DownSpeed * Time.deltaTime), gameObject.transform.position.z);   
    }

    private void OnTriggerEnter(Collider other)
    {
        Vector3 transform = new Vector3(gameObject.transform.position.x, 0.5f, gameObject.transform.position.z);

        if(other.tag == "Plane")
        {
            audioSource = other.gameObject.gameObject.GetComponent<AudioSource>();
            audioSource.PlayOneShot(HitSound);
            GameObject.Instantiate(Explosion, gameObject.transform.position, Quaternion.Euler(0,0,0));
            Destroy(gameObject);
            needDestroy = true;
        }

        if(other.tag == "Player")
        {
            playerManager = other.gameObject.GetComponent<PlayerManager>();
            playerManager._gameManager.audioSource.PlayOneShot(HitSound);
            GameObject.Instantiate(Explosion, gameObject.transform.position, Quaternion.Euler(0, 0, 0));
            playerManager.playerHealth -= 6;
            needDestroy = true;
        }

    }
}
