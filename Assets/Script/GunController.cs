using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunController : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public Camera GunCamera;
    public ParticleSystem muzzlePoint;
    public GameObject GunEffect;
    public float impactForce = 30f;
    public float fireRate = 15f;
    private float CD_Shooting = 0f;
    public int CurrentAmmo = 0;
    public int MaxAmmo = 5;
    public float reloadtime = 1f;
    private bool isReloading;
    public Animator GunAnimator;
    public Text Ammo;
    public AudioSource ShootAudio, ReloadSound;
    // Start is called before the first frame update
    void Start()
    {
        CurrentAmmo = MaxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        if (isReloading)
            return;
        if(CurrentAmmo <= 0)
        {
            StartCoroutine(AmmoReload());
            
            return;
        }
        if (Input.GetButton("Fire1") && Time.time >= CD_Shooting)
        {
            CD_Shooting = Time.time + 1f / fireRate;
            Shooting();
        }
        Ammo.text = CurrentAmmo.ToString() + " / " + MaxAmmo.ToString();
    }
    void Shooting()
    {

        muzzlePoint.Play();
        ShootAudio.Play();
        GunAnimator.SetTrigger("Shoot");

        CurrentAmmo--;
        RaycastHit hit;
        if(Physics.Raycast(GunCamera.transform.position, GunCamera.transform.forward, out hit, range))
        {

            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if(enemy != null)
            {
                enemy.OnDamage(damage);
            }
            if(hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
            GameObject tempGunEffect = Instantiate(GunEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(tempGunEffect, 2f);

        }

    }

    IEnumerator AmmoReload()
    {
        ReloadSound.Play();
        isReloading = true;
        GunAnimator.SetTrigger("DoReload");
        yield return new WaitForSeconds(reloadtime);
        CurrentAmmo = MaxAmmo;
        isReloading = false;
    }
}
