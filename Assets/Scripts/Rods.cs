using System;
using UnityEngine;

public class Rods : MonoBehaviour
{
    #region Fields
    [SerializeField]
    Vector3 angle;                          //player will be in angle to look like he fell and to be funny
    [SerializeField]
    Vector3 forceDirections;                //The amount of force in every direction (X,Y,Z)
    #endregion

    #region Events
    public static event Action OnThrowPlayer;
    #endregion

    #region Methods
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == ("Player"))
        {
            ThrowPlayer(collision.rigidbody);
            #region Observer
            OnThrowPlayer?.Invoke();
            #endregion
        }
    }
    /// <summary>
    /// Push/Throw player to make it funny
    /// </summary>
    /// <param name="playerBody"></param>
    void ThrowPlayer(Rigidbody playerBody)
    {
        playerBody.AddForce(forceDirections,ForceMode.Impulse);
        playerBody.transform.eulerAngles = angle;
    }
    #endregion
}
