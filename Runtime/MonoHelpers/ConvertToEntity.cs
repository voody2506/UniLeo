using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Voody.UniLeo
{
    public enum ConvertMode
    {
        ConvertAndInject,
        ConvertAndDestroy
    }
    public class ConvertToEntity : MonoBehaviour
    {
        public ConvertMode convertMode;
    }
}
