using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DetailsPage : MonoBehaviour
{
    [SerializeField]
    PoseEstimator poseEstimator;

    [SerializeField]
    TextMeshProUGUI shoulderDistText;

    private bool shouldersDetected = false;

    [SerializeField]
    private int[] animBands;
    private int currentAnimBand = 0;

    // Update is called once per frame
    void Update()
    {
        float dist = 0;
        currentAnimBand = 0;
        shouldersDetected = false;

        if (poseEstimator != null && poseEstimator.Poses != null)
        {
            for (int i = 0; i < poseEstimator.Poses.Length; ++i)
            {
                Utils.Keypoint[] pose = poseEstimator.Poses[i];

                if (pose[5].active && pose[6].active)
                {
                    shouldersDetected = true;
                    float shoulderDist = Vector2.Distance(pose[5].position, pose[6].position);

                    if (shoulderDist > dist)
                    {
                        dist = shoulderDist;
                    }
                }
            }
            //Debug.Log(dist);
        }

        if (dist != 0)
        {
            for (int i = 0; i < animBands.Length; ++i)
            {
                if (dist > animBands[i])
                {
                    currentAnimBand = i + 1;
                }
            }
        }

        shoulderDistText.text = currentAnimBand.ToString() + " " + dist.ToString();
    }
}
