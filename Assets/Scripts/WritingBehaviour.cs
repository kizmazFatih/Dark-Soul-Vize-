using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WritingBehaviour : MonoBehaviour
{

    public RawImage writing;
    private float trancperency = 0.15f;
    private float direction = 1f;

    void Update()
    {
        trancperency += 0.35f * direction * Time.deltaTime;

        if (trancperency < 0.15 || trancperency > 1)
        {
            direction *= -1f;
        }
        writing.canvasRenderer.SetAlpha(trancperency);

    }
}
