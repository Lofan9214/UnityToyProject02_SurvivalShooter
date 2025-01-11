using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GunData gundata;
    public Transform firePosition;
    public ParticleSystem fireEffect;

    public float fireDistance = 50f;

    private AudioSource audioSource;
    private LineRenderer lineRenderer;

    private float lastFireTime;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        audioSource = GetComponent<AudioSource>();

        lineRenderer.enabled = false;
        lineRenderer.positionCount = 2;
    }

    private void OnEnable()
    {
        lastFireTime = 0f;
    }

    public void Fire()
    {
        if (Time.time > lastFireTime + gundata.fireRate)
        {
            lastFireTime = Time.time;

            var endPos = Vector3.zero;

            Ray ray = new Ray(firePosition.position, firePosition.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, fireDistance))
            {
                endPos = hit.point;
                var damageable = hit.collider.GetComponent<IDamageable>();
                damageable?.OnDamage(gundata.damage, hit.point, hit.normal);
            }
            else
            {
                endPos = firePosition.position + firePosition.forward * fireDistance;
            }

            StartCoroutine(ShotEffect(endPos));
        }
    }

    private IEnumerator ShotEffect(Vector3 hitPoint)
    {
        audioSource.PlayOneShot(gundata.fireSound);

        lineRenderer.SetPosition(0, firePosition.position);
        lineRenderer.SetPosition(1, hitPoint);
        lineRenderer.enabled = true;
        fireEffect.transform.position = firePosition.position;
        fireEffect.Play();

        yield return new WaitForSeconds(0.2f);

        lineRenderer.enabled = false;
    }
}
