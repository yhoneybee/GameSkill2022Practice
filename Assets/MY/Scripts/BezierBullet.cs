using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierBullet : BaseBullet
{
    public Vector3[] points = new Vector3[3];
    public Transform target;
    public float needTime;
    public float time;
    public bool isInit;
    public bool isTargetMiss;

    private void OnDisable()
    {
        isInit = false;
    }

    public void Init(Transform trnStart, Transform trnEnd, float startToPoint, float endToPoint)
    {
        needTime = Random.Range(0.8f, 1.0f);

        points[0] = trnStart.position;
        points[1] = trnStart.position + (startToPoint * Random.Range(-1.0f, 1.0f) * trnStart.right) + /*(startToPoint * Random.Range(-0.15f, 1.0f) * trnStart.up) +*/ (startToPoint * Random.Range(-1.0f, -0.8f) * trnStart.forward);
        points[2] = trnEnd.position + (endToPoint * Random.Range(-1.0f, 1.0f) * trnEnd.right) + /*(endToPoint * Random.Range(-1.0f, 1.0f) * trnEnd.up) +*/ (endToPoint * Random.Range(0.8f, 1.0f) * trnEnd.forward);

        target = trnEnd;

        transform.position = points[0];

        isInit = true;
        isTargetMiss = false;
        dir = Vector3.zero;

        time = 0;
    }

    private void Update()
    {
        if (!isInit) return;
        if (!target.gameObject.activeSelf)
        {
            if (!K.GetNearEnemy(K.player.transform))
            {
                K.Pool(poolType).Return(gameObject);
            }
            else
            {
                if (!isTargetMiss)
                {
                    dir = (target.position - transform.position).normalized;
                    isTargetMiss = true;
                }
            }
        }

        if (isTargetMiss) return;

        time += Time.deltaTime;

        float t = time / needTime;

        float x = K.BezierCurve(points[0].x, points[1].x, points[2].x, target.position.x, t);
        float y = K.BezierCurve(points[0].y, points[1].y, points[2].y, target.position.y, t);
        float z = K.BezierCurve(points[0].z, points[1].z, points[2].z, target.position.z, t);

        transform.position = new Vector3(x, y, z);
    }
}
