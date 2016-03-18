public class Player
{
    private string name;
    private Weapon currentWeapon;
    private float speedModifier = 1;
    private float movementSpeed;
    private float jumpSpeed;

    public string Name
    {
        get
        {
            return name;
        }

        set
        {
            name = value;
        }
    }

    public Weapon CurrentWeapon
    {
        get
        {
            return currentWeapon;
        }

        set
        {
            currentWeapon = value;
        }
    }

    public float SpeedModifier
    {
        get
        {
            return speedModifier;
        }

        set
        {
            speedModifier = value;
        }
    }

    public float MovementSpeed
    {
        get
        {
            return movementSpeed * SpeedModifier;
        }

        set
        {
            movementSpeed = value;
        }
    }

    public float JumpSpeed
    {
        get
        {
            return jumpSpeed * SpeedModifier;
        }

        set
        {
            jumpSpeed = value;
        }
    }
}
