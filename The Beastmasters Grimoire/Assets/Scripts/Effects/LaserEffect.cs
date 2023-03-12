using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEffect : MonoBehaviour
{
    [SerializeField] private float defDistanceRay = 100;
    public Transform laserFirePoint;
    public Transform target;
    public LineRenderer lineRenderer;
    public Material material;
    float timer = 2f;
    Transform m_transform;


    private void Awake()
    {
        m_transform = GetComponent<Transform>();
    }

    private void Update()
    {
        ShootLaser();

        if (timer > 0)
            timer -= Time.deltaTime;
        else
            lineRenderer.material = material;

    }

    void ShootLaser()
    {
        Vector3 mousePos = Input.mousePosition;

        if (Physics2D.Raycast(m_transform.position, target.position)){
            RaycastHit2D hit = Physics2D.Raycast(m_transform.position, target.position);
            Draw2DRay(laserFirePoint.position, hit.point);
        }
        else
        {
            Draw2DRay(laserFirePoint.position, laserFirePoint.transform.right * defDistanceRay);
        }
    }

    void Draw2DRay(Vector2 start, Vector2 end)
    {
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
    }

}
