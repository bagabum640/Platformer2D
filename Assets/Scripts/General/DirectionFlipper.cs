using UnityEngine;

public class DirectionFlipper : MonoBehaviour
{
    private bool _isFlipped = true;

    public void SetDirection(float direction)
    {
        if ((direction > 0 && _isFlipped == false) || (direction < 0 && _isFlipped == true))
        {
            transform.localScale *= new Vector2(-1f, 1f);
            _isFlipped = !_isFlipped;
        }
    }
}