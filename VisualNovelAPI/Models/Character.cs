using System;
namespace VisualNovelAPI.Models
{
    public class Character
    {
            public int CharacterID { get; set; }
            public string? CharacterName { get; set; }
            public string? VoiceActor { get; set; }
            public string? CharacterRole { get; set; }
            public string? Gender { get; set; }
    }
}

