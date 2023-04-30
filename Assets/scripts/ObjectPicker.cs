using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ObjectPicker : MonoBehaviour
{
    public GameObject requiredObject;
    public AudioClip correctObjectSound;
    private XRGrabInteractable grabInteractable;
    private AudioSource audioSource;

    private void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.onSelectEntered.AddListener(CheckObject);
        audioSource = GetComponent<AudioSource>();
    }

    private void CheckObject(XRBaseInteractor interactor)
    {
        GameObject selectedObject = interactor.selectTarget.gameObject;
        if (selectedObject == requiredObject)
        {
            Debug.Log("Object picked: " + selectedObject.name);
            Debug.Log("AudioSource: " + GetComponent<AudioSource>());
            Debug.Log("AudioClip: " + correctObjectSound);
            if (correctObjectSound != null)
            {
                
                audioSource.PlayOneShot(correctObjectSound);
            }
        }
        else
        {
            Debug.Log("Wrong object picked");
        }
    }
}