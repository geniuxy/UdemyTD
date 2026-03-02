using System;
using System.Collections;
using UnityEngine;

public class Visuals_CrossBow : MonoBehaviour
{
    private Tower_CrossBow myTower;
    
    [SerializeField] private LineRenderer attackVisuals;
    [SerializeField] private float attackVisualDuration = 0.1f;

    [Header("Glowing Visuals")] 
    [SerializeField] private MeshRenderer meshRenderer;
    private Material material;

    [Space]
    private float currentIntensity;
    [SerializeField] private float maxIntensity = 150.0f;
    
    [Space]
    [SerializeField] private Color startColor;
    [SerializeField] private Color endColor;
    
    protected void Awake()
    {
        myTower = GetComponent<Tower_CrossBow>();

        material = new Material(meshRenderer.material);

        meshRenderer.material = material;
        
        StartCoroutine(ChangeEmission(1));
    }

    private void Update()
    {
        UpdateEmissionColor();
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
}
