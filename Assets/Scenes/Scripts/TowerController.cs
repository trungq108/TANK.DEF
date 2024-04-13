using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    [SerializeField] Transform playerHead;
    [SerializeField] Transform muzzle;
    LineRenderer laserLine;
    bool isShooting;

    public static TowerController instance;

    private void Awake()
    {
        instance = this;
        laserLine = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        PlayerRotate();
    }

    void PlayerRotate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Vector3 direction = new Vector3();
        Collider hitTarget = new Collider();

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {            
            direction = hit.point;
            playerHead.transform.LookAt(direction);
            hitTarget = hit.collider;
        }

        if(Input.GetMouseButton(0) && !isShooting)
        {
            Shooting(direction, hitTarget);
        }
    }

    void Shooting(Vector3 shootingDirection, Collider target)
    {
        isShooting = true;
        int damage = Random.Range(1, 5); 
        Health targetHealth = target.GetComponent<Health>();
        if (targetHealth != null)
        {
            targetHealth.TakeDamage(damage);
        }
        StartCoroutine(ShootingProcess(shootingDirection));
    }

    IEnumerator ShootingProcess(Vector3 shotDirection)
    {
        laserLine.enabled = true;
        laserLine.SetPosition(0, muzzle.position);
        laserLine.SetPosition(1, shotDirection);

        yield return new WaitForSeconds(0.3f);

        isShooting = false;
        laserLine.enabled = false;
    }

}
