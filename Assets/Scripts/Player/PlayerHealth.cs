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

    private UIManager manager;

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

        if (manager == null)
        {
            manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().uiManager;
        }

        manager.UpdateHpBar(Hp / maxHp);

        playerMove.enabled = true;
        playerShooter.enabled = true;

        lastHitTime = 0f;
    }

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        if (lastHitTime + hitRate > Time.time)
        {
            return;
        }

        lastHitTime = Time.time;

        base.OnDamage(damage, hitPoint, hitNormal);

        manager.UpdateHpBar(Hp / maxHp);
        if (!IsDead)
        {
            audioSource.PlayOneShot(hitSound);
        }
    }

    public override void Die()
    {
        base.Die();

        animator.SetTrigger(hashDie);

        audioSource.PlayOneShot(dieSound);

        playerMove.enabled = false;
        playerShooter.enabled = false;
    }
}
