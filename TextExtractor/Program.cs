using System;
using AngleSharp;
using AngleSharp.Html.Parser;
using System.Net;
using System.Text;

namespace TextExtractor;

public class TextExtractor
{
    static void Main(string[] args)
    {
        string urlAddress = "https://towardsdatascience.com/deploying-a-fake-news-detector-web-application-with-google-cloud-run-and-flask-eb750cce986d";

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        if (response.StatusCode == HttpStatusCode.OK)
        {
            Stream receiveStream = response.GetResponseStream();
            StreamReader readStream = null;

            if (response.CharacterSet == null) readStream = new StreamReader(receiveStream);
            else readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
            string data = readStream.ReadToEnd();
            response.Close();
            readStream.Close();

            File.WriteAllText("html.txt", data);
            //Console.WriteLine(data);



            var config = Configuration.Default;

            //Create a new context for evaluating webpages with the given config
            var context = BrowsingContext.New(config);

            //Create a virtual request to specify the document to load (here from our fixed string)

            var parser = context.GetService<IHtmlParser>();
            var document = parser.ParseDocument(data);

            //Do something with document like the following
            Console.WriteLine(document.DocumentElement.TextContent);
        }
    }
}