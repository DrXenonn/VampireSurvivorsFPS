using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float translation;
    private float straffe;

    public float Translation
    {
        get { return translation; }
    }

    public float Straffe
    {
        get { return straffe; }
    }

    private float currentMovementSpeed;

    private void Start()
    {
        currentMovementSpeed = PlayerStats.Instance.movementSpeed;
    }

    private void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        translation = Input.GetAxis("Vertical") * currentMovementSpeed * Time.deltaTime;
        straffe = Input.GetAxis("Horizontal") * currentMovementSpeed * Time.deltaTime;
        transform.Translate(straffe, 0, translation);
    }
}
