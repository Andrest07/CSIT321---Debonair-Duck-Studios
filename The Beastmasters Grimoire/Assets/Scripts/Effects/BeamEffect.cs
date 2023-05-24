/*
    DESCRIPTION: Enemy laser spell

    AUTHOR DD/MM/YY: Quentin 14/03/23

	- EDITOR DD/MM/YY CHANGES:
    - Quentin 10/5/23: Update for hitting the player 
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;

public class BeamEffect : MonoBehaviour
{
    public GameObject particles;
    public GameObject aimParticles;
    public GameObject laserAim;
    public GameObject laserFire;
    public Transform firePoint;
    public GameObject target;

    [Header("Delay for firing & firing time")]
    [SerializeField] private float delay = 3f;
    [SerializeField] private float firingTime = 4f;
    private float elapsed = 0;
    private float gracePeriod = 0.5f;

    [Header("Visible for testing")]
    public bool fire = false;
    private bool isFiring = false;
    private bool isAiming = false;

    private LineRenderer lineAim;
    private LineRenderer lineFire;
    private RaycastHit2D hit;
    private Vector2 direction;
    private int layerMask = 1 << 9;     // player layer
    [SerializeField] private float distanceRay = 100;

    private EnemyController enemyController;
    private bool hitsOnce = false;

    private float destroySpeed = 10.0f;

    private AudioSource audioSource;

    private void Start()
    {
        enemyController = GetComponentInParent<EnemyController>();
        if(enemyController)
            distanceRay = enemyController.data.AttackDistance;


        lineAim = laserAim.GetComponent<LineRenderer>();
        lineFire = laserFire.GetComponent<LineRenderer> ();

        audioSource = GetComponent<AudioSource> ();
    }

    private void FixedUpdate()
    {
        // when fire is set to true, begin laser
        if (fire && !isFiring)
        {
            fire = false;
            isFiring = isAiming = true;
            FireBeam();
        }
        // if in aiming stage
        else if (isFiring && isAiming)
        {
            if(elapsed < gracePeriod) GetPlayerDirection();

            if (Physics2D.Raycast(firePoint.position, direction * distanceRay, distanceRay, layerMask))
            {
                hit = Physics2D.Raycast(firePoint.position, direction * distanceRay, distanceRay, layerMask);
            }

            // move partical system to location
            aimParticles.transform.position = hit.point;
            // draw ray
            Draw2DRay(lineAim, firePoint.position, hit.point);
        }
        // if ready to fire
        else if (isFiring)
        {
            if (Physics2D.Raycast(firePoint.position, direction * distanceRay, distanceRay, layerMask))
                hit = Physics2D.Raycast(firePoint.position, direction * distanceRay, distanceRay, layerMask);
             

            if (Physics2D.Raycast(firePoint.position, direction * distanceRay, distanceRay, layerMask))
            {
                Debug.Log("hits");
                if (!hitsOnce)
                {
                    PlayerManager.instance.GetComponent<PlayerHealth>().TakeDamage(enemyController.data.ProjDamage);
                    hitsOnce = true;
                }
            }
            else
            {
                Debug.Log("doesn't hit");
            }

            particles.transform.position = hit.point;

            transform.position = Vector2.Lerp(transform.position, hit.point, Time.deltaTime * destroySpeed);
            
            Draw2DRay(lineFire, firePoint.position, hit.point);
        }
        
    }

    private void FireBeam()
    {
        laserAim.SetActive(true);

        GetPlayerDirection();
        if (Physics2D.Raycast(firePoint.position, direction * distanceRay, distanceRay, layerMask))
        {
            hit = Physics2D.Raycast(firePoint.position, direction * distanceRay);
            Debug.Log("hit");
        }
        else
        {   
            direction = firePoint.right * distanceRay - firePoint.position;
        }
        
        StartCoroutine(BeamTimer());
    }

    private void GetPlayerDirection()
    {
        direction = (PlayerManager.instance.transform.position - firePoint.position).normalized;
    }

    void Draw2DRay(LineRenderer lineRenderer, Vector2 start, Vector2 end)
    {
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
    }

    private IEnumerator BeamTimer()
    {
        elapsed = 0;
        while (elapsed < delay)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }

        // turn off aiming
        laserAim.SetActive(false);
        isAiming = false;
        
        // turn on laser
        laserFire.SetActive(true);
        aimParticles.SetActive(false);
        particles.SetActive(true);

        audioSource.Play();

        elapsed = 0;
        while (elapsed < firingTime)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }


        laserFire.SetActive(false);

        isFiring = false;

        // destroy after firing
        Destroy(this.gameObject, 2.0f);
    }
}
