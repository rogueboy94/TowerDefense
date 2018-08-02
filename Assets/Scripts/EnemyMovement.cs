using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour {

    private Transform target;
    private Transform canvasTransform;

    private int wavepointIndex = 0;

    private Enemy enemy;

    public GameObject enemyPrefab;
    
    void Start()
    {
        target = WayPoints.way_points[0];
        enemy = GetComponent<Enemy>();
        canvasTransform = enemy.healthBar.GetComponentInParent<Canvas>().transform;
        canvasTransform.position = new Vector3(canvasTransform.position.x + .8f, canvasTransform.position.y, canvasTransform.position.z);
        canvasTransform.eulerAngles = new Vector3(40f, 0, 0);
    }

    void Update()
    {
        Vector3 direction = target.position - transform.position;
        transform.Translate(direction.normalized * enemy.speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
            GetNextWayPoint();


        enemy.speed = enemy.startSpeed;

		if (!enemy.slowed && GetComponentInChildren<Animator>() != null)
          GetComponentInChildren<Animator>().speed = enemy.speed;

        enemyPrefab.transform.LookAt(target);
    }

    void GetNextWayPoint()
    {
        if (wavepointIndex >= WayPoints.way_points.Length - 1)
        {
            EndPath();
        }
        wavepointIndex++;
        target = WayPoints.way_points[wavepointIndex];
    }

    void EndPath()
    {
        PlayerStats.Lives--;
        WaveSpawner.EnemiesAlive--;
        Destroy(gameObject);
    }
}
