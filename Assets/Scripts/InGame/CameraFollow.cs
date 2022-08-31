
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Vector3 offset = new Vector3(0, 0, -10);
    private float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;

    private bool isPlayerAlive = false;
    private Transform target;


    private void Update()
    {
        if(isPlayerAlive)
        {
            Vector3 targetPosition = target.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
        

    }

    public void ResetOnPlayer(Transform _target)   // Ne fonctionne pas, a modifer avec les point de respawn
    {
        print("test");
        transform.position = _target.position;
    }

    public void SetActive(GameObject _target)
    {
        isPlayerAlive = true;
        target = _target.transform;
    }

    public void SetInactive()
    {
        isPlayerAlive = false;
    }
}