using UnityEngine.UI;
using UnityEngine;
using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

public static class UnityExtensionMethods
{
	#region MEMBERS

	#endregion

	#region PROPERTIES

	#endregion

	#region FUNCTIONS

	// VECTOR3 EXTENSIONS
	public static Vector3 Set (this Vector3 target, float valueX, float valueY, float valueZ)
	{
		Vector3 output = target;

		output.x = valueX;
		output.y = valueY;
		output.z = valueZ;

		return output;
	}

	public static Vector3 SetX (this Vector3 target, float value)
	{
		Vector3 output = target;

		output.x = value;

		return output;
	}

	public static Vector2 SetX (this Vector2 target, float value)
	{
		return (Vector2)((Vector3)target).SetX(value);
	}

	public static Vector3 SetY (this Vector3 target, float value)
	{
		Vector3 output = target;

		output.y = value;

		return output;
	}

	public static Vector2 SetY (this Vector2 target, float value)
	{
		return (Vector2)((Vector3)target).SetY(value);
	}

	public static Vector3 SetZ (this Vector3 target, float value)
	{
		Vector3 output = target;

		output.z = value;

		return output;
	}

	public static bool Approximately (this Vector3 target, float xDelta, float yDelta, float zDelta)
	{
		return (Mathf.Approximately(target.x, xDelta) == true && Mathf.Approximately(target.y, yDelta) == true && Mathf.Approximately(target.z, zDelta) == true);
	}

	// VECTOR2 EXTENSIONS
	public static bool Approximately (this Vector2 target, float xDelta, float yDelta)
	{
		return (Mathf.Approximately(target.x, xDelta) == true && Mathf.Approximately(target.y, yDelta) == true);
	}

	public static bool Approximately (this Vector2 target, Vector2 other)
	{
		return (Mathf.Approximately(target.x, other.x) == true && Mathf.Approximately(target.y, other.y) == true);
	}

	// TRANSFORM EXTENSIONS
	public static void ResetLocal (this Transform target)
	{
		target.localPosition = Vector3.zero;
		target.localRotation = Quaternion.identity;
		target.localScale = Vector3.one;
	}

	public static void ResetLocal (this Transform target, Transform parent)
	{
		target.SetParent(parent);

		ResetLocal(target);
	}

	public static void MoveTo (this Transform obj, Transform target)
	{
		obj.position = target.position;
		obj.rotation = target.rotation;
	}

	public static void MoveTo (this Rigidbody obj, Transform target)
	{
		obj.position = target.position;
		obj.rotation = target.rotation;
	}

	public static void MergeWith (this Transform obj, Transform target, bool keepWorldPosition = true)
	{
		obj.SetParent(target, keepWorldPosition);
		obj.position = target.position;
		obj.rotation = target.rotation;
	}

	public static void MergeCopy (this Transform obj, Transform target, bool keepWorldPosition = true)
	{
		obj.SetParent(target, keepWorldPosition);
		obj.position = target.position;
		obj.rotation = target.rotation;
		obj.localScale = target.localScale;
	}

	public static void CopySetupFrom (this Transform obj, Transform target)
	{
		obj.SetParent(target.parent);
		obj.position = target.position;
		obj.rotation = target.rotation;
		obj.localScale = target.localScale;
	}

	public static bool IsCloserInHierarchyThan (this Transform source, Transform target)
	{
		int sourceDepth = GetHierarchyDepth(source);
		int targetDepth = GetHierarchyDepth(target);

		bool sourceIsDeeper = (sourceDepth > targetDepth);
		int depthDifference = Mathf.Max(sourceDepth, targetDepth) - Mathf.Min(sourceDepth, targetDepth);

		Transform sourceParent = null;
		Transform targetParent = null;

		// find mutual parent
		for (int i = 0; i < Mathf.Max(sourceDepth, targetDepth); i++)
		{
			int sourceIndex = (sourceIsDeeper == true) ? i + depthDifference : i;
			int targetIndex = (sourceIsDeeper == false) ? i + depthDifference : i;

			sourceParent = source.GetParentByIndex(sourceIndex);
			targetParent = target.GetParentByIndex(targetIndex);

			// save mutual parent
			if (sourceParent == targetParent)
			{
				sourceParent = source.GetParentByIndex(sourceIndex - 1);
				targetParent = target.GetParentByIndex(targetIndex - 1);

				break;
			}
		}

		// compare depths
		return sourceParent.GetSiblingIndex() > targetParent.GetSiblingIndex();
	}

	public static Transform GetParentByIndex (this Transform source, int depth)
	{
		Transform targetParent = source;

		for (int i = 0; i < depth; i++)
		{
			if (targetParent != null)
			{
				targetParent = targetParent.parent;
			}
		}

		return targetParent;
	}

	public static int GetHierarchyDepth (this Transform source)
	{
		Transform targetParent = source;
		int depth = 0;

		while (targetParent != null)
		{
			targetParent = targetParent.parent;

			if (targetParent != null)
			{
				depth++;
			}
		}

		return depth;
	}

	public static void MoveChildrenTo (this Transform from, Transform to)
	{
		if (from == null || to == null)
		{
			return;
		}

		while (from.childCount > 0)
		{
			from.GetChild(0).SetParent(to);
		}
	}

	public static int ChildCountDeep (this Transform target)
	{
		int count = 0;

		if (target == null)
		{
			return count;
		}

		count = target.childCount;

		for (int i = 0; i < target.childCount; ++i)
		{
			Transform childTransform = target.GetChild(i);
			count += (childTransform != null) ? target.GetChild(i).ChildCountDeep() : 0;
		}

		return count;
	}

	public static List<Transform> GetFirstLevelChildren (this Transform target)
	{
		if (target == null)
		{
			return new List<Transform>();
		}

		List<Transform> result = new List<Transform>();

		for (int i = 0; i < target.childCount; ++i)
		{
			result.Add(target.GetChild(i));
		}

		return result;
	}

	public static string GetObjectHierarchyPath (this GameObject target)
	{
		return GetObjectHierarchyPath(target.transform);
	}

	public static string GetObjectHierarchyPath (this Transform target, bool addSelfName = false)
	{
		string path = "";
		Transform currentParent = target.parent;

		if (addSelfName == true)
		{
			path += target.name;
		}

		while (currentParent != null)
		{
			path = currentParent.name + "/" + path;
			currentParent = currentParent.parent;
		}

		return path;
	}

	// COMPONENT EXTENSION
	public static void ActivateGameObject (this Component component, bool state = true)
	{
		if (component != null)
		{
			component.gameObject.SetActiveOptimized(state);
		}
	}

	public static void DeactivateGameObject (this Component component)
	{
		if (component != null)
		{
			component.gameObject.SetActiveOptimized(false);
		}
	}

	public static void SetGameObjectState (this Component component, bool state)
	{
		if (component != null)
		{
			component.gameObject.SetActiveOptimized(state);
		}
	}

	public static T GetComponentInParentExcludeSelf<T> (this Component component) where T : Component
	{
		if (component == null || component.transform.parent == null)
		{
			return default(T);
		}

		return component.transform.parent.GetComponentInParent<T>();
	}

	public static float GetMaxValue (this AnimationCurve source)
	{
		Keyframe[] keys = source.keys;
		float maxValue = -Mathf.Infinity;

		for (int i = 0; i < keys.Length; i++)
		{
			maxValue = (keys[i].value > maxValue) ? keys[i].value : maxValue;
		}

		return maxValue;
	}

	public static float GetMinValue (this AnimationCurve source)
	{
		Keyframe[] keys = source.keys;
		float minValue = Mathf.Infinity;

		for (int i = 0; i < keys.Length; i++)
		{
			minValue = (keys[i].value < minValue) ? keys[i].value : minValue;
		}

		return minValue;
	}

	public static void ClearChildren (this Transform target, bool immediate = false)
	{
		List<GameObject> children = new List<GameObject>();

		foreach (Transform child in target)
		{
			children.Add(child.gameObject);
		}

		for (int i = 0; i < children.Count; i++)
		{
			if (immediate == true)
			{
				GameObject.DestroyImmediate(children[i]);
			}
			else
			{
				GameObject.Destroy(children[i]);
			}
		}
	}

	public static void CopyChildrenFrom (this Transform target, Transform source)
	{
		if (target == null || source == null)
		{
			return;
		}

		foreach (Transform child in source.transform)
		{
			GameObject objectCopy = GameObject.Instantiate<GameObject>(child.gameObject, target);
			objectCopy.transform.SetParent(target, true);
		}
	}

	public static void SetStaticRecuerively (this GameObject target, bool staticFlag)
	{
		if (target == null)
		{
			return;
		}

		Transform[] children = target.GetComponentsInChildren<Transform>(true);

		for (int i = 0; i < children.Length; ++i)
		{
			children[i].gameObject.isStatic = staticFlag;
		}
	}

	public static T GetComponentRecursively<T> (this GameObject target) where T : Component
	{
		if (target == null || target.transform == null)
		{
			return null;
		}

		T foundComponent = null;
		Transform currentCheck = target.transform;

		// iterate over target parents to check if it is a child of a road section
		while (foundComponent == null)
		{
			foundComponent = currentCheck.GetComponent<T>();

			if (foundComponent == null)
			{
				currentCheck = currentCheck.parent;
			}

			// break out of loop if nothing found
			if (currentCheck == null)
			{
				break;
			}
		}

		return foundComponent;
	}

	public static T[] GetComponentsInChildrenPath<T> (this Component target, int inPathOccurences) where T : Component
	{
		List<T> result = new List<T>();

		if (target == null || target.transform == null)
		{
			return result.ToArray();
		}

		T inObjectComponent = target.GetComponent<T>();

		if (inObjectComponent != null)
		{
			result.Add(inObjectComponent);
			inPathOccurences--;
		}

		if (inPathOccurences > 0)
		{
			for (int i = 0; i < target.transform.childCount; ++i)
			{
				Transform child = target.transform.GetChild(i);
				result.AddRange(GetComponentsInChildrenPath<T>(child, inPathOccurences));
			}
		}

		return result.ToArray();
	}

	// ARRAY EXTENSIONS
	public static void DestroyClear<T> (this List<T> target, bool immediate = false) where T : Component
	{
		for (int i = 0; i < target.Count; i++)
		{
			if (target[i] == null)
			{
				continue;
			}

			if (immediate == true || (Application.isEditor == true && Application.isPlaying == false))
			{
				GameObject.DestroyImmediate(target[i].gameObject);
			}
			else
			{
				GameObject.Destroy(target[i].gameObject);
			}
		}

		target.Clear();
	}

	public static void SetParentAll<T> (this List<T> target, Transform parent, bool worldPositionStays = true) where T : Component
	{
		if (target == null || parent == null)
		{
			return;
		}

		for (int i = 0; i < target.Count; i++)
		{
			if (target[i] == null)
			{
				continue;
			}

			target[i].transform.SetParent(parent, worldPositionStays);
		}
	}

	public static void DestroyClear (this List<GameObject> target, bool immediate = false)
	{
		for (int i = 0; i < target.Count; i++)
		{
			if (target[i] == null)
			{
				continue;
			}

			if (immediate == true)
			{
				GameObject.DestroyImmediate(target[i]);
			}
			else
			{
				GameObject.Destroy(target[i]);
			}
		}

		target.Clear();
	}

	public static void RemoveNullElements<T> (this List<T> target)
	{
		for (int i = 0; i < target.Count; ++i)
		{
			if (target[i] == null)
			{
				target.RemoveAt(i);
				--i;
			}
		}
	}

	public static void SetActiveAll (this GameObject[] target, bool state)
	{
		for (int i = 0; i < target.Length; i++)
		{
			if (target[i] != null)
			{
				target[i].SetActiveOptimized(state);
			}
		}
	}

	public static void SetActiveAll (this List<GameObject> target, bool state)
	{
		for (int i = 0; i < target.Count; i++)
		{
			if (target[i] != null)
			{
				target[i].SetActiveOptimized(state);
			}
		}
	}

	public static void SetGameObjectActiveAll<T> (this List<T> target, bool state) where T : Component
	{
		for (int i = 0; i < target.Count; i++)
		{
			if (target[i] != null)
			{
				target[i].gameObject.SetActiveOptimized(state);
			}
		}
	}

	public static void SetActiveOptimized (this GameObject target, bool state)
	{
		if (target != null && target.activeSelf != state)
		{
			target.SetActive(state);
		}
	}

	public static void Shuffle<T> (this List<T> target, int times) where T : class
	{
		int firstElementSwapIndex = -1;
		int secondElementSwapIndex = -1;
		T temporary;

		for (int currentShuffle = 0; currentShuffle < times; ++currentShuffle)
		{
			for (int i = 0; i < target.Count; ++i)
			{
				firstElementSwapIndex = UnityEngine.Random.Range(0, target.Count - 1);
				secondElementSwapIndex = UnityEngine.Random.Range(0, target.Count - 1);

				temporary = target[secondElementSwapIndex];
				target[secondElementSwapIndex] = target[firstElementSwapIndex];
				target[firstElementSwapIndex] = temporary;
			}
		}
	}

	public static bool ContainsElement (this string[] target, string lookupObject)
	{
		for (int i = 0; i < target.Length; i++)
		{
			if (target[i] == lookupObject)
			{
				return true;
			}
		}

		return false;
	}

	public static bool ContainsAny (this string[] target, string[] lookupArray)
	{
		for (int i = 0; i < target.Length; i++)
		{
			for (int j = 0; j < lookupArray.Length; j++)
			{
				if (target[i] == lookupArray[j])
				{
					return true;
				}
			}
		}

		return false;
	}

	public static bool ContainsAny (this List<string> target, List<string> lookupArray)
	{
		for (int i = 0; i < target.Count; i++)
		{
			for (int j = 0; j < lookupArray.Count; j++)
			{
				if (target[i] == lookupArray[j])
				{
					return true;
				}
			}
		}

		return false;
	}

	public static bool ContainsElement<T> (this T[] target, T lookupObject) where T : UnityEngine.Object
	{
		for (int i = 0; i < target.Length; i++)
		{
			if (target[i] == lookupObject)
			{
				return true;
			}
		}

		return false;
	}

	public static void ReduceTo<T> (this List<T> target, int targetCount)
	{
		if (target == null || targetCount < 0 || target.Count < targetCount)
		{
			return;
		}

		int toRemove = target.Count - targetCount;

		for (int i = 0; i < toRemove; ++i)
		{
			target.RemoveAt(target.Count - 1);
		}
	}


	public static void ReduceTo<T> (this T[] target, int targetCount)
	{
		if (target == null || targetCount < 0 || target.Length < targetCount)
		{
			return;
		}

		Array.Resize(ref target, targetCount);
	}

	public static List<GameObject> ConvertToGameObjectList<T> (this List<T> target) where T : Component
	{
		if (target == null)
		{
			return new List<GameObject>();
		}

		List<GameObject> result = new List<GameObject>(target.Count);

		for (int i = 0; i < target.Count; ++i)
		{
			result.Add(target[i].gameObject);
		}

		return result;
	}

	public static void ChangeLayerRecursively (this GameObject target, string layerName)
	{
		target.layer = LayerMask.NameToLayer(layerName);

		Transform[] children = target.GetComponentsInChildren<Transform>();

		for (int i = 0; i < children.Length; ++i)
		{
			children[i].gameObject.layer = LayerMask.NameToLayer(layerName);
		}
	}

	public static void ChangeLayerRecursively (this GameObject target, int layerIndex)
	{
		if (target == null)
		{
			return;
		}

		target.layer = layerIndex;

		Transform[] children = target.GetComponentsInChildren<Transform>(true);

		for (int i = 0; i < children.Length; ++i)
		{
			children[i].gameObject.layer = layerIndex;
		}
	}

	public static int GetLayerMaskBitValue (this GameObject target)
	{
		if (target == null)
		{
			return 0;
		}

		return (1 << target.layer);
	}

	public static bool IsObjectOnLayerMask (this GameObject target, LayerMask checkLayer)
	{
		if (target == null)
		{
			return false;
		}

		return (target.GetLayerMaskBitValue() & checkLayer) != 0;
	}

	public static void SetPivot (this RectTransform rectTransform, Vector2 pivot)
	{
		if (rectTransform == null)
		{
			return;
		}

		Vector2 size = rectTransform.rect.size;
		Vector2 deltaPivot = rectTransform.pivot - pivot;
		Vector3 deltaPosition = new Vector3(deltaPivot.x * size.x, deltaPivot.y * size.y);

		rectTransform.pivot = pivot;
		rectTransform.localPosition -= deltaPosition;
	}

	// ENUMERATOR EXTENSIONS
	public static bool EnumContains<T> (this T[] keys, T flag) where T : struct, IConvertible
	{
		if (typeof(T).IsEnum == false)
		{
			return false;
		}

		for (int i = 0; i < keys.Length; i++)
		{
			UInt32 flagValue = Convert.ToUInt32(flag);
			UInt32 keyValue = Convert.ToUInt32(keys[i]);

			if (flagValue == keyValue)
			{
				return true;
			}
		}

		return false;
	}

	public static bool BuiltinContains<T> (this T[] collection, T value) where T : UnityEngine.Object
	{
		for (int i = 0; i < collection.Length; i++)
		{
			if (collection[i] == value)
			{
				return true;
			}
		}

		return false;
	}

	// FLOAT EXTENSIONS
	public static float Warp (this float value, float min, float max)
	{
		value = (value > max) ? min + value - max : value;
		value = (value < min) ? max - min - value : value;

		return value;
	}

	public static float Clamp (this float value, float min, float max)
	{
		value = (value > max) ? max : value;
		value = (value < min) ? min : value;

		return value;
	}

	public static bool IsInRange (this float source, float min, float max)
	{
		if (min > max)
		{
			return (source >= max && source <= min);
		}

		return (source >= min && source <= max);
	}

	public static bool IsInRange (this int source, int min, int max)
	{
		if (min > max)
		{
			return (source >= max && source < min);
		}

		return (source >= min && source < max);
	}

	public static bool IsInRange (this float source, Vector2 range)
	{
		if (range.x > range.y)
		{
			return (source >= range.y && source <= range.x);
		}

		return (source >= range.x && source <= range.y);
	}

	public static bool IsInRange (this int source, Vector2 range)
	{
		if ((int)range.x > (int)range.y)
		{
			return (source >= (int)range.y && source < (int)range.x);
		}

		return (source >= (int)range.x && source < (int)range.y);
	}

	public static bool IsInRect (this Vector3 point, Rect targetRect)
	{
		Vector2 convertedPoint = new Vector2(point.x, point.y);

		return convertedPoint.IsInRect(targetRect);
	}

	public static bool IsInRect (this Vector2 point, Rect targetRect)
	{
		Vector2[] rectCorners = new Vector2[]{
			new Vector2(targetRect.xMin, targetRect.yMin),
			new Vector2(targetRect.xMin, targetRect.yMax),
			new Vector2(targetRect.xMax, targetRect.yMax),
			new Vector2(targetRect.xMax, targetRect.yMin)
		};

		return point.IsInRect(rectCorners);
	}
	public static bool IsInRect (this Vector3 point, Vector3[] corners)
	{
		return
			point.x >= corners[0].x &&
			point.x <= corners[1].x &&
			point.y >= corners[0].y &&
			point.y <= corners[2].y;
	}

	public static bool IsInRect (this Vector2 point, Vector2[] corners)
	{
		return
			point.x >= corners[0].x &&
			point.x <= corners[1].x &&
			point.y >= corners[0].y &&
			point.y <= corners[2].y;
	}

	public static bool IsInBounds2D (this Vector3 point, Bounds bounds)
	{
		return
			point.x >= bounds.min.x &&
			point.x <= bounds.max.x &&
			point.y >= bounds.min.y &&
			point.y <= bounds.max.y;
	}

	public static int Wrap (this int value, int min, int max)
	{
		if (value >= max)
		{
			return (value - (max - min)).Wrap(min, max);
		}
		else if (value < min)
		{
			return (value + (max - min)).Wrap(min, max);
		}

		return value;
	}

	// BUILTIN ARRAY EXTENSIONS
	// skips "out of range" error in List<T> arrays
	public static T GetValue<T> (this IList<T> collection, int index)
	{
		if (collection == null)
		{
			return default(T);
		}

		if (index >= 0 && index < collection.Count)
		{
			return collection[index];
		}

		return default(T);
	}

	public static T GetValue<T> (this T[] collection, int index)
	{
		if (collection == null)
		{
			return default(T);
		}

		if (index >= 0 && index < collection.Length)
		{
			return collection[index];
		}

		return default(T);
	}

	/// <summary>
	/// Returns value even if out of range. 
	/// </summary>
	public static T GetValueClamped<T> (this IList<T> collection, int index)
	{
		if (collection == null || collection.Count == 0)
		{
			return default(T);
		}

		index = Mathf.Clamp(index, 0, collection.Count - 1);

		return collection[index];
	}

	/// <summary>
	/// Returns max value instead of null when out of range. 
	/// </summary>
	public static T GetValueClamped<T> (this T[] collection, int index)
	{
		if (collection == null || collection.Length == 0)
		{
			return default(T);
		}

		index = Mathf.Clamp(index, 0, collection.Length - 1);

		return collection[index];
	}

	public static int IndexOf<T> (this T[] collection, T target) where T : Component
	{
		for (int i = 0; i < collection.Length; i++)
		{
			if (target == collection[i])
			{
				return i;
			}
		}

		return -1;
	}

	public static T[] PushNewValue<T> (this T[] array, T valueToPush)
	{
		for (int i = 0; i < array.Length - 1; i++)
		{
			array[i] = array[i + 1];
		}

		array[array.Length - 1] = valueToPush;

		return array;
	}

	public static float GetAverageValue (this float[] array)
	{
		float average = 0;

		if (array.Length == 0)
		{
			return 0;
		}

		for (int i = 0; i < array.Length; i++)
		{
			average += array[i];
		}

		return average / array.Length;
	}

	public static Vector3 GetAverageValue (this Vector3[] array)
	{
		Vector3 average = Vector3.zero;

		if (array.Length == 0)
		{
			return Vector3.zero;
		}

		for (int i = 0; i < array.Length; i++)
		{
			average += array[i];
		}

		return average / array.Length;
	}

	public static List<MeshRenderer> GetMeshRenderersByTag (this GameObject source, string targetTag)
	{
		List<MeshRenderer> foundMeshRenderers = new List<MeshRenderer>();

		if (source == null)
		{
			return foundMeshRenderers;
		}

		MeshRenderer[] sourceRenderers = source.GetComponentsInChildren<MeshRenderer>() as MeshRenderer[];

		// get mesh renderers by tag
		foreach (MeshRenderer airplaneMeshRenderer in sourceRenderers)
		{
			GameObject currentElement = airplaneMeshRenderer.gameObject;
			if (currentElement.tag != targetTag)
			{
				continue;
			}

			foundMeshRenderers.Add(airplaneMeshRenderer);
		}

		return foundMeshRenderers;
	}

	public static Mesh GetCopy (this Mesh original)
	{
		if (original == null)
		{
			return null;
		}

		Mesh result = new Mesh();

		result.vertices = original.vertices;
		result.triangles = original.triangles;
		result.uv = original.uv;
		result.uv2 = original.uv2;
		result.uv3 = original.uv3;
		result.uv4 = original.uv4;
		result.tangents = original.tangents;
		result.normals = original.normals;
		result.colors = original.colors;
		result.bounds = original.bounds;
		result.bindposes = original.bindposes;
		result.boneWeights = original.boneWeights;

		return result;
	}

	public static void WriteColor (this BinaryWriter binaryWriter, Color color)
	{
		binaryWriter.Write(color.r);
		binaryWriter.Write(color.g);
		binaryWriter.Write(color.b);
		binaryWriter.Write(color.a);
	}

	public static Color ReadColor (this BinaryReader binaryReader)
	{
		Color result = new Color();

		result.r = binaryReader.ReadSingle();
		result.g = binaryReader.ReadSingle();
		result.b = binaryReader.ReadSingle();
		result.a = binaryReader.ReadSingle();

		return result;
	}

	public static void WriteVector3 (this BinaryWriter binaryWriter, Vector3 vector)
	{
		binaryWriter.Write(vector.x);
		binaryWriter.Write(vector.y);
		binaryWriter.Write(vector.z);
	}

	public static Vector3 ReadVector3 (this BinaryReader binaryReader)
	{
		Vector3 result = Vector3.zero;

		result.x = binaryReader.ReadSingle();
		result.y = binaryReader.ReadSingle();
		result.z = binaryReader.ReadSingle();

		return result;
	}

	public static void WriteQuaternion (this BinaryWriter binaryWriter, Quaternion quaternion)
	{
		binaryWriter.Write(quaternion.w);
		binaryWriter.Write(quaternion.x);
		binaryWriter.Write(quaternion.y);
		binaryWriter.Write(quaternion.z);
	}

	public static Quaternion ReadQuaternion (this BinaryReader binaryReader)
	{
		Quaternion result = Quaternion.identity;

		result.w = binaryReader.ReadSingle();
		result.x = binaryReader.ReadSingle();
		result.y = binaryReader.ReadSingle();
		result.z = binaryReader.ReadSingle();

		return result;
	}

	public static T ReadEnum<T> (this BinaryReader binaryReader)
	{
		T enumValue = (T)Enum.ToObject(typeof(T), binaryReader.ReadInt32());
		enumValue = (Enum.IsDefined(typeof(T), enumValue) == true) ? enumValue : default(T);

		return enumValue;
	}

	// STRING EXTENSIONS
	public static string SetColor (this string sourceText, Color textColor)
	{
		return string.Format("<color=#{0}>{1}</color>", textColor.ToHex(), sourceText);
	}

	public static string ToRainbow (this string sourceText)
	{
		string finalText = "";

		foreach (char letter in sourceText)
		{
			Color randomColor = new Color(
				UnityEngine.Random.Range(0.0f, 1.0f),
				UnityEngine.Random.Range(0.0f, 1.0f),
				UnityEngine.Random.Range(0.0f, 1.0f)
			);

			if (letter == ' ')
			{
				finalText += letter;
			}
			else
			{
				finalText += string.Format("<color=#{0}>{1}</color>", randomColor.ToHex(), letter);
			}
		}

		return finalText;
	}

	public static string ToHex (this Color sourceColor)
	{
		Color32 color = (Color32)sourceColor;

		return string.Format("{0:X2}{1:X2}{2:X2}", color.r, color.g, color.b);
	}

	public static void PrintContent (this List<string> listToPrint)
	{
		string finalDebug = string.Empty;

		for (int i = 0; i < listToPrint.Count; i++)
		{
			finalDebug += string.Format("- {0}{1}", listToPrint[i], (i < listToPrint.Count) ? "\n" : "");
		}

		Debug.Log("List contents: \n" + finalDebug);
	}

	public static void PrintContent (this List<object> listToPrint)
	{
		string finalDebug = string.Empty;

		for (int i = 0; i < listToPrint.Count; i++)
		{
			finalDebug += string.Format("- {0}{1}", listToPrint[i].ToString(), (i < listToPrint.Count) ? "\n" : "");
		}

		Debug.Log("List contents: \n" + finalDebug);
	}

	public static void PrintContent (this List<UICharInfo> listToPrint)
	{
		string finalDebug = string.Empty;

		for (int i = 0; i < listToPrint.Count; i++)
		{
			finalDebug += string.Format("- {0}{1}", listToPrint[i].ToString(), (i < listToPrint.Count) ? "\n" : "");
		}

		Debug.Log("List contents: \n" + finalDebug);
	}

	public static Bounds GetObjectBoundsFromColliders (this GameObject target)
	{
		Bounds bounds = new Bounds(target.transform.position, Vector3.zero);
		Collider[] targetColliders = target.GetComponentsInChildren<Collider>();

		for (int i = 0; i < targetColliders.Length; i++)
		{
			bounds.Encapsulate(targetColliders[i].bounds);
		}

		return bounds;
	}

	public static Bounds GetObjectBoundsFromRenderers (this GameObject target)
	{
		Bounds bounds = new Bounds(target.transform.position, Vector3.zero);
		MeshRenderer[] targetRenderers = target.GetComponentsInChildren<MeshRenderer>();

		for (int i = 0; i < targetRenderers.Length; i++)
		{
			bounds.Encapsulate(targetRenderers[i].bounds);
		}

		return bounds;
	}

	public static Vector2 GetPointOnPathByTime (this Vector2[] curvePoints, float time)
	{
		Vector3[] convertedPoints = new Vector3[curvePoints.Length];

		for (int i = 0; i < curvePoints.Length; i++)
		{
			convertedPoints[i] = curvePoints[i];
		}

		return GetPointOnPathByTime(convertedPoints, time);
	}

	public static Vector3 GetPointOnPathByTime (this Vector3[] curvePoints, float time)
	{
		if (curvePoints.Length < 2)
		{
			return curvePoints[0];
		}

		if (time >= 1.0f)
		{
			return curvePoints[curvePoints.Length - 1];
		}

		float totalPathDistance = curvePoints.GetPathDistance();
		float currentPathStartTime = 0.0f;

		for (int i = 0; i < curvePoints.Length - 1; i++)
		{
			Vector3 startPoint = curvePoints[i];
			Vector3 endPoint = curvePoints[i + 1];

			Vector3 nextAnchorDirection = (endPoint - startPoint).normalized;
			float nextAnchorDistance = Vector3.Distance(startPoint, endPoint);
			float normalizedSectionDistance = nextAnchorDistance / totalPathDistance;

			if (time > currentPathStartTime + normalizedSectionDistance)
			{
				currentPathStartTime += normalizedSectionDistance;
				continue;
			}

			float timeBasedDistance = (time - currentPathStartTime) / normalizedSectionDistance;
			Vector3 foundPoint = startPoint + nextAnchorDirection * timeBasedDistance * nextAnchorDistance;

			return foundPoint;
		}

		return curvePoints[curvePoints.Length - 1];
	}

	public static Vector3 GetPointOnPathByTime (this Transform curvePointsRoot, float time)
	{
		Vector3[] points = new Vector3[curvePointsRoot.childCount];

		for (int i = 0; i < points.Length; i++)
		{
			points[i] = curvePointsRoot.GetChild(i).position;
		}

		return GetPointOnPathByTime(points, time);
	}

	public static float GetPathDistance (this Vector3[] curvePoints)
	{
		float totalDistance = 0;

		for (int i = 0; i < curvePoints.Length - 1; i++)
		{
			totalDistance += Vector3.Distance(curvePoints[i], curvePoints[i + 1]);
		}

		return totalDistance;
	}

	public static T GetRandomElement<T> (this List<T> collection)
	{
		if ((collection == null) || (collection.Count == 0))
		{
			return default(T);
		}

		int index = UnityEngine.Random.Range(0, collection.Count);

		return collection[index];
	}

	public static T PopRandomElement<T> (this List<T> collection)
	{
		if ((collection == null) || (collection.Count == 0))
		{
			return default(T);
		}

		int index = UnityEngine.Random.Range(0, collection.Count);
		T randomElement = collection[index];
		collection.RemoveAt(index);

		return randomElement;
	}

	public static T GetRandomElement<T> (this T[] collection)
	{
		if ((collection == null) || (collection.Length == 0))
		{
			return default(T);
		}

		int index = UnityEngine.Random.Range(0, collection.Length);

		return collection[index];
	}

	public static int GetRandomElementIndex<T> (this List<T> collection)
	{
		if ((collection == null) || (collection.Count == 0))
		{
			return -1;
		}

		return UnityEngine.Random.Range(0, collection.Count);
	}

	public static int GetRandomElementIndex<T> (this T[] collection)
	{
		if ((collection == null) || (collection.Length == 0))
		{
			return -1;
		}

		return UnityEngine.Random.Range(0, collection.Length);
	}

	public static T GetFirstElement<T> (this List<T> target) where T : class
	{
		if (target == null || target.Count < 1)
		{
			return default(T);
		}

		return target[0];
	}

	public static T GetLastElement<T> (this List<T> collection)
	{
		if (collection == null || collection.Count < 1)
		{
			return default(T);
		}

		return collection[collection.Count - 1];
	}

	public static bool IsLastElementIndex<T> (this List<T> collection, int index)
	{
		return (collection != null) ? index == collection.Count - 1 : false;
	}

	public static T GetFirstElement<T> (this T[] collection)
	{
		if (collection == null || collection.Length < 1)
		{
			return default(T);
		}

		return collection[0];
	}
	public static T GetLastElement<T> (this T[] collection)
	{
		if (collection == null || collection.Length < 1)
		{
			return default(T);
		}

		return collection[collection.Length - 1];
	}

	public static bool IsEmpty<T> (this List<T> collection)
	{
		return (collection != null) ? collection.Count < 1 : true;
	}

	public static T ChangeType<T> (this object target)
	{
		Type conversionType = Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);

		return (T)Convert.ChangeType(target, conversionType);
	}

	// BUTTON EXTENSIONS
	public static void SetNormalColor (this Button target, Color colorToSet)
	{
		target.SetColor(ButtonColor.NORMAL, colorToSet);
	}

	public static void SetHighlightedColor (this Button target, Color colorToSet)
	{
		target.SetColor(ButtonColor.HIGHLIGHTED, colorToSet);
	}

	public static void SetPressedColor (this Button target, Color colorToSet)
	{
		target.SetColor(ButtonColor.PRESSED, colorToSet);
	}

	public static void SetDisabledColor (this Button target, Color colorToSet)
	{
		target.SetColor(ButtonColor.DISABLED, colorToSet);
	}

	public static void SetColor (this Button target, ButtonColor colorToChange, Color colorToSet)
	{
		ColorBlock colors = target.colors;

		switch (colorToChange)
		{
			case ButtonColor.NORMAL:
				colors.normalColor = colorToSet;
				break;
			case ButtonColor.HIGHLIGHTED:
				colors.highlightedColor = colorToSet;
				break;
			case ButtonColor.PRESSED:
				colors.pressedColor = colorToSet;
				break;
			case ButtonColor.DISABLED:
				colors.disabledColor = colorToSet;
				break;
		}

		target.colors = colors;
	}
	
	public static string BuildString (this string extensionClass, params object[] elements)
	{
		StringBuilder builder = new StringBuilder();

		for (int i = 0; i < elements.Length; i++)
		{
			builder.Append(elements[i].ToString());
		}

		return builder.ToString();
	}
	
	public static string BuildString (this string extensionClass, params string[] elements)
	{
		StringBuilder builder = new StringBuilder();

		for (int i = 0; i < elements.Length; i++)
		{
			builder.Append(elements[i]);
		}

		return builder.ToString();
	}


	// INAMEABLE EXTENSIONS
	public static string[] GetNames (this INameable[] collection)
	{
		string[] names = new string[collection.Length];

		for (int i = 0; i < names.Length; i++)
		{
			names[i] = collection[i].Name;
		}

		return names;
	}

	public static string[] GetNames<T> (this List<T> collection)
	{
		string[] names = new string[collection.Count];

		for (int i = 0; i < names.Length; i++)
		{
			names[i] = ((INameable)collection[i]).Name;
		}

		return names;
	}

	public static int GetNameIndex (this INameable[] collection, string name)
	{
		for (int i = 0; i < collection.Length; i++)
		{
			if (collection[i].Name == name)
			{
				return i;
			}
		}

		return -1;
	}

	public static int GetNameIndex (this string[] collection, string name)
	{
		for (int i = 0; i < collection.Length; i++)
		{
			if (collection[i] == name)
			{
				return i;
			}
		}

		return -1;
	}

	public static int GetNameIndex<T> (this List<T> collection, string name) where T : INameable
	{
		for (int i = 0; i < collection.Count; i++)
		{
			if (((INameable)collection[i]).Name == name)
			{
				return i;
			}
		}

		return -1;
	}

	public static T GetElementByName<T> (this INameable[] collection, string name)
	{
		int index = collection.GetNameIndex(name);

		return (index != -1) ? (T)collection[index] : default(T);
	}

	public static T GetElementByName<T> (this List<T> collection, string name) where T : INameable
	{
		int index = collection.GetNameIndex(name);

		return (index != -1) ? collection[index] : default(T);
	}

	public static bool ContainsName<T> (this List<T> collection, string name) where T : INameable
	{
		return collection.GetNameIndex(name) != -1;
	}

	public static void ApplyTextureToMaterial (this Sprite source, Material target, string propertyName = "_MainTex")
	{
		target.mainTexture = source.texture;

		if (source.packed == true)
		{
			Rect spriteTexFrag = source.textureRect;
			Vector2 atlasSize = new Vector2(source.texture.width, source.texture.height);

			Vector2 textureOffset = new Vector2(spriteTexFrag.xMin / atlasSize.x, spriteTexFrag.yMin / atlasSize.y);
			Vector2 textureTiling = new Vector2(spriteTexFrag.width / atlasSize.x, spriteTexFrag.height / atlasSize.y);

			target.SetTextureOffset(propertyName, textureOffset);
			target.SetTextureScale(propertyName, textureTiling);
		}
	}

	#endregion

	#region CLASS_ENUMS

	public enum ButtonColor
	{
		NORMAL,
		HIGHLIGHTED,
		PRESSED,
		DISABLED
	}

	#endregion
}