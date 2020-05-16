using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointingTask : MonoBehaviour
{
    //input variables
    public float length = 1;

    // internal variables
    int layerMaskButton;
    int layerMaskGround;
    
    float triggerThreshold;
    float originalLength;
    bool nextTask;

    float counterQuestion;
    float counterTask;

    bool isUnlocked = false;

    TextMesh instance;
    GameObject buttons;
    GameObject pointer1;
    GameObject pointer2;
    Transform avatarPos;
    Transform hand;
    GameObject textDist;

    // Start is called before the first frame update
    void Start()
    {   
      layerMaskButton = 1 << 5;
      layerMaskGround = 1 << 8;
      triggerThreshold = 0.9f;  

      counterQuestion = 0f;
      counterTask = 0f;
      nextTask = false;  

      buttons = GameObject.Find("Buttons");
      pointer1 = GameObject.Find("OVRPlayerController/OVRCameraRig/TrackingSpace/RightHandAnchor/Pointer1");
      pointer2 = GameObject.Find("OVRPlayerController/OVRCameraRig/TrackingSpace/RightHandAnchor/Pointer2");
      avatarPos = GameObject.Find("OVRPlayerController/OVRCameraRig/TrackingSpace/CenterEyeAnchor").GetComponent<Transform>();
      hand = GameObject.Find("OVRPlayerController/OVRCameraRig/TrackingSpace/RightHandAnchor").GetComponent<Transform>();
      textDist = GameObject.Find("Text");

        textDist.SetActive(false);

      laserLength(length);
    }

    // Update is called once per frame
    void Update()
    {
        OVRInput.Update();
        RaycastHit hit;

        if(nextTask == false)
        {
            counterQuestion += Time.deltaTime;

            if (Physics.Raycast(hand.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMaskButton))
            {
                pointer1.SetActive(true);
                pointer1.transform.position = new Vector3(hit.point.x + 0.01f, hit.point.y, hit.point.z);
                laserLength(hit.distance);

                instance = hit.collider.gameObject.GetComponent<TextMesh>();
                instance.color = Color.red;
                
                if(OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger) > triggerThreshold)
                {
                    recordAnswer(instance.tag, counterQuestion);

                    buttons.SetActive(false);
                    pointer1.SetActive(false);
                    nextTask = true;
                }
            }
            else
            {
                if(instance != null)
                {
                    pointer1.SetActive(false);
                    laserLength(length);
                    instance.color = Color.white;
                }
            }
        }
        else if(nextTask && isUnlocked)
        {
            if (!textDist.activeSelf)
            {
                textDist.SetActive(true);
            }
            counterTask += Time.deltaTime;
             if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMaskGround))
            { 
                pointer2.SetActive(true);
                pointer2.transform.position = hit.point;
                laserLength(hit.distance);

                if(OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger) > triggerThreshold)
                {
                    recordDistance(hit.point, counterTask); // record the distance
                    // load next scene
                    enabled = false;
                }
            }
            else
            {
                pointer2.SetActive(false);
                laserLength(length);
            }
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            isUnlocked = true;
        }
    }

    void laserLength(float length)
    {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, length);
    }

    void recordDistance(Vector3 point, float time)
    {
        float distance = Vector3.Distance(new Vector3(avatarPos.position.x, 0, avatarPos.position.z), point);
        distance -= 0.2f;
        float estimation = (distance / 2.90f) * 100; //2.90 is the physical distance between two points

        Debug.Log(estimation + " completed in: " + time + " sec");

        ExperienceManager.Instance.setDistanceEstimation(estimation);
        ExperienceManager.Instance.setDistanceEstimationTime(time);

        ExperienceManager.Instance.saveLog();
        StartCoroutine(loadNextScene());
    }

    void recordAnswer(string answer, float time)
    {
        Debug.Log(answer + " completed in: " + time + " sec");
        ExperienceManager.Instance.setAnswer(answer);
        ExperienceManager.Instance.setAnswerTime(time);
    }

    IEnumerator loadNextScene()
    {
        yield return new WaitForSeconds(2);
        ExperienceManager.Instance.loadScene();
    }
}
