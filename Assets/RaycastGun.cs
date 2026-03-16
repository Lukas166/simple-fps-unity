using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RaycastGun : MonoBehaviour
{
    public Camera playerCamera;
    public Transform laserOrigin;

    public float gunRange = 50f;
    public float fireRate = 0.2f;
    public float laserDuration = 0.05f;

    public AudioSource gunAudio;
    public AudioClip gunShot;

    public ParticleSystem muzzleFlash;

    // LIGHT FLASH
    public Light muzzleLight;
    public float lightDuration = 0.02f;

    public Transform gunTransform;
    public float recoilAmount = 0.05f;
    public float recoilSpeed = 10f;

    Vector3 originalPosition;

    LineRenderer laserLine;
    float fireTimer;

    void Awake()
    {
        laserLine = GetComponent<LineRenderer>();
        originalPosition = gunTransform.localPosition;

        if(muzzleLight != null)
            muzzleLight.enabled = false;
    }

    void Update()
    {
        fireTimer += Time.deltaTime;

        if(Input.GetButtonDown("Fire1") && fireTimer > fireRate)
        {
            fireTimer = 0;
            Shoot();
        }

        HandleRecoil();
    }

    void Shoot()
    {
        // SOUND
        if(gunAudio && gunShot)
            gunAudio.PlayOneShot(gunShot);

        // PARTICLE
        if(muzzleFlash)
            muzzleFlash.Play();

        // LIGHT FLASH
        if(muzzleLight)
            StartCoroutine(MuzzleLightFlash());

        // RECOIL
        gunTransform.localPosition -= new Vector3(0,0,recoilAmount);

        laserLine.SetPosition(0, laserOrigin.position);

        Vector3 rayOrigin = playerCamera.ViewportToWorldPoint(new Vector3(0.5f,0.5f,0));

        RaycastHit hit;

        if(Physics.Raycast(rayOrigin, playerCamera.transform.forward, out hit, gunRange))
        {
            laserLine.SetPosition(1, hit.point);

            // PENAMBAHAN KODE ADA DI BLOK INI
            if(hit.transform.CompareTag("target"))
            {
                // 1. Lapor ke GameManager agar skor bertambah dan stage terupdate
                if (GameManager.instance != null)
                {
                    GameManager.instance.TargetDestroyed();
                }

                // 2. Hancurkan objek target
                Destroy(hit.transform.gameObject);
            }
        }
        else
        {
            laserLine.SetPosition(1, rayOrigin + (playerCamera.transform.forward * gunRange));
        }

        StartCoroutine(ShootLaser());
    }

    IEnumerator MuzzleLightFlash()
    {
        muzzleLight.enabled = true;
        yield return new WaitForSeconds(lightDuration);
        muzzleLight.enabled = false;
    }

    void HandleRecoil()
    {
        gunTransform.localPosition = Vector3.Lerp(
            gunTransform.localPosition,
            originalPosition,
            Time.deltaTime * recoilSpeed
        );
    }

    IEnumerator ShootLaser()
    {
        laserLine.enabled = true;
        yield return new WaitForSeconds(laserDuration);
        laserLine.enabled = false;
    }
}