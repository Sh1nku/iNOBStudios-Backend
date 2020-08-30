using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iNOBStudios.Models {
    public static class ModelStateHelper {
        public static string GetModelStateErrors(this ModelStateDictionary modelState) {
            IEnumerable<KeyValuePair<string, string[]>> errors = modelState.IsValid
                ? null
                : modelState
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray())
                    .Where(m => m.Value.Any());

            string output = "{ \"errors\": {";

            if (errors != null) {
                foreach (KeyValuePair<string, string[]> kvp in errors) {
                    output += "\"" + kvp.Key.Replace(".", "") + "\": [";
                    foreach (var s in kvp.Value) {
                        output += '"' + s + '"' + ",";
                    }
                    output = output.TrimEnd(',');
                    output += "],";
                }
            }
            output = output.TrimEnd(',');
            output += "}}";
            return output;
        }
    }
}