using System.Collections.Generic;
using UnityEngine;

public class Director
{
    static List<Camera> cameras = new List<Camera>();
    public static int currentCameraIndex = 0;
    public Director()
    {
        GetCameras();
        foreach (Camera camera in cameras)
        {
            camera.gameObject.SetActive(false);
        }
        ActivateCamera(currentCameraIndex);
    }

    private static void GetCameras()
    {
        GameObject[] cameraObjects = GameObject.FindGameObjectsWithTag("Camera");
        foreach (GameObject cameraObject in cameraObjects)
        {
            Camera camera = cameraObject.GetComponent<Camera>();
            if (camera != null)
            {
                cameras.Add(camera);
            }
        }
    }

    private static void ActivateCamera(int index)
    {
        if (index >= 0 && index < cameras.Count)
        {
            cameras[index].gameObject.SetActive(true);
        }
    }

    private static void DeactivateCamera(int index)
    {
        if (index >= 0 && index < cameras.Count)
        {
            cameras[index].gameObject.SetActive(false);
        }
    }

    public static void SwitchCamera(int newindex)
    {
        if (newindex < 0 || newindex >= cameras.Count)
        {
            Debug.LogError("Invalid camera index: " + newindex);
            return;
        }
        if (newindex == currentCameraIndex)
        {
            return; // No need to switch if it's the same camera
        } else
        {
            cameras[newindex].gameObject.SetActive(true);
            cameras[currentCameraIndex].gameObject.SetActive(false);
            currentCameraIndex = newindex;
        }
    }
}