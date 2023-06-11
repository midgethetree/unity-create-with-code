using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject POVCamera;
    [SerializeField] private KeyCode key;

    // Update is called once per frame
    void Update()
    {
      if (Input.GetKeyDown(key))
      {
          mainCamera.SetActive(!mainCamera.activeSelf);
          POVCamera.SetActive(!POVCamera.activeSelf);
      }
    }
}
