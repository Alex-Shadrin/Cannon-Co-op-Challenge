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
    //float smoothing = 1.15f;
    //private Vector3 center;
    private void Start()
    {
        _floatingBody = GetComponent<Rigidbody>();
        //Water = GetComponent<BoxCollider>();
        //center = Water.center;
    }

    private void FixedUpdate()
    {
        //Vector3 power = Vector3.Lerp(center.y, gameObject.transform.position.y, smoothing * Time.deltaTime);
        //float difference = Water.transform.position.y - transform.position.y;
        //isUnderWater = difference < 0;
        SwitchState(isUnderWater);
        if (isUnderWater)
            //_floatingBody.AddForce(new Vector3(0f, Mathf.Clamp((Mathf.Abs(Physics.gravity.y) * difference), 0, Mathf.Abs(Physics.gravity.y) * smoothing), 0f), ForceMode.Acceleration);
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
