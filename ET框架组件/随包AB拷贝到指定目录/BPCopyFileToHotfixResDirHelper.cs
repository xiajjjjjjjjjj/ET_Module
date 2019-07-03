using UnityEngine;
using System;
using System.IO;
using System.Threading.Tasks;

// using PathHelper = ETModel.PathHelper;
// using UnityWebRequestAsync = ETModel.UnityWebRequestAsync;
// using ComponentFactory = ETModel.ComponentFactory;

namespace ETModel
{
    public static class BPCopyFileToHotfixResDirHelper
    {
        /// <summary>
        /// 复制文件到
        /// </summary>
        public static async Task<bool> Copy()
        {
            // 1, 得到包内的所有ab包名字
            string[] allABNameArray = BPCopyFileToHotfixResDirHelper.GetAllAssetBundleName();
            if(allABNameArray == null)
                return false;

            // 2, 开始copy
            foreach (string abName in allABNameArray)
            {
                string fullPath = Path.Combine(PathHelper.AppHotfixResPath, abName);
                if(Directory.Exists(fullPath) == false)
                {
                    // 第一种情况.不存在.应该要copy包里的过去
                    await CopyFileToHotfixRes(Path.Combine(PathHelper.AppResPath, abName), abName);
                }
            }

            return true;
        }
        
        /// <summary>
        /// 获取所有的ab包名
        /// </summary>
        public static string[] GetAllAssetBundleName()
        {
            // 1, 开始逻辑.指定是包内的StreamingAssets. 遍历ab包
            string streamingABName = "StreamingAssets";
            string streamingABPath  = Path.Combine(PathHelper.AppResPath, streamingABName);
            AssetBundle assetBundle = AssetBundle.LoadFromFile(streamingABPath);
            AssetBundleManifest manifestObject = (AssetBundleManifest)assetBundle.LoadAsset("AssetBundleManifest");

            if(manifestObject == null)
            {
                Log.Debug("manifestObject is null ==> ");
                assetBundle.Unload(true);
                return null;
            }

            string [] allABNameArray = manifestObject.GetAllAssetBundles();
            assetBundle.Unload(true);
            return allABNameArray;
        }

        /// <summary>
        /// 遍历目录
        /// </summary>
        /// <param name="path"></param>
        public static async Task<bool> CopyFileToHotfixRes(string orginPath, string abName)
        {
            using(UnityWebRequestAsync www = ComponentFactory.Create<UnityWebRequestAsync>())
			{
				Log.Debug("加载本地orginPath ===> " + orginPath);
				await www.DownloadAsync(orginPath);

                Log.Debug("读取本地ab包成功,准备写入热更新目录 size ==> " + www.Request.downloadHandler.data.Length);
                string hotfixResPath = Path.Combine(PathHelper.AppHotfixResPath + abName);

                Log.Debug("hotfixResPath ==> " + hotfixResPath);
                File.WriteAllBytes(hotfixResPath, www.Request.downloadHandler.data);
			}

            return true;
        }

        
    }

}
