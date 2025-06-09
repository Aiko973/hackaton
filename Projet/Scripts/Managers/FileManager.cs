using Com.IsartDigital.Hackaton;
using Godot;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Author : Thomas Tran

namespace Com.IsartDigital.Hackaton {
	
	public static class FileManager
	{
        public static List<Dilemma> GetDilemmaFromJson(string pPath)
        {
            FileAccess lFile = FileAccess.Open(pPath, FileAccess.ModeFlags.Read);
            string lJsonString = lFile.GetAsText();
            lFile.Close();

            JsonSerializerOptions lOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            };

            List<Dilemma> lList = JsonSerializer.Deserialize<List<Dilemma>>(lJsonString, lOptions);

            return lList;
        }

        public static List<PlayerProfiles> GetPlayersProfilesFromJson(string pPath)
        {
            FileAccess lFile = FileAccess.Open(pPath, FileAccess.ModeFlags.Read);
            string lJsonString = lFile.GetAsText();
            lFile.Close();

            JsonSerializerOptions lOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            };

            List<PlayerProfiles> lList = JsonSerializer.Deserialize<List<PlayerProfiles>>(lJsonString, lOptions);

            return lList;
        }
    }
}
