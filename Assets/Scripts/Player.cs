using UnityEngine;
using Blocker;

public class Player : MonoBehaviour, IResettable
{
    Animator anim;
    Vector2 direction = Vector2.right;
    enum RotationType { Clockwise, Counterclockwise }

    Vector2 startingPosition;

    void Start()
    {
        startingPosition = transform.position;
        anim = GetComponent<Animator>();
        UpdateDirectionSprite();
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

        UpdateDirectionSprite();
    }  

    [Command]
    void Speak(string message, bool shout)
    {
        print(shout ? message.ToUpper() : message);
    }

    public void Reset()
    {
        transform.position = startingPosition;
        direction = Vector2.right;
        UpdateDirectionSprite();
    }

    void UpdateDirectionSprite()
    {
        anim.SetInteger("directionX", (int)direction.x);
        anim.SetInteger("directionY", (int)direction.y);
    }
}
