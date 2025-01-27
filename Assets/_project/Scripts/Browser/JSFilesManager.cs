using UnityEngine;
using System.Collections.Generic;

namespace javascript {
    public class JSFilesManager : Singleton<JSFilesManager>
    {
        public Dictionary<string, string> JsScripts = new Dictionary<string, string>();

        protected override void Awake()
        {
            base.Awake();
            LoadJSFiles();
        }

        private void LoadJSFiles()
        {
            TextAsset[] jsFiles = Resources.LoadAll<TextAsset>("JSFiles");
            foreach (TextAsset jsFile in jsFiles)
            {
                if (!JsScripts.ContainsKey(jsFile.name))
                {
                    JsScripts.Add(jsFile.name, jsFile.text);
                }
                else
                {
                    Debug.LogWarning($"Duplicate script name found: {jsFile.name}");
                }
            }
        }

        public string GetScript(string scriptName)
        {
            if (JsScripts.TryGetValue(scriptName, out string scriptContent))
            {
                return scriptContent;
            }
            else
            {
                Debug.LogError($"Script not found: {scriptName}");
                return null;
            }
        }
    }
}
