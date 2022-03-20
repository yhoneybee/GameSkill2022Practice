using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum eLUNCHER_TYPE
{
    NormalBullet,
    Laser,
}

public class Luncher : MonoBehaviour
{
    public eLUNCHER_TYPE luncherType;
    public GameObject goAim;
    public GameObject goFirePos;

    private void OnEnable()
    {
        StartCoroutine(EUpdate());
    }

    private void FixedUpdate()
    {
        K.player.rtrnAim.localPosition = Input.mousePosition - new Vector3(Screen.width / 2, Screen.height / 2);
        goAim.transform.LookAt(K.player.rtrnAim.position);
    }

    private IEnumerator EUpdate()
    {
        yield return null;
        var wait = new WaitForSeconds(K.player.rateTime);
        Ray ray;
        while (true)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, 100, 1 << LayerMask.NameToLayer("Enemy")))
            {
                switch (luncherType)
                {
                    case eLUNCHER_TYPE.NormalBullet: ShotBullet(hit.point); break;
                }
                yield return wait;
            }
            yield return null;
        }
    }

    private void ShotBullet(Vector3 point)
    {
        var bullet = K.GetPool(ePOOL_TYPE.Bullet).Get<Bullet>();
        bullet.transform.position = goFirePos.transform.position;
        bullet.speed = 300;
        bullet.damage = K.player.damage;
        bullet.dir = (point - bullet.transform.position).normalized;
        bullet.targetTag = "Enemy";
    }
}
