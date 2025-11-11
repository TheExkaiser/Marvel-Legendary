using UnityEngine;

public class SchemeAbilities : MonoBehaviour
{
    public virtual void Setup(GameManager gameManager)
    {

    }

    public virtual void Twist(GameManager gameManager)
    {

    }

    public virtual bool EvilWinsCondition(GameManager gameManager)
    { 
        return false;
    }
}
