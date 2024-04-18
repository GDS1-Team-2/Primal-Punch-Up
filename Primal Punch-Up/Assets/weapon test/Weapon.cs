using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private bool isPickUp = false;
    public bool IsPickUp => isPickUp;

    public int damage = 10;
    [Header("weapon time")]
    public float duration = 10;
    public int bulletNum = -1;
    public bool isGun = false;
    [Header("startpoint weapon")]
    public Transform bulletInitPos;
    public Bullet bullet;

    private float remainingDuration = 0;
    public PlayerBase PlayerBase { private set; get; }

    private void Start()
    {
        bullet.gameObject.SetActive(false);
        bullet.Init(this, damage);
    }

    public void PickUp(PlayerBase playerBase)
    {
        PlayerBase = playerBase;
        isPickUp = true;
        remainingDuration = duration;
        transform.GetComponentInChildren<BoxCollider>().enabled = false;
    }

    private void Update()
    {
        if (!isPickUp)
        {
            return;
        }
        remainingDuration -= Time.deltaTime;
        if (remainingDuration <= 0 || (isGun && bulletNum <= 0))
        {
            Destroy(gameObject);
            PlayerBase.weapon = null;
        }
    }

    public void AttackStart()
    {
        if (isGun)
        {
            bulletNum--;
            var newBullet = Instantiate(bullet, PlayerBase.transform.parent);
            newBullet.Init(this, damage);
            newBullet.gameObject.SetActive(true);
            newBullet.SetDir(PlayerBase.transform.rotation);
            newBullet.transform.position = bulletInitPos.position;
            //newBullet.transform.localScale = bullet.transform.localScale;
        }
        else
        {
            bullet.gameObject.SetActive(true);
        }
    }

    public void AttackEnd()
    {
        if (!isGun)
        {
            bullet.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerBase otherPlayer = other.gameObject.GetComponent<PlayerBase>();
        PlayerBase thisPlayer = PlayerBase;

        if (otherPlayer != null && otherPlayer != thisPlayer && !other.isTrigger)
        {
            StartCoroutine(otherPlayer.TakeDamage(damage));
            Debug.Log(otherPlayer.gameObject.name + " has been hit");
        }
    }
}
