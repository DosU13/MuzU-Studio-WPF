using MuzUStandard.data;
using System.IO;
using System.Text.Json;

namespace MuzUStandard
{
    public class MuzUProject
    {        
        public MuzUData MuzUData;
        public MuzUProject() => MuzUData = new MuzUData();

        public MuzUProject(Stream stream) => MuzUData = LoadFromStream(stream);

        private MuzUData LoadFromStream(Stream stream)
        {
            using var reader = new StreamReader(stream);
            var json = reader.ReadToEnd();
            return JsonSerializer.Deserialize<MuzUData>(json);
        }

        public void Save(Stream stream)
        {
            var json = JsonSerializer.Serialize(MuzUData);
            stream.SetLength(0);
            using var writer = new StreamWriter(stream);
            writer.Write(json);
            writer.Flush();
            writer.Close();
        }
    }
}
