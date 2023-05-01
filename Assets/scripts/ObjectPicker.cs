using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ObjectPicker : MonoBehaviour
{
    public GameObject requiredObject;
    public AudioClip correctObjectSound;
    public AudioClip incorrectObjectSound;
    private XRGrabInteractable grabInteractable;
    private AudioSource[] audioSource;

    private void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.onSelectEntered.AddListener(CheckObject);
        audioSource = GetComponents<AudioSource>();
    }

    private void CheckObject(XRBaseInteractor interactor)
    {
        GameObject selectedObject = interactor.selectTarget.gameObject;
        
        if (selectedObject == requiredObject)
        {
            Debug.Log("Object picked: " + selectedObject.name);
            Debug.Log("AudioSource: " + audioSource[0]);
            Debug.Log("AudioSource: " + audioSource[1]);
            Debug.Log("AudioClip: " + correctObjectSound);
            if (correctObjectSound != null)
            {
                
                audioSource[0].PlayOneShot(correctObjectSound);
            }
        }
        else
        {
            if (incorrectObjectSound != null)
            {
                
                audioSource[1].PlayOneShot(incorrectObjectSound);
            }
            Debug.Log("Wrong object picked");
        }
    }
}