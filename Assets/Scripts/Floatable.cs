using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Floatable : MonoBehaviour
{
    [SerializeField] private float underWaterDrag = 3;
    [SerializeField] private float underWaterAngulariDrag = 1;
    [SerializeField] private float airDrag = 0f;
    [SerializeField] private float airAngulariDrag = 0.05f;
    [SerializeField] private float floatingPower = 15f;
    [SerializeField] private float waterHigth = 0f;

    Rigidbody rigidbody;

    public bool isUnderWater;
    public float difference;
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        //float difference = transform.position.y - waterHigth;
        //isUnderWater = difference < 0;
        SwitchState(isUnderWater);
        if(isUnderWater)
            rigidbody.AddForceAtPosition(Vector3.up * floatingPower * Mathf.Abs(difference), transform.position, ForceMode.Force);
           
    }

    void SwitchState(bool isUnderAcid)
    {
        if (isUnderAcid)
        {
            rigidbody.drag = underWaterDrag;
            rigidbody.angularDrag = underWaterAngulariDrag;
        }
        else 
        {
            rigidbody.drag = airDrag;
            rigidbody.angularDrag = airAngulariDrag;
        }

    }
    
}
