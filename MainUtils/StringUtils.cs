using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Utils
{
   public static partial class Util
   {
      public static string ReturnUniqueValue(string ID)
      {
         var result = default(byte[]);

         using (var stream = new MemoryStream())
         {
            using (var writer = new BinaryWriter(stream, Encoding.UTF8, true))
            {
               writer.Write(DateTime.Now.Ticks);
               writer.Write(ID);
            }

            stream.Position = 0;

            using (var hash = System.Security.Cryptography.SHA256.Create())
            {
               result = hash.ComputeHash(stream);
            }
         }

         var text = new string[20];

         for (var i = 0; i < text.Length; i++)
         {
            text[i] = result[i].ToString("x2");
         }

         return string.Concat(text);
      }
   }
}
