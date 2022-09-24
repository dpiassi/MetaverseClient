using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using System.IO;

namespace Patterns.Editor
{
    public class DesignPatternsWindow : EditorWindow
    {
        /* ============================================================
         * STATIC CLASS
         * ============================================================*/
        static class Templates
        {
            public static readonly string Assets = Application.dataPath;
            public static readonly string Patterns = $"{Assets}/Patterns";
            public static readonly string Scripts = $"{Patterns}/Scripts";

            private static void RefreshAssetsAndHighlightFile(string filePath)
            {
                // BUG @dpiassi it doesn't work...
                SelectAndHighlightFile(filePath);
                AssetDatabase.Refresh();
            }

            private static string DuplicateTemplate(
                string templatePath,
                string outputFolder,
                params string[] pairsToReplace
            )
            {
                if (pairsToReplace == null || pairsToReplace.Length < 2)
                {
                    throw new System.Exception("At least a pair string must be provided!");
                }
                if (pairsToReplace.Length % 2 > 0)
                {
                    throw new System.Exception("pairsToReplace must be provided in pairs.");
                }

                string text = File.ReadAllText(templatePath);
                string fileName = Path.GetFileNameWithoutExtension(templatePath);

                for (int i = 0; i < pairsToReplace.Length; i += 2)
                {
                    text = text.Replace(pairsToReplace[i], pairsToReplace[i + 1]);
                    fileName = fileName.Replace(pairsToReplace[i], pairsToReplace[i + 1]);
                }

                string path = $"{outputFolder ?? Application.dataPath}/{fileName}.cs";
                File.WriteAllText(path, text);
                return path;
            }

            public static class MVC
            {
                public const string PrefixToReplace = "My";
                private static readonly string Base = $"{Scripts}/ModelViewController";
                public static readonly string Model = $"{Base}/MyModel.cs";
                public static readonly string View = $"{Base}/MyView.cs";
                public static readonly string Controller = $"{Base}/MyController.cs";

                public static void Create(string prefix, string outputFolder)
                {
                    DuplicateTemplate(Model, outputFolder, "My", prefix);
                    DuplicateTemplate(View, outputFolder, "My", prefix);
                    string path = DuplicateTemplate(Controller, outputFolder, "My", prefix);
                    RefreshAssetsAndHighlightFile(path);
                }
            }

            public static class State
            {
                private const string PrefixToReplace = "__State__";
                private static readonly string Base = $"{Scripts}/State";
                private static readonly string Controller = $"{Base}/__State__Controller.cs";
                private static readonly string StateContext = $"{Base}/__State__StateContext.cs";
                private static readonly string IState = $"{Base}/I__State__State.cs";
                private static readonly string ExampleState = $"{Base}/__State__ExampleState.cs";

                public static void Create(string prefix, ref string[] states, string outputFolder)
                {
                    Debug.LogError(states);

                    DuplicateTemplate(StateContext, outputFolder, PrefixToReplace, prefix);
                    DuplicateTemplate(IState, outputFolder, PrefixToReplace, prefix);

                    foreach (string state in states)
                    {
                        DuplicateTemplate(
                            ExampleState,
                            outputFolder,
                            PrefixToReplace,
                            prefix,
                            "Example",
                            state
                        );
                    }

                    string path = DuplicateTemplate(
                        Controller,
                        outputFolder,
                        PrefixToReplace,
                        prefix,
                        "Example",
                        states[0]
                    );

                    RefreshAssetsAndHighlightFile(path);
                }
            }
        }

        private readonly Regex regex = new("[A-Za-z_].[A-Za-z0-9_]*", RegexOptions.IgnoreCase);

        /* ============================================================
         * GUI VALUES
         * ============================================================*/
        private string prefixName__MVC;
        private string prefixName__State;

        [SerializeField]
        private string[] m_States = { "Idle", "Start", "Loop", "End" };

        /* ============================================================
         * AUXILIAR MEMBERS
         * ============================================================*/
        SerializedObject _serializedObject;
        SerializedProperty _statesProperty;

        [MenuItem("Tools/Design Patterns")]
        static void Init()
        {
            // Get existing open window or if none, make a new one:
            DesignPatternsWindow window = (DesignPatternsWindow)
                EditorWindow.GetWindow(typeof(DesignPatternsWindow), true, "Design Patterns");
            window.Show();
        }

        void Awake()
        {
            _serializedObject = new SerializedObject(this);
            _statesProperty = _serializedObject.FindProperty("m_States");
        }

        void OnGUI()
        {
            GUILayout.Label("Model-View-Controller", EditorStyles.boldLabel);
            prefixName__MVC = EditorGUILayout.TextField("Class Prefix (keyword)", prefixName__MVC);

            if (GUILayout.Button("Create MVC"))
            {
                if (string.IsNullOrEmpty(prefixName__MVC) || !regex.Match(prefixName__MVC).Success)
                {
                    EditorUtility.DisplayDialog(
                        "Unable to create assets",
                        "Please specify a valid class name.",
                        "Close"
                    );
                }
                else
                {
                    string path = EditorUtility.OpenFolderPanel(
                        "Save scripts on folder",
                        Templates.Assets,
                        ""
                    );
                    Templates.MVC.Create(prefixName__MVC, path);
                    Close();
                }
            }

            GUILayout.Space(20);

            GUILayout.Label("State", EditorStyles.boldLabel);
            prefixName__State = EditorGUILayout.TextField(
                "Class Prefix (keyword)",
                prefixName__State
            );

            EditorGUILayout.PropertyField(_statesProperty, true); // True means show children
            _serializedObject.ApplyModifiedProperties(); // Remember to apply modified properties

            if (GUILayout.Button("Create State"))
            {
                if (
                    string.IsNullOrEmpty(prefixName__State)
                    || !regex.Match(prefixName__State).Success
                )
                {
                    EditorUtility.DisplayDialog(
                        "Unable to create assets",
                        "Please specify a valid class name.",
                        "Close"
                    );
                }
                else
                {
                    string path = EditorUtility.OpenFolderPanel(
                        "Save scripts on folder",
                        Templates.Assets,
                        ""
                    );
                    Templates.State.Create(prefixName__State, ref m_States, path);
                    Close();
                }
            }
        }

        static void SelectAndHighlightFile(string path)
        {
            // BUG @dpiassi "highlight" doesn't work yet.
            path = path.Replace(Templates.Assets, "Assets");

            // Load object
            UnityEngine.Object obj = AssetDatabase.LoadAssetAtPath(
                path,
                typeof(UnityEngine.Object)
            );

            // Select the object in the project folder
            Selection.activeObject = obj;

            // Also flash the folder yellow to highlight it
            EditorGUIUtility.PingObject(obj);
        }
    }
}
