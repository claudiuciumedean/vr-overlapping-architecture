using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelManagger : MonoBehaviour
{

    float[] overlapLevels = {0f, 0.075f, 0.15f, 0.225f, 0.3f, 0.35f};
    int count;

    public GameObject trialState; // trial prefab
    public GameObject questionState; // data collection prefab

    wallShift shiftScript; // accesing overlap level control and assigning the controller as avatar

    private GameObject trialInstance;
    private GameObject questionInstance;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        instantiateTrial(overlapLevels[count]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("trigger"))
        {
            if(trialInstance != null)
            {
                Destroy(trialInstance);
                instantiateQuestion();
            }

            else
            {
                Destroy(questionInstance);
                instantiateTrial(overlapLevels[count]);
            }
        }
    }

    void instantiateTrial(float overlapLevel)
    {   
        count++;
        shiftScript = trialState.transform.Find("wallsOverlap").gameObject.GetComponent<wallShift>();
        shiftScript.overlap = overlapLevel;
        shiftScript.avatar = gameObject.transform;
        trialInstance = Instantiate(trialState, Vector3.zero, Quaternion.identity);
    }

    void instantiateQuestion()
    {
        questionInstance = Instantiate(questionState, Vector3.zero, Quaternion.identity);
    }
}
