using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyPatrol : MonoBehaviour
{
    public GameObject PointA;
    public GameObject PointB;

    Rigidbody2D rigidb;
    public float speed;
    public Transform goingTowards;
    float direction;

    void Start()
    {
        rigidb = GetComponent<Rigidbody2D>();
        goingTowards = PointA.transform;
    }
    void OnEnable()
    {
        rigidb = GetComponent<Rigidbody2D>();
        rigidb.linearVelocity = Vector2.zero;
    }

    void FixedUpdate()
    {
        if (goingTowards == PointA.transform)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }

        rigidb.linearVelocity = new Vector2(speed * direction, rigidb.linearVelocity.y);

        if (math.abs(transform.position.x - goingTowards.transform.position.x) < 0.5f)
        {
            transform.position = new Vector3(
                goingTowards.transform.position.x,
                transform.position.y,
                transform.position.z
            );
            if (goingTowards == PointA.transform)
            {
                goingTowards = PointB.transform;
            }
            else if (goingTowards == PointB.transform)
            {
                goingTowards = PointA.transform;
            }


        }
    }


}
