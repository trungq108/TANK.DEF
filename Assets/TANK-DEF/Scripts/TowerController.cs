using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    [SerializeField] Transform playerHead;
    [SerializeField] Transform muzzle;
    [SerializeField] ParticleSystem fireVFX;

    LineRenderer laserLine;
    bool isShooting;
    Vector3 direction;

    public static TowerController instance;


    private void Awake()
    {
        instance = this;
        laserLine = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        PlayerRotate();

        if (Input.GetMouseButton(0) && !isShooting) { Shooting(); }
    }

    void PlayerRotate()
    {
        Vector3 mousePosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 50, Color.yellow);
        int layerMask = 1 << 6;
        layerMask = ~layerMask;

        if(Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
        {
            direction = hit.point;
            playerHead.LookAt(direction - playerHead.position + Vector3.up);
        }
    }

    void Shooting()
    {
        isShooting = true;
        int damage = Random.Range(1, 5);

        Ray ray = new Ray(muzzle.position, muzzle.forward);
        RaycastHit hit;
        bool cast = Physics.Raycast(ray, out hit, 50);
        if (cast)
        { 
            Collider target = hit.collider;
            Health targetHealth = target.GetComponent<Health>();
            if (targetHealth != null)
            {
                targetHealth.TakeDamage(damage);
            }
        }

        Vector3 hitPosition = cast ? hit.point : muzzle.forward * 50;
        fireVFX.gameObject.transform.position = hitPosition;
        fireVFX.Play();
        

        StartCoroutine(ShootingProcess(hitPosition));
    }

    IEnumerator ShootingProcess(Vector3 shotDirection)
    {
        laserLine.enabled = true;
        laserLine.SetPosition(0, muzzle.position);
        laserLine.SetPosition(1, shotDirection);

        yield return new WaitForSeconds(0.25f);

        isShooting = false;
        laserLine.enabled = false;
    }

}
