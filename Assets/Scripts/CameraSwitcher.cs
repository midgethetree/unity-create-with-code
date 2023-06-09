using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject POVCamera;
    [SerializeField] private GameObject mainCamera2;
    [SerializeField] private GameObject POVCamera2;

    // Update is called once per frame
    void Update()
    {
      if (Input.GetKeyDown(KeyCode.Z))
        {
            mainCamera.SetActive(!mainCamera.activeSelf);
            POVCamera.SetActive(!POVCamera.activeSelf);
        }
      if (Input.GetKeyDown(KeyCode.M))
        {
            mainCamera2.SetActive(!mainCamera2.activeSelf);
            POVCamera2.SetActive(!POVCamera2.activeSelf);
        }
    }
}
