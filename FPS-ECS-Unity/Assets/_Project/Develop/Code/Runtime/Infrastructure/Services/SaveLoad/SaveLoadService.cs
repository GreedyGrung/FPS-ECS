using System.IO;
using System.Text;
using FpsEcs.Runtime.Infrastructure.Services.SaveLoad.Data;
using FpsEcs.Runtime.Utils;
using UnityEngine;

namespace FpsEcs.Runtime.Infrastructure.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private readonly string _path;
        
        public SaveLoadService()
        {
            _path = Path.Combine(Application.persistentDataPath, Constants.Utils.SaveKey);
        }

        public void SaveProgress(PlayerProgress playerProgress)
        {
            string json = playerProgress.ToJson();
            byte[] bytes = Encoding.UTF8.GetBytes(json);

            WriteAtomic(_path, bytes);
        }

        public PlayerProgress LoadProgress()
        {
            if (!File.Exists(_path))
            {
                return null;
            }

            byte[] bytes = File.ReadAllBytes(_path);
            string json = Encoding.UTF8.GetString(bytes);
                    
            return json.ToDeserizalized<PlayerProgress>();
        }

        public void DeleteProgress()
        {
            if (File.Exists(_path))
            {
                File.Delete(_path);
            }
        }

        private void WriteAtomic(string path, byte[] bytes)
        {
            string dir = Path.GetDirectoryName(path);
            
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            string tmp = path + ".tmp";
            File.WriteAllBytes(tmp, bytes);

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            File.Move(tmp, path);
        }
    }
}