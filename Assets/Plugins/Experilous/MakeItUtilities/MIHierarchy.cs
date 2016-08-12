/******************************************************************************\
* Copyright Andy Gainey                                                        *
\******************************************************************************/

using UnityEngine;
using System;
using System.Collections.Generic;

namespace Experilous.MakeIt.Utilities
{
	public static class MIGameObjectHierarchy
	{
		public static void ForAllRigidbodyColliders(Transform transform, Action<Collider> action)
		{
			foreach (var collider in transform.GetComponents<Collider>())
			{
				action(collider);
			}

			int childCount = transform.childCount;
			for (int i = 0; i < childCount; ++i)
			{
				var child = transform.GetChild(i);
				if (child.GetComponent<Rigidbody>() == null)
				{
					ForAllRigidbodyColliders(child, action);
				}
			}
		}

		public static void ForAllRigidbodyColliders(Transform transform, Action<Collider2D> action)
		{
			foreach (var collider in transform.GetComponents<Collider2D>())
			{
				action(collider);
			}

			int childCount = transform.childCount;
			for (int i = 0; i < childCount; ++i)
			{
				var child = transform.GetChild(i);
				if (child.GetComponent<Rigidbody2D>() == null)
				{
					ForAllRigidbodyColliders(child, action);
				}
			}
		}

		public static void ForAllComponents<TComponent>(Transform transform, Action<TComponent> action) where TComponent : Component
		{
			foreach (var component in transform.GetComponents<TComponent>())
			{
				action(component);
			}

			int childCount = transform.childCount;
			for (int i = 0; i < childCount; ++i)
			{
				ForAllComponents(transform.GetChild(i), action);
			}
		}

		public static Bounds GetColliderGroupAxisAlignedBoxBounds(Transform transform)
		{
			var bounds = new Bounds();
			bool initialized = false;
			ForAllRigidbodyColliders(transform,
				(Collider collider) =>
				{
					if (!initialized)
					{
						bounds = collider.bounds;
						initialized = true;
					}
					else
					{
						bounds.Encapsulate(collider.bounds);
					}
				});
			return bounds;
		}

		public static Sphere GetColliderGroupSphereBounds(Transform transform)
		{
			var bounds = new Sphere();
			bool initialized = false;
			ForAllRigidbodyColliders(transform,
				(Collider collider) =>
				{
					var sphereCollider = collider as SphereCollider;
					if (sphereCollider != null)
					{
						var worldCenter = transform.TransformPoint(sphereCollider.center);
						var worldRadius = sphereCollider.radius * transform.lossyScale.MaxAbsComponent();
						if (!initialized)
						{
							bounds.center = worldCenter;
							bounds.radius = worldRadius;
							initialized = true;
						}
						else
						{
							bounds.Encapsulate(new Sphere(worldCenter, worldRadius));
						}
					}
					else
					{
						if (!initialized)
						{
							bounds.center = collider.bounds.center;
							bounds.radius = collider.bounds.extents.magnitude;
							initialized = true;
						}
						else
						{
							bounds.Encapsulate(collider.bounds);
						}
					}
				});
			return bounds;
		}

		public static Bounds GetCollider2DGroupAxisAlignedBoxBounds(Transform transform)
		{
			var bounds = new Bounds();
			bool initialized = false;
			ForAllRigidbodyColliders(transform,
				(Collider2D collider) =>
				{
					if (!initialized)
					{
						bounds = collider.bounds;
						initialized = true;
					}
					else
					{
						bounds.Encapsulate(collider.bounds);
					}
				});
			return bounds;
		}

		public static Sphere GetCollider2DGroupSphereBounds(Transform transform)
		{
			var bounds = new Sphere();
			bool initialized = false;
			ForAllRigidbodyColliders(transform,
				(Collider2D collider) =>
				{
					var circleCollider = collider as CircleCollider2D;
					if (circleCollider != null)
					{
						var worldCenter = transform.TransformPoint(circleCollider.offset);
						var worldRadius = circleCollider.radius * transform.lossyScale.MaxAbsComponent();
						if (!initialized)
						{
							bounds.center = worldCenter;
							bounds.radius = worldRadius;
							initialized = true;
						}
						else
						{
							bounds.Encapsulate(new Sphere(worldCenter, worldRadius));
						}
					}
					else
					{
						if (!initialized)
						{
							bounds.center = collider.bounds.center;
							bounds.radius = collider.bounds.extents.magnitude;
							initialized = true;
						}
						else
						{
							bounds.Encapsulate(collider.bounds);
						}
					}
				});
			return bounds;
		}

		public static Bounds GetLightGroupAxisAlignedBoxBounds(Transform transform)
		{
			var bounds = new Bounds();
			bool initialized = false;
			ForAllComponents(transform,
				(Light light) =>
				{
					Bounds lightBounds;
					if (light.type == LightType.Point)
					{
						var lightDiameter = light.range * 2f;
						lightBounds = new Bounds(transform.position, new Vector3(lightDiameter, lightDiameter, lightDiameter));
					}
					else if (light.type == LightType.Spot)
					{
						var radians = light.spotAngle * Mathf.Deg2Rad * 0.5f;
						var sine = Mathf.Sin(radians);
						var cosine = Mathf.Cos(radians);
						var forward = transform.forward;
						var ringRadius = sine * light.range;
						var xRingExtent = Mathf.Sqrt(1f - forward.x * forward.x) * ringRadius;
						var yRingExtent = Mathf.Sqrt(1f - forward.y * forward.y) * ringRadius;
						var zRingExtent = Mathf.Sqrt(1f - forward.z * forward.z) * ringRadius;
						var ringCenter = forward * cosine * light.range;

						// Calculate the extents of outer circular ring at the far end of the spotlight cone, along with the (0, 0, 0) point of the cone apex.
						float xMin = Mathf.Min(0f, ringCenter.x - xRingExtent);
						float xMax = Mathf.Max(0f, ringCenter.x + xRingExtent);
						float yMin = Mathf.Min(0f, ringCenter.y - yRingExtent);
						float yMax = Mathf.Max(0f, ringCenter.y + yRingExtent);
						float zMin = Mathf.Min(0f, ringCenter.z - zRingExtent);
						float zMax = Mathf.Max(0f, ringCenter.z + zRingExtent);

						// Expand the bounds for any of the primary axes that are within the cone.
						if (forward.x >= cosine)
							xMax = light.range;
						else if (forward.x <= cosine)
							xMin = -light.range;

						if (forward.y >= cosine)
							yMax = light.range;
						else if (forward.y <= cosine)
							yMin = -light.range;

						if (forward.z >= cosine)
							zMax = light.range;
						else if (forward.z <= cosine)
							zMin = -light.range;

						lightBounds = new Bounds(
							new Vector3((xMin + xMax) * 0.5f, (yMin + yMax) * 0.5f, (zMin + zMax) * 0.5f) + transform.position,
							new Vector3(xMax - xMin, yMax - yMin, zMax - zMin));
					}
					else
					{
						return;
					}

					if (!initialized)
					{
						bounds = lightBounds;
						initialized = true;
					}
					else
					{
						bounds.Encapsulate(lightBounds);
					}
				});
			return bounds;
		}

		public static Sphere GetLightGroupSphereBounds(Transform transform)
		{
			var bounds = new Sphere();
			bool initialized = false;
			ForAllComponents(transform,
				(Light light) =>
				{
					Sphere lightBounds;
					if (light.type == LightType.Point)
					{
						lightBounds = new Sphere(transform.position, light.range);
					}
					else if (light.type == LightType.Spot)
					{
						var radians = light.spotAngle * Mathf.Deg2Rad * 0.5f;
						var sine = Mathf.Sin(radians);
						var cosine = Mathf.Cos(radians);
						if (light.spotAngle < 90f)
						{
							// The bounding sphere intersects both the outer circular ring at the far end of the spotlight cone and the apex of the cone.
							var radius = (cosine + sine * sine / cosine) * light.range * 0.5f;
							lightBounds = new Sphere(transform.position + transform.forward * radius, radius);
						}
						else
						{
							// The bounding sphere intersects only the outer circular ring at the far end of the spotlight cone.
							var offset = cosine * light.range;
							var radius = sine * light.range;
							lightBounds = new Sphere(transform.position + transform.forward * offset, radius);
						}
					}
					else
					{
						return;
					}

					if (!initialized)
					{
						bounds = lightBounds;
						initialized = true;
					}
					else
					{
						bounds.Encapsulate(lightBounds);
					}
				});
			return bounds;
		}

		public static Bounds GetMeshGroupAxisAlignedBoxBounds(Transform transform)
		{
			var bounds = new Bounds();
			bool initialized = false;
			ForAllComponents(transform,
				(MeshRenderer meshRenderer) =>
				{
					if (!initialized)
					{
						bounds = meshRenderer.bounds;
						initialized = true;
					}
					else
					{
						bounds.Encapsulate(meshRenderer.bounds);
					}
				});
			return bounds;
		}

		public static Sphere GetMeshGroupSphereBounds(Transform transform)
		{
			var bounds = new Sphere();
			bool initialized = false;
			ForAllComponents(transform,
				(MeshRenderer meshRenderer) =>
				{
					var meshBounds = meshRenderer.bounds;
					if (!initialized)
					{
						bounds = new Sphere(meshBounds.center, meshBounds.extents.magnitude);
						initialized = true;
					}
					else
					{
						bounds.Encapsulate(meshBounds);
					}
				});
			return bounds;
		}

		public static Bounds GetSpriteGroupAxisAlignedBoxBounds(Transform transform)
		{
			var bounds = new Bounds();
			bool initialized = false;
			ForAllComponents(transform,
				(SpriteRenderer spriteRenderer) =>
				{
					if (!initialized)
					{
						bounds = spriteRenderer.bounds;
						initialized = true;
					}
					else
					{
						bounds.Encapsulate(spriteRenderer.bounds);
					}
				});
			return bounds;
		}

		public static Sphere GetSpriteGroupSphereBounds(Transform transform)
		{
			var bounds = new Sphere();
			bool initialized = false;
			ForAllComponents(transform,
				(SpriteRenderer spriteRenderer) =>
				{
					var spriteBounds = spriteRenderer.bounds;
					if (!initialized)
					{
						bounds = new Sphere(spriteBounds.center, spriteBounds.extents.magnitude);
						initialized = true;
					}
					else
					{
						bounds.Encapsulate(spriteBounds);
					}
				});
			return bounds;
		}
	}
}
