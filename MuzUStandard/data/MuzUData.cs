using System.Collections.Generic;
using System.Reflection;
using System.Xml.Linq;

namespace MuzUStandard.data
{
    public class MuzUData: XmlBase
    {
        public MuzUData() { }
        internal MuzUData(XElement xElement) : base(xElement) { }

        private Identity _identity;
        public Identity Identity { 
            get { 
                if (_identity == null) _identity = new Identity();
                return _identity;
            } set => _identity = value;}

        private Music _music;
        public Music Music { 
            get {
                if(_music == null) _music = new Music();
                return _music;
            } set => _music = value;}

        private MusicLocal _musicLocal;
        public MusicLocal MusicLocal { 
            get { 
                if(_musicLocal==null) _musicLocal = new MusicLocal();
                return _musicLocal;
            } set => _musicLocal = value;}

        private Tempo _tempo;
        public Tempo Tempo { 
            get { 
                if(_tempo == null) _tempo = new Tempo();
                return _tempo;
            } set => _tempo = value;}

        private SequenceList _sequenceList;
        public SequenceList SequenceList {
            get
            {
                if (_sequenceList == null) _sequenceList = new SequenceList();
                return _sequenceList;
            } set => _sequenceList = value;}

        internal override XElement ToXElement()
        {
            var xElement = base.ToXElement();
            xElement.Add(Identity.ToXElement());
            xElement.Add(Music.ToXElement());
            xElement.Add(MusicLocal.ToXElement());
            xElement.Add(Tempo.ToXElement());
            xElement.Add(SequenceList.ToXElement());
            return xElement;
        }

        internal override XElement LoadFromXElement(XElement xElement)
        {
            var thisElement = xElement;
            Identity = new Identity(thisElement);
            Music = new Music(thisElement);
            MusicLocal = new MusicLocal(thisElement);
            Tempo = new Tempo(thisElement);
            SequenceList = new SequenceList(thisElement);
            return thisElement;
        }
    }

    public class Identity : XmlInfo
    {
        internal Identity() : base() {
            Infos[nameof(Name)] = "";
            Infos[nameof(Creator)] = "";
            Infos[nameof(Description)] = "";
        }
        internal Identity(XElement xElement) : base(xElement) { }

        public string Name { get => Infos[nameof(Name)]; set => Infos[nameof(Name)] = value; }
        public string Creator { get => Infos[nameof(Creator)]; set => Infos[nameof(Creator)] = value; }
        public string Description { get => Infos[nameof(Description)]; set => Infos[nameof(Description)] = value; }
    }

    public class Music : XmlInfo
    {
        internal Music() : base() {
            Infos[nameof(Name)] = "";
            Infos[nameof(Author)] = "";
            Infos[nameof(Version)] = "";
        }
        internal Music(XElement xElement) : base(xElement) { }

        public string Name { get => Infos[nameof(Name)]; set => Infos[nameof(Name)] = value; }
        public string Author { get => Infos[nameof(Author)]; set => Infos[nameof(Author)] = value; }
        public string Version { get => Infos[nameof(Version)]; set => Infos[nameof(Version)] = value; }
    }

    public class MusicLocal : XmlBase
    {
        internal MusicLocal() { }
        internal MusicLocal(XElement xElement): base(xElement) {}

        public string MusicPath { get; set; } = "";
        /// <summary>
        /// if positive audio has excess part
        /// </summary>
        public long MusicOffsetMicroseconds { get; set; } = 0;
        
        internal override XElement ToXElement()
        {
            var xElement = base.ToXElement();
            xElement.Add(new XElement(nameof(MusicPath), MusicPath),
                         new XElement(nameof(MusicOffsetMicroseconds), MusicOffsetMicroseconds));
            return xElement;
        }

        internal override XElement LoadFromXElement(XElement xElement)
        {
            var thisElement = base.LoadFromXElement(xElement);
            MusicPath = thisElement.Element(nameof(MusicPath)).Value;
            MusicOffsetMicroseconds = long.Parse(thisElement.Element(nameof(MusicOffsetMicroseconds))?.Value ?? "0");
            return thisElement;
        }
    }
}
