﻿using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gsudo.Commands
{
    [Verb("config")]
    class ConfigCommand : ICommand
    {
        [Value(0)]
        public string key { get; set; }

        [Value(1)]
        public IEnumerable<string> value { get; set; }

        public Task<int> Execute()
        {
            RegistrySetting setting = null;

            if (key == null)
            {
                foreach (var k in GlobalSettings.AllKeys)
                    Console.WriteLine($"{k.Value.Name} = { Newtonsoft.Json.JsonConvert.SerializeObject(k.Value.GetStringValue()).ToString()}");

                return Task.FromResult(0);
            }

            GlobalSettings.AllKeys.TryGetValue(key, out setting);

            if (setting == null)
            {
                Console.WriteLine($"Invalid Setting '{key}'.", LogLevel.Error);
                return Task.FromResult(Constants.GSUDO_ERROR_EXITCODE);
            }
            
            if (value!=null && value.Any())
            {
                // SAVE 
                setting.Save($"\"{string.Join(" ", value.ToArray())}\"");
            }

            // READ
            Console.WriteLine($"{setting.Name} = { Newtonsoft.Json.JsonConvert.SerializeObject(setting.GetStringValue()).ToString()}");
            return Task.FromResult(0);
        }
    }
}
