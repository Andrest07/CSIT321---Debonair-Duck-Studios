using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;

public class BeamEffect : MonoBehaviour
{
    public ParticleSystem particles;
    public GameObject laserAim;
    public GameObject laserFire;
    public Transform firePoint;

    public GameObject target;
    public float timer = 3f;

    public bool fire = false;
    public bool isFiring = false;

    private Transform m_transform;
    private LineRenderer lineAim;
    private LineRenderer lineFire;
    private RaycastHit2D hit;
    private Vector2 finalTarget;
    [SerializeField] private float distanceRay = 100;

    private void Start()
    {
        lineAim = laserAim.GetComponent<LineRenderer>();
        lineFire = laserFire.GetComponent<LineRenderer> ();

        m_transform = GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        if (fire && !isFiring)
        {
            fire = false;
            isFiring = true;
            Debug.Log("true");
            FireBeam();
        }

        ShootLaser();
        
    }

    void ShootLaser()
    {
        Vector3 dir = (target.transform.position - firePoint.position).normalized;
        Debug.DrawRay(firePoint.position, dir, Color.green);

        if (Physics2D.Raycast(firePoint.position, dir * distanceRay))
        {
            RaycastHit2D hit = Physics2D.Raycast(firePoint.position, dir * distanceRay);
            Draw2DRay(lineAim, firePoint.position, hit.point);
        }
        else
        {
            Draw2DRay(lineAim, firePoint.position, firePoint.transform.right * distanceRay);
        }
    }

    private void FireBeam()
    {
        laserAim.SetActive(true);
        if (Physics2D.Raycast(m_transform.position, target.transform.position))
        {
            Debug.Log("hit");
            hit = Physics2D.Raycast(transform.position, target.transform.position);
            finalTarget = hit.point;
        }
        else
        {
            finalTarget = m_transform.right * distanceRay;
        }
        /*
        StartCoroutine(BeamTimer());
        laserAim.SetActive(false);
        laserFire.SetActive(true);
        StartCoroutine(BeamTimer());
        laserFire.SetActive(false);

        isFiring = false;*/
    }
    void Draw2DRay(LineRenderer lineRenderer, Vector2 start, Vector2 end)
    {
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
    }

    private IEnumerator BeamTimer()
    {
        float elapsed = 0;
        while (elapsed < timer)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
}
