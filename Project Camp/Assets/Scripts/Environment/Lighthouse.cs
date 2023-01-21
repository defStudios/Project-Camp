using UnityEngine;

public class Lighthouse : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Transform lampRotationOrigin;
    [SerializeField] private Transform lamp;
    [SerializeField] private GameObject lampLight;

    private bool lampActive;

    private void Start()
    {
        TurnOffLights();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
            TurnOnLights();

        if (lampActive)
        {
            lamp.RotateAround(lampRotationOrigin.position, Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }

    private void TurnOnLights()
    {
        lampActive = true;
        lampLight.SetActive(true);
    }

    private void TurnOffLights()
    {
        lampActive = false;
        lampLight.SetActive(false);
    }
}
