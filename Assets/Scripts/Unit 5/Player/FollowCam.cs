using UnityEngine;

/// <summary>
/// This script sets the camera to transform and follow a target, it has boundaries so that the camera doesn't move
/// past the visible map.
/// </summary>
public class FollowCam : MonoBehaviour
{
    public Transform target;
    public float upperBound;
    public float lowerBound;
    public float leftBound;
    public float rightBound;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
        transform.position = new Vector3(Mathf.Clamp(target.position.x,leftBound, rightBound), 
            Mathf.Clamp(target.position.y, lowerBound, upperBound), transform.position.z);
    }
}
