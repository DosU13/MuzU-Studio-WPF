using MuzUStandard.data;
using Newtonsoft.Json;
using System.IO;
//using System.Text.Json;

namespace MuzUStandard
{
    public class MuzUProject
    {        
        public MuzUData MuzUData;
        public MuzUProject() => MuzUData = new MuzUData();

        public MuzUProject(MuzUData data) => MuzUData = data;

        public MuzUProject(Stream stream) => MuzUData = LoadFromStream(stream);

        public MuzUProject(string text) => MuzUData = LoadFromText(text);

        private MuzUData LoadFromStream(Stream stream)
        {
            using (var reader = new StreamReader(stream))
            {
                var json = reader.ReadToEnd();
                return LoadFromText(json);
            }
        }

        private MuzUData LoadFromText(string text)
        {
            //return JsonSerializer.Deserialize<MuzUData>(text);
            return JsonConvert.DeserializeObject<MuzUData>(text);
        }

        public void Save(Stream stream)
        {
            //var json = JsonSerializer.Serialize(MuzUData);
            var json = JsonConvert.SerializeObject(MuzUData);
            stream.SetLength(0);
            using (var writer = new StreamWriter(stream))
            {
                writer.Write(json);
                writer.Flush();
                writer.Close();
            }
        }
    }
}
