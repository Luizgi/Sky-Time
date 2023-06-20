using UnityEngine;
using UnityEditor;

public class MovablePlatform : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float distance = 10f;
    public Axis movementAxis = Axis.X; // Variável para escolher o eixo de movimento

    private Vector3 initialPosition;
    private bool movingForward = true;

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        // Determinar a direção do movimento com base no eixo escolhido
        Vector3 movementDirection = Vector3.zero;
        switch (movementAxis)
        {
            case Axis.X:
                movementDirection = Vector3.right;
                break;
            case Axis.Y:
                movementDirection = Vector3.up;
                break;
            case Axis.Z:
                movementDirection = Vector3.forward;
                break;
        }

        if (movingForward)
        {
            transform.Translate(movementDirection * moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(-movementDirection * moveSpeed * Time.deltaTime);
        }

        // Verificar a distância percorrida
        float currentDistance = Vector3.Distance(transform.position, initialPosition);
        if (currentDistance >= distance)
        {
            // Inverter a direção do movimento
            movingForward = !movingForward;
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
    private SerializedProperty distanceProp;
    private SerializedProperty movementAxisProp;

    private void OnEnable()
    {
        moveSpeedProp = serializedObject.FindProperty("moveSpeed");
        distanceProp = serializedObject.FindProperty("distance");
        movementAxisProp = serializedObject.FindProperty("movementAxis");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(moveSpeedProp);
        EditorGUILayout.PropertyField(distanceProp);
        EditorGUILayout.PropertyField(movementAxisProp);

        serializedObject.ApplyModifiedProperties();
    }
}
#endif
