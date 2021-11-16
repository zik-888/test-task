using Enemy;
using UnityEngine;
using UnityEngine.UI;

class DetectedProcessesState : DetectableState
{
    public DetectedProcessesState(Image image, ILevelController levelController, EnemyMovement enemyMovement) : base(image, levelController, enemyMovement)
    {
    }
    
    public override bool ChangeState(Detectable detectable, float value)
    {
        if (value >= 1)
        {
            Debug.Log($"{EnemyMovement} - new DetectedState");
            detectable.State = new DetectedState(Image, LevelController, EnemyMovement);
            return false;
        }
        else if (value <= 0)
        {
            detectable.State = new NotDetectedState(Image, LevelController, EnemyMovement);
        }
        else
        {
            ValueIndicate = value;
        }

        return true;
    }
}