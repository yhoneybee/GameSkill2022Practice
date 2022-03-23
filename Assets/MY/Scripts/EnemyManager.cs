using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum MoveType
{
    MoveTowards,
    Lerp,
    Slerp,
}

public class EnemyManager : Singletone<EnemyManager>
{
    private void Start()
    {
    }



    public IEnumerator EMove(Transform trn, Vector3 pos, float speed, float loopTime, MoveType moveType)
    {
        float time = 0;
        while (Vector3.Distance(trn.transform.position, pos) > 0.1f)
        {
            time += Time.deltaTime;
            if (time > loopTime) break;
            switch (moveType)
            {
                case MoveType.MoveTowards:
                    trn.position = Vector3.MoveTowards(trn.transform.position, pos, speed * K.DT);
                    break;
                case MoveType.Lerp:
                    trn.position = Vector3.Lerp(trn.transform.position, pos, speed * K.DT);
                    break;
                case MoveType.Slerp:
                    trn.position = Vector3.Slerp(trn.transform.position, pos, speed * K.DT);
                    break;
            }
            yield return K.waitPointZeroOne;
        }
    }
}
