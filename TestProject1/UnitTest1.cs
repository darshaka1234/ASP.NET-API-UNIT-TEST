using static RestAssured.Dsl;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace TestProject1
{
    public class Tests
    {
        private readonly string Baseurl = "https://api.restful-api.dev/objects";
        readonly static string jsonListFilePath = "./../../../Assets/ObjectList.json";
        readonly static string SingleJsonFilePath = "./../../../Assets/SingleObject.json";
        readonly static string NewObjectFilePath = "./../../../Assets/NewObject.json";
        readonly static string UpdateObjectFilePath = "./../../../Assets/UpdateObject.json";
        public static JToken GetJsonArrayAsString(string filePath)
        {
            string json = File.ReadAllText(filePath);

            JArray jsonArray = JArray.Parse(json);
            return jsonArray.ToString(Formatting.None);
        }

        public static JToken GetJsonContentAsString(string filePath)
        {
            string json = File.ReadAllText(filePath);

            JObject jsonObject = JObject.Parse(json);
            return jsonObject.ToString(Formatting.None);
        }


    [Test]
        public void GetObjectList()
        {
            Given()
            .When()
            .Get(Baseurl)
            .Then()
            .StatusCode(200)
            .ContentType("application/json")
            .Body($"{GetJsonArrayAsString(jsonListFilePath)}");
        }

        [Test]
        public void GetObjectById()
        {
            Given()
            .When()
            .PathParam("id", 1)
            .Get($"{Baseurl}/[id]")
            .Then()
            .StatusCode(200)
            .ContentType("application/json")
            .Body($"{GetJsonContentAsString(SingleJsonFilePath)}");
        }

        [Test]
        public void CreateObject()
        {
            Given()
            .Body(File.ReadAllText(NewObjectFilePath))
            .When()
            .Post(Baseurl)
            .Then()
            .StatusCode(200)
            .ContentType("application/json");
        }

        [Test]
        public void UpdateObject()
        {
            Given()
            .Body(File.ReadAllText(UpdateObjectFilePath))
            .When()
            .PathParam("id", 1)
            .Patch($"{Baseurl}/[id]")
            .Then()
            .StatusCode(405)
            .ContentType("application/json");
        }


        [Test]
        public void deleteObject()
        {
            Given()
            .When()
            .PathParam("id", 10)
            .Delete($"{Baseurl}/[id]")
            .Then()
            .StatusCode(405)
            .ContentType("application/json");
        }
    }
}
