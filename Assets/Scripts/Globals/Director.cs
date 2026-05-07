using System.Collections.Generic;
using UnityEngine;

public static class Director
{
    static List<Camera> cameras = new List<Camera>();
    private static int currentCameraIndex = 0;

    public static int GetCurrentCameraIndex()
    {
        return currentCameraIndex;
    }

    public static int GetNextCameraIndex()
    {
        return (currentCameraIndex + 1) % cameras.Count;
    }

    public static void InitDirector()
    {
        cameras.Clear();
        GetCameras();
        foreach (Camera camera in cameras)
        {
            if (camera == null)
            {
                Debug.LogError("Camera reference is null. Please check the camera setup.");
                continue;
            }
            camera.gameObject.SetActive(false);
        }
        currentCameraIndex = 0;
        ActivateCamera(currentCameraIndex);
    }

    private static void GetCameras()
    {
        GameObject[] cameraObjects = GameObject.FindGameObjectsWithTag("MainCamera");
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