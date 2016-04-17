using Glitch;

public class Weapon
{
    private string name;
    private float fireRate;
    private float reloadTime;
    private int magSize;
    private int roundsInMag;
    private int magsRemaining;
    private int roundsRemaining;
    private float damage;
    private FiringStyle firingStyle;

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

    public float FireRate
    {
        get
        {
            return fireRate;
        }

        set
        {
            fireRate = value;
        }
    }

    public int MagSize
    {
        get
        {
            return magSize;
        }

        set
        {
            magSize = value;
        }
    }

    public int RoundsInMag
    {
        get
        {
            return roundsInMag;
        }

        set
        {
            roundsInMag = value;
        }
    }

    public int MagsRemaining
    {
        get
        {
            return magsRemaining;
        }

        set
        {
            magsRemaining = value;
        }
    }

    public int RoundsRemaining
    {
        get
        {
            return roundsRemaining;
        }

        set
        {
            roundsRemaining = value;
        }
    }

    public float Damage
    {
        get
        {
            return damage;
        }

        set
        {
            damage = value;
        }
    }

    public FiringStyle FiringStyle
    {
        get
        {
            return firingStyle;
        }

        set
        {
            firingStyle = value;
        }
    }
}
