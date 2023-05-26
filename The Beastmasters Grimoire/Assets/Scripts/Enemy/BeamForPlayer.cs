/*
    DESCRIPTION: Beam spell for player

    AUTHOR DD/MM/YY: Quentin 11/05/23

	- EDITOR DD/MM/YY CHANGES:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;

public class BeamForPlayer : MonoBehaviour
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
    private float attackDamage = 2.0f;

    [Header("Player Controls")]
    public bool aim = false;

    private bool isFiring = false;
    private bool isAiming = false;

    private LineRenderer lineAim;
    private LineRenderer lineFire;
    private RaycastHit2D hit;
    private Vector2 direction;
    private int layerMask = 1 << 3;     // enemy layer
    private int ignoreLayer = (1 << 6) | (1 << 9);     // interaction layer & player layer
    [SerializeField] private float distanceRay = 3.0f;
    private Vector2 mousePosition;
    private Vector3 firingOrigin;

    private bool hitsOnce = false;

    private float destroySpeed = 10.0f;
    private AudioSource audioSource;

    private void Start()
    {
        lineAim = laserAim.GetComponent<LineRenderer>();
        lineFire = laserFire.GetComponent<LineRenderer>();
        firePoint = PlayerManager.instance.book.transform;

        isFiring = isAiming = true;
        laserAim.SetActive(true);

        audioSource = GetComponent<AudioSource>();
    }

    public void SetBeamStats(float aimTime, float fireTime, float damage)
    {
        delay = aimTime; firingTime = fireTime; attackDamage = damage;
    }

    private void FixedUpdate()
    {
        // in aiming stage
        if (isFiring && isAiming)
        {
            GetMouseDirection();

            if (Physics2D.Raycast(firePoint.position, direction, distanceRay, ~ignoreLayer))
            {
                hit = Physics2D.Raycast(firePoint.position, direction, distanceRay, ~ignoreLayer);

                aimParticles.transform.position = hit.point;
                Draw2DRay(lineAim, firePoint.position, hit.point);
            }
            else
            {
                mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                // Offsets the target position so that the object keeps its distance.
                if (Vector2.Distance(firePoint.position, mousePosition) > distanceRay)
                {
                    mousePosition = distanceRay * (mousePosition- (Vector2)firePoint.position).normalized + (Vector2)firePoint.position;
                }


                aimParticles.transform.position = mousePosition;
                Draw2DRay(lineAim, firePoint.position, mousePosition);
            }

        }
        // if ready to fire
        else if (isFiring)
        {
            // if hits anything else
            if (Physics2D.Raycast(firingOrigin, direction, distanceRay, ~ignoreLayer))
            {
                hit = Physics2D.Raycast(firingOrigin, direction, distanceRay, ~ignoreLayer);
                Debug.Log("hits other");

                particles.transform.position = hit.point;
                Draw2DRay(lineFire, firingOrigin, hit.point);
            }
            else
            {
                Debug.Log("doesn't hit");

                particles.transform.position = mousePosition;
                Draw2DRay(lineFire, firingOrigin, mousePosition);
            }


            // if hits an enemy
            if (!hitsOnce && Physics2D.Raycast(firingOrigin, direction , distanceRay, layerMask))
            {
                Debug.Log("hits enemy");

                hit.collider.GetComponent<EnemyHealth>().TakeDamage(attackDamage, transform.position);
                hitsOnce = true;
            }


            laserFire.SetActive(true);
            particles.SetActive(true);

            transform.position = Vector2.Lerp(transform.position, hit.point, Time.deltaTime * destroySpeed);

        }

    }

    
    // Fire beam for player 2nd click
    public void FireBeam()
    {
        firingOrigin = firePoint.position;

        // turn off aiming
        laserAim.SetActive(false);
        aimParticles.SetActive(false);
        isAiming = false;

        // turn on laser
        audioSource.Play();

        StartCoroutine(BeamTimer());
    }


    private void GetMouseDirection()
    {
        direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - firePoint.position;
        if (Vector2.Distance(direction, firePoint.position) > distanceRay) direction = direction.normalized * distanceRay;
        
    }

    void Draw2DRay(LineRenderer lineRenderer, Vector2 start, Vector2 end)
    {
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
    }

    public void DestroySelf()
    {
        Destroy(this.gameObject);
    }

    private IEnumerator BeamTimer()
    {
        elapsed = 0;
        while (elapsed < firingTime)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }


        laserFire.SetActive(false);

        isFiring = false;

        // destroy after firing
        PlayerManager.instance.usingBeamSpell = false;
        Destroy(this.gameObject, 2.0f);
    }
}
