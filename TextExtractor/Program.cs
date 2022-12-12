

using System.Net;
using System.Text;

namespace TextExtractor;

public class TextExtractor
{
    static void Main(string[] args)
    {
        string urlAddress = "http://google.com";

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        if (response.StatusCode == HttpStatusCode.OK)
        {
            Stream receiveStream = response.GetResponseStream();
            StreamReader readStream = null;
            if (response.CharacterSet == null)
                readStream = new StreamReader(receiveStream);
            else
                readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
            string data = readStream.ReadToEnd();
            response.Close();
            readStream.Close();

            File.WriteAllText("html.txt", data);
            Console.WriteLine(data);
        }
    }
}