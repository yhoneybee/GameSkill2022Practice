using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseEnemy : BaseObject
{
    [Header("BaseEnemy---------------------------------------------------------------------------------------------------------------------------------")]
    public SphereCollider sphereCollider;

    public Vector3 dir;

    public float changeFormTime = 1;

    public int form = 0;

    public int score;

    public abstract void Shot();

    public abstract IEnumerator EMove();

    protected override void OnEnable()
    {
        base.OnEnable();
        if (!sphereCollider) sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.radius *= 3;
        StartCoroutine(EMove());
        StartCoroutine(EScreenCheck());
    }

    private IEnumerator EScreenCheck()
    {
        while (true)
        {
            var cmpX = Mathf.Clamp(transform.position.x, K.player.transform.position.x - 60, K.player.transform.position.x + 60);
            var cmpY = Mathf.Clamp(transform.position.y, K.player.transform.position.y - 35, K.player.transform.position.y + 35);
            transform.position = new Vector3(cmpX, cmpY, transform.position.z);

            yield return K.waitPointZeroOne;
        }
    }

    public override void Die()
    {
        base.Die();
        var pool = K.GetPool(ePOOL_TYPE.ScoreText);
        var obj = pool.Get<Text>();
        obj.transform.SetParent(K.canvas.transform);
        obj.transform.localPosition = K.player.rtrnAim.localPosition;
        obj.text = $"{score}";
        pool.WaitReturn(obj.gameObject, 1);
    }
}
