using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace _9AnimeModelPusher
{
    internal class ModelPusher
    {
        static ModelPusher()
        {
            Instance = new ModelPusher();
        }

        public static ModelPusher Instance { get; set; }

        public HttpClient HttpClient { get; set; } = new HttpClient {BaseAddress = new Uri("https://9anime.to/")};
        public event EventHandler<ModelPusherProgressEventArgs> MadeProgress;

        public async Task Push()
        {
            var animes = await _9AnimeApi.Instance.GetAnimesOnList(1);
        }

        protected virtual void OnMadeProgress(ModelPusherProgressEventArgs e)
        {
            MadeProgress?.Invoke(this, e);
        }
    }

    public class ModelPusherProgressEventArgs : EventArgs
    {
        public float ProgressPercentage { get; set; }
        public string Status { get; set; }
    }
}