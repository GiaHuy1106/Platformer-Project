using UnityEngine;

public class bossScipts : MonoBehaviour
{
    public Transform rayCast;
    public LayerMask raycastMask;
    public float rayCastLength;
    public float attackDistance; // Distance at which the enemy will start attacking
    public float moveSpeed;
    public float timer; // Timer to control attack intervals
    public Transform playerTransform;
    public bool isChasing;
    public float chaseRange;

    private RaycastHit2D hitInfo;
    private Animator anim;
    private bool attackMode;
    private GameObject target;
    private float intTimer;
    private bool inRange;
    private bool cooling; // Flag to indicate if the enemy is cooling down after an attack
    private float distance; // Distance between enemy and player

    //[serializeField] private AudioSource damageSound;
    //private AudioSource deathSound;

    private void Awake()
    {
        intTimer = timer;
        anim = GetComponent<Animator>();
        //deathSound = GetComponent<AudioSource>();
    }

    private void Update()
    {

        if (isChasing)
        {
            if (transform.position.x > playerTransform.position.x)
            {
                // Quay mặt sang trái
                transform.localScale = new Vector3(-1, 1, 1);

                // Hướng raycast sang trái
                rayCast.localPosition = new Vector3(-Mathf.Abs(rayCast.localPosition.x), rayCast.localPosition.y, 0);

                transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            }
            if (transform.position.x < playerTransform.position.x)
            {
                // Quay mặt sang phải
                transform.localScale = new Vector3(1, 1, 1);

                // Hướng raycast sang phải
                rayCast.localPosition = new Vector3(Mathf.Abs(rayCast.localPosition.x), rayCast.localPosition.y, 0);

                transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            }
        }
        else
        {
            if (Vector2.Distance(transform.position, playerTransform.position) < chaseRange)
            {
                isChasing = true;
                target = playerTransform.gameObject;
                anim.SetBool("Run", true);
            }
        }

        if (inRange)
        {
            hitInfo = Physics2D.Raycast(rayCast.position, Vector2.left, rayCastLength, raycastMask);
            rayCastDebugger();
        }


        if (hitInfo.collider != null)
        {
            enemyLogic();
        }
        else if (hitInfo.collider == null)
        {
            inRange = false;

        }

        if (!inRange)
        {
            anim.SetBool("Run", false);
            stopAttack();
        }
    }

    void enemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.transform.position);
        if (distance > attackDistance)
        {
            move();
            stopAttack();
        }
        else if (attackDistance >= distance && cooling == false)
        {
            Attack();
        }
        if (cooling)
        {
            anim.SetBool("Fight", false);
        }
    }

    private void move()
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Fight"))
        {
            Vector2 targetPos = new Vector2(target.transform.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
        }
    }

    void Attack()
    {
        timer = intTimer;
        attackMode = true;
        anim.SetBool("Run", false);
        anim.SetBool("Fight", true);
    }

    void stopAttack()
    {
        cooling = false;
        attackMode = false;
        anim.SetBool("Fight", false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            target = collision.gameObject;
            inRange = true;
        }
    }

    void rayCastDebugger()
    {
        if (distance > attackDistance)
        {
            Debug.DrawRay(rayCast.position, Vector2.left * rayCastLength, Color.red);
        }
        else
        {
            Debug.DrawRay(rayCast.position, Vector2.left * rayCastLength, Color.green);
        }
    }
}
