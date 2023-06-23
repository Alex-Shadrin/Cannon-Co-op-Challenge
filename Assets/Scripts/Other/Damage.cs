//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Damage : MonoBehaviour
//{
//    public int collisionDamage = 10;
//    public string collisionTag;

//    private void OnParticleCollision(Collision coll)
//    {
//        if (coll.gameObject.tag == collisionTag)
//        {
//            Health health = coll.gameObject.GetComponent<Health>();
//            health.TakeHit(collisionDamage);
//        }
//    }
//}
