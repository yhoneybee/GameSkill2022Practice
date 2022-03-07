using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject[] goBodies;
    public Camera camRaycast;
    public RectTransform rtrnAim;

    void Start()
    {
        StartCoroutine(ERotation());
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        transform.Translate(5 * Time.deltaTime * new Vector3(x, 0, z));
    }

    private void FixedUpdate()
    {
        Debug.DrawRay(camRaycast.transform.position, (rtrnAim.position - camRaycast.transform.position).normalized * 1000, Color.red, 0.1f);
    }

    private IEnumerator ERotation()
    {
        float x = 0;
        while (true)
        {
            x = -Input.GetAxisRaw("Horizontal");
            for (int i = 0; i < goBodies.Length; i++)
            {
                var go = goBodies[i];
                go.transform.rotation = Quaternion.Lerp(go.transform.rotation, Quaternion.Euler(Vector3.forward * x * 20), Time.deltaTime * 5);
            }
            yield return K.waitPointZeroOne;
        }
    }
}
