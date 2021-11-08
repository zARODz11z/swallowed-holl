using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//just acts as a tagging system for the grab code to know the size of an object it is picking up
public class objectSize : MonoBehaviour
{
    public enum objectSizes{tiny, small, medium, large};
    public objectSizes sizes;
}
