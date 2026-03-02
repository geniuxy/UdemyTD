using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visuals_CrossBow : MonoBehaviour
{
    [SerializeField] private LineRenderer attackVisuals;
    [SerializeField] private float attackVisualDuration = 0.1f;

    public void EnableAttackVisuals(Vector3 startPoint, Vector3 endPoint)
    {
        attackVisuals.enabled = true;
        
        attackVisuals.SetPosition(0, startPoint);
        attackVisuals.SetPosition(1, endPoint);

        StartCoroutine("DisableLaserRoutine");
    }

    private IEnumerator DisableLaserRoutine()
    {
        yield return new WaitForSeconds(attackVisualDuration);

        attackVisuals.enabled = false;
    }
}
