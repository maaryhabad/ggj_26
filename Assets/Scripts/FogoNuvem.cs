using UnityEngine;

public class FogoNuvem : MonoBehaviour
{
    public Sprite fogo, nuvem;
    public SpriteRenderer minhaSprite;

    public void mudarFogo(MaskType m) {


        if(m == MaskType.mFogo) {
            minhaSprite.sprite = fogo;
        } else if (m == MaskType.mNuvem){
            minhaSprite.sprite = nuvem;
        }
    
    }


}
