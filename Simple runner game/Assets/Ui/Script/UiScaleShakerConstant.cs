using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiScaleShakerConstant : UiScaleShaker
{
    protected override IEnumerator AnimateSize()
    {
        while (true)
        {
            yield return base.AnimateSize();
            yield return null;
        }
    }

    private void Start()
    {
        ShakeScale();
    }
}
