using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighFiveArm : MonoBehaviour
{
    private static List<HighFiveArm> highFiveArms = new List<HighFiveArm>();
    void Awake()
    {
        highFiveArms.Add(this);
    }

    static public HighFiveArm GetNearestWithin(Vector3 pos, float range)
    {
        int nearestIndex = -1;
        float nearestDist = range;
        for (int i = 0; i < highFiveArms.Count; i++)
        {
            Vector3 diff = highFiveArms[i].transform.position - pos;
            float dist = diff.magnitude;
            if (dist > 0.01f && dist < nearestDist)
            {
                nearestDist = dist;
                nearestIndex = i;
            }
        }
        if (nearestIndex >= 0)
        {
            return highFiveArms[nearestIndex];
        }
        return null;
    }
   

    private void OnDestroy()
    {
        highFiveArms.Remove(this);
    }
}
