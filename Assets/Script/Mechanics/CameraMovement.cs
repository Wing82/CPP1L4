using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float minXPos;
    [SerializeField] private float maxXPos;
    [SerializeField] private float minYPos;
    [SerializeField] private float maxYPos;

    public Transform playerRef;

    //Camera mainCamera;

    //// Start is called once before the first execution of Update after the MonoBehaviour is created
    //private void Start()
    //{
    //    mainCamera = Camera.main;
    //}

    // Update is called once per frame
    void Update()
    {
        if(!playerRef) return;

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(playerRef.position.x, minXPos, maxXPos);
        pos.y = Mathf.Clamp(playerRef.position.y, minYPos, maxYPos);
        transform.position = pos;
    }
}
