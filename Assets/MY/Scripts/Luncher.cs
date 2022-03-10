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
        while (true)
        {
            if (Input.GetMouseButton(0))
            {
                switch (luncherType)
                {
                    case eLUNCHER_TYPE.NormalBullet:  ShotBullet();   break;
                }
                yield return wait;
            }
            yield return null;
        }
    }

    private void ShotBullet()
    {
        var bullet = K.GetPool(ePOOL_TYPE.Bullet).Get<Bullet>();
        bullet.transform.position = goFirePos.transform.position;
        bullet.speed = 20;
        bullet.dir = K.player.rtrnAim.position - goFirePos.transform.position;
    }
}
