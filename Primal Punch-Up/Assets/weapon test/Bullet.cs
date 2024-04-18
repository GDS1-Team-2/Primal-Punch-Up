using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float maxFlyTime = 5;
    public float flySpeed = 0;

    private Weapon weapon;
    private int damage;

    public void Init(Weapon weapon, int damage)
    {
        this.weapon = weapon;
        this.damage = damage;
    }

    public void SetDir(Quaternion dir)
    {
        transform.rotation = dir;
    }

    void Update()
    {
        if (!weapon.isGun)
        {
            return;
        }
        transform.position += transform.forward * flySpeed * Time.deltaTime;
        maxFlyTime -= Time.deltaTime;
        if (maxFlyTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerBase playerBase = other.GetComponent<PlayerBase>();
        if (playerBase != null)
        {
            if (weapon.PlayerBase == playerBase)
            {
                return;
            }
            weapon.PlayerBase.StartCoroutine(playerBase.TakeDamage(damage));
        }
        if (weapon.isGun)
        {
            Destroy(gameObject);
        }
    }
}
