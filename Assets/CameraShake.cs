using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // Reference of using it from other class
    // CameraShake.Instance.OnShakeCameraPosition(x.xf, x.xf);
    // CameraShake.Instance.OnShakeCameraRotation(x.xf, x.xf);

    private static CameraShake instance;
    public static CameraShake Instance => instance;
    
    private float shakeTime;
    private float shakeIntensity;

    private CameraController cameraController;
    private CameraShake()
    {
        instance= this;
    }

    private void Awake()
    {
        cameraController= GetComponent<CameraController>();
    }

    public void OnShakeCameraPosition(float shakeTime = 1.0f, float shakeIntensity=0.1f)
    {
        this.shakeTime = shakeTime;
        this.shakeIntensity = shakeIntensity;

        StopCoroutine("ShakeByPosition");   // Stop first becuase it could be still running
        StartCoroutine("ShakeByPosition");
    }

    public void OnShakeCameraRotation(float shakeTime = 1.0f, float shakeIntensity = 0.1f)
    {
        this.shakeTime = shakeTime;
        this.shakeIntensity = shakeIntensity;

        StopCoroutine("ShakeByRotation");   // Stop first becuase it could be still running
        StartCoroutine("ShakeByRotation");
    }

    private IEnumerator ShakeByPosition()
    {
        // Turn on the camera shake
        cameraController.IsOnShake = true;

        Vector3 startPosition = transform.position;

        while (shakeTime > 0.0f)
        {
            transform.position = startPosition + Random.insideUnitSphere * shakeIntensity;
            
            shakeTime -= Time.deltaTime;

            yield return null;
        }

        transform.position = startPosition;

        // Turn off the camera shake
        cameraController.IsOnShake = false;
    }

    private IEnumerator ShakeByRotation()
    {
        // Turn on the camera shake
        cameraController.IsOnShake= true;

        Vector3 startPosition = new Vector3(0, 0, 0);

        float power = 10f;

        while (shakeTime > 0.0f)
        {
            float x = 0;
            float y = 0;
            float z = Random.Range(-1.0f, 1.0f);
            transform.rotation = Quaternion.Euler(startPosition + new Vector3(x,y,z) * shakeIntensity * power);

            shakeTime -= Time.deltaTime;

            yield return null;
        }

        transform.rotation = Quaternion.Euler(startPosition);

        // Turn off the camera shake
        cameraController.IsOnShake = false;
    }
}


