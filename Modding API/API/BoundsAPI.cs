using UnityEngine;

namespace Modding.API {
    public enum BoundsType {
        MESH_COLLIDER,
        BOX_COLLIDER,
        SPHERE_COLLIDER,
        CAPSULE_COLLIDER
    }

    public class BoundsAPI {
        private static Color meshColliderColor = new Color(1f, 0f, 0f, 0.05f);
        private static Color boxColliderColor = new Color(0f, 1f, 0f, 0.05f);
        private static Color capsuleColliderColor = new Color(0f, 0f, 0f, 0.05f);
        private static Color sphereColliderColor = new Color(0f, 1f, 1f, 0.05f);
        private static Color triggerColor = new Color(1f, 0f, 1f, 0.05f);

        public static int DrawBoxColliders(BoxCollider[] boxColliders) {
            int drew = 0;

            foreach (BoxCollider boxCollider in boxColliders) {
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.localScale = Vector3.Scale(boxCollider.transform.localScale, boxCollider.size);
                cube.transform.parent = boxCollider.transform.parent;
                cube.transform.position = boxCollider.transform.position + boxCollider.center;
                cube.transform.localEulerAngles = boxCollider.transform.localEulerAngles;

                cube.name = "BoundsAPI | " + BoundsType.BOX_COLLIDER.ToString();

                Object.Destroy(cube.GetComponent<BoxCollider>());

                Renderer renderer = cube.GetComponent<Renderer>();
                renderer.material = boxCollider.isTrigger ? GetDebugColliderMaterial(triggerColor) : GetDebugColliderMaterial(boxColliderColor);

                drew++;
            }

            return drew;
        }

        public static int DrawSphereColliders(SphereCollider[] sphereColliders) {
            int drew = 0;

            foreach (SphereCollider sphereCollider in sphereColliders) {
                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sphere.transform.localScale = sphereCollider.transform.localScale * sphereCollider.radius * 2;
                sphere.transform.parent = sphereCollider.transform.parent;
                sphere.transform.position = sphereCollider.transform.position + sphereCollider.center;
                sphere.transform.localEulerAngles = sphereCollider.transform.localEulerAngles;

                sphere.name = "BoundsAPI | " + BoundsType.SPHERE_COLLIDER.ToString();

                Object.Destroy(sphere.GetComponent<SphereCollider>());

                Renderer renderer = sphere.GetComponent<Renderer>();
                renderer.material = sphereCollider.isTrigger ? GetDebugColliderMaterial(triggerColor) : GetDebugColliderMaterial(sphereColliderColor);

                drew++;
            }

            return drew;
        }

        public static int DrawCapsuleColliders(CapsuleCollider[] capsuleColliders) {
            int drew = 0;

            foreach (CapsuleCollider capsuleCollider in capsuleColliders) {
                GameObject capsule = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                capsule.transform.localScale = capsuleCollider.transform.localScale;
                capsule.transform.parent = capsuleCollider.transform.parent;
                capsule.transform.position = capsuleCollider.transform.position;

                switch (capsuleCollider.direction) {
                    case 0:
                        capsule.transform.localEulerAngles = new Vector3(
                            capsuleCollider.transform.localEulerAngles.x,
                            capsuleCollider.transform.localEulerAngles.y,
                            capsuleCollider.transform.localEulerAngles.z + 90
                        );
                        break;
                    case 1:
                        capsule.transform.localEulerAngles = new Vector3(
                            capsuleCollider.transform.localEulerAngles.x,
                            capsuleCollider.transform.localEulerAngles.y + 90,
                            capsuleCollider.transform.localEulerAngles.z
                        );
                        break;
                    case 2:
                        capsule.transform.localEulerAngles = new Vector3(
                            capsuleCollider.transform.localEulerAngles.x + 90,
                            capsuleCollider.transform.localEulerAngles.y,
                            capsuleCollider.transform.localEulerAngles.z
                        );
                        break;
                }

                capsule.name = "BoundsAPI | " + BoundsType.CAPSULE_COLLIDER.ToString();

                Object.Destroy(capsule.GetComponent<CapsuleCollider>());

                Renderer renderer = capsule.GetComponent<Renderer>();
                renderer.material = capsuleCollider.isTrigger ? GetDebugColliderMaterial(triggerColor) : GetDebugColliderMaterial(capsuleColliderColor);

                drew++;
            }

            return drew;
        }

        public static int DrawMeshColliders(MeshCollider[] meshColliders) {
            int drew = 0;

            foreach (MeshCollider meshCollider in meshColliders) {
                MeshFilter meshFilterCollider = meshCollider.gameObject.GetComponent<MeshFilter>();

                if (meshFilterCollider == null || meshFilterCollider.mesh == null)
                    continue;

                GameObject meshObject = new GameObject("", typeof(MeshFilter), typeof(MeshRenderer));
                meshObject.transform.localScale = meshCollider.transform.localScale;
                meshObject.transform.parent = meshCollider.transform.parent;
                meshObject.transform.position = meshCollider.transform.position;
                meshObject.transform.localEulerAngles = meshCollider.transform.localEulerAngles;

                meshObject.name = "BoundsAPI | " + BoundsType.MESH_COLLIDER.ToString();

                MeshRenderer renderer = meshObject.GetComponent<MeshRenderer>();
                renderer.material = meshCollider.isTrigger ? GetDebugColliderMaterial(triggerColor) : GetDebugColliderMaterial(meshColliderColor);

                MeshFilter meshFilter = meshObject.GetComponent<MeshFilter>();
                meshFilter.mesh = meshFilterCollider.mesh;

                drew++;
            }

            return drew;
        }

        public static int DestroyBounds(BoundsType type) {
            int destroyedBounds = 0;
            foreach (GameObject o in Resources.FindObjectsOfTypeAll(typeof(GameObject))) {
                if (o.name != "BoundsAPI | " + type.ToString())
                    continue;

                GameObject.Destroy(o);
                destroyedBounds++;
            }

            return destroyedBounds;
        }

        public static Material GetDebugColliderMaterial(Color color) {
            return new Material(Shader.Find("Sprites/Default")) {
                color = color
            };
        }
    }
}
