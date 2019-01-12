using UnityEngine;
using Blocker;

public class Player : MonoBehaviour 
{
    Animator anim;
    Vector2 direction = Vector2.right;
    enum RotationType { Clockwise, Counterclockwise }

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetInteger("directionX", (int)direction.x);
        anim.SetInteger("directionY", (int)direction.y);
    }

    [Command]
    void Move(int amount)
    {
        transform.Translate(direction * amount);
    }

    [Command]
    void Rotate(RotationType rotation)
    {
        switch (rotation)
        {
            case RotationType.Counterclockwise:
                direction = new Vector2(-direction.y, direction.x);
                break;
            case RotationType.Clockwise:
                direction = new Vector2(direction.y, -direction.x);
                break;
        }

        anim.SetInteger("directionX", (int)direction.x);
        anim.SetInteger("directionY", (int)direction.y);
    }  

    [Command]
    void Speak(string message, bool shout)
    {
        print(shout ? message.ToUpper() : message);
    }
}
