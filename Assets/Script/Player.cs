using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Stat")]
    public string nameTag;
    public int score;
    public float size;
    public float cameraChange = 0f;
    public float statusPosition = 0f;
    public float movementSpeed = 6f;
    public float rotateSpeed = 720f;
    public float range = 7f;
    public float weaponSize = 80f;

    [Header("Other")]
    public GameObject weaponHold;
    public GameObject targetCircle;
    public FloatingStatus statusUI;
    private List<Projectile> projectiles = new List<Projectile>();
    public Transform target;
    public Transform playerBody;
    public Transform throwLocation;
    public GameObject projectile;
    public bool inRange = false;
    public bool isDead = false;
    private bool isThrowed = false;

    void Start()
    {
        statusUI = GetComponentInChildren<FloatingStatus>();
        statusUI.GetPlayerNameUI(nameTag);
        statusUI.GetPlayerScoreUI(score.ToString());
        score = 0;
        size = 2f;
        InvokeRepeating("UpdateTarget", 0f, 0.01f);
    }

    void Update()
    {
        if (target == null) return;
    }

    void UpdateTarget()
    {
        GameObject nearestEnemy = null;
        GameObject[] players = PlayerAlive.playerList;
        if (players == null) return;
        
        float shortestDistance = Mathf.Infinity;

        foreach (GameObject enemy in players)
        {
            if (enemy == gameObject) continue;
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
                targetCircle.transform.position = enemy.transform.position;
            }
        }

        if (nearestEnemy != null && shortestDistance < this.range)
        {
            targetCircle.SetActive(true);
            inRange = true;
            target = nearestEnemy.transform;
        }
        else
        {
            targetCircle.SetActive(false);
            inRange = false;
            target = null;
        }
    }

    public void SpawnProjectile()
    {
        if (target != null)
        {
            if (isThrowed == false)
            {
                isThrowed = true;
                weaponHold.SetActive(false);
                if (throwLocation == null || projectile == null) return;
                GameObject weaponObject = Instantiate(projectile, throwLocation.position, throwLocation.rotation);
                weaponObject.transform.localScale = new Vector3(weaponSize, weaponSize, weaponSize);
                Projectile weapon = weaponObject.GetComponent<Projectile>();
                weapon.SetOwner(this);
                projectiles.Add(weapon);

                if (weapon != null)
                {
                    weapon.Seek(target);
                }

                Destroy(weaponObject, 1.5f);
                StartCoroutine(ThrowDelay());
            }
        }
    }

    public void DestroyProjectile(Projectile destroyProjectile)
    {
        projectiles.Remove(destroyProjectile);
    }

    public void RotateToEnemy()
    {
        if (target == null) return;
        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = lookRotation.eulerAngles;

        if (playerBody != null)
        {
            playerBody.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        }
    }

    public void Dead()
    {
        PlayerAlive.playerDead = true;
        isDead = true;
    }

    public void RangeChange(float value)
    {
        range += value;
    }

    public void ScoreChange(int value)
    {
        score += value;
        statusUI.GetPlayerScoreUI(score.ToString());
    }

    public void SizeChange(float value)
    {
        size += value;
        transform.localScale = new Vector3(size, size, size);
    }

    public void WeaponSizeChange(float value)
    {
        weaponSize += value;
    }

    public void CamChange(float value)
    {
        cameraChange += value;
    }

    public void StatusChange(float value)
    {
        statusPosition += value;
    }

    IEnumerator ThrowDelay()
    {
        yield return new WaitForSeconds(0.5f);
        weaponHold.SetActive(true);
        isThrowed = false;
    }
}
