using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public bool IsMoving
    {
        get => isMoving;
        set
        {
            isMoving = value;
            StopAllCoroutines();
            if (!value) StartCoroutine(EBack());
            else StartCoroutine(EMove());
        }
    }
    public bool isMoving;

    private void Awake()
    {
        K.camera = this;
    }

    private void Start()
    {
        isMoving = true;
    }

    public IEnumerator EMove()
    {
        K.timeScale = 0;
        float i = 0;
        bool first = true;
        while (isMoving)
        {
            i += Time.deltaTime * 300;
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, K.player.transform.position.y + 10, transform.position.z), Time.deltaTime);
            var pos = K.Cricle(i, 30, K.player.transform.position);
            transform.position = new Vector3(pos.x, transform.position.y, pos.z);
            transform.LookAt(K.player.transform);

            if (first)
            {
                if (Mathf.Abs(transform.position.y - K.player.transform.position.y + 10) > 0.01f)
                {
                    first = false;
                    StartCoroutine(UIManager.Instance.EFade(true));
                }
            }

            yield return K.waitPointZeroOne;
        }
        yield return null;
    }

    public IEnumerator EBack()
    {
        yield return StartCoroutine(K.EMove(transform, Vector3.up * 100, 3, 5, MoveType.Slerp));

        var toRot = Quaternion.Euler(90, 0, 0);

        while (Quaternion.Angle(transform.rotation, toRot) > 0.1f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, toRot, Time.deltaTime * 3);
            yield return null;
        }

        K.timeScale = 1;
    }
}
