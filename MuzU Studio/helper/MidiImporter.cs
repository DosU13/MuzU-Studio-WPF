using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using MuzUStandard;
using MuzUStandard.data;
using System.Diagnostics;
using System.IO;

namespace MuzU_Studio.Model;

internal class MidiImporter
{
    internal static MuzUProject Import(Stream stream, string displayName)
    {
        MidiFile midiFile = MidiFile.Read(stream);
        MuzUProject result = new MuzUProject();
        MuzUData data = result.MuzUData;
        data.Identity.Name = displayName;
        TempoMap tempoMap = midiFile.GetTempoMap();
        long? microsecondPerQuarterNote = tempoMap.GetTempoChanges().LastOrDefault()?.Value?.MicrosecondsPerQuarterNote;
        var timeSignature = tempoMap.GetTimeSignatureChanges().LastOrDefault()?.Value;
        CheckTempo(tempoMap);
        data.Tempo.MicrosecondsPerQuarterNote = microsecondPerQuarterNote ??
            Melanchall.DryWetMidi.Interaction.Tempo.Default.MicrosecondsPerQuarterNote;
        data.Tempo.TimeSignature.Numerator = timeSignature?.Numerator ?? 4;
        data.Tempo.TimeSignature.Denominator = timeSignature?.Denominator ?? 4;
        data.SequenceList.List.AddRange(ImportSequences(midiFile, displayName));
        return result;
    }

    private static void CheckTempo(TempoMap tempoMap)
    {
        double mpq = tempoMap.GetTempoChanges().First().Value.MicrosecondsPerQuarterNote;
        bool isSingleTempo = true;
        foreach (ValueChange<Melanchall.DryWetMidi.Interaction.Tempo> t in tempoMap.GetTempoChanges())
        {
            if (mpq != t.Value.MicrosecondsPerQuarterNote) isSingleTempo = false;
        }
        if (!isSingleTempo)
        {
            Debug.WriteLine("This midi file has multiple tempos: ");
            foreach (ValueChange<Melanchall.DryWetMidi.Interaction.Tempo> t in tempoMap.GetTempoChanges()) Debug.WriteLine(t.Value.BeatsPerMinute);
        }
    }

    internal static List<Sequence> ImportSequences(MidiFile midiFile, string displayName)
    {
        List<Sequence> result = new List<Sequence>();
        TempoMap tempoMap = midiFile.GetTempoMap();
        int timingSeqNameNumber = 1;
        foreach (TrackChunk trackChunk in midiFile.GetTrackChunks())
        {
            var trackNotes = trackChunk.GetNotes();
            if (trackNotes.Count <= 0) continue;
            Sequence sequence = new Sequence{
                Name = displayName + ((timingSeqNameNumber != 1) ? timingSeqNameNumber.ToString() : "")};
            timingSeqNameNumber++;
            SequenceTemplate template = new SequenceTemplate();
            template.LengthEnabled = true;
            template.NoteEnabled = true;
            template.TimeUnit = TimeUnite.Both;
            sequence.SequenceTemplate = template;
            foreach (Note note in trackNotes)
            {
                //var musicalTime = note.TimeAs<Melanchall.DryWetMidi.Interaction.MusicalTimeSpan>(tempoMap);
                //BarBeatFractionTimeSpan barBeatFractionTime = note.TimeAs<BarBeatFractionTimeSpan>(tempoMap);
                //BarBeatTicksTimeSpan barBeatTicksTime = note.TimeAs<BarBeatTicksTimeSpan>(tempoMap);
                var metricTime = note.TimeAs<Melanchall.DryWetMidi.Interaction.MetricTimeSpan>(tempoMap);
                //MidiTimeSpan midiTime = note.TimeAs<MidiTimeSpan>(tempoMap);

                //Melanchall.DryWetMidi.Interaction.MusicalTimeSpan musicalLength = note.LengthAs<Melanchall.DryWetMidi.Interaction.MusicalTimeSpan>(tempoMap);
                var metricLength = note.LengthAs<Melanchall.DryWetMidi.Interaction.MetricTimeSpan>(tempoMap);
                //Melanchall.DryWetMidi.Interaction.MusicalTimeSpan length = note.LengthAs<Melanchall.DryWetMidi.Interaction.MusicalTimeSpan>(tempoMap);
               
                Node item = new()
                {
                    Time = metricTime.TotalMicroseconds,
                    //item.Time.Numerator = musicalTime.Numerator;
                    //item.Time.Denominator = musicalTime.Denominator;
                    Length = metricLength.TotalMicroseconds,
                    Note = note.NoteNumber
                };
                sequence.NodeList.List.Add(item);
            }
            result.Add(sequence);
        }
        return result;
    }

    /// <summary>
    /// It is not going to be used, Just this code make sense to thing
    /// </summary>
    /// <param name="eventTime"></param>
    /// <param name="ticksPerQuarterNote"></param>
    /// <param name="timeSignature"></param>
    /// <returns></returns>
    private string ToMBT(long eventTime, int ticksPerQuarterNote, TimeSignatureEvent timeSignature)
    {
        int beatsPerBar = timeSignature == null ? 4 : timeSignature.Numerator;
        int ticksPerBar = timeSignature == null ? ticksPerQuarterNote * 4 : 
            (timeSignature.Numerator * ticksPerQuarterNote * 4) / (1 << timeSignature.Denominator);
        int ticksPerBeat = ticksPerBar / beatsPerBar;
        long bar = 1 + (eventTime / ticksPerBar);
        long beat = 1 + ((eventTime % ticksPerBar) / ticksPerBeat);
        long tick = eventTime % ticksPerBeat;
        return String.Format("{0}:{1}:{2}", bar, beat, tick);
    }

    //private MuzUProject Project;
    //private async Task BlendWithNewProject(MuzUProject newProject)
    //{
    //    if (Project.data.Name == "NoName") Project.data.Name = newProject.data.Name;
    //    if (Project.data.MicrosecondsPerQuarterNote == null)
    //    {
    //        Project.data.MicrosecondsPerQuarterNote = newProject.data.MicrosecondsPerQuarterNote;
    //        Project.data.TimeSignature = newProject.data.TimeSignature;
    //    }
    //    else if (Project.data.MicrosecondsPerQuarterNote != newProject.data.MicrosecondsPerQuarterNote ||
    //               Project.data.TimeSignature != newProject.data.TimeSignature)
    //        await AskForChoosingTempo(newProject.data.MicrosecondsPerQuarterNote, newProject.data.TimeSignature);
    //    Project.data.Sequences.AddRange(newProject.data.Sequences);
    //}

    //private async Task AskForChoosingTempo(long? microsecondsPerQuarterNote, string timeSignature)
    //{
    //    if (Project.data.MicrosecondsPerQuarterNote == null || Project.data.Sequences == null)
    //    {
    //        Project.data.MicrosecondsPerQuarterNote = microsecondsPerQuarterNote;
    //        Project.data.TimeSignature = timeSignature;
    //    }
    //    else if (microsecondsPerQuarterNote != Project.data.MicrosecondsPerQuarterNote
    //       || Project.data.TimeSignature != timeSignature)
    //    {
    //        ContentDialog dialog = new ContentDialog();
    //        dialog.Title = "Tempo of midi not same as Project's tempo. Which one to use?";
    //        dialog.PrimaryButtonText = "Project";
    //        dialog.SecondaryButtonText = "Midi file";
    //        dialog.CloseButtonText = "Cancel";
    //        dialog.DefaultButton = ContentDialogButton.Primary;

    //        var result = await dialog.ShowAsync();
    //        if (result == ContentDialogResult.Primary) return;
    //        else if (result == ContentDialogResult.Secondary)
    //        {
    //            Project.data.MicrosecondsPerQuarterNote = microsecondsPerQuarterNote;
    //            Project.data.TimeSignature = timeSignature;
    //        }
    //    }
    //}
}
