using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Germ : BaseEnemy
{
    public override IEnumerator EMove()
    {
        yield return null;
    }

    public override void Shot()
    {

    }

    // Start is called before the first frame update
    protected override void OnEnable()
    {
        base.OnEnable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
