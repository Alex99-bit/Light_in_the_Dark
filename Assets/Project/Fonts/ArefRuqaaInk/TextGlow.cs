using UnityEngine;
using TMPro;

public class TextGlow : MonoBehaviour
{
    public float glowIntensity = 1.0f;
    public Color glowColor = Color.white;

    private Material material;
    private TextMeshProUGUI textMesh;

    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        material = textMesh.fontMaterial;
        material.EnableKeyword("GLOW_ON");
    }

    void Update()
    {
        material.SetColor("_GlowColor", glowColor);
        material.SetFloat("_GlowPower", glowIntensity);
    }
}
