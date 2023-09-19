using UnityEngine;

public class RotationStone : MonoBehaviour
{
    public float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       // transform.rotation.y = Time.deltaTime * speed;

        transform.Rotate(0, rotationSpeed, 0);
    }
}
