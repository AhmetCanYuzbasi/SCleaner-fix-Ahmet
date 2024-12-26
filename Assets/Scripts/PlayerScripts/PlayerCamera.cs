using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    
    [SerializeField] Transform playerTransform;
    
    float _smoothTime = 0.25f;
    Vector3 _offset = new Vector3(0, 0, -10f);
    Vector3 _velocity = Vector3.zero;

    void Start(){

    }


    void Update()
    {
        //TrackPlayer();
    }

    private void TrackPlayer(){
        //Vector3 targetPosition = _offset + playerTransform.position;
        //transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, _smoothTime);
        //transform.position = playerTransform.position;
    }

    //public Transform player;   // The player transform to follow
    public float smoothSpeed = 0.125f;  // The speed at which the camera follows the player
    public Vector3 offset = new Vector3(0, 0, -10f);     // The offset between the player and camera (set in the Inspector)

    void LateUpdate()
    {
        // The desired position is the player's position + the offset
        Vector3 desiredPosition = playerTransform.position + offset;

        // Smoothly move the camera towards the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Update the camera's position
        transform.position = smoothedPosition;
    }
    
}
