using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthText : MonoBehaviour
{
    public Vector3 movespeed = new Vector3(0,75,0);
    RectTransform textTransform;
    public float timeToFade = 1f;
    private float timeElapsed = 0f;
    TextMeshProUGUI textMeshPro;
    private Color startColor;
    private void Awake()
    {
        textTransform = GetComponent<RectTransform>();
        textMeshPro = GetComponent<TextMeshProUGUI>();
        startColor = textMeshPro.color;
    }
    private void Update()
    {
        transform.position = textTransform.position + movespeed * Time.deltaTime;
        timeElapsed = timeElapsed + Time.deltaTime;
        if (timeElapsed < timeToFade)
        {
            float AlphaTime = startColor.a * (1 - timeElapsed / timeToFade);
            textMeshPro.color = new Color(startColor.r, startColor.g, startColor.b, AlphaTime);
        } else Destroy(gameObject);
    }

}
