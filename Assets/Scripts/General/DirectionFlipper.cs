using UnityEngine;

public class DirectionFlipper : MonoBehaviour
{
    private const float FlipScale = -1f;
    private const float NonFlipScale = 1f;

    private bool _isFlipped = true;

    public void SetDirection(float direction)
    {
        if ((direction > 0 && _isFlipped == false) || (direction < 0 && _isFlipped == true))
        {
            transform.localScale *= new Vector2(FlipScale, NonFlipScale);
            _isFlipped = !_isFlipped;
        }
    }
}