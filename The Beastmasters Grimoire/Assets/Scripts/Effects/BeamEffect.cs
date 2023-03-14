using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;

public class BeamEffect : MonoBehaviour
{
    public GameObject particles;
    public GameObject laserAim;
    public GameObject laserFire;
    public Transform firePoint;
    public GameObject target;

    [Header("Delay for firing & firing time")]
    [SerializeField] private float delay = 3f;
    [SerializeField] private float firingTime = 4f;

    [Header("Visible for testing")]
    public bool fire = false;
    private bool isFiring = false;
    private bool isAiming = false;

    private LineRenderer lineAim;
    private LineRenderer lineFire;
    private RaycastHit2D hit;
    private Vector2 finalTarget;
    private Vector2 direction;
    private int layerMask = 1 << 3;
    [SerializeField] private float distanceRay = 100;


    private void Start()
    {
        lineAim = laserAim.GetComponent<LineRenderer>();
        lineFire = laserFire.GetComponent<LineRenderer> ();
    }

    private void FixedUpdate()
    {
        if (fire && !isFiring)
        {
            fire = false;
            isFiring = isAiming = true;
            FireBeam();
        }
        else if (isFiring && isAiming)
        {
            if (Physics2D.Raycast(firePoint.position, direction * distanceRay, distanceRay, layerMask))
                hit = Physics2D.Raycast(firePoint.position, direction * distanceRay);

            Draw2DRay(lineAim, firePoint.position, hit.point);
        }
        else if (isFiring)
        {
            if (Physics2D.Raycast(firePoint.position, direction * distanceRay, distanceRay, layerMask))
                hit = Physics2D.Raycast(firePoint.position, direction * distanceRay);

            if (Physics2D.Raycast(firePoint.position, direction * distanceRay, distanceRay, layerMask))
            {
                Debug.Log("hits");

                particles.transform.position = hit.point;
                particles.SetActive(true);
            }
            else
            {
                Debug.Log("doesn't hit");

                particles.SetActive(false);
            }

            Draw2DRay(lineFire, firePoint.position, hit.point);
        }
        
    }

    private void FireBeam()
    {
        laserAim.SetActive(true);

        direction = (target.transform.position - firePoint.position).normalized;
        if (Physics2D.Raycast(firePoint.position, direction * distanceRay))
        {
            hit = Physics2D.Raycast(firePoint.position, direction * distanceRay);
            finalTarget = hit.point;
        }
        else
        {   
            finalTarget = firePoint.right * distanceRay;
        }
        
        StartCoroutine(BeamTimer());
    }

    void Draw2DRay(LineRenderer lineRenderer, Vector2 start, Vector2 end)
    {
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
    }

    private IEnumerator BeamTimer()
    {
        float elapsed = 0;
        while (elapsed < delay)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }

        laserAim.SetActive(false);

        isAiming = false;
        laserFire.SetActive(true);

        elapsed = 0;
        while (elapsed < firingTime)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }

        laserFire.SetActive(false);
        particles.SetActive(false);

        isFiring = false;
    }
}
