using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAbilities : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void UseSpecialAbility(string set, string name) 
    {
        if (set == "Core Set")
        {
            switch (name) 
            {
                case "Covert Operation":
                    Debug.Log("Covert Ops Special ab");
                    break;
            
            
            }
        }
    }
}
