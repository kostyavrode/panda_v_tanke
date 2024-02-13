using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TankAtack : MonoBehaviour
{
    [SerializeField] private Material[] skins;
    [SerializeField] private MeshRenderer tankMat;
    [SerializeField] private MeshRenderer tankMat2;
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private Transform bulletStack;
    [SerializeField] private int bulletsCount;
    [SerializeField] private float recoil;
    [SerializeField] private float delayBetweenShoots;
    private bool canShoot = true;
    //private float timeAfterShoot;
    private int remainingsBullets;
    private void Awake()
    {
        if (PlayerPrefs.HasKey("Buy1"))
        {
            tankMat.material = skins[0];
            tankMat2.material = skins[0];
        }
        if (PlayerPrefs.HasKey("Buy1"))
        {
            tankMat2.material = skins[1];
            tankMat.material = skins[1];
        }
    }
    private void Start()
    {
        remainingsBullets = bulletsCount;
    }
    public void Fire()
    {
        if (canShoot)
        {
            SetCooldown();
            remainingsBullets--;
            Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity, bulletStack);
            if (remainingsBullets < 1)
            {
                transform.DOMoveZ(this.transform.position.z - recoil, delayBetweenShoots).SetLoops(2, LoopType.Yoyo).OnComplete(ReloadBullets);
                rigidbody.angularVelocity = new Vector3(0f, 0f, 0f);
            }
            else
            {
                transform.DOMoveZ(this.transform.position.z - recoil, delayBetweenShoots).SetLoops(2, LoopType.Yoyo).OnComplete(ResetCooldown);
            }

        }
    }
    private void ResetCooldown()
    {
        canShoot = true;
        gameObject.transform.DOLocalRotate(new Vector3(0f, 0f, 0f),0.01f);
        rigidbody.angularVelocity = new Vector3(0f, 0f, 0f);
    }
    private void SetCooldown()
    {
        canShoot = false;
    }
    private void ReloadBullets()
    {
        gameObject.transform.DOLocalRotate(new Vector3(0f, 180f, 0f)*RandomDirection(), 0.4f).SetEase(Ease.OutSine).SetLoops(2, LoopType.Yoyo).OnComplete(ResetCooldown);
        remainingsBullets = bulletsCount;
    }
    private int RandomDirection()
    {
        switch (Random.Range(1, 3))
        {
            case 1:
                return -1;
            case 2:
                return 1;
            default:
                { Debug.Log("WTF CHISLO NE TO"); return 1; }
        }
    }
}
