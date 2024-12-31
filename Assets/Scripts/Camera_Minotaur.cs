using System.Collections;
using UnityEngine;
using Cinemachine;

public class CameraSwitcher : MonoBehaviour
{
    public CinemachineVirtualCamera VCam1;
    public CinemachineVirtualCamera VCam2;
    public float switchDuration = 2f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(SwitchCameraTemporarily());
        }
    }

    private IEnumerator SwitchCameraTemporarily()
    {
        VCam1.Priority = 0;
        VCam2.Priority = 10;
        yield return new WaitForSeconds(switchDuration);

        VCam1.Priority = 10;
        VCam2.Priority = 0;
        VCam2.enabled = false;
    }
}
