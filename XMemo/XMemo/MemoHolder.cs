using Newtonsoft.Json;
using PCLStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMemo
{
    public class MemoHolder
    {
        public static MemoHolder Current { get; } = new MemoHolder();
        public Memo Memo { get; set; }

        public async Task SaveAsync()
        {
            var jsonString = JsonConvert.SerializeObject(Memo);

            var rootFolder = FileSystem.Current.LocalStorage;
            var file = await rootFolder.CreateFileAsync("memo.json",
                CreationCollisionOption.ReplaceExisting);
            await file.WriteAllTextAsync(jsonString);
        }

        public async Task LoadAsync()
        {
            var rootFolder = FileSystem.Current.LocalStorage;
            var file = await rootFolder.CreateFileAsync("memo.json",
                CreationCollisionOption.OpenIfExists);
            var jsonString = await file.ReadAllTextAsync();

            Memo = JsonConvert.DeserializeObject<Memo>(jsonString);
        }
    }
}
