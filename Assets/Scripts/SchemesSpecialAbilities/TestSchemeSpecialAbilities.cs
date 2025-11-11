using UnityEngine;

public class TestScheme : SchemeAbilities
{
    public override bool EvilWinsCondition(GameManager gameManager)
    {
        if (gameManager.player.resources == 69)
        {
            return true;
        }
        else { return false; }
    }
}
