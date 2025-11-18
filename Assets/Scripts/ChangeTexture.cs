using UnityEngine;

public class ChangeTexture : MonoBehaviour
{
    [SerializeField]
    private Texture2D cardArt;

    private Material matFront;

    private void Awake()
    {
        matFront = GetComponent<Renderer>().materials[0];
        ChangeArt();
    }

    public void ChangeArt()
    {
        matFront.SetTexture("_BaseMap", cardArt);
    }
}
