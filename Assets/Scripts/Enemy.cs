using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : LivingEntity
{
    private readonly int hashMove = Animator.StringToHash("Move");
    private readonly int hashDie = Animator.StringToHash("Die");
    private readonly string player = "Player";
    public LayerMask targetLayer;
    public EnemyData enemyData;
    public ParticleSystem hitEffect;

    public float findTargetDistance = 10f;

    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private AudioSource audioSource;
    private Collider col;
    private Rigidbody rb;
    private GameManager gm;

    private LivingEntity target;

    private Coroutine coUpdatePath;

    public bool HasTarget
    {
        get
        {
            return target != null && !target.IsDead;
        }
    }

    private float lastAttackTime;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        col = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    private void Update()
    {
        animator.SetBool(hashMove, navMeshAgent.velocity.magnitude > 0.01f);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        navMeshAgent.enabled = true;
        coUpdatePath = StartCoroutine(CoUpdatePath());

        lastAttackTime = 0f;

        col.enabled = true;
    }

    private IEnumerator CoUpdatePath()
    {
        while (true)
        {
            if (!HasTarget)
            {
                navMeshAgent.isStopped = true;
                target = FindTarget();
            }

            if (HasTarget)
            {
                navMeshAgent.isStopped = false;
                navMeshAgent.SetDestination(target.transform.position);
            }

            yield return new WaitForSeconds(0.25f);
        }
    }

    protected void OnDisable()
    {
        coUpdatePath = null;
        target = null;
    }

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        base.OnDamage(damage, hitPoint, hitNormal);

        hitEffect.transform.position = hitPoint;
        hitEffect.transform.rotation = Quaternion.LookRotation(hitNormal);
        hitEffect.Play();

        audioSource.PlayOneShot(enemyData.hurtSound);
    }

    public override void Die()
    {
        base.Die();
        StopCoroutine(coUpdatePath);
        gm.AddScore(enemyData.score);

        audioSource.PlayOneShot(enemyData.dieSound);
        animator.SetTrigger(hashDie);

        navMeshAgent.isStopped = true;
        navMeshAgent.enabled = false;

        col.enabled = false;
        Destroy(gameObject, 2f);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag(player))
        {
            var damageable = collision.collider.GetComponent<IDamageable>();
            if (damageable != null)
            {
                Attack(damageable, collision.contacts[0].point, (transform.position - collision.collider.transform.position).normalized);
            }
        }
    }

    public void Attack(IDamageable target, Vector3 hitPoint, Vector3 hitNormal)
    {
        if (IsDead
            || lastAttackTime + enemyData.attackRate > Time.time)
        {
            return;
        }
        lastAttackTime = Time.time;

        target.OnDamage(enemyData.damage, hitPoint, hitNormal);
    }

    private LivingEntity FindTarget()
    {
        var cols = Physics.OverlapSphere(transform.position, findTargetDistance, targetLayer.value);

        foreach (var col in cols)
        {
            var livingEntity = col.GetComponent<LivingEntity>();
            if (livingEntity != null && !livingEntity.IsDead)
            {
                return livingEntity;
            }
        }
        return null;
    }

    private void StartSinking()
    {
        rb.constraints &= ~RigidbodyConstraints.FreezePositionY;
    }
}
