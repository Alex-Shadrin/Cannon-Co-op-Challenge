using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Floatable : MonoBehaviour
{
    [SerializeField] private float UnderWaterDrag = 3;
    [SerializeField] private float UnderWaterAngulariDrag = 1;
    [SerializeField] private float AirDrag = 0f;
    [SerializeField] private float AirAngulariDrag = 0.05f;
    [SerializeField] private float FloatingPower = 100f;

    Rigidbody _floatingBody;

    public bool isUnderWater;
    public float difference;
    private void Start()
    {
        _floatingBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        //float difference = transform.position.y - waterHigth;
        //isUnderWater = difference < 0;
        SwitchState(isUnderWater);
        if(isUnderWater)
            _floatingBody.AddForceAtPosition(Vector3.up * FloatingPower * Mathf.Abs(difference), transform.position, ForceMode.Force);
           
    }

    void SwitchState(bool isUnderWater)
    {
        if (isUnderWater)
        {
            _floatingBody.drag = UnderWaterDrag;
            _floatingBody.angularDrag = UnderWaterAngulariDrag;
        }
        else 
        {
            _floatingBody.drag = AirDrag;
            _floatingBody.angularDrag = AirAngulariDrag;
        }
    }
}
