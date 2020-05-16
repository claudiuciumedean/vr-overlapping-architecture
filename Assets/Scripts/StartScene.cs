using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : MonoBehaviour
{
    public float length = 1;

    int layerMaskButton;
    float triggerThreshold;
    float originalLength;

    Transform hand;
    TextMesh instance;
    GameObject pointer1;
    // Start is called before the first frame update
    void Start()
    {
        layerMaskButton = 1 << 5;
        triggerThreshold = 0.9f;
        pointer1 = GameObject.Find("OVRPlayerController/OVRCameraRig/TrackingSpace/RightHandAnchor/Pointer1");
        hand = GameObject.Find("OVRPlayerController/OVRCameraRig/TrackingSpace/RightHandAnchor").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        OVRInput.Update();
        RaycastHit hit;


        if (Physics.Raycast(hand.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMaskButton))
        {
            pointer1.SetActive(true);
            pointer1.transform.position = new Vector3(hit.point.x + 0.01f, hit.point.y, hit.point.z);
            laserLength(hit.distance);

            instance = hit.collider.gameObject.GetComponent<TextMesh>();
            instance.color = Color.red;

            if (OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger) > triggerThreshold)
            {
                ExperienceManager.Instance.startExperiment();
            }
        }
        else
        {
            if (instance != null)
            {
                pointer1.SetActive(false);
                laserLength(length);
                instance.color = Color.white;
            }
        }
    }

    void laserLength(float length)
    {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, length);
    }
}
