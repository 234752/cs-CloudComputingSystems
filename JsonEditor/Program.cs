

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace JsonEditor;

public class Editor
{

    static void Main(string[] args)
    {
        JArray reviews = JArray.Parse(File.ReadAllText("HealthStory.json"));
        JArray stories = new JArray();


        for (int i = 0; i < 1700; i++)
        {
            string baseName = "story_reviews_";
            string zeros = new string('0', 5 - i.ToString().Length);
            string fullNumber = zeros + i.ToString();
            string name = baseName + fullNumber;

            JObject story;
            try
            {
                story = JObject.Parse(File.ReadAllText(@"HealthStory\" + name + ".json"));
                //stories.Add(story);
                //continue;
            }
            catch (Exception ex)
            {
                Console.WriteLine(name);
                continue;
            }
            
            List<JToken> tokensToBeYeeted = new List<JToken>();
            var rating = (int)reviews.AsQueryable().Single(t => t.SelectToken("news_id").ToString() == name).SelectToken("rating");
            var criteria = reviews.AsQueryable().Single(t => t.SelectToken("news_id").ToString() == name).SelectToken("criteria");
            //Yeet that
            tokensToBeYeeted.Add(story.SelectToken("meta_data")!);
            tokensToBeYeeted.Add(story.SelectToken("canonical_link")!);
            tokensToBeYeeted.Add(story.SelectToken("images")!);
            tokensToBeYeeted.Add(story.SelectToken("movies")!);
            tokensToBeYeeted.Add(story.SelectToken("publish_date")!);
            tokensToBeYeeted.Add(story.SelectToken("source")!);
            tokensToBeYeeted.Add(story.SelectToken("top_img")!);
            tokensToBeYeeted.Add(story.SelectToken("summary")!);
            tokensToBeYeeted.Add(story.SelectToken("keywords")!);
            //Leave this
            //tokensToBeYeeted.Add(story.SelectToken("url")!);
            //tokensToBeYeeted.Add(story.SelectToken("title")!);
            //tokensToBeYeeted.Add(story.SelectToken("text")!);
            //tokensToBeYeeted.Add(story.SelectToken("authors")!);
            foreach (JToken token in tokensToBeYeeted)
            {
                token.Parent.Remove();
            }
            //story.Add(new JProperty("news_id", name));
            story.Add(new JProperty("rating", rating));
            story.Add(new JProperty("criteria", criteria));
            string updatedJsonString = story.ToString();
            //File.WriteAllText(@"data\" + name + ".json", updatedJsonString);

            //using (StreamWriter file = File.CreateText(@"data\" + name + ".json"))
            //using (JsonTextWriter writer = new JsonTextWriter(file))
            //{
            //    story.WriteTo(writer);
            //}
            stories.Add(story);
        }
        
        
        using (StreamWriter file = File.CreateText(@"data3\initial.json"))
        using (JsonTextWriter writer = new JsonTextWriter(file))
        {
            stories.WriteTo(writer);
        }
    }
}
