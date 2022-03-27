using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletInfo : MonoBehaviour
{
    public int multi = 1;

    public int throughCount = 0;

    public int UpgradeLevel
    {
        get => upgradeLevel;
        set
        {
            if (value >= 5) return;
            upgradeLevel = value;
        }
    }
    public int upgradeLevel;

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UpgradeLevel++;
        }
    }

    public IEnumerator EShot(int damage)
    {
        for (int i = 0; i < multi; i++)
        {
            switch (UpgradeLevel)
            {
                case 0:
                    K.Shot(transform.position, Vector3.forward, 200, damage, true, throughCount);
                    break;
                case 1:
                    K.Shot(transform.position + Vector3.right * 3 + Vector3.back * 0.5f, Vector3.forward, 200, damage, true, throughCount);
                    K.Shot(transform.position + Vector3.right * -3 + Vector3.back * 0.5f, Vector3.forward, 200, damage, true, throughCount);
                    break;
                case 2:
                    K.Shot(transform.position, Vector3.forward, 200, damage, true);
                    K.Shot(transform.position + Vector3.right * 3 + Vector3.back * 0.5f, Vector3.forward, 200, damage, true, throughCount);
                    K.Shot(transform.position + Vector3.right * -3 + Vector3.back * 0.5f, Vector3.forward, 200, damage, true, throughCount);
                    break;
                case 3:
                    K.Shot(transform.position + Vector3.right * 3 + Vector3.back * 0.5f, Vector3.forward, 200, damage, true, throughCount);
                    K.Shot(transform.position + Vector3.right * 6 + Vector3.back, Vector3.forward, 200, damage, true, throughCount);
                    K.Shot(transform.position + Vector3.right * -3 + Vector3.back * 0.5f, Vector3.forward, 200, damage, true, throughCount);
                    K.Shot(transform.position + Vector3.right * -6 + Vector3.back, Vector3.forward, 200, damage, true, throughCount);
                    break;
                case 4:
                    K.Shot(transform.position, Vector3.forward, 200, damage, true);
                    K.Shot(transform.position + Vector3.right * 3 + Vector3.back * 0.5f, Vector3.forward, 200, damage, true, throughCount);
                    K.Shot(transform.position + Vector3.right * 6 + Vector3.back, Vector3.forward, 200, damage, true, throughCount);
                    K.Shot(transform.position + Vector3.right * -3 + Vector3.back * 0.5f, Vector3.forward, 200, damage, true, throughCount);
                    K.Shot(transform.position + Vector3.right * -6 + Vector3.back, Vector3.forward, 200, damage, true, throughCount);
                    break;
            }
            yield return new WaitForSeconds(0.07f);
        }
        yield return K.waitPointZeroOne;
    }
}
