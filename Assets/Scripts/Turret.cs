using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class Turret : MonoBehaviour {

    private Transform target;
    private Enemy enemy;

    [Header("General")]
    public float range = 10f;

    [Header("Bullets")]
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("Laser")]
    public bool useLaser = false;
    public LineRenderer lineRenderer;
    public ParticleSystem laserImpactEffect;
    public Light laserImpactLight;
    public int damageOverTime = 30;
    public float slowAmount = 1f;

    [Header("Unity Setup Fields")]
    public string enemyTag = "Enemy";

    public Transform partToRotate;
    public float turnSpeed = 10f;

    public GameObject bulletPrefab;
    public Transform[] firePoint;
    GameObject[] enemies;
    GameObject nearestEnemy = null;


    void Awake()
    {
        foreach (var item in firePoint)
            if (item == null)
            {
                throw new ApplicationException("ОШИБКА!!! Transform не должен быть пустым!!!");
            }
    }

	// Use this for initialization
	void Start () {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        slowAmount /= 10;
	}

    void UpdateTarget()
    {
        enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                enemy.GetComponent<Enemy>().slowed = false;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            enemy = nearestEnemy.GetComponent<Enemy>();
        }
        else
        {
            target = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            if (useLaser)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    laserImpactEffect.Stop();
                    laserImpactLight.enabled = false;
                }
            }
            return;
        }
        //Target LockOn

        LockOnTarget();

        if (useLaser)
        {
            Laser();
        }
        else
        {
            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }

            fireCountdown -= Time.deltaTime;
        }
    }

    void LockOnTarget()
    {
        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Laser()
    {
        enemy.TakeDamage(damageOverTime * Time.deltaTime);
        enemy.Slow(slowAmount);

        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            laserImpactEffect.Play();
            laserImpactLight.enabled = true;
        }

        lineRenderer.SetPosition(0, firePoint[0].position);
        lineRenderer.SetPosition(1, target.position);

        Vector3 direction = firePoint[0].position - target.position;

        laserImpactEffect.transform.position = target.position + direction.normalized;
        laserImpactEffect.transform.rotation = Quaternion.LookRotation(direction);

    }

    void Shoot()
    {
        GameObject[] bulletGO = new GameObject[firePoint.Length];
        Bullet[] bullet = new Bullet[bulletGO.Length];

        for (int i = 0; i < firePoint.Length; i++)
        {
            bulletGO[i] = Instantiate(bulletPrefab, firePoint[i].position, firePoint[i].rotation);
            bullet[i] = bulletGO[i].GetComponent<Bullet>();
            if (bullet[i] != null)
                bullet[i].Seek(target);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
