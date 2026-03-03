using System;
using System.Collections;
using UnityEngine;

public class Visuals_CrossBow : MonoBehaviour
{
    private Tower_CrossBow myTower;

    [SerializeField] private LineRenderer attackVisuals;
    [SerializeField] private float attackVisualDuration = 0.1f;

    [Header("Glowing Visuals")] [SerializeField]
    private MeshRenderer meshRenderer;

    private Material material;

    [Space] private float currentIntensity;
    [SerializeField] private float maxIntensity = 150.0f;

    [Space] [SerializeField] private Color startColor;
    [SerializeField] private Color endColor;

    [Header("Rotor Visuals")] [SerializeField]
    private Transform rotor;

    [SerializeField] private Transform rotorUnloaded;
    [SerializeField] private Transform rotorLoaded;

    [Header("Front Glow String")] [SerializeField]
    private LineRenderer frontString_L;

    [SerializeField] private LineRenderer frontString_R;

    [Space] [SerializeField] private Transform frontStartPoint_L;
    [SerializeField] private Transform frontStartPoint_R;
    [SerializeField] private Transform frontEndPoint_L;
    [SerializeField] private Transform frontEndPoint_R;

    [Header("Back Glow String")] [SerializeField]
    private LineRenderer backString_L;

    [SerializeField] private LineRenderer backString_R;

    [Space] [SerializeField] private Transform backStartPoint_L;
    [SerializeField] private Transform backStartPoint_R;
    [SerializeField] private Transform backEndPoint_L;
    [SerializeField] private Transform backEndPoint_R;
    
    [SerializeField] private LineRenderer[] lineRenderers;

    protected void Awake()
    {
        myTower = GetComponent<Tower_CrossBow>();

        material = new Material(meshRenderer.material);

        meshRenderer.material = material;
        
        foreach (var lr in lineRenderers)
        {
            lr.material = material;
        }

        StartCoroutine(ChangeEmission(1));
    }

    private void Update()
    {
        UpdateEmissionColor();
        UpdateStringVisual(frontString_L, frontStartPoint_L, frontEndPoint_L);
        UpdateStringVisual(frontString_R, frontStartPoint_R, frontEndPoint_R);
        UpdateStringVisual(backString_L, backStartPoint_L, backEndPoint_L);
        UpdateStringVisual(backString_R, backStartPoint_R, backEndPoint_R);
    }

    private void UpdateEmissionColor()
    {
        Color emissionColor = Color.Lerp(startColor, endColor, currentIntensity / maxIntensity);

        emissionColor *= Mathf.LinearToGammaSpace(currentIntensity);

        material.SetColor("_EmissionColor", emissionColor);
    }

    public void PlayReloadFX(float attackDuration)
    {
        float reloadDuration = attackDuration / 2;

        StartCoroutine(ChangeEmission(reloadDuration));
        StartCoroutine(UpdateRotorPosition(reloadDuration));
    }

    public void PlayAttackFX(Vector3 startPoint, Vector3 endPoint)
    {
        myTower.SetCanRotate(false);

        attackVisuals.enabled = true;

        attackVisuals.SetPosition(0, startPoint);
        attackVisuals.SetPosition(1, endPoint);

        StartCoroutine("DisableLaserRoutine");
    }

    private IEnumerator DisableLaserRoutine()
    {
        yield return new WaitForSeconds(attackVisualDuration);

        myTower.SetCanRotate(true);
        attackVisuals.enabled = false;
    }

    private IEnumerator ChangeEmission(float duration)
    {
        float startTime = Time.time;
        float startIntensity = 0;

        while (Time.time - startTime < duration)
        {
            float fValue = (Time.time - startTime) / duration;
            currentIntensity = Mathf.Lerp(startIntensity, maxIntensity, fValue);
            yield return null;
        }

        currentIntensity = maxIntensity;
    }

    private IEnumerator UpdateRotorPosition(float duration)
    {
        float startTime = Time.time;

        while (Time.time - startTime < duration)
        {
            float fValue = (Time.time - startTime) / duration;
            rotor.position = Vector3.Lerp(rotorUnloaded.position, rotorLoaded.position, fValue);
            yield return null;
        }

        rotor.position = rotorLoaded.position;
    }

    private void UpdateStringVisual(LineRenderer lineRenderer, Transform startPoint, Transform endPoint)
    {
        lineRenderer.SetPosition(0, startPoint.position);
        lineRenderer.SetPosition(1, endPoint.position);
    }
}