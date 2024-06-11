using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Player owner;
    private Transform target;
    public GameObject projectile;
    private Rigidbody rb;
    public float throwSpeed = 8f;
    public float spinSpeed = 7f;
    public int scoreUpgrade = 1;
    public float sizeUpgrade = 0.5f;
    public Vector3 throwDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }
        throwDirection = target.transform.position - transform.position;
        rb.linearVelocity = new Vector3(throwDirection.x, throwDirection.y, throwDirection.z).normalized * throwSpeed;
    }

    void Update()
    {
        this.transform.Rotate(0f, 0f, spinSpeed);
    }

    public void SetOwner(Player owner)
    {
        this.owner = owner;
    }

    public void Seek(Transform _Target)
    {
        target = _Target;
    }

    public void Damage(GameObject obj)
    {
        Player p = obj.GetComponent<Player>();
        p.Dead();
    }

    public void Scoring()
    {
        if (owner != null)
        {
            owner.ScoreChange(1);
            owner.SizeChange(0.5f);
            owner.RangeChange(2f);
            owner.CamChange(8f);
            owner.StatusChange(0.5f);
            owner.WeaponSizeChange(20f);
        }
        else
        {
            Debug.Log("Fail!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && other.gameObject.name != "Player")
        {
            if (!owner)
            {
                return;
            }
            Scoring();
            owner.DestroyProjectile(this);
            Destroy(gameObject);
            Damage(other.gameObject);
            other.gameObject.tag = "Untagged";
            Destroy(other.gameObject, 1.5f);
        }
        else if (other.gameObject.CompareTag("Player") && other.gameObject.name == "Player")
        {
            if (!owner)
            {
                return;
            }
            Scoring();
            owner.DestroyProjectile(this);
            Destroy(gameObject);
            Damage(other.gameObject);
            other.gameObject.tag = "Untagged";
        }
        else return;
    }
}