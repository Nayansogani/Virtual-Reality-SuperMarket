using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ObjectPicker : MonoBehaviour
{
    public GameObject appleObject;
    public GameObject avocadoObject;
    public GameObject bananaObject;
    public AudioClip correctObjectSound1;
    public AudioClip correctObjectSound2;
    public AudioClip correctObjectSound3;
    public AudioClip incorrectObjectSound;
    private XRGrabInteractable grabInteractable;
    private AudioSource[] audioSource;
    public int counter = 1;
    private Vector3 originalPosition;
    private GameObject expectedObject;

    private const string CounterKey = "ObjectPicker_Counter"; // Key for PlayerPrefs
    private static bool isInitialized = false;

    private void Start()
    {
        
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.onSelectEntered.AddListener(CheckObject);
        audioSource = GetComponents<AudioSource>();
        originalPosition = transform.position;
        
        SetExpectedObject();
        Debug.Log("AudioSource array size: " + audioSource.Length);

        if (isInitialized)
            return;
        LoadCounter();
        isInitialized = true;
    }

    private void OnDestroy()
    {
        SaveCounter();
    }

    private void LoadCounter()
    {
        if (PlayerPrefs.HasKey(CounterKey))
        {
            counter = PlayerPrefs.GetInt(CounterKey);
            Debug.Log("&&&&Counter: " + counter);
        }
        else{
            counter = 1;
            Debug.Log("not loaded: " + counter);

        }
    }

    private void SaveCounter()
    {
        PlayerPrefs.SetInt(CounterKey, counter);
        PlayerPrefs.Save();
    }

    private void IncrementCounter()
    {
        counter++;
        SaveCounter();
    }

    private void RestartCounter()
    {
        counter = 1;
        SaveCounter();
    }

    private void SetExpectedObject()
    {
        Debug.Log("@@Counter: " + counter);
        switch (counter)
        {
            case 1:
                expectedObject = appleObject;
                break;
            case 2:
                expectedObject = avocadoObject;
                break;
            case 3:
                expectedObject = bananaObject;
                break;
            default:
                expectedObject = bananaObject;
                break;
        }
    }

    private void CheckObject(XRBaseInteractor interactor)
    {
        LoadCounter();
        SetExpectedObject();
        Debug.Log("Counter: " + counter);
        GameObject selectedObject = interactor.selectTarget.gameObject;
        Debug.Log("Object picked: " + selectedObject.name);
        Debug.Log("Object expected: " + expectedObject.name);
        if (selectedObject.name == expectedObject.name)
        {
            if (counter == 1)
            {
                Debug.Log("AudioClip: " + correctObjectSound1);
                if (correctObjectSound1 != null)
                {
                    audioSource[0].PlayOneShot(correctObjectSound1);
                }
            }
            else if (counter == 2)
            {
                Debug.Log("AudioClip: " + correctObjectSound2);
                if (correctObjectSound2 != null)
                {
                    audioSource[1].PlayOneShot(correctObjectSound2);
                }
            }
            else if (counter == 3)
            {
                Debug.Log("AudioClip: " + correctObjectSound3);
                if (correctObjectSound3 != null)
                {
                    audioSource[2].PlayOneShot(correctObjectSound3);
                }
            }

            IncrementCounter();
            SetExpectedObject();
        }
        else
        {
            Debug.Log("AudioClip: " + incorrectObjectSound);
            if (incorrectObjectSound != null)
            {
                audioSource[3].PlayOneShot(incorrectObjectSound);
                transform.position = originalPosition;
                Debug.Log("Wrong object picked");
            }
            RestartCounter();
            SetExpectedObject();

        }
        
        Debug.Log("**Counter: " + counter);
    }
}

