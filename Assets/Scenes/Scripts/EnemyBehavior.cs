using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] GameObject headCanon;
    [SerializeField] Transform muzzle;

    EnemyPool EnemyPool;
    Vector3 nextPosition;
    Vector3 currentPosition;
    Quaternion originalRotation;
    bool isShooted;
    Transform playerPosition;


    private void OnEnable()
    {
        EnemyPool = FindAnyObjectByType<EnemyPool>();
        playerPosition = EnemyPool.PlayerPosition;
        originalRotation = headCanon.transform.rotation;

        EnemyLoopWorking();
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    void EnemyLoopWorking()
    {
        currentPosition = transform.position;
        nextPosition = EnemyPool.CaculatePositionAroundPlayer(5);

        StopAllCoroutines();
        StartCoroutine(MovingSequence(currentPosition, nextPosition));
    }

    IEnumerator MovingSequence(Vector3 startPos, Vector3 endPos)
    {
        float elapsed = 0;
        float duration = 2;
        float t = 0;

        while (t <= 1)
        {
            t = elapsed / duration;
            Vector3 diretion = Vector3.Lerp(startPos, endPos, t);

            transform.position = diretion;
            elapsed += Time.deltaTime;
            yield return null;
        }

        StartCoroutine(HeadCanonRotate(originalRotation, Quaternion.LookRotation(playerPosition.position - transform.position)));
    }

    IEnumerator HeadCanonRotate(Quaternion currentRotation, Quaternion nexRotation)
    {
        Quaternion startRotation = currentRotation;
        Quaternion targetRotation = nexRotation;

        float elapsed = 0;
        float duration = 2;
        float t = 0;

        while (t <= 1)
        {
            t = elapsed / duration;
            headCanon.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);

            elapsed += Time.deltaTime;
            yield return null;
        }

        if (!isShooted)
        {
            StartCoroutine(EnemyShoting());
        }
        else 
        {
            isShooted = false;
            EnemyLoopWorking(); 
        }
    }

    IEnumerator EnemyShoting()
    {
        Transform playerPosition = EnemyPool.PlayerPosition;
        LineRenderer laserLine = GetComponent<LineRenderer>();
        laserLine.enabled = true;
        laserLine.SetPosition(0, muzzle.position);
        laserLine.SetPosition(1, playerPosition.position);
        
        int enemyDamage = Random.Range(1, 5);
        TowerController.instance.GetComponent<Health>().TakeDamage(enemyDamage);

        yield return new WaitForSeconds(0.5f);
        laserLine.enabled = false;

        isShooted = true;
        StartCoroutine(HeadCanonRotate(Quaternion.LookRotation(playerPosition.position - transform.position), originalRotation));
    }
}
