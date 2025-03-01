/*
    Copyright(C) 2023 Altom Consulting

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program. If not, see <https://www.gnu.org/licenses/>.
*/

using System;
using AltTester.AltTesterUnitySDK.Editor;
using AltTester.AltTesterUnitySDK;
using AltTester.AltTesterUnitySDK.Editor.Logging;
using UnityEditor;

namespace AltTesterTools
{
    public class BuildAltTester
    {
        private static readonly NLog.Logger logger = EditorLogManager.Instance.GetCurrentClassLogger();


        [MenuItem("Build/Mac")]
        protected static void MacBuildFromCommandLine()
        {
            try
            {
                string versionNumber = DateTimeOffset.Now.ToUnixTimeSeconds().ToString();

                PlayerSettings.companyName = "Altom";
                PlayerSettings.productName = "sampleGame";
                PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Standalone, "com.altom.sampleGame");
                PlayerSettings.bundleVersion = versionNumber;
                PlayerSettings.SetApiCompatibilityLevel(BuildTargetGroup.Standalone, ApiCompatibilityLevel.NET_4_6);
                AltBuilder.AddAltTesterInScriptingDefineSymbolsGroup(BuildTargetGroup.Standalone);
                var instrumentationSettings = getInstrumentationSettings();
                PlayerSettings.fullScreenMode = UnityEngine.FullScreenMode.Windowed;
                PlayerSettings.defaultScreenHeight = 1080;
                PlayerSettings.defaultScreenWidth = 1920;


                logger.Debug("Starting Mac build..." + PlayerSettings.productName + " : " + PlayerSettings.bundleVersion);
                var buildPlayerOptions = new BuildPlayerOptions
                {
                    scenes = GetScene(),

                    locationPathName = "sampleGame",
                    target = BuildTarget.StandaloneOSX,
                    options = BuildOptions.Development | BuildOptions.IncludeTestAssemblies | BuildOptions.AutoRunPlayer
                };



                AltBuilder.InsertAltInScene(buildPlayerOptions.scenes[0], instrumentationSettings);

                var results = BuildPipeline.BuildPlayer(buildPlayerOptions);
                AltBuilder.RemoveAltTesterFromScriptingDefineSymbols(BuildTargetGroup.Standalone);


#if UNITY_2017
            if (results.Equals(""))
            {
                logger.Info("Build succeeded!");
                EditorApplication.Exit(0);

            }
            else
            {
                logger.Error("Build failed!");
                EditorApplication.Exit(1);
            }

#else
                if (results.summary.totalErrors == 0)
                {
                    logger.Info("Build succeeded!");
                    logger.Info("Finished. " + PlayerSettings.productName + " : " + PlayerSettings.bundleVersion);
                    EditorApplication.Exit(0);
                }

                logger.Error("Total Errors: " + results.summary.totalErrors);
                logger.Error("Build failed! " + results.steps + "\n Result: " + results.summary.result + "\n Stripping info: " + results.strippingInfo);
                EditorApplication.Exit(1);
#endif

            }
            catch (Exception exception)
            {
                logger.Error(exception);
                // EditorApplication.Exit(1);
            }

        }
        [MenuItem("Build/Android")]
        protected static void AndroidBuildFromCommandLine()
        {
            try
            {
                string versionNumber = DateTimeOffset.Now.ToUnixTimeSeconds().ToString();

                PlayerSettings.companyName = "Altom";
                PlayerSettings.productName = "sampleGame";
                PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, "com.altom.sampleGame");
                PlayerSettings.bundleVersion = versionNumber;
                PlayerSettings.Android.bundleVersionCode = int.Parse(versionNumber);
                PlayerSettings.Android.minSdkVersion = AndroidSdkVersions.AndroidApiLevel23;
                PlayerSettings.SetApiCompatibilityLevel(BuildTargetGroup.Android, ApiCompatibilityLevel.NET_4_6);
#if UNITY_2018_1_OR_NEWER
                PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARMv7;
#endif
                AltBuilder.AddAltTesterInScriptingDefineSymbolsGroup(BuildTargetGroup.Android);
                var instrumentationSettings = getInstrumentationSettings();


                logger.Debug("Starting Android build..." + PlayerSettings.productName + " : " + PlayerSettings.bundleVersion);
                var buildPlayerOptions = new BuildPlayerOptions
                {
                    scenes = GetScene(),

                    locationPathName = "sampleGame.apk",
                    target = BuildTarget.Android,
                    options = BuildOptions.Development | BuildOptions.IncludeTestAssemblies
                };



                AltBuilder.InsertAltInScene(buildPlayerOptions.scenes[0], instrumentationSettings);

                var results = BuildPipeline.BuildPlayer(buildPlayerOptions);
                AltBuilder.RemoveAltTesterFromScriptingDefineSymbols(BuildTargetGroup.Android);


#if UNITY_2017
            if (results.Equals(""))
            {
                logger.Info("Build succeeded!");
                EditorApplication.Exit(0);

            }
            else
            {
                logger.Error("Build failed!");
                EditorApplication.Exit(1);
            }

#else
                if (results.summary.totalErrors == 0)
                {
                    logger.Info("Build succeeded!");

                }
                else
                {
                    logger.Error("Total Errors: " + results.summary.totalErrors);
                    logger.Error("Build failed! " + results.steps + "\n Result: " + results.summary.result + "\n Stripping info: " + results.strippingInfo);
                    // EditorApplication.Exit(1);
                }

#endif

                logger.Info("Finished. " + PlayerSettings.productName + " : " + PlayerSettings.bundleVersion);
                // EditorApplication.Exit(0);
            }
            catch (Exception exception)
            {
                logger.Error(exception);
                // EditorApplication.Exit(1);
            }

        }
        public static string[] GetScene()
        {
            return new string[]
                    {
                    "Assets/Examples/Scenes/Scene 1 AltDriverTestScene.unity",
                    "Assets/Examples/Scenes/Scene 2 Draggable Panel.unity",
                    "Assets/Examples/Scenes/Scene 3 Drag And Drop.unity",
                    "Assets/Examples/Scenes/Scene 4 No Cameras.unity",
                    "Assets/Examples/Scenes/Scene 5 Keyboard Input.unity",
                    "Assets/Examples/Scenes/Scene6.unity",
                    "Assets/Examples/Scenes/Scene 7 Drag And Drop NIS.unity",
                    "Assets/Examples/Scenes/Scene 8 Draggable Panel NIP.unity",
                    "Assets/Examples/Scenes/scene 9 NIS.unity",
                    "Assets/Examples/Scenes/Scene 10 Sample NIS.unity",
                    "Assets/Examples/Scenes/Scene 7 New Input System Actions.unity",
                    "Assets/Examples/Scenes/Scene 11 ScrollView Scene.unity"
                    };
        }

        [MenuItem("Build/iOS")]
        protected static void IosBuildFromCommandLine()
        {
            try
            {
                string versionNumber = DateTimeOffset.Now.ToUnixTimeSeconds().ToString();
                PlayerSettings.companyName = "Altom";
                PlayerSettings.productName = "sampleGame";
                PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.iOS, "com.altom.sampleGame");
                PlayerSettings.bundleVersion = versionNumber;
                PlayerSettings.iOS.appleEnableAutomaticSigning = true;
                PlayerSettings.iOS.appleDeveloperTeamID = "59ESG8ELF5";
                PlayerSettings.SetApiCompatibilityLevel(BuildTargetGroup.iOS, ApiCompatibilityLevel.NET_4_6);
                logger.Debug("Starting IOS build..." + PlayerSettings.productName + " : " + PlayerSettings.bundleVersion);

                var buildPlayerOptions = new BuildPlayerOptions
                {
                    locationPathName = "sampleGame",
                    scenes = GetScene(),

                    target = BuildTarget.iOS,
                    options = BuildOptions.Development | BuildOptions.IncludeTestAssemblies | BuildOptions.AutoRunPlayer
                };

                AltBuilder.AddAltTesterInScriptingDefineSymbolsGroup(BuildTargetGroup.iOS);
                var instrumentationSettings = getInstrumentationSettings();
                AltBuilder.InsertAltInScene(buildPlayerOptions.scenes[0], instrumentationSettings);

                var results = BuildPipeline.BuildPlayer(buildPlayerOptions);

#if UNITY_2017
            if (results.Equals(""))
            {
                logger.Info("Build succeeded!");

            }
            else
            logger.Error("Build failed!");
            // EditorApplication.Exit(1);

#else
                if (results.summary.totalErrors == 0)
                {
                    logger.Info("Build succeeded!");

                }
                else
                {
                    logger.Error("Build failed!");
                    // EditorApplication.Exit(1);
                }

#endif
                logger.Info("Finished. " + PlayerSettings.productName + " : " + PlayerSettings.bundleVersion);
                // EditorApplication.Exit(0);

            }
            catch (Exception exception)
            {
                logger.Error(exception);
                // EditorApplication.Exit(1);
            }
        }

        [MenuItem("Build/WebGL")]
        protected static void WebGLBuildFromCommandLine()
        {
            try
            {
                string versionNumber = DateTimeOffset.Now.ToUnixTimeSeconds().ToString();

                PlayerSettings.companyName = "Altom";
                PlayerSettings.productName = "sampleGame";
                PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.WebGL, "com.altom.sampleGame");
                PlayerSettings.bundleVersion = versionNumber;
                PlayerSettings.Android.bundleVersionCode = int.Parse(versionNumber);
                PlayerSettings.Android.minSdkVersion = AndroidSdkVersions.AndroidApiLevel23;
                PlayerSettings.SetApiCompatibilityLevel(BuildTargetGroup.WebGL, ApiCompatibilityLevel.NET_4_6);
                PlayerSettings.WebGL.compressionFormat = WebGLCompressionFormat.Disabled;
                PlayerSettings.WebGL.exceptionSupport = WebGLExceptionSupport.FullWithStacktrace;

                logger.Debug("Starting WebGL build..." + PlayerSettings.productName + " : " + PlayerSettings.bundleVersion);
                var buildPlayerOptions = new BuildPlayerOptions
                {
                    scenes = GetScene(),

                    locationPathName = "build/webgl",
                    target = BuildTarget.WebGL,
                    options = BuildOptions.Development | BuildOptions.IncludeTestAssemblies | BuildOptions.AutoRunPlayer
                };

                AltBuilder.AddAltTesterInScriptingDefineSymbolsGroup(BuildTargetGroup.WebGL);
                AltBuilder.AddScriptingDefineSymbol("UNITY_WEBGL", BuildTargetGroup.WebGL);

                var instrumentationSettings = getInstrumentationSettings();
                AltBuilder.InsertAltInScene(buildPlayerOptions.scenes[0], instrumentationSettings);

                var results = BuildPipeline.BuildPlayer(buildPlayerOptions);
                AltBuilder.RemoveAltTesterFromScriptingDefineSymbols(BuildTargetGroup.WebGL);
                AltBuilder.RemoveScriptingDefineSymbol("UNITY_WEBGL", BuildTargetGroup.WebGL);


#if UNITY_2017
            if (results.Equals(""))
            {
                logger.Info("Build succeeded!");
                EditorApplication.Exit(0);

            }
            else
                {
                    logger.Error("Build failed!");
                    EditorApplication.Exit(1);
                }

#else
                if (results.summary.totalErrors == 0)
                {
                    logger.Info("Build succeeded!");
                }
                else
                {
                    logger.Error("Total Errors: " + results.summary.totalErrors);
                    logger.Error("Build failed! " + results.steps + "\n Result: " + results.summary.result + "\n Stripping info: " + results.strippingInfo);
                    EditorApplication.Exit(1);
                }

#endif

                logger.Info("Finished. " + PlayerSettings.productName + " : " + PlayerSettings.bundleVersion);
                EditorApplication.Exit(0);
            }
            catch (Exception exception)
            {
                logger.Error(exception);
                EditorApplication.Exit(1);
            }
        }

        private static AltInstrumentationSettings getInstrumentationSettings()
        {
            var instrumentationSettings = new AltInstrumentationSettings();

            var host = System.Environment.GetEnvironmentVariable("ALTSERVER_HOST");
            if (!string.IsNullOrEmpty(host))
            {
                instrumentationSettings.AltServerHost = host;
            }

            var port = System.Environment.GetEnvironmentVariable("ALTSERVER_PORT");
            if (!string.IsNullOrEmpty(port))
            {
                instrumentationSettings.AltServerPort = int.Parse(port);
            }
            else
            {
                instrumentationSettings.AltServerPort = 13010;
            }

            return instrumentationSettings;
        }
    }
}
