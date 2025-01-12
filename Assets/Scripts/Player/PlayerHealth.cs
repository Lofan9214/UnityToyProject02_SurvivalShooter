using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : LivingEntity
{
    private readonly int hashDie = Animator.StringToHash("Die");

    public float hitRate = 0.5f;

    public AudioClip hitSound;
    public AudioClip dieSound;

    private Animator animator;
    private AudioSource audioSource;
    private PlayerMove playerMove;
    private PlayerShooter playerShooter;

    private float lastHitTime;

    private GameManager gameManager;
    private UIManager uiManager;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        playerMove = GetComponent<PlayerMove>();
        playerShooter = GetComponent<PlayerShooter>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        if (gameManager == null)
        {
            gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        }
        if (uiManager == null)
        {
            uiManager = gameManager.uiManager;
        }

        uiManager.UpdateHpBar(Hp / maxHp);

        playerMove.enabled = true;
        playerShooter.enabled = true;

        lastHitTime = 0f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            OnDamage(10f, Vector3.zero, Vector3.zero);
        }
    }

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        if (lastHitTime + hitRate > Time.time)
        {
            return;
        }

        lastHitTime = Time.time;

        base.OnDamage(damage, hitPoint, hitNormal);

        uiManager.UpdateHpBar(Hp / maxHp);
        uiManager.OnHit();

        if (!IsDead)
        {
            audioSource.PlayOneShot(hitSound);
        }
    }

    public override void Die()
    {
        if (IsDead)
            return;
        base.Die();


        animator.SetTrigger(hashDie);

        audioSource.PlayOneShot(dieSound);

        uiManager.OnPlayerDie();

        playerMove.enabled = false;
        playerShooter.enabled = false;
    }

    private void RestartLevel()
    {
        StartCoroutine(gameManager.OnDie());
    }
}
