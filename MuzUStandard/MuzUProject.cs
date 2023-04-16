using MuzUStandard.data;
using System.IO;
using System.Xml.Linq;

namespace MuzUStandard
{
    public class MuzUProject
    {        
        public MuzUData MuzUData;
        public MuzUProject() => MuzUData = new MuzUData();

        public MuzUProject(Stream stream) => MuzUData = LoadFromStream(stream);
        public MuzUProject(TextReader txtReader) => MuzUData = LoadFromStream(txtReader);

        private MuzUData LoadFromStream(Stream stream) 
        {
            var x = XDocument.Load(stream);
            return new MuzUData(x.Root);
        }
        private MuzUData LoadFromStream(TextReader txtReader)
        {
            var x = XDocument.Load(txtReader);
            return new MuzUData(x.Root);
        }

        public void Save(Stream stream)
        { 
            stream.SetLength(0);
            ToXDocument().Save(stream);
        }

        private XDocument ToXDocument()
        {
            XDocument doc = new XDocument(MuzUData.ToXElement());
            doc.Declaration = new XDeclaration("1.0", "utf-8", "true");
            return doc;
        }
    }
}
