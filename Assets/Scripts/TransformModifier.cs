using UnityEngine;

public class TransformModifier : MonoBehaviour
{
	private const float SIN_PERIOD = 2f * Mathf.PI;

#pragma warning disable 0649
	[Header("Position")]
	[SerializeField] private Vector2 minPositionChange;
	[SerializeField] private Vector2 maxPositionChange;
	[SerializeField] private float minPositionFrequency;
	[SerializeField] private float maxPositionFrequency;
	[Header("Rotation")]
	[SerializeField] private float minRotationChange;
	[SerializeField] private float maxRotationChange;
	[SerializeField] private float minRotationFrequency;
	[SerializeField] private float maxRotationFrequency;
	[SerializeField] private bool backAndForth;
	[Header("Scale")]
	[SerializeField] private Vector2 minScaleChange;
	[SerializeField] private Vector2 maxScaleChange;
	[SerializeField] private float minScaleFrequency;
	[SerializeField] private float maxScaleFrequency;
#pragma warning restore 0649

	private Transform transformToModify;

	private Vector2 initialPosition;
	private Vector2 positionChange;
	private float positionFrequency;

	private Quaternion initialRotation;
	private float rotationChange;
	private float rotationFrequency;

	private Vector2 initialScale;
	private Vector2 scaleChange;
	private float scaleFrequency;

	private void Start()
	{
		transformToModify = transform;

		initialPosition = transformToModify.localPosition;
		positionChange = new Vector2(Random.Range(minPositionChange.x, maxPositionChange.x), Random.Range(minPositionChange.y, maxPositionChange.y));
		positionFrequency = Random.Range(minPositionFrequency, maxPositionFrequency);

		initialRotation = transformToModify.localRotation;
		rotationChange = Random.Range(minRotationChange, maxRotationChange);
		rotationFrequency = Random.Range(minRotationFrequency, maxRotationFrequency);

		initialScale = transformToModify.localScale;
		scaleChange = new Vector2(Random.Range(minScaleChange.x, maxScaleChange.x), Random.Range(minScaleChange.y, maxScaleChange.y));
		scaleFrequency = Random.Range(minScaleFrequency, maxScaleFrequency);
	}

	private void Update()
	{
		transformToModify.localPosition = initialPosition + Mathf.Sin(Time.time * SIN_PERIOD * positionFrequency) * positionChange;

		if (backAndForth)
		{
			transformToModify.localRotation = initialRotation * Quaternion.Euler(0f, 0f, Mathf.Sin(Time.time * SIN_PERIOD * rotationFrequency) * rotationChange);
		}
		else
		{
			transformToModify.localRotation *= Quaternion.Euler(0f, 0f, Time.deltaTime * rotationChange);
		}

		transformToModify.localScale = initialScale + Mathf.Sin(Time.time * SIN_PERIOD * scaleFrequency) * scaleChange;
	}
}
