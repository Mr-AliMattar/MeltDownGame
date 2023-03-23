using UnityEngine;

public class Spinner : MonoBehaviour
{
    #region Fields
    [SerializeField]                        
    float speed;                            //Rotate speed
    [SerializeField]                        
    float acceleration;                     //Speed increase per frame
    [SerializeField]
    float maxSpeed;                         //Maximum speed
    #endregion

    #region Properties
    public bool Stop { get; set; }
    public float Speed
    {
        get { return speed; }
    }
    public float MaxSpeed
    {
        get { return maxSpeed; }
    }
    #endregion

    #region Methods
    private void Update()
    {
        if (Stop)
        {
            return;
        }
        AccelratedRotation();
    }

    void AccelratedRotation()
    {
        speed += acceleration * Time.deltaTime;
        speed = Mathf.Clamp(speed, 0.0f, maxSpeed);
        transform.Rotate(Vector3.up, speed * Time.deltaTime);
    }
    #endregion
}
