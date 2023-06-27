using UnityEngine;
using UnityEditor;

public class MovablePlatform : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float forwardDistance = 10f;
    public float backwardDistance = 10f;
    public Axis movementAxis = Axis.X;

    private Vector3 initialPosition;
    private bool movingForward = true;
    private bool isCharacterOnPlatform = false;
    private Transform characterTransform;



    private void Start()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        Vector3 movementDirection = GetMovementDirection();

        if (isCharacterOnPlatform && characterTransform != null)
        {
            characterTransform.Translate(movementDirection * moveSpeed * Time.deltaTime);
        }
        else
        {
            if (movingForward)
            {
                transform.Translate(movementDirection * moveSpeed * Time.deltaTime);
            }
            else
            {
                transform.Translate(-movementDirection * moveSpeed * Time.deltaTime);
            }

            float currentDistance = Vector3.Distance(transform.position, initialPosition);
            float maxDistance = movingForward ? forwardDistance : backwardDistance;
            if (currentDistance >= maxDistance)
            {
                movingForward = !movingForward;
            }
        }
    }



    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Character"))
        {
            //isCharacterOnPlatform = false;
            //characterTransform = null;


        }
    }

    private Vector3 GetMovementDirection()
    {
        switch (movementAxis)
        {
            case Axis.X:
                return Vector3.right;
            case Axis.Y:
                return Vector3.up;
            case Axis.Z:
                return Vector3.forward;
            default:
                return Vector3.zero;
        }
    }
}

public enum Axis
{
    X,
    Y,
    Z
}

#if UNITY_EDITOR
[CustomEditor(typeof(MovablePlatform))]
public class MovablePlatformEditor : Editor
{
    private SerializedProperty moveSpeedProp;
    private SerializedProperty forwardDistanceProp;
    private SerializedProperty backwardDistanceProp;
    private SerializedProperty movementAxisProp;

    private void OnEnable()
    {
        moveSpeedProp = serializedObject.FindProperty("moveSpeed");
        forwardDistanceProp = serializedObject.FindProperty("forwardDistance");
        backwardDistanceProp = serializedObject.FindProperty("backwardDistance");
        movementAxisProp = serializedObject.FindProperty("movementAxis");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(moveSpeedProp);
        EditorGUILayout.PropertyField(forwardDistanceProp);
        EditorGUILayout.PropertyField(backwardDistanceProp);
        EditorGUILayout.PropertyField(movementAxisProp);

        serializedObject.ApplyModifiedProperties();
    }
}
#endif
