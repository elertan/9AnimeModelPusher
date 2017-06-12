using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using _9AnimeModels;

namespace _9AnimeModelPusher
{
    public class _9AnimeApi
    {
        static _9AnimeApi()
        {
            Instance = new _9AnimeApi();
        }

        public HttpClient HttpClient { get; set; } = new HttpClient {BaseAddress = new Uri("https://9anime.to/")};
        public static _9AnimeApi Instance { get; set; }

        public async Task<List<Anime>> GetAnimesOnList(int pageIndex)
        {
            var url = $"filter?sort=title%3Aasc&page={pageIndex}";
            var response = await HttpClient.GetStringAsync(url);
            return ParseAnimesOnList(response);
        }

        private List<Anime> ParseAnimesOnList(string response)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(response);

            return
                htmlDocument.DocumentNode.Descendants("div")
                    .First(el =>
                    {
                        var condition = el.Attributes["class"] != null && el.Attributes["class"].Value == "widget list-link";
                        return condition;
                    })
                    .Descendants("div")
                    .Where(div => div.Attributes["class"].Value == "item")
                    .Select(
                        div =>
                        {
                            var img = div.Descendants("img").First();
                            var a = div.Descendants("a").First();

                            var anime = new Anime();

                            anime.ImageUrl = img.Attributes["src"].Value;
                            anime.Name = a.InnerText;
                            anime.PageId = a.Attributes["href"].Value.Replace("https://9anime.to/watch/", "");

                            return anime;
                        }).ToList();
        }
    }
}