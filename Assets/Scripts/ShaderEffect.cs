using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderEffect : MonoBehaviour
{
    public IEnumerator PlayerShadeFX(float duration)
    {
        Color originalColor = transform.GetComponent<Renderer>().material.color;
        float elapsed = 0.0f;
        bool toggle = true;
        while(elapsed < duration)
        {
            if(toggle)
            {
                transform.GetComponent<Renderer>().material.color = new Color(0.868f, 0.209f, 0.209f, 1.000f);
                toggle = false;
            }
            else
            {
                transform.GetComponent<Renderer>().material.color = originalColor;
                toggle = true;
            }

            elapsed += Time.deltaTime;

            yield return new WaitForSeconds(0.05f);
        }
        transform.GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, 1f);
    }

    public IEnumerator BossShadeFX(float duration)
    {
        yield return null;
    }
}
