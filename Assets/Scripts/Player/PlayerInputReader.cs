using UnityEngine;

public class PlayerInputReader : MonoBehaviour
{
    public const string Horizontal = "Horizontal";
    private const KeyCode JumpKey = KeyCode.Space;
    private const KeyCode JumpDownKey = KeyCode.S;
    private const KeyCode VampirismKey = KeyCode.E;
    private const int AttackMouseButton = 0;

    private bool _isJump;
    private bool _isJumpDown;
    private bool _isAttack;
    private bool _isVampirismActivated;

    public float Direction { get; private set; }

    private void Update()
    {
        Direction = Input.GetAxis(Horizontal);        

        if(Input.GetKeyDown(JumpKey)) 
            _isJump = true;

        if(Input.GetKeyDown(JumpDownKey))
            _isJumpDown = true;

        if(Input.GetKeyDown(VampirismKey))
            _isVampirismActivated = true;

        if(Input.GetMouseButtonDown(AttackMouseButton))
            _isAttack = true;

    }

    public bool GetIsJump() => GetBoolAsTrigger(ref _isJump);

    public bool GetIsJumpDown() => GetBoolAsTrigger(ref _isJumpDown);

    public bool GetIsAttack() => GetBoolAsTrigger(ref _isAttack);

    public bool GetIsActivatedVampirism() => GetBoolAsTrigger(ref _isVampirismActivated);
    
    private bool GetBoolAsTrigger(ref bool value)
    {
        bool localValue = value;
        value = false;
        return localValue;
    }
}