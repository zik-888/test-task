using Enemy;
using UnityEngine.UI;


public abstract class DetectableState
{
    // protected enum StateEnum
    // {
    //     DetectedState,
    //     DetectionProcessesState,
    //     NotDetectedState
    // }
    //
    // protected StateEnum State = StateEnum.NotDetectedState;
    protected Image Image { set; get; }

    protected ILevelController LevelController;

    protected EnemyMovement EnemyMovement;

    public float ValueIndicate
    {
        get => Image.fillAmount;
        set => Image.fillAmount = value;
    }

    public DetectableState(Image image, ILevelController levelController, EnemyMovement enemyMovement)
    {
        Image = image;
        LevelController = levelController;
        EnemyMovement = enemyMovement;
    }

    public virtual bool ChangeState(Detectable detectable, float value) => true;
}