using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifetime = 5f;
    [SerializeField] float firingRate = 0.2f;

    [Header("AI")]
    [SerializeField] float firingRateVariance = 0.5f;
    [SerializeField] float minimumFireRate = 0.1f;
    [SerializeField] bool isAI;
    
    [HideInInspector] public bool isShooting;
    Coroutine shootingCoroutine;
    AudioPlayer audioPlayer;

    private void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    void Update()
    {
        if (isAI)
        {
            isShooting = true;
        }

        Fire();
    }

    private void Fire()
    {
        if (isShooting && shootingCoroutine == null)
        {
            shootingCoroutine = StartCoroutine(FireContinuously());
        }
        else if(!isShooting && shootingCoroutine != null)
        {
            StopCoroutine(shootingCoroutine);
            shootingCoroutine = null;
        }
            
    }

    private float RandomProjectileSpawn()
    {
        float timeToNextProjectile = Random.Range(firingRate - firingRateVariance, firingRate + firingRateVariance);
        return Mathf.Clamp(timeToNextProjectile, minimumFireRate, float.MaxValue);
    }

    IEnumerator FireContinuously()
    {
        while(true)
        {
            GameObject instance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            Rigidbody2D rigidProjectile = instance.GetComponent<Rigidbody2D>();

            audioPlayer.PlayShootingClip();

            if (instance != null)
            {
                rigidProjectile.velocity = transform.up * projectileSpeed;
            }

            if (isAI)
                yield return new WaitForSeconds(RandomProjectileSpawn());
            else
                yield return new WaitForSeconds(firingRate);

            Destroy(instance, projectileLifetime);

        }
    }
}
