using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;

namespace MuzUStandard.data
{
    public class MuzUData
    {
        public MuzUData() { }

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

        private List<Sequence> _sequenceList;
        public List<Sequence> SequenceList {
            get
            {
                if (_sequenceList == null) _sequenceList = new List<Sequence>();
                return _sequenceList;
            } set => _sequenceList = value;}
    }

    public class Identity
    {
        public string Name { get; set; }
        public string Creator { get; set; }
        public string Description { get; set; }
    }

    public class Music
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public string Version { get; set; }
    }

    public class MusicLocal
    {
        public string MusicPath { get; set; } = "";
        /// <summary>
        /// if positive audio has excess part
        /// </summary>
        public long MusicOffsetMicroseconds { get; set; } = 0;
    }
}
