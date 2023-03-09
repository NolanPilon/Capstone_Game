using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DamageVignette : MonoBehaviour
{
    public VolumeProfile vignetteProfile;
    private Vignette damageVignette;
    public static float vignetteValue;

    private void Start()
    {
        damageVignette = (Vignette)vignetteProfile.components[0];

        damageVignette.intensity.value = 0.35f;
    }

    private void Update()
    {
        // Fade vignette overtime
        if (damageVignette.intensity.value > 0)
        {
            vignetteValue -= Time.deltaTime * 0.2f;
        }

        damageVignette.intensity.value = vignetteValue;
    }
}
