using UnityEngine;

public class Bullet : MonoBehaviour {

    private Transform target;
    public float speed = 70f;

    public float explosionRadius = 0f;

    public int damage = 50;

    public GameObject impactEffect;

    public void Seek(Transform _target)
    {
        target = _target;
    }

	// Update is called once per frame
	void Update () {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }
        
        /*Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(gameObject.transform.rotation, lookRotation, Time.deltaTime * 10f).eulerAngles;
        gameObject.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        */
        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }

    void HitTarget()
    {
        GameObject effectInstance = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectInstance, 5f);

        if(explosionRadius > 0f)
        {
            Explode();
        }
        else
        {
            Damage(target);
        }

        Destroy(gameObject);
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
            if (collider.tag == "Enemy")
                Damage(collider.transform);
    }

    void Damage(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();

        if(e != null)
            e.TakeDamage(damage);        
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
