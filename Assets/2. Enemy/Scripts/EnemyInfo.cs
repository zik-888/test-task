using UnityEngine;

namespace Enemy
{
    public class EnemyInfo
    {
        public Transform Transform;
        public bool IsDetected { set; get; } = false;

        public string GetDetectedStatus()
        {
            if (IsDetected)
            {
                return "detect :)";
            }
            else
            {
                return "not detect :((";
            }
            
        }
    }
}